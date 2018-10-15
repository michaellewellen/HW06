using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace HW06
{
    public class Mortgage : INotifyPropertyChanged , IDataErrorInfo
    {
        public Mortgage()
        {
            StartDate = DateTime.Now;
            PurchasePrice = 100000;
            IntSlider = 4;
            YrsSlider = 30;
            SolveForMonthlyPayment = true;
            SolveForLoanPeriod = false;
            SolveForPurchasePrice = false;
            termList = new ObservableCollection<LoanPeriod>();
            LoanPeriod temp = new LoanPeriod(1, 5);
            LoanPeriod temp1 = new LoanPeriod(2, 10);
            LoanPeriod temp2 = new LoanPeriod(3, 15);
            LoanPeriod temp3 = new LoanPeriod(4, 30);
            TermList.Add(temp);
            TermList.Add(temp1);
            TermList.Add(temp2);
            TermList.Add(temp3);
        }

        public Mortgage(double purchasePrice, double downPayment, double intSlider, int yrsSlider)
        {
            PurchasePrice = purchasePrice;
            DownPayment = downPayment;
            IntSlider = intSlider;
            YrsSlider = yrsSlider;
            StartDate = DateTime.Now;
        }
        private Boolean solveForMonthlyPayment;
        private Boolean solveForLoanPeriod;
        private Boolean solveForPurchasePrice;
        private double monthlyPayment;
        private double purchasePrice;
        private double downPayment;
        private double intSlider;
        private int yrsSlider;
        private DateTime startDate;
        private double totalInterest;
        private double totalPrincipal;
        private double interestHeight;
        private double principalHeight;
        private double percentInterest;
        private double percentPrincipal;
        private BitmapImage emotionImage;

        private ICommand solveForMP;
        public ICommand SolveForMP
        {
            get
            {
                return solveForMP
                    ??(solveForMP = new ActionCommand(() =>
                    {
                        makeMPChanges();
                    }));
            }
        }
        private ICommand solveForLP;
        public ICommand SolveForLP
        {
            get
            {
                return solveForLP
                    ?? (solveForLP = new ActionCommand(() =>
                    {
                        makeLPChanges();
                    }));
            }
        }
        private ICommand solveForPP;
        public ICommand SolveForPP
        {
            get
            {
                return solveForPP
                    ?? (solveForPP = new ActionCommand(() =>
                    {
                        makePPChanges();
                    }));
            }
        }

        public void makeMPChanges()
        {
            SolveForMonthlyPayment = true;
            SolveForLoanPeriod = false;
            SolveForPurchasePrice = false;
        }
        public void makeLPChanges()
        {
            SolveForMonthlyPayment = false;
            SolveForLoanPeriod = true;
            SolveForPurchasePrice = false;
        }
        public void makePPChanges()
        {
            SolveForMonthlyPayment = false;
            SolveForLoanPeriod = false;
            SolveForPurchasePrice = true;
        }

        public Boolean SolveForMonthlyPayment
        {
            get { return solveForMonthlyPayment; }
            set { SetField(ref solveForMonthlyPayment, value); }
        }

        public Boolean SolveForLoanPeriod
        {
            get { return solveForLoanPeriod; }
            set { SetField(ref solveForLoanPeriod, value); }
        }

        public Boolean SolveForPurchasePrice
        {
            get { return solveForPurchasePrice; }
            set { SetField(ref solveForPurchasePrice, value); }
        }

        private ObservableCollection<LoanPeriod> termList;
        public ObservableCollection<LoanPeriod> TermList
        {
            get { return termList; }
        }


        public double MortgageAmount
        {
            get { return PurchasePrice-DownPayment; }
        }

        public double MonthlyPayment
        {
            get { return monthlyPayment; }
            set { SetField(ref monthlyPayment, value); }
        }
        public double PurchasePrice
        {
            get { return purchasePrice; }
            set { SetField(ref purchasePrice, value);
                OnPropertyChanged(nameof(MortgageAmount));
                Calculate();
            }
        }



        private void Calculate()
        {
            // Calculate Monthly Payment
            double expo = Math.Pow((1 + (IntSlider/1200)), YrsSlider*12);
            double d = (expo - 1) / ((IntSlider/1200) * expo);
            double amount = MortgageAmount / d;
            MonthlyPayment = Math.Round(amount, 2);

            // Other fields
            TotalInterest = MonthlyPayment * YrsSlider*12 - MortgageAmount;
            TotalPrincipal = MortgageAmount;
            PercentInterest = TotalInterest / (MonthlyPayment * 12 * YrsSlider);
            PercentPrincipal = 1 - PercentInterest;
            InterestHeight = 150 * PercentInterest;
            PrincipalHeight = 150 * PercentPrincipal;
            if (PercentInterest <= .3)
            {
                EmotionImage = new BitmapImage(new Uri(@"Resources\Happy.png", UriKind.Relative));
            }
            else if (PercentInterest >= .5)
            {
                EmotionImage = new BitmapImage(new Uri(@"Resources\Sad.png", UriKind.Relative));
            }
            else
            {
                EmotionImage = new BitmapImage(new Uri(@"Resources\neutral.png", UriKind.Relative));
            }
        }

        public double DownPayment
        {
            get { return downPayment; }
            set { SetField(ref downPayment, value);
                OnPropertyChanged(nameof(MortgageAmount));
                Calculate();
            }
        }
        public double IntSlider
        {
            get { return intSlider; }
            set
            {
                SetField(ref intSlider, value);
                Calculate();
            }
        }
        public int YrsSlider
        {
            get { return yrsSlider; }
            set { SetField(ref yrsSlider, value);
                OnPropertyChanged(nameof(FinalDate));
                Calculate();
            }
        }
        public DateTime StartDate
        {
            get { return startDate; }
            set { SetField(ref startDate, value);
                OnPropertyChanged(nameof(FinalDate));
            }
        }
        public DateTime FinalDate => StartDate.AddYears((int)YrsSlider);
       
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

        public string Error => throw new NotImplementedException();

        public string this[string propertyName]
        {
            get
            {
                if(propertyName == nameof(IntSlider))
                {
                    if(IntSlider >= 0) { return null; }
                    else { return "Interest must be a positive value"; }
                }

                if(propertyName == nameof(YrsSlider))
                {
                    if(YrsSlider >=0) { return null; }
                    else { return "Mortgage Period must be a postiive value"; }
                }

                if(propertyName == nameof(PurchasePrice))
                {
                    if(PurchasePrice >=0) { return null; }
                    else { return "Purchase Price must be a postiive value"; }
                }

                if(propertyName == nameof(DownPayment))
                {
                    if(DownPayment < PurchasePrice) { return null; }
                    else { return "Down Payment cannot equal or exceed Purchase Price"; }
                }
                return null;
            }
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

    public class ActionCommand : ICommand
    {
        private readonly Action _action;

        public ActionCommand(Action action)
        {
            _action = action;
        }

        public void Execute(object parameter)
        {
            _action();
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;
    }
}
