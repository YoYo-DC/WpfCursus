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

namespace BloemSamenstelling
{
    /// <summary>
    /// Interaction logic for BloemWindow.xaml
    /// </summary>
    public partial class BloemWindow : Window
    {
         public BloemWindow()
        {
            InitializeComponent();
            foreach (PropertyInfo info in typeof(Colors).GetProperties())
            {
                BrushConverter bc = new BrushConverter();
                SolidColorBrush kleurke = (SolidColorBrush)bc.ConvertFromString(info.Name);
                Kleur kleurNaam = new Kleur();
                kleurNaam.Borstel = kleurke;
                kleurNaam.Naam = info.Name;
                kleurNaam.Hex = kleurke.ToString();
                kleurNaam.Rood = kleurke.Color.R;
                kleurNaam.Groen = kleurke.Color.G;
                kleurNaam.Blauw = kleurke.Color.B;
                foreach (FrameworkElement child in panelKleuren.Children)
                {
                    if(child is ComboBox)
                    {
                        ComboBox box = (ComboBox)child;
                        box.Items.Add(kleurNaam);
                        if (kleurNaam.Naam == "Black")
                            box.SelectedItem = kleurNaam;
                    }
                }
                    
            }
        }
    }
}
