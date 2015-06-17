using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using System.IO;
using System.Windows;

namespace OpdrachtParkeerBon.ViewModel
{
    public class BonInfoVM : ViewModelBase
    {
        private Model.BonInfo bonInfoValue;

        public BonInfoVM(Model.BonInfo bonInfo)
        {
            bonInfoValue = bonInfo;
        }

        
        public DateTime DatumBon
        {
            get
            { return bonInfoValue.Datum; }
            set
            {
                bonInfoValue.Datum = value;
                RaisePropertyChanged("DatumBon");
            }
        }

        public DateTime AankomstTijdBon
        {
            get
            { return bonInfoValue.AankomstTijd; }
            set
            {
                bonInfoValue.AankomstTijd = value;
                RaisePropertyChanged("AankomstTijdBon");
            }
        }

        public decimal BedragBon
        {
            get
            { return bonInfoValue.Bedrag; }
            set
            {
                bonInfoValue.Bedrag = value;
                RaisePropertyChanged("BedragBon");
            }
        }

        public DateTime VertrekTijdBon
        {
            get
            { return bonInfoValue.VertrekTijd; }
            set
            {
                bonInfoValue.VertrekTijd = value;
                RaisePropertyChanged("VertrekTijdBon");
            }
        }

        public RelayCommand NieuwCommand
        {
            get
            { return new RelayCommand(NieuwBestand); }
        }

        private void NieuwBestand()
        {
            DatumBon = DateTime.Today;
            AankomstTijdBon = DateTime.Now;
            VertrekTijdBon = DateTime.Now;
        }

        public RelayCommand OpenCommand
        {
            get
            { return new RelayCommand(OpenBestand); }
        }

        private void OpenBestand()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.FileName = string.Empty;
            dlg.DefaultExt = ".bon";
            dlg.Filter = "parkeerbonnen |*.bon";
            if (dlg.ShowDialog() == true)
            {
                try
                {
                    using (StreamReader bestand = new StreamReader(dlg.FileName))
                    {
                        DatumBon = Convert.ToDateTime(bestand.ReadLine());
                        AankomstTijdBon = Convert.ToDateTime(bestand.ReadLine());
                        BedragBon = decimal.Parse(bestand.ReadLine());
                        VertrekTijdBon = Convert.ToDateTime(bestand.ReadLine());
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Openen mislukt: " + ex.Message);
                }
            }
        }

        private void SaveBestand()
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.FileName = DatumBon.ToString("dd-MM-yyyy") + "om" + AankomstTijdBon.ToLongTimeString();
            dlg.DefaultExt = ".bon";
            dlg.Filter = "parkeerbonnen |*.bon";
            if (dlg.ShowDialog() == true)
            {
                try
                {
                    using (StreamWriter bestand = new StreamWriter(dlg.FileName))
                    {
                        bestand.WriteLine(DatumBon.ToShortDateString());
                        bestand.WriteLine(AankomstTijdBon.ToLongTimeString());
                        //next
                    }
                }
            }
        }
    }
}
