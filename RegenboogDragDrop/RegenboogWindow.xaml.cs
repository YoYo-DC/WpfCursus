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

namespace RegenboogDragDrop
{
    /// <summary>
    /// Interaction logic for WindowRegenboog.xaml
    /// </summary>
    public partial class WindowRegenboog : Window
    {
        public WindowRegenboog()
        {
            InitializeComponent();
        }

        private Rectangle sleepRechthoekValue = new Rectangle();

        private void Rectangle_MouseMove(object sender, MouseEventArgs e)
        {
            sleepRechthoekValue = (Rectangle)sender;
            if((e.LeftButton == MouseButtonState.Pressed)&&
                (sleepRechthoekValue.Fill != Brushes.White))
            {
                DataObject sleepKleur = new DataObject("deKleur",
                    sleepRechthoekValue.Fill);
                DragDrop.DoDragDrop(sleepRechthoekValue, sleepKleur,
                    DragDropEffects.Move);
            }

        }

        private void Rectangle_DragEnter(object sender, DragEventArgs e)
        {
            Rectangle kader = (Rectangle)sender;
            kader.StrokeThickness = 5;
        }

        private void Rectangle_DragLeave(object sender, DragEventArgs e)
        {
            Rectangle kader = (Rectangle)sender;
            kader.StrokeThickness = 3;
        }

        private void Rectangle_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("deKleur"))
            {
                Brush gesleepteKleur = (Brush)e.Data.GetData("deKleur");
                Rectangle rechthoek = (Rectangle)sender;
                if (rechthoek.Fill == Brushes.White)
                {
                    rechthoek.Fill = gesleepteKleur;
                    sleepRechthoekValue.Fill = Brushes.White;
                }
                rechthoek.StrokeThickness = 3;
            }
        }

        private void ButtonCheck_Click(object sender, RoutedEventArgs e)
        {
            foreach(Rectangle rechthoek in DropZone.Children)
            {
                string naam = rechthoek.Name.Substring(4);
                Brush naamKleur = (Brush)new BrushConverter().ConvertFromString(naam);
                Brush kleur = rechthoek.Fill;
                rechthoek.Stroke = (naamKleur == kleur) ? Brushes.Green : Brushes.Red;
                
            }
        }
        
    }
}

