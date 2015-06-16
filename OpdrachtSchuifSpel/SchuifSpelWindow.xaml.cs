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

namespace SchuifSpel
{
    /// <summary>
    /// Interaction logic for SchuifSpelWindow.xaml
    /// </summary>

    /*
     * Feedback:
     * 
     * Zorg ervoor dat de de posities van het gesleepte en lege stuk bijgehouden worden.
     * Onthoudt de Image als data voor de DragDrop en niet de ImageSource
     * Ga voor de code van LigtNaastLeegStuk uit van 2 scenario's:
     *  1) Gesleept stuk ligt links of rechts van leeg stuk
     *  2) Gesleept stuk ligt boven of onder leeg stuk
     * 
     * 
     */ 

    public partial class SchuifSpelWindow : Window
    {

        public SchuifSpelWindow()
        {
            InitializeComponent();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Shuffle();
        }


        private void Check()
        {
            int irij, ikolom, grij, gkolom;
            int aantalfout = 0;
            foreach (Image stukje in puzzelGrid.Children)
            {
                irij = Convert.ToInt16(stukje.Name.Substring(4, 1));
                ikolom = Convert.ToInt16(stukje.Name.Substring(5, 1));
                grij = Grid.GetRow(stukje);
                gkolom = Grid.GetColumn(stukje);
                if ((irij != grij) || (ikolom != gkolom))
                {
                    aantalfout++;
                }
            }
            if (aantalfout == 0)
                Oplossing();
        }


        private void OplossingButton_Click(object sender, RoutedEventArgs e)
        {
            Oplossing();
        }


        private void Oplossing()
        {
            puzzelGrid.Children.Clear();
            for (int r = 0; r <= 3; r++)
            {
                for (int k = 0; k <= 3; k++)
                {
                    Image stuk = new Image();
                    BitmapImage bi = new BitmapImage(new Uri(@"images/vdab" + r + k + ".jpg", UriKind.Relative));
                    stuk.Name = "stuk" + r + k;
                    stuk.Source = bi;
                    zetImage(r, k, stuk);
                }
            }
        }

        private void zetImage(int rij, int kolom, Image zetstuk)
        {
            Image stuk = new Image();
            stuk = zetstuk;
            Grid.SetColumn(stuk, kolom);
            Grid.SetRow(stuk, rij);
            if (stuk.Name == "stuk33")
            {
                stuk.Drop += puzzelGrid_Drop;
                stuk.AllowDrop = true;
            }
            else
            {
                stuk.MouseMove += stuk_MouseMove;
                stuk.AllowDrop = false;
            }
            puzzelGrid.Children.Add(stuk);
        }



        private void Shuffle()
        {
            puzzelGrid.Children.Clear();
            int[,] checken = new int[4, 4];
            for (int r = 0; r <= 3; r++)
            {
                for (int k = 0; k <= 3; k++)
                {
                    checken[r, k] = 0;
                }
            }
            checken[3, 3] = 1;

            Random rnd = new Random();
            int rij, kolom;
            for (int r = 0; r <= 3; r++)
            {
                for (int k = 0; k <= 3; k++)
                {
                    if (k < 3 || r < 3)
                    {
                        do
                        {
                            rij = rnd.Next(0, 4);
                            kolom = rnd.Next(0, 4);
                        } while (checken[rij, kolom] == 1);

                        checken[rij, kolom] = 1;
                        Image stuk = new Image();
                        BitmapImage bi = new BitmapImage(new Uri(@"images/vdab" + r + k + ".jpg", UriKind.Relative));
                        stuk.Name = "stuk" + r + k;
                        stuk.Source = bi;
                        zetImage(rij, kolom, stuk);
                    }
                }
            }

            Image leegstuk = new Image();
            BitmapImage bl = new BitmapImage(new Uri(@"images/leeg33.jpg", UriKind.Relative));
            leegstuk.Name = "stuk33";
            leegstuk.Source = bl;
            zetImage(3, 3, leegstuk);
        }



        private void ShuffleButton_Click(object sender, RoutedEventArgs e)
        {
            Shuffle();
        }

        private Image sleepStuk;

        private void stuk_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                sleepStuk = (Image)sender;
                DataObject sleepInfo = new DataObject("sleepInfo", sleepStuk.Source);
                DragDrop.DoDragDrop(sleepStuk, sleepInfo, DragDropEffects.Move);
            }
        }

        private void puzzelGrid_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("sleepInfo"))
            {

                Image dropZone = (Image)sender;

                if (LigtNaastLeegStuk(dropZone))
                {
                    BitmapImage sleepImage = (BitmapImage)e.Data.GetData("sleepInfo");
                    Image nieuwStuk = new Image();
                    nieuwStuk.Name = sleepStuk.Name;
                    nieuwStuk.Source = sleepImage;
                    int rijNieuwStuk = Grid.GetRow(sleepStuk);
                    int kolomNieuwStuk = Grid.GetColumn(sleepStuk);
                    puzzelGrid.Children.Remove(sleepStuk);
                    zetImage(Grid.GetRow(dropZone), Grid.GetColumn(dropZone), nieuwStuk);
                    puzzelGrid.Children.Remove(dropZone);
                    zetImage(rijNieuwStuk, kolomNieuwStuk, dropZone);
                    Check();
                }


            }
        }

        private bool LigtNaastLeegStuk(Image leegStuk)
        {
            int rijSleepStuk = Grid.GetRow(sleepStuk);
            int kolomSleepstuk = Grid.GetColumn(sleepStuk);

            int rijLeegStuk = Grid.GetRow(leegStuk);
            int minimumRij = rijLeegStuk - 1;
            if (minimumRij < 0)
                minimumRij = rijLeegStuk;
            int maximumRij = rijLeegStuk + 1;
            if (maximumRij > puzzelGrid.RowDefinitions.Count - 1)
                maximumRij = rijLeegStuk;

            int kolomLeegstuk = Grid.GetColumn(leegStuk);
            int minimumKolom = kolomLeegstuk - 1;
            if (minimumKolom < 0)
                minimumKolom = kolomLeegstuk;
            int maximumKolom = kolomLeegstuk + 1;
            if (maximumKolom > puzzelGrid.ColumnDefinitions.Count - 1)
                maximumKolom = kolomLeegstuk;

            for (int mogelijkeRij = minimumRij; mogelijkeRij <= maximumRij; mogelijkeRij++)
            {
                for (int mogelijkeKolom = minimumKolom; mogelijkeKolom <= maximumKolom; mogelijkeKolom++)
                {
                    if (mogelijkeRij == rijSleepStuk && mogelijkeKolom == kolomSleepstuk)
                        return true;
                }
            }
            return false;     
        }

    }
}
