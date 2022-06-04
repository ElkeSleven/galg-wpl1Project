using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using Microsoft.VisualBasic;




namespace GALGmain
{
    /// <summary>
    /// Interaction logic for GALG2.xaml
    /// </summary>
    public partial class GALG2 : Window
    {
        public GALG2()
        {
            InitializeComponent();
            InitializeComponent();
            Startklok();
            StartWaarden();
        }

        // hier word de naam van de spelers gevraagt 
        // start als spel opstart en de CountTimer stopt 
        private void NaamSpeler2()
        {
            naamSpeler2 = Interaction.InputBox("wat is je naam ? ", "heyy daar");
            if  (naamSpeler2 =="")
            {
                naamSpeler2 = "onKnown";
            }
        }
        private void NaamSpeler1()
        {
            naamSpeler1 = Interaction.InputBox("wie gaat het woord raden ? ", "heyy uitdager");
            if (naamSpeler1 == "")
            {
                naamSpeler1 = "onKnown Challenger";
            }
        }




        // timers en klok
        private DispatcherTimer klokTimer;
        private DispatcherTimer timer;
        private DispatcherTimer countDown;



        // list en array
        char[] alfabet = new char[26] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
        List<char> fouteLetters = new List<char>();
        List<char> justeLetters = new List<char>();
        List<string> levenKwijt = new List<string> {
            "/Assets_img/krijt_levens10.jpg" , "/Assets_img/krijt_levens9.jpg" , "/Assets_img/krijt_levens8.jpg" , "/Assets_img/krijt_levens7.jpg" , "/Assets_img/krijt_levens6.jpg" ,
            "/Assets_img/krijt_levens5.jpg" , "/Assets_img/krijt_levens4.jpg" , "/Assets_img/krijt_levens3.jpg" , "/Assets_img/krijt_levens2.jpg" , "/Assets_img/krijt_levens.jpg" , "/Assets_img/tekst.jpg" }; 


        // sting ; bool ; int ; 
        string geheimWoord; string maskedWoord; string naamSpeler1; string naamSpeler2;
        string fouteLettersAsString;
        bool spelAfgelopen; bool spelOpstart; bool boolLeterToegestaan;
        int beurten; int levens; int timeSec; int countSec;



        // klok
        public DispatcherTimer KlokTimer { get => klokTimer; set => klokTimer = value; }
        private void klokTimer_Tick(object sender, EventArgs e)
        {

            // lblDateTime.Content = DateTime.Now.ToString();                       // 4/01/2022    10:31:44
            // lblDateTime.Content = DateTime.Now.ToLongDateString();                 // dinsdag 4 januari 2022
        }
        private void Startklok()
        {
            KlokTimer = new DispatcherTimer();
            KlokTimer.Interval = TimeSpan.FromSeconds(1);
            KlokTimer.Start();
        }

        // timer
        private void StartTimer()
        {
            /// GrindGalig2.Background = Brushes.DarkGray;
            timeSec = 10;
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            // timer.Interval = new TimeSpan(0, 0, 0);
            timer.Tick += timer_Tick;
            txtInput.Clear();
            timer.Start();

        }
        private void timer_Tick(object sender, EventArgs e)
        {
            lblTimer.Content = timeSec.ToString();
            if (timeSec == 0)
            {
                lblTimer.Content = timeSec.ToString();
                timer.Stop();
               // GrindGalig2.Background = Brushes.DarkGray;
                Levens();
            }
            if (timeSec == 1)
            {
                lblTimer.Content = timeSec.ToString();
                GrindGalig2.Background = Brushes.IndianRed;
                timeSec--;
            }
            else
            {
                timeSec--;
            }

        }
      
        // count down timer
        private void StartCount()
        {
            countDown = new DispatcherTimer();
            countDown.Interval = TimeSpan.FromSeconds(1);
            countDown.Tick += countDown_Tick;
            countDown.Start();
        }
        private void countDown_Tick(object sender, EventArgs e)
        {
            if (countSec == 0)
            {
                if(spelOpstart == true)
                {
                    countDown.Stop();
                    countSec = 3;
                    btnStart.IsEnabled = true;
                    spelOpstart = false;
                    NaamSpeler2(); NaamSpeler1();
                    lblVersus.Content = $"{naamSpeler1} VS {naamSpeler2}";
                    LblUitput.Content = $"geef een geheim woord op\n\rdat {naamSpeler1} zal proberen te raden";
                }
                else
                {
                  countDown.Stop();
                  LblUitput.Content = $"start met raden {naamSpeler1}";                 
                  SpelStartAfgelopen();
                  StartTimer();
                }
            }
            else
            {
                if(spelOpstart == false)
                {
                  txtInput.Text = $"{naamSpeler1} zet je klaar! {countSec}";
                }
              
                countSec--;
            }
        }


