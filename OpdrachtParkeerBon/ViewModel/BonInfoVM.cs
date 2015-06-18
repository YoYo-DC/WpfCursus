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
using System.ComponentModel;

namespace OpdrachtParkeerBon.ViewModel
{
    public class BonInfoVM : ViewModelBase
    {
        private Model.BonInfo bonInfoValue;

        public BonInfoVM(Model.BonInfo bonInfo)
        {
            bonInfoValue = bonInfo;
        }


        public string DatumBon
        {
            get
            { return bonInfoValue.Datum.ToShortDateString(); }
            set
            {
                bonInfoValue.Datum = Convert.ToDateTime(value);
                RaisePropertyChanged("DatumBon");
            }
        }

        public string AankomstTijdBon
        {
            get
            { return bonInfoValue.AankomstTijd.ToLongTimeString(); }
            set
            {
                bonInfoValue.AankomstTijd = Convert.ToDateTime(value);
                RaisePropertyChanged("AankomstTijdBon");
            }
        }

        public string BedragBon
        {
            get
            { return bonInfoValue.Bedrag.ToString() + " €"; }
            set
            {
                int pos = value.Trim().IndexOf(" ");
                bonInfoValue.Bedrag = decimal.Parse(value.Remove(pos));
                RaisePropertyChanged("BedragBon");
            }
        }

        public string VertrekTijdBon
        {
            get
            { return bonInfoValue.VertrekTijd.ToLongTimeString(); }
            set
            {
                bonInfoValue.VertrekTijd = Convert.ToDateTime(value);
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
            DatumBon = DateTime.Today.ToShortDateString();
            AankomstTijdBon = DateTime.Now.ToLongTimeString();
            VertrekTijdBon = DateTime.Now.ToLongTimeString();
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
                        DatumBon = bestand.ReadLine();
                        AankomstTijdBon = bestand.ReadLine();
                        BedragBon = bestand.ReadLine();
                        VertrekTijdBon = bestand.ReadLine();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Openen mislukt: " + ex.Message);
                }
            }
        }

        public RelayCommand SaveCommand
        {
            get
            { return new RelayCommand(SaveBestand); }
        }

        private void SaveBestand()
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.FileName = string.Format("{0}om{1}", DatumBon.Replace('/','-'), AankomstTijdBon);
            dlg.DefaultExt = ".bon";
            dlg.Filter = "parkeerbonnen |*.bon";
            if (dlg.ShowDialog() == true)
            {
                try
                {
                    using (StreamWriter bestand = new StreamWriter(dlg.FileName))
                    {
                        bestand.WriteLine(DatumBon);
                        bestand.WriteLine(AankomstTijdBon);
                        bestand.WriteLine(BedragBon);
                        bestand.WriteLine(VertrekTijdBon);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Opslaan mislukt: " + ex.Message);
                }
            }
        }

        public RelayCommand CloseCommand
        {
            get
            { return new RelayCommand(CloseApp); }
        }

        private void CloseApp()
        {
            Application.Current.MainWindow.Close();
        }

        public RelayCommand<CancelEventArgs> ClosingCommand
        {
            get
            { return new RelayCommand<CancelEventArgs>(ClosingApp); }
        }

        private void ClosingApp(CancelEventArgs e)
        {
            if (MessageBox.Show("Wilt u het programma afsluiten", "Afsluiten?", MessageBoxButton.YesNo,
                MessageBoxImage.Question, MessageBoxResult.No)
                 == MessageBoxResult.No)
                e.Cancel = true;
        }

        public RelayCommand VerhoogCommand
        {
            get
            { return new RelayCommand(VerhoogTeBetalen); }
        }

        private void VerhoogTeBetalen()
        {
            string bedrag = BedragBon.Trim().Remove(BedragBon.IndexOf(" "));
            decimal bedragWaarde = decimal.Parse(bedrag);
            bedragWaarde++;
            BedragBon = bedragWaarde.ToString() + " €";
            DateTime tijd = Convert.ToDateTime(VertrekTijdBon);
            VertrekTijdBon = tijd.AddMinutes(30).ToLongTimeString();
        }

        public RelayCommand VerlaagCommand
        {
            get
            { return new RelayCommand(VerlaagTeBetalen); }
        }

        private void VerlaagTeBetalen()
        {
            string bedrag = BedragBon.Trim().Remove(BedragBon.IndexOf(" "));
            decimal bedragWaarde = decimal.Parse(bedrag);
            if (bedragWaarde > 0)
            {
                bedragWaarde--;
                BedragBon = bedragWaarde.ToString() + " €";
                DateTime tijd = Convert.ToDateTime(VertrekTijdBon);
                tijd.AddMinutes(-30);
                VertrekTijdBon = tijd.ToLongTimeString();
            }
        }

    }
}
