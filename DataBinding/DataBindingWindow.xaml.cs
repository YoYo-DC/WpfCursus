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
using System.ComponentModel;
using Microsoft.Win32;

namespace DataBinding
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class DataBindingWindow : Window
    {
        public Persoon persoon = new Persoon("jan");

        public DataBindingWindow()
        {
            InitializeComponent();
            SortDescription sd = new SortDescription("Source", ListSortDirection.Ascending);
            comboBoxLettertype.Items.SortDescriptions.Add(sd);
            comboBoxLettertype.SelectedItem = new FontFamily("Arial");
            textBoxVerander.DataContext = persoon;
        }

        private void buttonToonNaam_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(persoon.Naam);
        }

        private void buttonVerander_Click(object sender, RoutedEventArgs e)
        {
            persoon.Naam = "piet";
        }
    }
}
