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
using System.Reflection;


namespace KleurenKiezer
{
    /// <summary>
    /// Interaction logic for KleurenKiezenWindow.xaml
    /// </summary>
    public partial class KleurenKiezenWindow : Window
    {
        public KleurenKiezenWindow()
        {
            /* 1) Informatie over iedere static Color-property van de klasse Colors wordt verzamelt via Reflection.
             *  a) Voor iedere Color-property wordt: 
             *      de Name-property omgezet in een Brush-object dmv een BrushConverter-object (vaak gebruikt!)
             *      een nieuw Kleur-object gemaakt.
             *      alle properties van dit object ingevuld met informatie uit het Brush-object
             *       (de ToString-method geeft de hexwaarde terug!
             *      dit object toegevoegd aan de lijst van items in de combobox
             *       (als de kleur "zwart" wordt toegevoegd, wordt dit item geselecteerd)
             */  

            InitializeComponent();
            foreach (PropertyInfo info in typeof(Colors).GetProperties())
            {
                BrushConverter bc = new BrushConverter();
                SolidColorBrush deKleur =
                    (SolidColorBrush)bc.ConvertFromString(info.Name);
                Kleur kleurke = new Kleur();
                kleurke.Borstel = deKleur;
                kleurke.Naam = info.Name;
                kleurke.Hex = deKleur.ToString();
                kleurke.Rood = deKleur.Color.R;
                kleurke.Groen = deKleur.Color.G;
                kleurke.Blauw = deKleur.Color.B;
                comboBoxKleuren.Items.Add(kleurke);
                if (kleurke.Naam == "Black")
                    comboBoxKleuren.SelectedItem = kleurke;
            }
        }

        private void buttonKleur_Click(object sender, RoutedEventArgs e)
        {
            if (radioVoorgrond.IsChecked == true)
            {
                rechthoek.Fill = new SolidColorBrush(Color.FromRgb(
                    Convert.ToByte(labelRed.Content.ToString()),
                    Convert.ToByte(labelGreen.Content.ToString()),
                    Convert.ToByte(labelBlue.Content.ToString())));
                comboBoxKleuren.SelectedIndex = -1;
                foreach (Kleur kleurnaam in comboBoxKleuren.Items)
                {
                    if(rechthoek.Fill.ToString() == kleurnaam.Hex)
                        comboBoxKleuren.SelectedItem = kleurnaam;
                }
            }

            if ((radioAchtergrond.IsChecked == true) &&
                (comboBoxKleuren.SelectedIndex > -1))
            {
                Kleur gekozenKleur = (Kleur)comboBoxKleuren.SelectedItem;
                panelVoorbeeld.Background = gekozenKleur.Borstel;
                sliderRed.Value = gekozenKleur.Rood;
                sliderGreen.Value = gekozenKleur.Groen;
                sliderBlue.Value = gekozenKleur.Blauw;
            }
        }
    }
}
