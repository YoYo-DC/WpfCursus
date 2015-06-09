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

        private void buttonBestellen_Click(object sender, RoutedEventArgs e)
        {
            List<string> bestellingGegevens = new List<string>();
            ToggleButton knop;
            StackPanel paneel;
            Label label;
            foreach (FrameworkElement vak in gridBestelling.Children)
            {
                if (vak is StackPanel)
                {
                    paneel = (StackPanel)vak;
                    foreach (FrameworkElement kind in paneel.Children)
                    {
                        if (kind is ToggleButton)
                        {
                            knop = (ToggleButton)kind;
                            if (knop.IsChecked == true)
                                bestellingGegevens.Add(knop.Content.ToString());
                        }
                        if (kind is Label)
                        {
                            label = (Label)kind;
                            bestellingGegevens.Add(label.Content.ToString());
                        }
                    }
                }
            }
            labelBestelling.Content = string.Format("U heeft {0} medium pizza('s) besteld met: {1}, {2} en {3} overstrooid met {4}",labelAantal.ToString());
        }
    }
}
