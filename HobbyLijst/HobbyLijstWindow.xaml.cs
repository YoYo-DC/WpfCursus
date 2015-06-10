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

namespace HobbyLijst
{
    /// <summary>
    /// Interaction logic for HobbyLijstWindow.xaml
    /// </summary>
    public partial class HobbyLijstWindow : Window
    {
        public List<Hobby> hobbies = new List<Hobby>();

        public HobbyLijstWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            hobbies.Add(new Hobby("sport", "voetbal", new BitmapImage(new Uri("pack://application:,,,/Images/voetbal.jpg", UriKind.Absolute))));
            hobbies.Add(new Hobby("sport", "atletiek", new BitmapImage(new Uri("pack://application:,,,/Images/atletiek.jpg", UriKind.Absolute))));
            hobbies.Add(new Hobby("sport", "basketbal", new BitmapImage(new Uri("pack://application:,,,/Images/basketbal.jpg", UriKind.Absolute))));
            hobbies.Add(new Hobby("sport", "tennis", new BitmapImage(new Uri("pack://application:,,,/Images/tennis.jpg", UriKind.Absolute))));
            hobbies.Add(new Hobby("sport", "turnen", new BitmapImage(new Uri("pack://application:,,,/Images/turnen.jpg", UriKind.Absolute))));
            hobbies.Add(new Hobby("muziek", "trompet", new BitmapImage(new Uri("pack://application:,,,/Images/trompet.jpg", UriKind.Absolute))));
            hobbies.Add(new Hobby("muziek", "drum", new BitmapImage(new Uri("pack://application:,,,/Images/drum.jpg", UriKind.Absolute))));
            hobbies.Add(new Hobby("muziek", "gitaar", new BitmapImage(new Uri("pack://application:,,,/Images/gitaar.jpg", UriKind.Absolute))));
            hobbies.Add(new Hobby("muziek", "piano", new BitmapImage(new Uri("pack://application:,,,/Images/piano.jpg", UriKind.Absolute))));

            ComboBoxCategorie.Items.Add("- alle categorieën -");
            ComboBoxCategorie.Items.Add("muziek");
            ComboBoxCategorie.Items.Add("sport");
            ComboBoxCategorie.SelectedIndex = 0;
        }

        private void ComboBoxCategorie_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            listBoxHobbies.Items.Clear();
            foreach (Hobby hobby in hobbies)
            {
                if (hobby.Categorie == ComboBoxCategorie.SelectedItem.ToString() || ComboBoxCategorie.SelectedIndex == 0)
                    listBoxHobbies.Items.Add(hobby);
            }
            listBoxHobbies.Items.SortDescriptions.Add(new SortDescription("Activiteit", ListSortDirection.Ascending));
        }

        private void buttonKies_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxHobbies.SelectedItem != null)
            {
                Hobby gekozenHobby = (Hobby)listBoxHobbies.SelectedItem;
                //listBoxGekozen.Items.Add(gekozenHobby.Categorie + " : " + gekozenHobby.Activiteit);
                listBoxGekozen.Items.Add(gekozenHobby);
                listBoxGekozen.Items.SortDescriptions.Add(new SortDescription("Categorie", ListSortDirection.Ascending));
                listBoxGekozen.Items.SortDescriptions.Add(new SortDescription("Activiteit", ListSortDirection.Ascending));
            }
        }

        private void buttonVerwijderen_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxGekozen.SelectedItem != null)
                listBoxGekozen.Items.Remove(listBoxGekozen.SelectedItem);
        }

        private void buttonSamenvatting_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Wil je de gekozen hobby's op een rijtje?", "Samenvatting", MessageBoxButton.YesNo,
                MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.Yes)
            {
                string mijnTekst = "Mijn hobby's zijn: ";
                string cat = string.Empty;

                foreach (Object item in listBoxGekozen.Items)
                {
                    Hobby mijnHobby = (Hobby)item;
                    if (cat != mijnHobby.Categorie)
                    {
                        cat = mijnHobby.Categorie;
                        mijnTekst += "\n" + mijnHobby.Categorie + " : " +
                        mijnHobby.Activiteit;
                    }
                    else
                    {
                        mijnTekst += ", " + mijnHobby.Activiteit;
                    }
                }
                MessageBox.Show((listBoxGekozen.Items.Count == 0) ? "Ik heb geen hobby's" : mijnTekst, "Samenvatting", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }


    }
}
