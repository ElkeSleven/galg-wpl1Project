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

namespace GALGmain
{

    /// <summary>
    /// Interaction logic for GALG1.xaml
    /// </summary>
    public partial class GALG1 : Window
    {
        // btn raad onzichtbaar start spell
        // tekstbox input beter zichbaar maken en labels vaste hoogt geven 
        // 


        public GALG1()
        {
            InitializeComponent();
            btnRaad.IsEnabled = false;
            btnVerberg.IsEnabled = true;
            txtInput.Focus();
        }




        List<string> levensKrijt = new List<string> {
            "/Assets_img/krijt_levens10.jpg" , "/Assets_img/krijt_levens9.jpg" , "/Assets_img/krijt_levens8.jpg" , "/Assets_img/krijt_levens7.jpg" , "/Assets_img/krijt_levens6.jpg" ,
            "/Assets_img/krijt_levens5.jpg" , "/Assets_img/krijt_levens4.jpg" , "/Assets_img/krijt_levens3.jpg" , "/Assets_img/krijt_levens2.jpg" , "/Assets_img/krijt_levens.jpg" , "/Assets_img/tekst.jpg" };

        string input;
        string gokGebruiker;
        string geheimWoord;
        char[] toegestaneKarakter = new char[26] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
        char inputIndex;
        bool lettersToegestaan;
        bool GokGoedGekeurd;
        int aantalLevens = 10;
        int aantalBeurten = 1;
        List<char> fouteLetters = new List<char>();
        List<char> justeLetters = new List<char>();
        string justeLettersAsString;
        string fouteLettersAsString;

        private void BtnVerverg_Click(object sender, RoutedEventArgs e)
        {         
            input = txtInput.Text;
            // woord langer als 3 karakters
            if (input.Length < 3)
            {
                lblUitput.Content = "woorden vanaf 3letters toegestaan";
            }
            else
            {
                IngavenControle();

            }

        }
        private void IngavenControle()
        {
            char[] toegestaneKarakter = new char[26] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
            geheimWoord = input.ToLower();
            if (geheimWoord.Length >= 3)
            {
                for (int i = 0; i < input.Length; i++)
                {
                    inputIndex = geheimWoord[i];
                    lettersToegestaan = Array.Exists(toegestaneKarakter, element => element.Equals(inputIndex));
                    if (lettersToegestaan == true)
                    {
                        lblUitput.Content = $"start met raden je hebt {aantalLevens} levens ";
                        btnVerberg.IsEnabled = false;
                        btnRaad.IsEnabled = true;
                    }
                    if(lettersToegestaan == false)
                    {
                        lblUitput.Content = "foute ingaven ingavecantrole";
                    }
                   
                } 
                txtInput.Clear();
                txtInput.Focus();
            }

            
      

        }

        private void BtnRaad_Click(object sender, RoutedEventArgs e)
        {
            // controleren als de input is goedgekeurd 
            GokGoedGekeurd = ControleGokIngave();
            if (GokGoedGekeurd == true)
            {
                KarakterOfWoord();
            }
            else
            {
                lblUitput.Content = "leter was al geraden";
                txtInput.Clear();
                txtInput.Focus();
            }


        }

       
     

        private bool ControleGokIngave()
        {
            gokGebruiker = txtInput.Text;
            for (int i = 0; i < gokGebruiker.Length; i++)
            {

                inputIndex = gokGebruiker[i];
                lettersToegestaan = Array.Exists(toegestaneKarakter, element => element.Equals(inputIndex));
                bool justeLetterKomtNietInListVoor = justeLetters.Contains(inputIndex);
                bool fouteLetterKomtNietInListVoor = fouteLetters.Contains(inputIndex);
                if (lettersToegestaan == true && justeLetterKomtNietInListVoor == false && fouteLetterKomtNietInListVoor == false)
                {
                    GokGoedGekeurd = true;

                }
                else
                {
                    GokGoedGekeurd = false;
                }
            }
            return GokGoedGekeurd;
        }

