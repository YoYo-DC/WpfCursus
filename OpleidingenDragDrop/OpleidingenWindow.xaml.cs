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

namespace Taak3
{
    /// <summary>
    /// Interaction logic for OpleidingenWindow.xaml
    /// </summary>
    public partial class OpleidingenWindow : Window
    {
        public OpleidingenWindow()
        {
            InitializeComponent();
            ListBoxProgrammas.Items.Add(new OfficeProgramma("Access", new BitmapImage(new Uri(@"images\access.png", UriKind.Relative))));
            ListBoxProgrammas.Items.Add(new OfficeProgramma("Excel", new BitmapImage(new Uri(@"images\excel.png", UriKind.Relative))));
            ListBoxProgrammas.Items.Add(new OfficeProgramma("Groove", new BitmapImage(new Uri(@"images\groove.png", UriKind.Relative))));
            ListBoxProgrammas.Items.Add(new OfficeProgramma("InfoPath", new BitmapImage(new Uri(@"images\infopath.png", UriKind.Relative))));
            ListBoxProgrammas.Items.Add(new OfficeProgramma("OneNote", new BitmapImage(new Uri(@"images\onenote.png", UriKind.Relative))));
            ListBoxProgrammas.Items.Add(new OfficeProgramma("Outlook", new BitmapImage(new Uri(@"images\outlook.png", UriKind.Relative))));
            ListBoxProgrammas.Items.Add(new OfficeProgramma("Powerpoint", new BitmapImage(new Uri(@"images\powerpoint.png", UriKind.Relative))));
            ListBoxProgrammas.Items.Add(new OfficeProgramma("Publisher", new BitmapImage(new Uri(@"images\publisher.png", UriKind.Relative))));
            ListBoxProgrammas.Items.Add(new OfficeProgramma("Word", new BitmapImage(new Uri(@"images\word.png", UriKind.Relative))));
            ListBoxProgrammas.SelectedIndex = 0;
        }

        //We gebruiken de VisualTreeHelper om op te klimmen in de hiërarchie zolang ons object geen ListBoxItem is
        private ListBoxItem VindListBoxItem(Object sleepItem)
        {
            DependencyObject keuze = (DependencyObject)sleepItem;
            while(keuze != null)
            {
                if (keuze is ListBoxItem)
                    return (ListBoxItem)keuze;
                keuze = VisualTreeHelper.GetParent(keuze);
            }
            return null;
        }
    }
}
