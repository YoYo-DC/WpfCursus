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
using System.ComponentModel;
using Microsoft.Win32;
using System.IO;

namespace Bars
{
    /// <summary>
    /// Interaction logic for BarsWindow.xaml
    /// </summary>
    public partial class BarsWindow : Window
    {
        public static RoutedCommand mijnRouteCtrlB = new RoutedCommand();
        public static RoutedCommand mijnRouteCtrlI = new RoutedCommand();

        /*Logica achter het gebruik van commands (via code-behind)
         * 
         * SAMENVATTING: zorg ervoor dat het command gebonden is aan de uit te voeren eventHandler-method, de vereiste toetscombinatie
         *               ,het object dat de method kan uitvoeren en het object dat de vereiste toetscombinatie registreert!
         * 
         * !) het command is nog niet gedefineerd
         * 1) Instantieer een nieuw Command-object van het type RoutedCommand.
         * !) De uit te voeren eventHandler-method is nog niet gedefineerd
         * 2) Maak een losse handler voor de Executed-events fired door de CommandManager.
         *    (Dit event vindt plaats wanneer een gebruiker een Command-toetscombinatie ingeeft.)
         * !) Het command is nog niet gebonden aan de uit te voeren method   
         * 3) Verbindt het Command-object met de handler dmv een nieuw CommandBinding-object.
         * !) het object weet nog niet dat het de method moet uitvoeren
         * 4) Voeg het CommandBinding-object toe aan een lijst met CommandBinding-objecten.
         * !) de vereiste toetscombinatie is nog niet gedefinieerd
         * 5) Definieer de toe te wijzen toetscombinatie dmv een nieuw KeyGesture-object.
         * !) het command is nog niet gebonden aan de vereiste toescombinatie
         * 6) Wijs de toetscombinatie toe aan het Command-object dmv een nieuw KeyBinding-object.
         * !) het object weet nog niet op welke toetscombinatie het moet reageren
         * 7) Voeg het KeyBinding-object toe aan een lijst met KeyBinding-objecten.
         */

        public BarsWindow()
        {
            InitializeComponent();

            //CommandBinding MijnCtrlB = new CommandBinding(mijnRouteCtrlB, ctrlB_Executed);
            //this.CommandBindings.Add(MijnCtrlB);
            //KeyGesture toetsCtrlB =
            //    new KeyGesture(Key.B, ModifierKeys.Control);
            //KeyBinding mijnKeyCtrlB = new KeyBinding(mijnRouteCtrlB, toetsCtrlB);
            //this.InputBindings.Add(mijnKeyCtrlB);

            //CommandBinding mijnCtrlI = new CommandBinding(mijnRouteCtrlI, ctrlI_Executed);
            //this.CommandBindings.Add(mijnCtrlI);
            //KeyGesture toetsCtrlI =
            //    new KeyGesture(Key.I, ModifierKeys.Control);
            //KeyBinding mijnKeyCtrlI = new KeyBinding(mijnRouteCtrlI, toetsCtrlI);
            //this.InputBindings.Add(mijnKeyCtrlI);
        }

        //De SortDescriptions-collection (van ComboBox- en ListBox-objecten) wordt gebruikt om de Items-collection voor deze objecten te sorteren
        //Deze class zit in de System.ComponentModel-namespace
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            comboBoxLettertype.Items.SortDescriptions.Add(new SortDescription("Source", ListSortDirection.Ascending));
            comboBoxLettertype.SelectedIndex = 0;
        }

        private void Vet_Aan_Uit(bool wissel = false)
        {
            //Als de geopende tekst vetgedrukt is of de tekst in het programma op vetgedrukt gezet wordt:
            if ((wissel == true && textBoxVoorbeeld.FontWeight == FontWeights.Bold)
                || (wissel == false && textBoxVoorbeeld.FontWeight == FontWeights.Normal))
            {
                textBoxVoorbeeld.FontWeight = FontWeights.Bold;
                menuItemVet.IsChecked = true;
                statusBarItemVet.FontWeight = FontWeights.Bold;
                buttonVet.IsChecked = true;
            }
            //Als de geopende tekst niet vetgedrukt is of de tekst in het programma terug op normaal gezet wordt
            else
            {
                textBoxVoorbeeld.FontWeight = FontWeights.Normal;
                menuItemVet.IsChecked = false;
                statusBarItemVet.FontWeight = FontWeights.Normal;
                buttonVet.IsChecked = false;
            }
            buttonVet.IsChecked = menuItemVet.IsChecked;
        }

