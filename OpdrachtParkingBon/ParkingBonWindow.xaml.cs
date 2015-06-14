using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.IO;

namespace ParkingBon
{
    /// <summary>
    /// Interaction logic for ParkingBonWindow.xaml
    /// </summary>
    public partial class ParkingBonWindow : Window
    {

        public ParkingBonWindow()
        {
            InitializeComponent();
            Nieuw();
        }


        private void Nieuw()
        {
            DatumBon.SelectedDate = DateTime.Now;
            AankomstLabelTijd.Content = DateTime.Now.ToLongTimeString();
            TeBetalenLabel.Content = "0 €";
            VertrekLabelTijd.Content = AankomstLabelTijd.Content;
            ControleerTeBetalen();
            statusBarBestand.Content = "nieuwe bon";

        }


        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (MessageBox.Show("Programma afsluiten ?", "Afsluiten", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.No)
                e.Cancel = true;
        }

        private void minder_Click(object sender, RoutedEventArgs e)
        {
            int bedrag = Convert.ToInt32(TeBetalenLabel.Content.ToString().Replace(" €", ""));
            if (bedrag > 0)
                bedrag -= 1;
            TeBetalenLabel.Content = bedrag.ToString() + " €";
            VertrekLabelTijd.Content = Convert.ToDateTime(AankomstLabelTijd.Content).AddHours(0.5 * bedrag).ToLongTimeString();
            ControleerTeBetalen();
        }

        private void meer_Click(object sender, RoutedEventArgs e)
        {
            int bedrag = Convert.ToInt32(TeBetalenLabel.Content.ToString().Replace(" €", ""));
            DateTime vertrekuur = Convert.ToDateTime(AankomstLabelTijd.Content).AddHours(0.5 * bedrag);
            if (vertrekuur.Hour < 22)
                bedrag += 1;
            TeBetalenLabel.Content = bedrag.ToString() + " €";
            VertrekLabelTijd.Content = Convert.ToDateTime(AankomstLabelTijd.Content).AddHours(0.5 * bedrag).ToLongTimeString();
            ControleerTeBetalen();
        }

        private void New_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Nieuw();
        }

        private void Open_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.FileName = "document";
                dlg.DefaultExt = ".bon";
                dlg.Filter = "parkeerbon |*.bon";

                if(dlg.ShowDialog()==true)
                {
                    using (StreamReader bestand = new StreamReader(dlg.FileName))
                    {
                        DatumBon.SelectedDate = Convert.ToDateTime(bestand.ReadLine());
                        AankomstLabelTijd.Content = bestand.ReadLine();
                        TeBetalenLabel.Content = bestand.ReadLine();
                        VertrekLabelTijd.Content = bestand.ReadLine();
                    }
                    statusBarBestand.Content = dlg.FileName;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Openen mislukt: " + ex.Message, "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                DateTime tijdstip = Convert.ToDateTime(AankomstLabelTijd.Content);
                DateTime datum = Convert.ToDateTime(DatumBon.Text.ToString());

                SaveFileDialog dlg = new SaveFileDialog();
                dlg.DefaultExt = ".bon";
                dlg.FileName = datum.ToString("dd-MM-yyyy") + "om" + tijdstip.ToString("HH-mm-ss" + "." + dlg.DefaultExt);
                dlg.Filter = "parkeerbon |*.bon";

                if (dlg.ShowDialog() == true)
                {
                    using (StreamWriter bestand = new StreamWriter(dlg.FileName))
                    {
                        bestand.WriteLine(datum.ToShortDateString());
                        bestand.WriteLine(AankomstLabelTijd.ToString());
                        bestand.WriteLine(TeBetalenLabel.Content.ToString());                
                        bestand.WriteLine(VertrekLabelTijd.ToString());                      
                    }
                    statusBarBestand.Content = dlg.FileName;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Opslaan mislukt: " + ex.Message, "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void PrintPreview_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OpdrachtParkingBon.Afdrukvoorbeeld voorbeeld = new OpdrachtParkingBon.Afdrukvoorbeeld();
            voorbeeld.Owner = this;
            voorbeeld.Document = StelAfdrukSamen();
            voorbeeld.ShowDialog();
        }

        private void Close_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
        }

        //Probeer eens een bool als parameter te gebruiken waar van je de waarde koppelt aan
        // elke IsEnabled-property
        private void ControleerTeBetalen()
        {
            string bedrag = TeBetalenLabel.Content.ToString().Trim();
            bedrag = bedrag.Remove(bedrag.IndexOf(" "));

            if(decimal.Parse(bedrag) > 0)
            {
                buttonOpslaan.IsEnabled = true;
                buttonAfdrukvoorbeeld.IsEnabled = true;
                menuItemBonOpslaan.IsEnabled = true;
                menuItemBonAfdrukken.IsEnabled = true;
            }
            else
            {
                buttonOpslaan.IsEnabled = false;
                buttonAfdrukvoorbeeld.IsEnabled = false;
                menuItemBonOpslaan.IsEnabled = false;
                menuItemBonAfdrukken.IsEnabled = false;
            }
        }

        private double docBreedteValue = 640;
        private double docLengteValue = 320;
        private double vertPositieValue;
        private double horizPositieValue;

        private FixedDocument StelAfdrukSamen()
        {
            FixedDocument afdruk = new FixedDocument();
            afdruk.DocumentPaginator.PageSize =
                new Size(docBreedteValue, docLengteValue);

            PageContent inhoud = new PageContent();
            afdruk.Pages.Add(inhoud);

            FixedPage pagina = new FixedPage();
            inhoud.Child = pagina;
            pagina.Width = docBreedteValue;
            pagina.Height = docLengteValue;
            vertPositieValue = 96;
            horizPositieValue = 96;
            pagina.Children.Add(Afbeelding(logoImage));
            horizPositieValue += 204;
            pagina.Children.Add(Regel("datum: " + DatumBon.Text.ToString()));
            pagina.Children.Add(Regel("starttijd: " + AankomstLabelTijd.Content.ToString()));
            pagina.Children.Add(Regel("eindtijd: " + VertrekLabelTijd.Content.ToString()));
            pagina.Children.Add(Regel("bedrag betaald: " + TeBetalenLabel.Content.ToString()));

            return afdruk;

        }

        private Image Afbeelding(Image bron)
        {
            Image afbeelding = new Image();
            afbeelding.Source = logoImage.Source;
            afbeelding.Margin =
                new Thickness(horizPositieValue, vertPositieValue, 96, 96);

            return afbeelding;
        }

        private TextBlock Regel(string tekst)
        {
            TextBlock deRegel = new TextBlock();
            deRegel.Text = tekst + "\n";
            deRegel.FontSize = 18;
            deRegel.Margin =
                new Thickness(horizPositieValue, vertPositieValue, 96, 96);
            vertPositieValue += deRegel.FontSize * 2;

            return deRegel;
        }
    }
}
