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

namespace OpdrachtVerkeerslicht
{
    /// <summary>
    /// Interaction logic for Verkeerslicht.xaml
    /// </summary>
    public partial class Verkeerslicht : Window
    {
        public Verkeerslicht()
        {
            InitializeComponent();
        }

        private void veranderButton_Click(object sender, RoutedEventArgs e)
        {
            Button knop = (Button)sender;
            SolidColorBrush kleur;
            if (knop.Name == opgeletButton.Name)
            {
                if (this.roodLicht.Opacity == 1)
                {
                    this.roodLicht.Opacity = 0;
                    this.goButton.IsEnabled = this.opgeletButton.IsEnabled;
                    this.opgeletButton.IsEnabled = this.stopButton.IsEnabled;
                    kleur = (SolidColorBrush)new BrushConverter().ConvertFromString(goButton.Tag.ToString());
                    this.goButton.Background = kleur;
                }
                else
                {
                    this.groenLicht.Opacity = 0;
                    this.stopButton.IsEnabled = this.opgeletButton.IsEnabled;
                    this.opgeletButton.IsEnabled = this.goButton.IsEnabled;
                    kleur = (SolidColorBrush)new BrushConverter().ConvertFromString(stopButton.Tag.ToString());
                    this.stopButton.Background = kleur;
                }
                this.oranjeLicht.Opacity = 1;
            }
            else
            {
                if (knop.Name == goButton.Name)
                {
                    this.opgeletButton.IsEnabled = this.goButton.IsEnabled;
                    this.goButton.IsEnabled = this.stopButton.IsEnabled;
                    this.groenLicht.Opacity = this.oranjeLicht.Opacity;
                }
                else
                {
                    this.opgeletButton.IsEnabled = this.stopButton.IsEnabled;
                    this.stopButton.IsEnabled = this.goButton.IsEnabled;
                    this.roodLicht.Opacity = this.oranjeLicht.Opacity;
                }
                this.oranjeLicht.Opacity = 0;
                kleur = (SolidColorBrush)new BrushConverter().ConvertFromString(opgeletButton.Tag.ToString());
                this.opgeletButton.Background = kleur;
            }

        }

    }
}
