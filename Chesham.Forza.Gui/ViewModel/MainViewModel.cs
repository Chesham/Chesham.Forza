using System.Diagnostics;
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

        public object gear
        {
            get => GetValue<object>();
            set => SetValue(value);
        }

        public object carClass
        {
            get => GetValue<object>();
            set => SetValue(value);
        }

        public object performanceIndexLiteral
        {
            get => GetValue<object>();
            set => SetValue(value);
        }

        public float performanceIndex
        {
            get => GetValue<float>();
            set => SetValue(value);
        }

        public object drivetrain
        {
            get => GetValue<object>();
            set => SetValue(value);
        }

        public int cylinders
        {
            get => GetValue<int>();
            set => SetValue(value);
        }

        public float speed
        {
            get => GetValue<float>();
            set => SetValue(value);
        }

        public float power
        {
            get => GetValue<float>();
            set => SetValue(value);
        }

        public float torque
        {
            get => GetValue<float>();
            set => SetValue(value);
        }

        public float boost
        {
            get => GetValue<float>();
            set => SetValue(value);
        }

        public object maxPowerRpm
        {
            get => GetValue<object>();
            set => SetValue(value);
        }

        public object maxTorqueRpm
        {
            get => GetValue<object>();
            set => SetValue(value);
        }

        public (float power, float rpm) maxPowerAtRpm
        {
            get => GetValue<(float, float)>();
            set
            {
                SetValue(value);
                maxPowerRpm = $"{value.power:N1}ps@{value.rpm:N0}";
                Notify(nameof(engineMaxPower));
            }
        }

        public (float torque, float rpm) maxTorqueAtRpm
        {
            get => GetValue<(float, float)>();
            set
            {
                SetValue(value);
                maxTorqueRpm = $"{value.torque:N1}kgm@{value.rpm:N0}";
                Notify(nameof(engineMaxTorque));
            }
        }

        public float engineMaxPower => maxPowerAtRpm.power;

        public float engineMaxTorque => maxTorqueAtRpm.torque;

        public object zero2HundredTime
        {
            get => GetValue<object>();
            set => SetValue(value);
        }

        public Stopwatch recording { get; set; } = new Stopwatch();

        public int carOrdinal
        {
            get => GetValue<int>();
            set => SetValue(value);
        }

        public int isRaceOn
        {
            get => GetValue<int>();
            set
            {
                SetValue(value);
                Notify(nameof(opacity));
            }
        }

        public double opacity => isRaceOn == 0 ? .5 : 1;

        public bool pinned
        {
            get => GetValue<bool>();
            set => SetValue(value);
        }

        public float? lastPower
        {
            get => GetValue<float?>();
            set => SetValue(value);
        }

        public void Reset()
        {
            maxPowerAtRpm = default;
            maxTorqueAtRpm = default;
            zero2HundredTime = default;
            recording.Reset();
        }
    }
}
