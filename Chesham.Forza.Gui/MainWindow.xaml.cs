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
using System.Windows.Threading;

namespace Chesham.Forza.Gui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ViewModel.MainViewModel mainViewModel { get; set; }

        public MainWindow()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
            var gearTextBlinker = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(50),
                IsEnabled = false,
                Tag = 0
            };
            gearTextBlinker.Tick += (sender, _) =>
            {
                var blinker = sender as DispatcherTimer;
                var opacity = 1 - (int)blinker.Tag;
                blinker.Tag = opacity;
                gearText.Opacity = opacity;
            };
            mainViewModel = new ViewModel.MainViewModel();
            var ipEndPoint = new IPEndPoint(IPAddress.Any, 2059);
            var dataReader = new ForzaDataReader();
            dataReader.Listen(ipEndPoint);
            dataReader
                .observable
                .Sample(TimeSpan.FromMilliseconds(10))
                .Cast<ForzaDataHorizon4CarDash>()
                //.Where(i => i?.IsRaceOn == 1)
                .Subscribe(data =>
                {
                    var m = mainViewModel;
                    try
                    {
                        if (data.IsRaceOn == 0)
                        {
                            if (m.pinned)
                            {
                                Dispatcher.InvokeAsync(() =>
                                {
                                    Topmost = false;
                                });
                            }
                            return;
                        }
                        if (m.pinned && m.isRaceOn == 0)
                        {
                            Dispatcher.InvokeAsync(() =>
                            {
                                Topmost = true;
                            });
                        }
                        if (data.CarOrdinal != m.carOrdinal || data.CarPerformanceIndex != m.performanceIndex)
                        {
                            m.carOrdinal = data.CarOrdinal;
                            m.performanceIndex = data.CarPerformanceIndex;
                            m.Reset();
                        }
                        m.engineMaxRpm = data.EngineMaxRpm;
                        m.engineCurrentRpm = data.CurrentEngineRpm;
                        var gear = data.Gear;
                        m.gear = gear == 0 ? "R" : $"{gear}";
                        var carClasses = new[] { "D", "C", "B", "A", "S1", "S2", "X" };
                        m.carClass = carClasses.ElementAtOrDefault((int)data.CarClass);
                        m.performanceIndexLiteral = $"({data.CarPerformanceIndex})";
                        var drivetrain = new[] { "FWD", "RWD", "AWD" };
                        m.drivetrain = drivetrain.ElementAtOrDefault((int)data.DrivetrainType);
                        m.cylinders = data.NumCylinders;
                        m.boost = data.Boost;
                        // In ps
                        m.power = data.Power * 0.0013596216f;
                        // In km/hr
                        m.speed = data.Speed * 3600 / 1000;
                        // In kg-m
                        m.torque = data.Torque / 9.8f;
                        if (m.power > m.maxPowerAtRpm.power)
                        {
                            m.maxPowerAtRpm = (m.power, m.engineCurrentRpm);
                        }
                        if (m.torque > m.maxTorqueAtRpm.torque)
                        {
                            m.maxTorqueAtRpm = (m.torque, m.engineCurrentRpm);
                        }
                        m.engineRpmRedline = m.maxPowerAtRpm.rpm;
                        // Record zero to hundred
                        if (m.speed <= .1f)
                        {
                            m.recording.Restart();
                        }
                        else if (m.speed >= 100 && m.recording.IsRunning)
                        {
                            m.recording.Stop();
                            m.zero2HundredTime = m.recording.Elapsed;
                        }
                        if (data.Power < m.lastPower && data.Accel > 0 && m.isRedlineReached)
                        {
                            gearTextBlinker.IsEnabled = true;
                        }
                        else if (gearTextBlinker.IsEnabled)
                        {
                            gearTextBlinker.IsEnabled = false;
                            Dispatcher.BeginInvoke(() => gearText.Opacity = 1);
                        }
                        m.lastPower = data.Power;
                    }
                    finally
                    {
                        m.isRaceOn = data.IsRaceOn;
                    }
                });
            DataContext = mainViewModel;
            // Debug
            mainViewModel.boost = 5;
            mainViewModel.carClass = "X";
            mainViewModel.cylinders = 12;
            mainViewModel.drivetrain = "AWD";
            mainViewModel.engineCurrentRpm = 800;
            mainViewModel.engineMaxRpm = 10000;
            mainViewModel.engineRpmRedline = 8000;
            mainViewModel.gear = 3;
            mainViewModel.performanceIndexLiteral = "(999)";
            mainViewModel.power = 380.3f;
            mainViewModel.speed = 86;
            mainViewModel.torque = 51.2f;
            mainViewModel.maxPowerRpm = "380.2ps@5000";
            mainViewModel.maxTorqueRpm = "51.2kgm@5000";
            mainViewModel.zero2HundredTime = TimeSpan.FromMilliseconds(123456);
        }

        private void PinClick(object sender, RoutedEventArgs e)
        {
            var control = e.Source as CheckBox;
            if (control.IsChecked ?? false)
            {
                WindowStyle = WindowStyle.None;
                mainViewModel.pinned = true;
            }
            else
            {
                WindowStyle = WindowStyle.SingleBorderWindow;
                mainViewModel.pinned = false;
                Topmost = false;
            }
        }
    }
}