        private void ctrlB_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Vet_Aan_Uit();
        }

        private void ctrlI_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Vet_Aan_Uit();
        }

        private void menuItemVet_Click(object sender, RoutedEventArgs e)
        {
            Vet_Aan_Uit();
        }

        //Als code meermaals terugkomt over verschillende eventHandlers, beter optimaliseren naar 1 method
        private void Schuin_Aan_Uit(bool wissel = false)
        {
            //Als de geopende tekst schuingedrukt is of de tekst in het programma op schuingedrukt gezet wordt:
            if ((wissel == true && textBoxVoorbeeld.FontStyle == FontStyles.Italic)
                || (wissel == false && textBoxVoorbeeld.FontStyle == FontStyles.Normal))
            {
                textBoxVoorbeeld.FontStyle = FontStyles.Italic;
                menuItemSchuin.IsChecked = true;
                statusBarItemSchuin.FontStyle = FontStyles.Italic;
                buttonSchuin.IsChecked = true;
            }
            //Als de geopende tekst niet schuingedrukt is of de tekst in het programma terug op normaal gezet wordt
            else
            {
                textBoxVoorbeeld.FontStyle = FontStyles.Normal;
                menuItemSchuin.IsChecked = false;
                statusBarItemSchuin.FontStyle = FontStyles.Normal;
                buttonVet.IsChecked = false;
            }
            buttonSchuin.IsChecked = menuItemSchuin.IsChecked;
        }

        private void menuItemSchuin_Click(object sender, RoutedEventArgs e)
        {
            Schuin_Aan_Uit();
        }

        //Als de logica binnen events van meerdere (en toekomstige) controls hetzelfde is, deze code optimaliseren naar 1 event en alle controls registreren voor dit ene event
        //Zeer handig om te anticiperen op eventuele extra features die in dezelfde lijn liggen!
        private void Lettertype_Click(object sender, RoutedEventArgs e)
        {
            MenuItem hetLettertype = (MenuItem)sender;
            foreach (MenuItem huidig in Fontjes.Items)
            {
                huidig.IsChecked = false;
            }
            hetLettertype.IsChecked = true;
            comboBoxLettertype.SelectedItem = new FontFamily(hetLettertype.Header.ToString());
        }

        private void Lettertype_Aan_Uit()
        {
            foreach (MenuItem huidig in Fontjes.Items)
                if (comboBoxLettertype.SelectedValue.ToString() == huidig.Header.ToString())
                    huidig.IsChecked = true;
                else
                    huidig.IsChecked = false;
        }

        private void comboBoxLettertype_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Lettertype_Aan_Uit();
        }

        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.FileName = "Document";
                dlg.DefaultExt = ".txt";
                dlg.Filter = "tekst documents |*.txt";

                if (dlg.ShowDialog() == true)
                {
                    using (StreamWriter bestand = new StreamWriter(dlg.FileName))
                    {
                        bestand.WriteLine(comboBoxLettertype.SelectedValue);
                        bestand.WriteLine(textBoxVoorbeeld.FontWeight.ToString());
                        bestand.WriteLine(textBoxVoorbeeld.FontStyle.ToString());
                        bestand.WriteLine(textBoxVoorbeeld.Text);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("opslaan mislukt : " + ex.Message);
            }
        }

        private void Open_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.FileName = "Document";
                dlg.DefaultExt = ".txt";
                dlg.Filter = "tekst documents |*.txt";

                if (dlg.ShowDialog() == true)
                {
                    using (StreamReader lezer = new StreamReader(dlg.FileName))
                    {
                        comboBoxLettertype.SelectedValue =
                            new FontFamily(lezer.ReadLine());
                        Lettertype_Aan_Uit();

                        TypeConverter convertBold =
                            TypeDescriptor.GetConverter(typeof(FontWeight));
                        textBoxVoorbeeld.FontWeight =
                            (FontWeight)convertBold.ConvertFromString(lezer.ReadLine());
                        Vet_Aan_Uit(true);

                        TypeConverter convertStyle =
                            TypeDescriptor.GetConverter(typeof(FontStyle));
                        textBoxVoorbeeld.FontStyle =
                            (FontStyle)convertStyle.ConvertFromString(lezer.ReadLine());
                        Schuin_Aan_Uit(true);

                        textBoxVoorbeeld.Text = lezer.ReadLine();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("openen mislukt : " + ex.Message);
            }
        }

        private double a4Breedte = 21 / 2.54 * 96;
        private double a4Hoogte = 29.7 / 2.54 * 96;
        private double vertPositie;

        /* Maakt het FixedDocument-object nodig voor de afdruk aan:
         * 
         * Belangrijke fields (private): grootte en breedte van papier (in pixels!), de positie van de "printkop"
         * 
         * 1) We hebben om de één of andere reden een DocumentPaginator-object nodig als parameter voor de PrintDocument-method, hoewel we vertrekken van een nieuw FixedDocument-object
         * 2) Dat DocumentPaginator-object bevat informatie over alle afdruk-pagina's
         * 3) Zo kunnen we informatie over de paginagrootte instellen via de PageSize-property
         * !) Het FixedDocument-object staat niet gelijk met één af te drukken pagina, maar bevat alle
         *    af te drukken pagina's als een verzameling van PageContent-objecten (standaard is die leeg)
         * 4) We maken een nieuw PageContent-object aan en stoppen dat in de verzameling
         * !) Dit object is nog steeds niet de af te drukken pagina zelf, maar bevat wel info over die pagina
         * 5) We maken een nieuw FixedPage-object aan (dit is eindelijk de af te drukken pagina) en maken de info
         *    hierover bekend aan het PageContent-object via de Child-property van dat object.
         * 6) We stellen (God knows why...) opnieuw de hoogte en breedte van de pagina in.
         * 7) We vullen de inhoud van de pagina in:
         *     Die inhoud bestaat uit een verzameling van TextBlock-objecten:
         *     
         *     In dit geval kan je best één TextBlock-object laten overeenkomen met 1 regel op de afdrukpagina
         *     Ieder TextBlock-object is de returnwaarde van een aparte method
         *     !) Hier is de GetLineText-property van TextBox zeer handig
        */
        private FixedDocument StelAfdrukSamen()
        {
            FixedDocument document = new FixedDocument();
            document.DocumentPaginator.PageSize =
                new Size(a4Breedte, a4Hoogte);

            PageContent inhoud = new PageContent();
            document.Pages.Add(inhoud);

            FixedPage page = new FixedPage();
            inhoud.Child = page;

            page.Width = a4Breedte;
            page.Height = a4Hoogte;
            vertPositie = 96;

            page.Children.Add(Regel("gebruikt lettertype : " +
                textBoxVoorbeeld.FontFamily.ToString()));
            page.Children.Add(Regel("gewicht van het lettertype : " +
                textBoxVoorbeeld.FontWeight.ToString()));
            page.Children.Add(Regel("stijl van het lettertype : " +
                textBoxVoorbeeld.FontStyle.ToString()));
            page.Children.Add(Regel(" "));
            page.Children.Add(Regel("inhoud van de tekstbox : "));
            for (int i = 0 ; i < textBoxVoorbeeld.LineCount; i++)
            {
                page.Children.Add(Regel(textBoxVoorbeeld.GetLineText(i)));
            }

            return document;


        }

        private TextBlock Regel(string tekst)
        {
            TextBlock deRegel = new TextBlock();
            deRegel.Text = tekst;
            deRegel.FontSize = textBoxVoorbeeld.FontSize;
            deRegel.FontFamily = textBoxVoorbeeld.FontFamily;
            deRegel.FontWeight = textBoxVoorbeeld.FontWeight;
            deRegel.FontStyle = textBoxVoorbeeld.FontStyle;
            deRegel.Margin = new Thickness(96, vertPositie, 96, 96);
            vertPositie += 30;

            return deRegel;
        }

        private void Print_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            PrintDialog afdrukken = new PrintDialog();
            if (afdrukken.ShowDialog() == true)
            {
                afdrukken.PrintDocument(StelAfdrukSamen().DocumentPaginator, "tekstbox");
            }
        }

        // Maak een nieuw Window-object aan van het type Afdrukvoorbeeld.
        // Leg een Parent-Child relatie tussen dit Window-object (BarsWindow) en dat Window-object (Afdrukvoorbeeld).
        // Toon het Window-object aan de gebruiker
        private void PrintPreview_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Afdrukvoorbeeld preview = new Afdrukvoorbeeld();
            preview.Owner = this;
            preview.AfdrukDocument = StelAfdrukSamen();
            preview.ShowDialog();
        }

        //Hier enkel specifiëren wat er moet gebeuren als de gebruiker het programma NIET wil afsluiten!
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (MessageBox.Show("Programma afsluiten ?", "Afsluiten",
                MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No)
                == MessageBoxResult.No)
                e.Cancel = true;
        }

        //Belangrijk om weten: de Close()-method sluit de applicatie hier niet direct af, 
        // maar roept eerst de Window_Closing EventHandler op!
        //Daarom moet je in deze method geen rekening houden met wat er moet gebeuren als
        // de gebruiker het programma toch niet wil afsluiten.
        private void Close_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
        }
    }
}
