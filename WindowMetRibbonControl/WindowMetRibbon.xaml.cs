using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace WindowMetRibbonControl
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    /*
     * Belangrijk voor inlezen van settings:
     *  Test eerst of de setting bestaat!
     */

    public partial class WindowMetRibbon : RibbonWindow
    {
        public WindowMetRibbon()
        {
            InitializeComponent();

            LeesMRU();

            if (WindowMetRibbonControl.Properties.Settings.Default.qat != null)
            {
                System.Collections.Specialized.StringCollection qatLijst =
                    WindowMetRibbonControl.Properties.Settings.Default.qat;
                int lijnNr = 0;
                while (lijnNr < qatLijst.Count)
                {
                    string commando = qatLijst[lijnNr];
                    string png = qatLijst[lijnNr + 1];
                    RibbonButton nieuweKnop = new RibbonButton();
                    BitmapImage icon = new BitmapImage();
                    icon.BeginInit();
                    icon.UriSource = new Uri(png);
                    icon.EndInit();
                    nieuweKnop.SmallImageSource = icon;

                    CommandBindingCollection ccol = this.CommandBindings;
                    foreach (CommandBinding cb in ccol)
                    {
                        RoutedUICommand rcb = (RoutedUICommand)cb.Command;
                        if (rcb.Text == commando)
                            nieuweKnop.Command = rcb;
                    }
                    Qat.Items.Add(nieuweKnop);
                    lijnNr += 2;
                }
                
            }
        }

        private void LeesBestand(string bestandsnaam)
        {
            try
            {
                using (StreamReader bestand = new StreamReader(bestandsnaam))
                {
                    TextBoxVoorbeeld.Text = bestand.ReadLine();
                }
                BijwerkenMRU(bestandsnaam);
            }
            catch (Exception ex)
            {
                MessageBox.Show("openen mislukt : " + ex.Message);
            }
        }

        private void OpenExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.DefaultExt = ".rvb";
            dlg.Filter = "Ribbon documents |*.rvb";

            if (dlg.ShowDialog() == true)
            {
                LeesBestand(dlg.FileName);
            }
        }

        private void SaveExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.DefaultExt = ".rvb";
                dlg.Filter = "Ribbon documents |*.rvb";

                if (dlg.ShowDialog() == true)
                {
                    using (StreamWriter bestand = new StreamWriter(dlg.FileName))
                    {
                        bestand.WriteLine(TextBoxVoorbeeld.Text);
                    }
                }
                BijwerkenMRU(dlg.FileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show("opslaan mislukt : " + ex.Message);
            }
        }

        private void CloseExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
        }

        private void NewExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            TextBoxVoorbeeld.Text = string.Empty;
        }

        private void PrintExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            PrintDialog afdrukken = new PrintDialog();
            if (afdrukken.ShowDialog() == true)
            {
                MessageBox.Show("Hier zou worden afgedrukt");
            }
        }

        private void PreviewExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("Hier zou een afdrukvoorbeeld moeten verschijnen");
        }

        private void HelpExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("Dit is helpscherm", "Help", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
        }

        private void MRUGallery_SelectionChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            LeesBestand(MRUGallery.SelectedValue.ToString());
        }

        private void RibbonRadioButtonKleur_Click(object sender, RoutedEventArgs e)
        {
            RibbonRadioButton keuze = (RibbonRadioButton)sender;
            BrushConverter bc = new BrushConverter();
            SolidColorBrush kleur = (SolidColorBrush)bc.ConvertFromString(keuze.Tag.ToString());
            TextBoxVoorbeeld.Foreground = kleur;
        }

        private void RibbonWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            System.Collections.Specialized.StringCollection qatLijst = new System.Collections.Specialized.StringCollection();
            if (WindowMetRibbonControl.Properties.Settings.Default.qat != null)
                WindowMetRibbonControl.Properties.Settings.Default.qat.Clear();
            foreach (Object li in Qat.Items)
            {
                if (li is RibbonButton)
                {
                    RibbonButton knop = (RibbonButton)li;
                    RoutedUICommand commando = (RoutedUICommand)knop.Command;
                    qatLijst.Add(commando.Text);
                    qatLijst.Add(knop.SmallImageSource.ToString());
                }
            }
            if (qatLijst.Count > 0)
            {
                WindowMetRibbonControl.Properties.Settings.Default.qat = qatLijst;
            }
            WindowMetRibbonControl.Properties.Settings.Default.Save();
        }

        private void LeesMRU()
        {
            MRUGalleryCat.Items.Clear();
            if (WindowMetRibbonControl.Properties.Settings.Default.mru != null)
            {
                System.Collections.Specialized.StringCollection mruLijst =
                    WindowMetRibbonControl.Properties.Settings.Default.mru;
                for (int lijnNr = 0; lijnNr < mruLijst.Count; lijnNr++)
                {
                    MRUGalleryCat.Items.Add(mruLijst[lijnNr]);
                }
            }
        }

        private void BijwerkenMRU(string bestandsnaam)
        {
            System.Collections.Specialized.StringCollection mruLijst =
                new System.Collections.Specialized.StringCollection();

            if (WindowMetRibbonControl.Properties.Settings.Default.mru != null)
            {
                mruLijst = WindowMetRibbonControl.Properties.Settings.Default.mru;
                int positie = mruLijst.IndexOf(bestandsnaam);
                if (positie >= 0)
                {
                    mruLijst.RemoveAt(positie);
                }
                else
                {
                    if(mruLijst.Count >= 6)
                    {
                        mruLijst.RemoveAt(5);
                    }
                }
            }
            mruLijst.Insert(0, bestandsnaam);
            WindowMetRibbonControl.Properties.Settings.Default.mru = mruLijst;
            WindowMetRibbonControl.Properties.Settings.Default.Save();
            LeesMRU();
        }
    }



    public class BooleanToFontWeight : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((Boolean)value)
                return "Bold";
            else
                return "Normal";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
    public class BooleanToFontStyle : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((Boolean)value)
                return "Italic";
            else
                return "Normal";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}

