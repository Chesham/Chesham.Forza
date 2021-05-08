using Chesham.Forza.ForzaHorizon4.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Chesham.Forza.Gui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var mainViewModel = new ViewModel.MainViewModel();
            var ipEndPoint = new IPEndPoint(IPAddress.Any, 2059);
            var dataReader = new ForzaDataReader();
            dataReader.Listen(ipEndPoint);
            dataReader
                .observable
                .SubscribeOn(TaskPoolScheduler.Default)
                .Sample(TimeSpan.FromMilliseconds(10))
                .Where(i => i.HorizonCarDash.IsRaceOn == 1)
                .Subscribe(data =>
                {
                    var m = mainViewModel;
                    var sled = data.HorizonCarDash;
                    m.engineMaxRpm = sled.EngineMaxRpm;
                    m.engineCurrentRpm = sled.CurrentEngineRpm;
                    //var gear = (int)data.Remain.ToArray().ElementAt(87);
                    var gear = sled.Gear;
                    m.gear = gear == 0 ? "R" : $"{gear}";
                    var carClasses = new[] { "D", "C", "B", "A", "S1", "S2", "X" };
                    m.carClass = carClasses.ElementAtOrDefault(sled.CarClass);
                    m.performanceIndex = $"({sled.CarPerformanceIndex})";
                    var drivetrain = new[] { "FWD", "RWD", "AWD" };
                    m.drivetrain = drivetrain.ElementAtOrDefault(sled.DrivetrainType);
                    m.cylinders = sled.NumCylinders;
                });
            DataContext = mainViewModel;
        }

        private void PinClick(object sender, RoutedEventArgs e)
        {
            var control = e.Source as CheckBox;
            if (control.IsChecked ?? false)
            {
                Topmost = true;
                WindowStyle = WindowStyle.None;
            }
            else
            {
                Topmost = false;
                WindowStyle = WindowStyle.SingleBorderWindow;
            }
        }
    }
}
