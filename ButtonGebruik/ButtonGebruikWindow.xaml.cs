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

namespace ButtonGebruik
{
    /// <summary>
    /// Interaction logic for ButtonGebruikWindow.xaml
    /// </summary>
    public partial class ButtonGebruikWindow : Window
    {
        public ButtonGebruikWindow()
        {
            InitializeComponent();
        }

        private void buttonBold_Checked(object sender, RoutedEventArgs e)
        {
            labelTekst.FontWeight = FontWeights.Bold;
            checkBoxBold.IsChecked = buttonBold.IsChecked;
        }

        private void buttonBold_Unchecked(object sender, RoutedEventArgs e)
        {
            labelTekst.FontWeight = FontWeights.Normal;
            checkBoxBold.IsChecked = buttonBold.IsChecked;
        }

        private void buttonItalic_Click(object sender, RoutedEventArgs e)
        {
            ToggleButton knop = (ToggleButton)sender;

            if (knop.IsChecked == true)
                labelTekst.FontStyle = FontStyles.Italic;
            else
                labelTekst.FontStyle = FontStyles.Normal;
            checkBoxItalic.IsChecked = knop.IsChecked;
            buttonItalic.IsChecked = knop.IsChecked;
        }

        private void repeatButtonGroter_Click(object sender, RoutedEventArgs e)
        {
            if (labelTekst.FontSize < 25)
                labelTekst.FontSize++;
        }

        private void repeatButtonKleiner_Click(object sender, RoutedEventArgs e)
        {
            if (labelTekst.FontSize > 1)
                labelTekst.FontSize--;
        }

        private void Kleur_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton knop = (RadioButton)sender;
            labelTekst.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString(knop.Content.ToString());
        }

        private void checkBoxBold_Click(object sender, RoutedEventArgs e)
        {
            buttonBold.IsChecked = checkBoxBold.IsChecked;
        }

        
    }
}