        private void KarakterOfWoord()
        {
            try
            {
                char gokGebruikerAsChar = Convert.ToChar(gokGebruiker);
                //komt karkter voor in het geheim woord ?
                bool letterKomtVoorInGeheimWoord = geheimWoord.Contains(gokGebruiker);
                if (letterKomtVoorInGeheimWoord == true)
                {
                    justeLetters.Add(gokGebruikerAsChar);
                    JusteLettersAsString();
                    FouteLettersAsString();
                    lblFouteLetters.Content = $"{fouteLettersAsString}";
                    lblJusteLetters.Content = $"{justeLettersAsString}";
                    txtInput.Clear();
                    txtInput.Focus();
                }
                else
                {
                    aantalLevens--;             
                    aantalBeurten++;
                    fouteLetters.Add(gokGebruikerAsChar);
                    JusteLettersAsString();
                    FouteLettersAsString();
                    lblUitput.Content = $"fout geraden je hebt nog {aantalLevens} levens ";
                    lblFouteLetters.Content = $"{fouteLettersAsString} ";
                    lblJusteLetters.Content = $"{justeLettersAsString}";
                    imageLevens.Source = new BitmapImage(new Uri(levensKrijt[aantalLevens], UriKind.Relative));
                   
                    txtInput.Clear();
                    txtInput.Focus();
                }
            }
            catch
            {

                // woord 
                if (geheimWoord == gokGebruiker && aantalLevens != 0)
                {
                    lblUitput.Content = $"je hebt het woord geraden in {aantalBeurten} beurten ";
                    txtInput.Clear();
                    txtInput.Focus();
                }

                else
                {
                    aantalLevens--;

                   
                    JusteLettersAsString();
                    FouteLettersAsString();
                    lblUitput.Content = $"Woord kwam niet overheen met het geheimwoord \n\r je hebt {aantalLevens} levens ";
                    lblFouteLetters.Content = $" {fouteLettersAsString} ";
                    lblJusteLetters.Content = $" {justeLettersAsString}";
                   
                    imageLevens.Source = new BitmapImage(new Uri(levensKrijt[aantalLevens], UriKind.Relative));
                    txtInput.Clear();
                    txtInput.Focus();


                }


            }
            if (aantalLevens == 0)
            {
                lblUitput.Content = $"je hebt geen levens meer je hangt \n\rhet geheim woord was {geheimWoord}";
                btnRaad.IsEnabled = false;
            }


        }

        private void JusteLettersAsString()
        {
            justeLettersAsString = "";
            for (int i = 0; i < justeLetters.Count; i++)
            {
                justeLettersAsString += justeLetters[i];

            }
            lblJusteLetters.Content = justeLettersAsString;
        }
        private void FouteLettersAsString()
        {
            fouteLettersAsString = "";
            for (int i = 0; i < fouteLetters.Count; i++)
            {
                fouteLettersAsString += fouteLetters[i];

            }
            lblJusteLetters.Content = fouteLettersAsString;
        }
        private void BtnNieuw_Click(object sender, RoutedEventArgs e)
        {
            txtInput.Text = "";
            lblUitput.Content = "geef een woord in";
            justeLetters.Clear();
            fouteLetters.Clear();
            txtInput.Focus();
            geheimWoord = "";
            input = "";
            gokGebruiker = "";
            justeLettersAsString = "";
            fouteLettersAsString = "";
            lblJusteLetters.Content = "";
            lblFouteLetters.Content = "";
            aantalBeurten = 1;
            btnRaad.IsEnabled = false;
            btnVerberg.IsEnabled = true;        
            aantalLevens = 10;
            imageLevens.Source = new BitmapImage(new Uri(levensKrijt[10], UriKind.Relative));

        }


        // niet zichtbaar door blouw hover effect 
        private void RandMouseEnter(object sender, MouseEventArgs e)
        {
            Button btnRaad = (Button)sender;
            btnRaad.BorderBrush = Brushes.Red;

            Button BtnVerbergWoord = (Button)sender;
            btnVerberg.BorderBrush = Brushes.Red;

            Button BtnNieuw = (Button)sender;
            BtnNieuw.BorderBrush = Brushes.Red;
        }

        private void RandMouseLeave(object sender, MouseEventArgs e)
        {
            Button BtnNieuw = (Button)sender;
            BtnNieuw.BorderBrush = Brushes.Gray;

            Button btnRaad = (Button)sender;
            btnRaad.BorderBrush = Brushes.Gray;

            Button BtnVerbergWoord = (Button)sender;
            btnVerberg.BorderBrush = Brushes.Gray;
        }

        private void BtnTerug_Click(object sender, RoutedEventArgs e)
        {
            MainWindow a = new MainWindow();
            Hide();
            a.ShowDialog();
            Close();
        }
    }
}

    

