using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Chesham.Forza.Gui.ViewModel
{
    abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void Notify([CallerMemberName] string propertyName = default)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void Notify(params string[] propertyNames)
        {
            foreach (var propertyName in propertyNames)
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void SetValue<T>(T value, [CallerMemberName] string propertyName = default)
        {
            lock (properties)
            {
                properties[propertyName] = value;
                Notify(propertyName);
            }
        }

        protected T GetValue<T>([CallerMemberName] string propertyName = default)
        {
            lock (properties)
            {
                if (properties.TryGetValue(propertyName, out var value))
                    return (T)value;
                var defaultValue = default(T);
                properties.Add(propertyName, defaultValue);
                return defaultValue;
            }
        }

        private IDictionary<string, object> properties { get; } = new Dictionary<string, object>();
    }
}