        // hier staat de value van alle varabelen  en hoe het spel moet worden gestart na dat er op niet spel worde geklickt 
        private void StartWaarden()
        {
            lblDateTime.Content = DateTime.Now.ToString();                       // 4/01/2022    10:31:44
            geheimWoord = "";
            lblLevens.Content = "";
            lblFouteLetters.Content = "";
            spelOpstart = true;
            levens = 10;
            fouteLetters.Clear();
            justeLetters.Clear();
            fouteLettersAsString = "";
            lblFouteLetters.Content = "";         
            lblMaskedWoord.Content = "";
            lblTimer.Content = "";
           
            btnStart.IsEnabled = false;
            countSec = 2;
            txtInput.Text = "";        
            btnRaad.IsEnabled = false;
            GrindGalig2.Background = Brushes.DarkGray;
            btnNieuw.IsEnabled = false; imageLevens.Source = new BitmapImage(new Uri(levenKwijt[levens], UriKind.Relative));
            StartCount();

        }

        // deze method zorgt er voor dat de label met foute leter op to date blijft 
        private void FouteLettersAsString()
        {
            fouteLettersAsString = "";
            for (int i = 0; i < fouteLetters.Count; i++)
            {
                fouteLettersAsString += fouteLetters[i];

            }
            lblFouteLetters.Content = fouteLettersAsString;
        }



        // deze method zorgt er voor dat er geen getallen of andere tekens in het geheimwoord/gok dat is opgegeven zitten
        private bool IngavenControle()
        {
            int fouteGevonden = 0;
            char index;
            string input;
            
            input = (txtInput.Text).ToLower();
            for (int i = 0; i < input.Length; i++)
            {
                index = input[i];
                boolLeterToegestaan = Array.Exists(alfabet, element => element.Equals(index));
                if (boolLeterToegestaan == false)
                {
                    fouteGevonden++;
                    if (fouteGevonden == input.Length - 1)
                    {
                        MessageBox.Show("contoleer je ingaven geen getallen, tekens of spacies! (woord)");
                    }


                }
                if (boolLeterToegestaan == true && fouteGevonden == 0 && i == input.Length - 1)
                {
                    // woord 100% just
                    fouteGevonden = 0;

                }


            }
            return boolLeterToegestaan;




        }


        // deze method zorgd dat het geheim te voorschijnt komt in ******  
        // en contoleer als alle letters zijn geraden ( speler wint ) 
        private void WoordMasked()
        {
            maskedWoord = "";
            for (int i = 0; i < geheimWoord.Length; i++)
            {
                if (justeLetters.Contains(geheimWoord[i]))
                {
                    maskedWoord += geheimWoord[i];
                }
                else
                {
                    maskedWoord += " * ";
                }

            }
            if (geheimWoord == maskedWoord)
            {
                beurten = 10 - levens;
                LblUitput.Content = $"proficiat je hebt {geheimWoord} geraden\n\rin {beurten} beurten";
                timer.Stop();
                spelAfgelopen = true;
                SpelStartAfgelopen();
          
            }

            lblMaskedWoord.Content = maskedWoord;
        }
        // deze method houd de levens in de gaten en als de spelers levens op zijn ( speler verliest)
        private void Levens()
        {
            levens--;
            beurten++;
            if (levens <= 0)
            {
                timer.Stop();
                lblDateTime.Content = DateTime.Now.ToString();                       // 4/01/2022    10:31:44
                LblUitput.Content = $"je hebt geen levens meer je hangt \n\rhet geheim woord was {geheimWoord}";
                lblLevens.Content = $" aantal levens:  {levens} ";
                spelAfgelopen = true;
                GrindGalig2.Background = Brushes.DarkRed;
                countSec = 2;
                SpelStartAfgelopen();
            }
            else
            {
                lblDateTime.Content = DateTime.Now.ToString();                       // 4/01/2022    10:31:44
                lblLevens.Content = $" aantal levens:  {levens} ";
                imageLevens.Source = new BitmapImage(new Uri(levenKwijt[levens], UriKind.Relative));
                StartTimer();
            }
        }

