using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HW06
{
    public class MortgageViewModel : INotifyPropertyChanged
    {
        // Global Variables

        // ViewModel Constructor
        public MortgageViewModel()
        {
            Mortgage NewLoan = new Mortgage(100000, 0, 4, 30);
        }

        internal void CalendarChanged(object key)
        {
            DateTime temp = (DateTime)key;
            NewLoan.StartDate = temp;
            NewLoan.FinalDate = temp.AddYears(30);

        }

        public Mortgage NewLoan { get; private set; }

        #region INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
                return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }


        #endregion
    }
}
