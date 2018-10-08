using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace HW06
{
    public class Mortgage : INotifyPropertyChanged
    {
        public Mortgage(double purchasePrice, double downPayment, double intSlider, int yrsSlider)
        {
            PurchasePrice = purchasePrice;
            DownPayment = downPayment;
            IntSlider = intSlider;
            YrsSlider = yrsSlider;
            MortgageAmount = PurchasePrice - DownPayment;

            // Calculate Monthly Payment
            double expo = Math.Pow((1 + IntSlider), YrsSlider);
            double d = (expo - 1) / (IntSlider * expo);
            double amount = MortgageAmount / d;
            MonthlyPayment = Math.Round(amount, 2);
            TotalInterest = MonthlyPayment * 12 * YrsSlider - MortgageAmount;
            TotalPrincipal = MortgageAmount;
            PercentInterest = TotalInterest / (MonthlyPayment * 12 * YrsSlider);
            PercentPrincipal = 1 - PercentInterest;
            InterestHeight = 100 * PercentInterest;
            PrincipalHeight = 100 * PercentPrincipal;
            if (PercentInterest <= .4)
            {
                EmotionImage = new BitmapImage(new Uri(@"Resources\p2.png", UriKind.Relative));
            }
            else if (PercentInterest >= .6)
            {
                EmotionImage = new BitmapImage(new Uri(@"Resources\p2.png", UriKind.Relative));
            }
            else
            {
                EmotionImage = new BitmapImage(new Uri(@"Resources\p2.png", UriKind.Relative));
            }


        }
        private double mortgageAmount;
        private double monthlyPayment;
        private double purchasePrice;
        private double downPayment;
        private double intSlider;
        private int yrsSlider;
        private DateTime startDate;
        private DateTime finalDate;
        private double totalInterest;
        private double totalPrincipal;
        private double interestHeight;
        private double principalHeight;
        private double percentInterest;
        private double percentPrincipal;
        private BitmapImage emotionImage;



        public double MortgageAmount
        {
            get { return mortgageAmount; }
            set { SetField(ref mortgageAmount, value); }
        }
        public double MonthlyPayment
        {
            get { return monthlyPayment; }
            set { SetField(ref monthlyPayment, value); }
        }
        public double PurchasePrice
        {
            get { return purchasePrice; }
            set { SetField(ref purchasePrice, value); }
        }

        public double DownPayment
        {
            get { return downPayment; }
            set { SetField(ref downPayment, value); }
        }
        public double IntSlider
        {
            get { return intSlider; }
            set { SetField(ref intSlider, value); }
        }
        public int YrsSlider
        {
            get { return yrsSlider; }
            set { SetField(ref yrsSlider, value); }
        }
        public DateTime StartDate
        {
            get { return startDate; }
            set { SetField(ref startDate, value); }
        }
        public DateTime FinalDate
        {
            get { return finalDate; }
            set { SetField(ref finalDate, value); }
        }
        public double TotalInterest
        {
            get { return totalInterest; }
            set { SetField(ref totalInterest, value); }
        }
        public double TotalPrincipal
        {
            get { return totalPrincipal; }
            set { SetField(ref totalPrincipal, value); }
        }
        public double InterestHeight
        {
            get { return interestHeight; }
            set { SetField(ref interestHeight, value); }
        }
        public double PrincipalHeight
        {
            get { return principalHeight; }
            set { SetField(ref principalHeight, value); }
        }
        public double PercentInterest
        {
            get { return percentInterest; }
            set { SetField(ref percentInterest, value); }
        }
        public double PercentPrincipal
        {
            get { return percentPrincipal; }
            set { SetField(ref percentPrincipal, value); }
        }
        public BitmapImage EmotionImage
        {
            get { return emotionImage; }
            set { SetField(ref emotionImage, value); }
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
