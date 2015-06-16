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
            while (keuze != null)
            {
                if (keuze is ListBoxItem)
                    return (ListBoxItem)keuze;
                keuze = VisualTreeHelper.GetParent(keuze);
            }
            return null;
        }

        private ListBox dragLijst;

        // De eventHandler is toegekend aan de ListBox, niet het ListBoxItem!
        // Dit wil niet zeggen dat de ListBox het event opgeroepen heeft!
        // Het ListBoxItem kan je vinden door de OriginalSource-property van het event
        //  als parameter door te geven aan de VindListBoxItem-method.

        // Check zeker of het gesleepte item niet Null is!
        // Dat is immers het geval wanneer je uit een leeg gedeelte binnen de ListBox begint te slepen
        private void Item_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                dragLijst = (ListBox)sender;
                ListBoxItem programmaItem = VindListBoxItem(e.OriginalSource);
                if (dragLijst.SelectedIndex > -1 && programmaItem != null)
                {
                    DataObject sleepData = new DataObject("sleepProgramma", programmaItem.Content);
                    DragDrop.DoDragDrop(programmaItem, sleepData, DragDropEffects.Move);
                }
            }
        }

        private void Item_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("sleepProgramma"))
            {
                OfficeProgramma dropProgramma = (OfficeProgramma)e.Data.GetData("sleepProgramma");
                ListBox dropLijst = (ListBox)sender;
                if (dragLijst != dropLijst)
                {
                    dragLijst.Items.Remove(dropProgramma);
                    dropLijst.Items.Add(dropProgramma);
                }
            }
        }

        private void ButtonDoorgeven_Click(object sender, RoutedEventArgs e)
        {
            string boodschap = string.Empty;
            if (ListBoxProgrammas.Items.Count == 0)
                boodschap += "Alle programma's zijn toegewezen." + "\n";
            else
            {
                boodschap += "Niet toegewezen programma's zijn:" + "\n";
                foreach (OfficeProgramma programma in ListBoxProgrammas.Items)
                    boodschap += programma.naam + "\n";
            }
            if (ListBoxGekend.Items.Count == 0)
                boodschap += "Geen gekende programma's." + "\n";
            else
            {
                boodschap = "Gekende programma's zijn:" + "\n";
                foreach (OfficeProgramma programma in ListBoxGekend.Items)
                    boodschap += programma.naam + "\n";
            }
            if (ListBoxTeVolgen.Items.Count == 0)
                boodschap += "Geen te volgen programma's.";
            else
            {
                boodschap += "Te volgen programma's zijn:" + "\n";
                foreach (OfficeProgramma programma in ListBoxTeVolgen.Items)
                    boodschap += programma.naam + "\n";
            }
            MessageBox.Show(boodschap,"Overzicht");
        }
    }
}