        // wanneer het spel opstart of eindigt worden de juste buttons Enabled
        private void SpelStartAfgelopen()
        {
            if (spelAfgelopen == false)
            {
                btnRaad.IsEnabled = true;
                btnNieuw.IsEnabled = true;
                btnStart.IsEnabled = false;
            }
            if (spelAfgelopen == true)
            {
                spelAfgelopen = false;
                btnRaad.IsEnabled = false;
                btnNieuw.IsEnabled = true;
                btnStart.IsEnabled = false;
                imageLevens.Source = new BitmapImage(new Uri(levenKwijt[levens], UriKind.Relative));
            }

        }


        // button_click
        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            string i = txtInput.Text;

                
                if (!string.IsNullOrWhiteSpace(txtInput.Text) && i.Length > 2)
                {
                    
                    bool b = IngavenControle();
                    if (b == true)
                    {
                        geheimWoord = (txtInput.Text).ToLower();
                        txtInput.Text = $"{naamSpeler1} zet je klaar!";
                        WoordMasked();
                        StartCount();

                    }

                }            
            else
            {
                MessageBox.Show("contoleer je ingaven");
                GrindGalig2.Background = Brushes.Pink;
            }
        }
        private void BtnRaad_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtInput.Text))
            {
                bool geenGetalen = IngavenControle();
                if (geenGetalen == true)
                {
                    string input = (txtInput.Text).ToLower();
                    timer.Stop();
                    if (char.TryParse(input, out char gok))
                    {
                        // letter           
                        bool g = geheimWoord.Contains(gok);  // letter juste
                        if (g == true)
                        {
                            // letter juste gok
                            GrindGalig2.Background = Brushes.PaleGoldenrod;
                            justeLetters.Add(gok);
                            txtInput.Clear();
                            txtInput.Focus();
                            WoordMasked();
                            if (spelAfgelopen == false) { StartTimer(); }
                        }
                        else // letter fout
                        {
                            // foute letter 
                            GrindGalig2.Background = Brushes.DeepSkyBlue;
                            fouteLetters.Add(gok);
                            FouteLettersAsString();
                            lblFouteLetters.Content = $"{fouteLettersAsString}";
                            Levens();
                            txtInput.Clear();
                            txtInput.Focus();
                        }
                    }
                    else
                    {
                        // woord
                        if (geheimWoord == input)  // woord geraden (gewonnen )
                        {
                            GrindGalig2.Background = Brushes.Gold;
                            LblUitput.Content = $"proficiat je hebt {geheimWoord} geraden";
                            spelAfgelopen = true;
                            SpelStartAfgelopen();
                           
                        }
                        else  //  woord fout geraden 
                        {
                            GrindGalig2.Background = Brushes.DarkBlue;
                            Levens();
                            txtInput.Clear();
                            txtInput.Focus();
                        }
                    }

                }
                else { LblUitput.Content = $"raad een letter\n\rde tijd tikt en tikt\n\r{naamSpeler2} "; GrindGalig2.Background = Brushes.Purple;
                }
            }
            else
            {
                // foute ingave spasies of leeg 
                GrindGalig2.Background = Brushes.Purple;
                LblUitput.Content = "geen spacies of leeg\n\rprobeer een letter\n\rte raden";
                txtInput.Clear();
                txtInput.Focus();
            }
        }
        private void BtnNieuw_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            StartWaarden();
        }
        private void BtnTerug_Click(object sender, RoutedEventArgs e)
        {
            MainWindow a = new MainWindow();
            Hide();
            a.ShowDialog();
            Close();
        }

     



        // niet zichtbaar door blouw hover effect 
        private void RandMouseEnter(object sender, MouseEventArgs e)
        {

            Button btnStart = (Button)sender;
            btnStart.BorderBrush = Brushes.Red;

            Button btnRaad = (Button)sender;
            btnRaad.BorderBrush = Brushes.Red;

            Button btnNieuw = (Button)sender;
            btnNieuw.BorderBrush = Brushes.Red;

            Button btnTerug = (Button)sender;
            btnTerug.BorderBrush = Brushes.Red;
        }
        private void RandMouseLeave(object sender, MouseEventArgs e)
        {
            Button btnStart = (Button)sender;
            btnStart.BorderBrush = Brushes.Gray;

            Button btnRaad = (Button)sender;
            btnRaad.BorderBrush = Brushes.Gray;

            Button btnNieuw = (Button)sender;
            btnNieuw.BorderBrush = Brushes.Gray;

            Button btnTerug = (Button)sender;
            btnTerug.BorderBrush = Brushes.Gray;



        }



    }
}




