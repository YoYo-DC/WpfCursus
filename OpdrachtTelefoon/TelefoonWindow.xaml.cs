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
using System.Media;

namespace OpdrachtTelefoon
{
    /// <summary>
    /// Interaction logic for TelefoonWindow.xaml
    /// </summary>
    public partial class TelefoonWindow : Window
    {
        public List<Persoon> personen = new List<Persoon>();

        public TelefoonWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            personen.Add(new Persoon("anne", "012485926", Groep.Vrienden, new BitmapImage(new Uri("pack://application:,,,/Images/anne.jpg", UriKind.Absolute))));
            personen.Add(new Persoon("bob", "018445976", Groep.Vrienden, new BitmapImage(new Uri("pack://application:,,,/Images/bob.jpg", UriKind.Absolute))));
            personen.Add(new Persoon("collega1", "014423626", Groep.Werk, new BitmapImage(new Uri("pack://application:,,,/Images/collega1.jpg", UriKind.Absolute))));
            personen.Add(new Persoon("collega2", "014487596", Groep.Werk, new BitmapImage(new Uri("pack://application:,,,/Images/collega2.jpg", UriKind.Absolute))));
            personen.Add(new Persoon("collega3", "014123545", Groep.Werk, new BitmapImage(new Uri("pack://application:,,,/Images/collega3.jpg", UriKind.Absolute))));
            personen.Add(new Persoon("ed", "013456878", Groep.Vrienden, new BitmapImage(new Uri("pack://application:,,,/Images/ed.jpg", UriKind.Absolute))));
            personen.Add(new Persoon("grotezus", "018456595", Groep.Familie, new BitmapImage(new Uri("pack://application:,,,/Images/grotezus.jpg", UriKind.Absolute))));
            personen.Add(new Persoon("kleinezus", "018448567", Groep.Familie, new BitmapImage(new Uri("pack://application:,,,/Images/kleinezus.jpg", UriKind.Absolute))));
            personen.Add(new Persoon("tantenon", "018785625", Groep.Familie, new BitmapImage(new Uri("pack://application:,,,/Images/tantenon.jpg", UriKind.Absolute))));
            personen.Add(new Persoon("vader", "018487596", Groep.Familie, new BitmapImage(new Uri("pack://application:,,,/Images/vader.jpg", UriKind.Absolute))));

            comboBoxGroep.Items.Add("Iedereen");
            comboBoxGroep.Items.Add("Familie");
            comboBoxGroep.Items.Add("Vrienden");
            comboBoxGroep.Items.Add("Werk");
            comboBoxGroep.SelectedIndex = 0;
        }

        private void comboBoxGroep_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            listBoxPersonen.Items.Clear();
            foreach (Persoon persoon in personen)
            {
                if (persoon.Groep.ToString() == comboBoxGroep.SelectedItem.ToString() ||
                    comboBoxGroep.SelectedIndex == 0)
                    listBoxPersonen.Items.Add(persoon);
            }
        }

        private void buttonTelefoon_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxPersonen.SelectedItem == null)
                MessageBox.Show("Je moet eerst iemand selecteren", "Niemand gekozen", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            else
            {
                Persoon gebelde = (Persoon)listBoxPersonen.SelectedItem;
                if(MessageBox.Show(string.Format("Wil je {0} bellen\nop het nummer: {1}", gebelde.Naam, gebelde.TelefoonNr),
                    "Telefoon", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.Yes)
                {
                    MediaPlayer speler = new MediaPlayer();
                    speler.Open(new Uri("pack://application:,,,/PHONE.wav",UriKind.Absolute));
                    speler.Play();
                }
            }
        }
    }
}
