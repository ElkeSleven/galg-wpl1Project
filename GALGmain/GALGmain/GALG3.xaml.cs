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
using System.Timers;

namespace GALGmain
{
    /// <summary>
    /// Interaction logic for GALG3.xaml
    /// </summary>
    /// 



    public partial class GALG3 : Window
    {
        public GALG3()
        {
            InitializeComponent();
            Startklok();
            StartWaarden();
            TextboxInfoBubbel();
            KeyDown += new KeyEventHandler(KeyPressed);
        }

        // timers en klok
        private DispatcherTimer klokTimer;
        private DispatcherTimer timer;
        private DispatcherTimer countDown;

        // random
        Random hint = new Random();

        // list en array
        private string[] galgjeWoorden = new string[100]
{
    "grafeem",
    "tjiftjaf",
    "maquette",
    "kitsch",
    "pochet",
    "convocaat",
    "jakkeren",
    "collaps",
    "zuivel",
    "cesium",
    "voyant",
    "spitten",
    "pancake",
    "gietlepel",
    "karwats",
    "dehydreren",
    "viswijf",
    "flater",
    "cretonne",
    "sennhut",
    "tichel",
    "wijten",
    "cadeau",
    "trotyl",
    "chopper",
    "pielen",
    "vigeren",
    "vrijuit",
    "dimorf",
    "kolchoz",
    "janhen",
    "plexus",
    "borium",
    "ontweien",
    "quiche",
    "ijverig",
    "mecenaat",
    "falset",
    "telexen",
    "hieruit",
    "femelaar",
    "cohesie",
    "exogeen",
    "plebejer",
    "opbouw",
    "zodiak",
    "volder",
    "vrezen",
    "convex",
    "verzenden",
    "ijstijd",
    "fetisj",
    "gerekt",
    "necrose",
    "conclaaf",
    "clipper",
    "poppetjes",
    "looikuip",
    "hinten",
    "inbreng",
    "arbitraal",
    "dewijl",
    "kapzaag",
    "welletjes",
    "bissen",
    "catgut",
    "oxymoron",
    "heerschaar",
    "ureter",
    "kijkbuis",
    "dryade",
    "grofweg",
    "laudanum",
    "excitatie",
    "revolte",
    "heugel",
    "geroerd",
    "hierbij",
    "glazig",
    "pussen",
    "liquide",
    "aquarium",
    "formol",
    "kwelder",
    "zwager",
    "vuldop",
    "halfaap",
    "hansop",
    "windvaan",
    "bewogen",
    "vulstuk",
    "efemeer",
    "decisief",
    "omslag",
    "prairie",
    "schuit",
    "weivlies",
    "ontzeggen",
    "schijn",
    "sousafoon"
};
        char[] alfabet = new char[26] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
        List<char> fouteLetters = new List<char>();
        List<char> justeLetters = new List<char>();
        StringBuilder score = new StringBuilder();
        List<string> levenKwijt = new List<string> {
            "/Assets_img/krijt_levens10.jpg" , "/Assets_img/krijt_levens9.jpg" , "/Assets_img/krijt_levens8.jpg" , "/Assets_img/krijt_levens7.jpg" , "/Assets_img/krijt_levens6.jpg" ,
            "/Assets_img/krijt_levens5.jpg" , "/Assets_img/krijt_levens4.jpg" , "/Assets_img/krijt_levens3.jpg" , "/Assets_img/krijt_levens2.jpg" , "/Assets_img/krijt_levens.jpg" , "/Assets_img/tekst.jpg" };
        List<string> scorebord = new List<string>();


        // sting ; bool ; int ; 
        string geheimWoord; string maskedWoord; string naamSpeler1; string naamSpeler2;
        string fouteLettersAsString;
        bool boolLeterToegestaan; bool hintGevraagd; bool spelAfgelopen; bool spelBezig;
        int beurten; int levens; int gekozenTijd; int countSec; int aantalHints;



        // klok voor de tijd van nu ik heb hem wel niet laten tikken dat verstoort de timer; 
        // ik heb in plaats daar van de tijd laten opdaten op bepaalde momenten (in method: Levens(), ScoresOpslaan() )
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

