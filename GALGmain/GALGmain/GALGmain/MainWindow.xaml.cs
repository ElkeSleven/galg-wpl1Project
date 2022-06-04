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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GALGmain
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }
        int volAlfabet = 0 ;

        private void BtnGalg1Window_Click(object sender, RoutedEventArgs e)
        {
           
              GALG1 a = new GALG1();
              Hide();
              a.ShowDialog();
              Close();
        }

        private void BtnGalg2Window_Click(object sender, RoutedEventArgs e)
        {
            if(volAlfabet >= 26)
            {

              GALG2 a = new GALG2();
              Hide();
              a.ShowDialog();
              Close();
            }
            else
            {
                MessageBoxResult resaltaat = MessageBox.Show("je hebt het Alfabet gratis gekregen  ", "Sorry  De Shop Onder Constructie ", MessageBoxButton.OK, MessageBoxImage.Question );
                //MessageBox.Show(resaltaat.ToString());
                if (MessageBoxResult.OK == resaltaat)
                {
            
                    volAlfabet += 26;
                    lblAlfabet.Foreground = new SolidColorBrush(Colors.White);
                }
            }
     
        }

        private void BtnGalg3Window_Click(object sender, RoutedEventArgs e)
        {
            if (volAlfabet >= 26)
            {
              GALG3 a = new GALG3();
              Hide();
              a.ShowDialog();
              Close();
            }
            else
            {
                MessageBoxResult resaltaat = MessageBox.Show("je hebt het Alfabet gratis gekregen  ", "Sorry  De Shop Onder Constructie ", MessageBoxButton.OK, MessageBoxImage.Question);
                //MessageBox.Show(resaltaat.ToString());
                if (MessageBoxResult.OK == resaltaat)
                {

                    volAlfabet += 26;
                    lblAlfabet.Foreground = new SolidColorBrush(Colors.White);
                }
            }
        }

        private void BtnShop_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult resaltaat = MessageBox.Show(" Shop niet toegankelijk  ", "Sorry  De Shop Onder Constructie ", MessageBoxButton.OK,MessageBoxImage.Exclamation );
      
            if(MessageBoxResult.OK == resaltaat)
            {
                MessageBox.Show("je hebt het Alfabet gratis gekregen ");
                volAlfabet += 26;
                lblAlfabet.Foreground = new SolidColorBrush(Colors.White);

            }

        }
    }
}
