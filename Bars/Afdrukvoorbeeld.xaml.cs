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
using System.Windows.Shapes;

namespace Bars
{
    /// <summary>
    /// Interaction logic for Afdrukvoorbeeld.xaml
    /// </summary>
    public partial class Afdrukvoorbeeld : Window
    {
        public Afdrukvoorbeeld()
        {
            InitializeComponent();
        }

        //De Document-property van het DocumentViewer element kan in principe
        //rechtstreeks vanuit een ander Window-object ingevuld worden, maar die werkwijze gaat in tegen het OOP-principe
        //Deze property zorgt ervoor dat toegang tot het DocumentViewer-element in dit Window-object
        //strikt gereguleerd is: encapsulation van DocumentViewer-object
        //Het type is gelijk aan of of een supertype van het in te kapselen element
        public IDocumentPaginatorSource AfdrukDocument
        {
            get { return docViewerPrintPreview.Document; }
            set { docViewerPrintPreview.Document = value; }
        }
    }
}
