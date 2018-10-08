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
            NewLoan.PropertyChanged += NewLoan_PropertyChanged;
        }

        private void NewLoan_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(nameof(NewLoan));
        }

        

        //public Mortgage NewLoan { get; private set; }
        private Mortgage newLoan;
        public Mortgage NewLoan
        {
            get { return newLoan; }
            set { SetField(ref newLoan, value); }
        }


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
