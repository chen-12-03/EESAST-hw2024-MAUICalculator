using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MAUICalculator;

public class BaseViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;
    public void OnPropertyChanged([CallerMemberName] string name = null) =>
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

    double currentNumber = 0;
    double lastNumber = 0;
    string currentOperator = "";
    bool isResult = false;
    public enum Choice { num, opt, equ, none};
    Choice prechoice = Choice.none;

    public double CurrentNumber
    {
        get => currentNumber;
        set
        {
            if (currentNumber == value)
                return;
            currentNumber = value;
            OnPropertyChanged();
        }
    }

    public double LastNumber
    {
        get => lastNumber;
        set
        {
            if (lastNumber == value)
                return;
            lastNumber = value;
            OnPropertyChanged();
        }
    }

    public string CurrentOperator
    {
        get => currentOperator;
        set
        {
            if (currentOperator == value)
                return;
            currentOperator = value;
            OnPropertyChanged();
        }
    }

    public Choice Prechoice
    {
        get => prechoice;
        set
        {
            if (prechoice == value)
                return;
            prechoice = value;
            OnPropertyChanged();
        }
    }
}
