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
using System.Windows.Controls.Primitives;

namespace OpdrachtPizza
{
    /// <summary>
    /// Interaction logic for PizzaWindow.xaml
    /// </summary>
    public partial class PizzaWindow : Window
    {
        public PizzaWindow()
        {
            InitializeComponent();
        }

        private void repeatButtonVerhogen_Click(object sender, RoutedEventArgs e)
        {
            int aantal = int.Parse(labelAantal.Content.ToString());
            if (aantal < 10)
            { 
                aantal++;
                labelAantal.Content = aantal;
            }
        }

        private void repeatButtonVerlagen_Click(object sender, RoutedEventArgs e)
        {
            int aantal = int.Parse(labelAantal.Content.ToString());
            if(aantal>1)
            {
                aantal--;
                labelAantal.Content = aantal;
            }
        }

        //Doorloop ieder child van grid. Als dit een stackpanel is, voeg dan de content van ieder ToggleButton- en Label-element in stringvorm toe aan een verzameling.
        //Suggestie in dit geval: maak de eindboodschap aan het begin aan en vervolledig hem terwijl de foreach loop de child elementen doorloopt.
        //Maak het code-behind gedeelte nog eens opnieuw en probeer rekening te houden met de suggestie.
        private void buttonBestellen_Click(object sender, RoutedEventArgs e)
        {
            ToggleButton knop;
            StackPanel paneel;
            List<string> ingredienten = new List<string>();
            string grootte = string.Empty;
            List<string> extras = new List<string>();
            foreach (FrameworkElement vak in gridBestelling.Children)
            {
                if (vak is StackPanel)
                {
                    paneel = (StackPanel)vak;
                    foreach (FrameworkElement kind in paneel.Children)
                    {
                        if (kind is RadioButton)
                        {
                            knop = (RadioButton)kind;
                            if (knop.IsChecked == true)
                                grootte = knop.Content.ToString();
                        }

                        else if (kind is CheckBox)
                        {
                            knop = (CheckBox)kind;
                            if (knop.IsChecked == true)
                                ingredienten.Add(knop.Content.ToString());
                        }

                        else if (kind is ToggleButton)
                        {
                            knop = (ToggleButton)kind;
                            if (knop.IsChecked == true)
                                extras.Add(knop.Content.ToString());
                        }
                    }
                }
            }
            string ingredientenLijn = string.Empty;
            foreach (string woord in ingredienten)
            {
                if (ingredienten.IndexOf(woord) == ingredienten.Count-1)
                    ingredientenLijn += "en ";
                ingredientenLijn += woord+" ";

            }
            textBlockBestelling.Text = string.Format("U heeft {0} {1} pizza('s) besteld met: {2}",labelAantal.Content.ToString(),grootte,ingredientenLijn);
        }
    }
}
