using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace OpdrachtParkeerBon
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Model.BonInfo model = new Model.BonInfo();
            ViewModel.BonInfoVM vm = new ViewModel.BonInfoVM(model);
            ParkingBonMVVM.View.ParkingBonView view = new ParkingBonMVVM.View.ParkingBonView();
            view.DataContext = vm;
            view.Show();
        }
    }
}
