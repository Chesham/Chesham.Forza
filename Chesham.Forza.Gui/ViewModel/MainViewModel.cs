using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Chesham.Forza.Gui.ViewModel
{
    class MainViewModel : ViewModelBase
    {
        public float engineMaxRpm
        {
            get => GetValue<float>();
            set => SetValue(value);
        }

        public float engineCurrentRpm
        {
            get => GetValue<float>();
            set
            {
                SetValue(value);
                Notify(nameof(isRedlineReached), nameof(engineGaugeColor));
            }
        }

        public float engineRpmRedline
        {
            get => GetValue<float>();
            set
            {
                SetValue(value);
                Notify(nameof(isRedlineReached), nameof(engineGaugeColor));
            }
        }

        public bool isRedlineReached => engineCurrentRpm >= engineRpmRedline;

        public object engineGaugeColor => isRedlineReached ? Brushes.IndianRed : Brushes.LightGreen;

        public string gear
        {
            get => GetValue<string>();
            set => SetValue(value);
        }

        public string carClass
        {
            get => GetValue<string>();
            set => SetValue(value);
        }

        public string performanceIndex
        {
            get => GetValue<string>();
            set => SetValue(value);
        }

        public string drivetrain
        {
            get => GetValue<string>();
            set => SetValue(value);
        }

        public int cylinders
        {
            get => GetValue<int>();
            set => SetValue(value);
        }
    }
}