        // timer tikt 10 sec
        int timerSec;
        private void StartTimer()
        {
            timerSec = gekozenTijd;
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            // timer.Interval = new TimeSpan(0, 0, 0);
            timer.Tick += timer_Tick;
            txtInput.Clear();
            timer.Start();

        }
        private void timer_Tick(object sender, EventArgs e)
        {
            // txttimer.Text = increment.ToString();
            lblTimer.Content = timerSec.ToString();
            if (timerSec == 1)
            {
                lblTimer.Content = timerSec.ToString();
                // achtergrond word rood voor 1 sec 
                GrindGalig3.Background = Brushes.IndianRed;

                timerSec--;
            }
            if (timerSec == 0)
            {
                lblTimer.Content = timerSec.ToString();
                timerSec = gekozenTijd;
                timer.Stop();
                Levens();
            }
            else
            {
                timerSec--;
            }

        }

        // count down timer  op de start van het spel start deze timer zodat speler 2 zich klaar kan zetten
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
                countDown.Stop();
                LblUitput.Content = $"start met raden {naamSpeler1}";
                SpelStartAfgelopen();
                StartTimer();
            }
            else
            {
                txtInput.Text = $"{naamSpeler1} zet je klaar! {countSec}";
                countSec--;
            }
        }

        // hier staat de value van alle varabelen  en hoe het spel moet worden gestart na dat er op niet spel worde geklickt 
        private void StartWaarden()
        {
            lblDateTime.Content = DateTime.Now.ToString();                       // 4/01/2022    10:31:44
            aantalHints = 0;
            geheimWoord = "";
            lblLevens.Content = "";
            lblFouteLetters.Content = "";
            LblUitput.Content = "geef een geheim woord op\n\rof laat de PC een woord kiezen";
            levens = 10;
            fouteLetters.Clear();
            justeLetters.Clear();
            fouteLettersAsString = "";
            lblFouteLetters.Content = "";
            lblMaskedWoord.Content = "";
            lblTimer.Content = "";
            txtPlayer1.Clear();
            txtPlayer2.Clear();
            txtTimerInstel.Clear();
            boolLeterToegestaan = false;
            hintGevraagd = false;
            spelBezig = false;
            countSec = 3;
            txtInput.Clear();
            btnStart.IsEnabled = true;
            btnHint.IsEnabled = false;
            btnRaad.IsEnabled = false;
            btnNieuw.IsEnabled = false;
            mnuHint.IsEnabled = false;
            mnuNieuw.IsEnabled = false;
            mnuInstel.IsEnabled = false;
            txtPlayer1.IsEnabled = true;
            txtPlayer2.IsEnabled = true;
            txtTimerInstel.IsEnabled = true;
            PanelInstellingen.Visibility = Visibility.Visible;
            PanelTijdensSpel.Visibility = Visibility.Collapsed;
            PanelScores.Visibility = Visibility.Collapsed;
            imageLevens.Source = new BitmapImage(new Uri(levenKwijt[levens], UriKind.Relative));
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
            for (int i = 0; i < input.Length ; i++)
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

                spelAfgelopen = true;
                SpelStartAfgelopen();

                ScoresOpslaan();
            }

            lblMaskedWoord.Content = maskedWoord;
        }
        

        // deze method houd de levens in de gaten en als de spelers levens op zijn ( speler verliest)
        private void Levens()
        {
            levens--;
            
            if (levens <= 0)
            {
                timer.Stop();
                lblDateTime.Content = DateTime.Now.ToString();                       // 4/01/2022    10:31:44
                LblUitput.Content = $"je hebt geen levens meer je hangt \n\rhet geheim woord was {geheimWoord}";
                lblLevens.Content = $" aantal levens:  {levens} ";
                spelAfgelopen = true;
                GrindGalig3.Background = Brushes.DarkRed;
                gekozenTijd = 10;
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

        // na dat de speler gewonnen heeft en geen hint heefd gevraagt word zijn score opgeslagen 
        private void ScoresOpslaan()
        {
            if(hintGevraagd == true)
            {
                return;
            }
            else
            {
                string huidigeTijd;
                lblDateTime.Content = huidigeTijd = DateTime.Now.ToLongTimeString();
                string scoreSpeler = $"{levens} levens over - {naamSpeler1} - {huidigeTijd}";
                scorebord.Add(scoreSpeler);
                scorebord.Sort();
               
            }
        }

        // als op in menu op scorenbord word geklickt word de score op gevraagd en worden de 3 met de meeste levens op volegorde gerangschikt van goud , zilver en brons 
        private void ScoresOpvragen()
        {

            lblGoud.Content = "";
            lblZilver.Content = "";
            lblBrons.Content = "";
            if(scorebord.Count >= 3)
            {
                lblGoud.Content = scorebord[0];
                lblZilver.Content = scorebord[1];
                lblBrons.Content = scorebord[2];
            }
            if (scorebord.Count == 2)
            {
                lblGoud.Content = scorebord[0];
                lblZilver.Content = scorebord[1];
                lblBrons.Content = "nog leeg";
            }
            if(scorebord.Count == 1)
            {
                lblGoud.Content = scorebord[0];
                lblZilver.Content = "nog leeg";
                lblBrons.Content = "nog leeg";
            }
            if (scorebord.Count == 0)
            {
                lblGoud.Content = "er zijn";
                lblZilver.Content = "nog leeg";
                lblBrons.Content = "scores";
            }

        }
       
        // wanneer het spel opstart of eindigt worden de juste buttons en viewbox zichtbaar  
        private void SpelStartAfgelopen()
        {
            if(spelAfgelopen == false)
            { 
                spelBezig = true;
                btnHint.IsEnabled = true;
                btnRaad.IsEnabled = true;
                btnNieuw.IsEnabled = true;
                mnuHint.IsEnabled = true;
                mnuNieuw.IsEnabled = true;
                mnuInstel.IsEnabled = true;
                btnStart.IsEnabled = false;
               
                // panel instellingen 
                txtPlayer1.IsEnabled = false;
                txtPlayer2.IsEnabled = false;
                txtTimerInstel.IsEnabled = false;

            }
            if (spelAfgelopen == true)
               
            {  
                spelAfgelopen = false;
                spelBezig = false;
                btnHint.IsEnabled = false;
                btnRaad.IsEnabled = false;
                btnNieuw.IsEnabled = true;
                mnuHint.IsEnabled = false;
                mnuNieuw.IsEnabled = true;
                mnuInstel.IsEnabled = false;
                btnStart.IsEnabled = false;
             
                imageLevens.Source = new BitmapImage(new Uri(levenKwijt[levens], UriKind.Relative));


            }

        }



        // button_click
        /// <summary>
        // als er op de btn Start klikt word er gecontoleerd
        // als de namen van de speler zijn ingevult (een char volstaat) 
        // als de timer just is ingesteld ander word de warden veranderd 
        // als de ingegeven geheimwoord geen getallen of speciale tekens bevat
        /// </summary>
        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtTimerInstel.Text ))
            {
                txtTimerInstel.Text = "10"; 
            }
            if (!string.IsNullOrEmpty(txtTimerInstel.Text))
            {
                if (int.TryParse(txtTimerInstel.Text, out int t))
                {
                    if (t > 20) { t = 20; }
                    if (t < 5) { t = 5; }
                    else {t = Convert.ToInt32(txtTimerInstel.Text);}
                    gekozenTijd = t;
                }
                if (!int.TryParse(txtTimerInstel.Text, out int intje)) {t = 10; gekozenTijd = t; }
            }
            if (!string.IsNullOrWhiteSpace(txtTimerInstel.Text) && !string.IsNullOrWhiteSpace(txtPlayer1.Text) && !string.IsNullOrWhiteSpace(txtPlayer2.Text) )
            {
                
                // tegen pc
                if (txtPlayer2.Text.ToLower() == "pc")
                {
                    naamSpeler1 = txtPlayer1.Text;
                    naamSpeler2 = txtPlayer2.Text;
                    Random rdm = new Random();
                    int r = rdm.Next(0, 99);
                    geheimWoord = galgjeWoorden[r];
                    WoordMasked();
                    this.PanelTijdensSpel.Visibility = Visibility.Visible;
                    this.PanelInstellingen.Visibility = Visibility.Collapsed;
                    lblVersus.Content = $"{naamSpeler1} VS {naamSpeler2}";
                    txtInput.Text = $"{naamSpeler1} zet je klaar!";                    
                    StartCount();         
                }
                // tegen andere speler
                else if (!string.IsNullOrWhiteSpace(txtInput.Text) && (txtInput.Text).Length > 2)
                {
                    naamSpeler2 = txtPlayer2.Text.ToLower();
                     
                    bool boolIngavenGoedgekeurd = IngavenControle();
                    if (boolIngavenGoedgekeurd == true)
                    {
                        geheimWoord = (txtInput.Text).ToLower();
                        naamSpeler1 = txtPlayer1.Text;
                        naamSpeler2 = txtPlayer2.Text;
                        WoordMasked();
                        this.PanelTijdensSpel.Visibility = Visibility.Visible;
                        this.PanelInstellingen.Visibility = Visibility.Collapsed;
          
                        lblVersus.Content = $"{naamSpeler1} VS {naamSpeler2}";
                        txtInput.Text = $"{naamSpeler1} zet je klaar!";
                        StartCount();
              
                    }

                }
                else if (txtPlayer2.Text.ToLower() != "pc" )
                {
                    MessageBox.Show("contoleer je ingaven woord vanaf 3 letters toegestaan");
                }
            }
            else { MessageBox.Show("contoleer je ingaven"); }
        }


        /// <summary>
        // als er op de btn Raad word geklikt word er gecontroleerd 
        // als de gok geen getallen of speciale tekens bevat 
        // als de gok een char of woord is 
        // als de gok (char/woord) just of fout is
        /// </summary>
        private void btnRaad_Click(object sender, RoutedEventArgs e)
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
                            justeLetters.Add(gok);
                            //JusteLettersAsString();
                            txtInput.Clear();
                            txtInput.Focus();
                            WoordMasked();
                            if (spelAfgelopen == false) { StartTimer(); }
                        }
                        else // letter fout
                        {
                            // foute letter 
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

                            LblUitput.Content = $"proficiat je hebt {geheimWoord} geraden";
                            spelAfgelopen = true;
                            SpelStartAfgelopen();
                            ScoresOpslaan();
                        } 
                        else  //  woord fout geraden 
                        {
                            Levens();
                            txtInput.Clear();
                            txtInput.Focus();
                        }
                    }

                }
                else { LblUitput.Content = $"raad een letter\n\rde tijd tikt en tikt\n\r{naamSpeler2} "; }
            }            
            else
            {
                // foute ingave spasies of leeg 
                LblUitput.Content = "geen spacies of leeg\n\rprobeer een letter\n\rte raden";
                txtInput.Clear();
                txtInput.Focus();
            }
        }


        /// <summary>
        // als er op de btn Hind word geklikt word er gecontroleerd 
        // als de speler al 5 hinten heef gehad (geen hint meer)
        // er word een randum letter gerinereerd dat niet in het geheimwoord voorkomt
        // de foute letter word aan de list en string foute letters toegevoegt
        /// </summary>
        private void btnHint_Click(object sender, RoutedEventArgs e)
        {
            hintGevraagd = true;
            char hintLetter;
            aantalHints++;
            if (aantalHints > 5)
            {
                btnHint.IsEnabled = false;
                LblUitput.Content = "sorry geen hints mee toegestaan";
            }
            else
            {
                do
                {
                    int r = hint.Next(0, 24);
                    hintLetter = alfabet[r];
                }
                while (geheimWoord.Contains(hintLetter) && fouteLetters.Contains(hintLetter));
                fouteLetters.Add(hintLetter);
                FouteLettersAsString();
            }


        }


        /// <summary>
        // als er op de btn Nieuw Spel word geklikt
        // worden de timer gestopt en de StartWaarde() ingestelt
        /// </summary>
        private void BtnNieuw_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            StartWaarden();
        }


        /// <summary>
        /// als er op de btn afsluiten word geklikt word GALG3 afgesloten en komt de speler terug op mainGALG (startpagina)
        /// </summary>
        private void BtnTerug_Click(object sender, RoutedEventArgs e)
        {
            MainWindow a = new MainWindow();
            Hide();
            a.ShowDialog();
            Close();
        }
        

     



        // Mouse Mouse Mouse  enter enter enter 


        // de char p (Panal) is voor contole als panal t, i , s actief was 
        char p;
        private void MenuScores_Click(object sender, RoutedEventArgs e)
        {
            ScoresOpvragen();
            if (PanelScores.Visibility == Visibility.Collapsed)
            {
                if (PanelInstellingen.Visibility == Visibility.Visible)
                {
                        p = 'i';
                        PanelInstellingen.Visibility = Visibility.Collapsed;
                        PanelScores.Visibility = Visibility.Visible;
 
                }
                if (PanelTijdensSpel.Visibility == Visibility.Visible)
                {
                        p = 't';
                        PanelTijdensSpel.Visibility = Visibility.Collapsed;
                        PanelScores.Visibility = Visibility.Visible;
                }
            }
            else //(PanelScores.Visibility == Visibility.Visible)
            {

                if (p == 'i' && PanelScores.Visibility == Visibility.Visible)
                {
                    p = 'x';
                    PanelInstellingen.Visibility = Visibility.Visible;
                    PanelScores.Visibility = Visibility.Collapsed;
                }
                if (p == 't' && PanelScores.Visibility == Visibility.Visible)
                {

                    PanelTijdensSpel.Visibility = Visibility.Visible;
                    PanelScores.Visibility = Visibility.Collapsed;
                    p = 'x';

                }
            }



            


        }
        private void MenuInstel_Click(object sender, RoutedEventArgs e)
        {

            if (PanelInstellingen.Visibility == Visibility.Collapsed)
            {
                if (PanelTijdensSpel.Visibility == Visibility.Visible)
                {
                    p = 't';
                    PanelTijdensSpel.Visibility = Visibility.Collapsed;
                    PanelInstellingen.Visibility = Visibility.Visible;

                }
                if (PanelScores.Visibility == Visibility.Visible)
                {
                    p = 's';
                    PanelScores.Visibility = Visibility.Collapsed;
                    PanelInstellingen.Visibility = Visibility.Visible;
                }
            }
            else //(Panel .Visibility == Visibility.Visible)
            {

                if (p == 't' && PanelInstellingen.Visibility == Visibility.Visible)
                {
                    p = 'x';
                    PanelTijdensSpel.Visibility = Visibility.Visible;
                    PanelInstellingen.Visibility = Visibility.Collapsed;
                }
                if (p == 's' && PanelInstellingen.Visibility == Visibility.Visible)
                {

                    PanelScores.Visibility = Visibility.Visible;
                    PanelInstellingen.Visibility = Visibility.Collapsed;
                    p = 'x';

                }
            }





        }


        // voor de textbox ( tijd insteller )  heb ik een hoever efect toegevoeg waar 5 - 20 tevoorschijn komt 
        private void TextboxInfoBubbel()
        {       
            txtTimerInstel.Text = "10";
           txtTimerInstel.MouseEnter += new MouseEventHandler(tb_MouseEnter);
            txtTimerInstel.MouseLeave += new MouseEventHandler(tb_MouseDown);
        }
        void tb_MouseDown(object sender, MouseEventArgs e)
        {
            TextBlock tb = sender as TextBlock;
            // tb.Background = new SolidColorBrush(Colors.Transparent);
            this.txtTimerInstel.Text = "";
        }
        void tb_MouseEnter(object sender, MouseEventArgs e)
        {
            TextBlock tb = sender as TextBlock;
           
            this.txtTimerInstel.Text = "5 - 20";
        }



        // niet zichtbaar door blouw hover effect 
        private void RandMouseEnter(object sender, MouseEventArgs e)
         {   
            
            Button btnStart = (Button)sender;
            btnStart.BorderBrush = Brushes.Red;
        
            Button btnRaad = (Button)sender;
            btnRaad.BorderBrush = Brushes.Red;

            Button btnHint = (Button)sender;
            btnHint.BorderBrush = Brushes.Red;

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

            Button btnHint = (Button)sender;
            btnHint.BorderBrush = Brushes.Gray;

            Button btnNieuw = (Button)sender;
            btnNieuw.BorderBrush = Brushes.Gray;

            Button btnTerug = (Button)sender;
            btnTerug.BorderBrush = Brushes.Gray;



        }

        private void KeyPressed(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (spelAfgelopen == false && spelBezig == true)
                {
                    btnRaad_Click(null, null);
                }
                if (spelAfgelopen == false && spelBezig == false)
                {
                    btnStart_Click(null, null);
                }
            }


        }


    }
}

/////  klaaar 
      

