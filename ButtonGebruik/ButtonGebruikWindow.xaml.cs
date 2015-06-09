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
        }

        private void buttonBold_Unchecked(object sender, RoutedEventArgs e)
        {
            labelTekst.FontWeight = FontWeights.Normal;
        }

        
    }
}
