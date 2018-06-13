using System.ComponentModel;
using System.Runtime.CompilerServices;
using EksiSozluk.CloneUI.Annotations;

namespace EksiSozluk.CloneUI.Extension.Notify
{
    public class Base : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
