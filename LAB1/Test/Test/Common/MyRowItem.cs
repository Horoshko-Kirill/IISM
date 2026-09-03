using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Test.Common;

public class MyRowItem : INotifyPropertyChanged
{
    private string _probability = "0.5";
    private string _resultText = "";
    private bool _isResultVisible = false;

    public string Probability
    {
        get => _probability;
        set
        {
            if (_probability == value)
                return;

            _probability = value;
            OnPropertyChanged();
        }
    }

    public string ResultText
    {
        get => _resultText;
        set
        {
            if (_resultText == value)
                return;

            _resultText = value;
            OnPropertyChanged();
        }
    }

    public bool IsResultVisible
    {
        get => _isResultVisible;
        set
        {
            if (_isResultVisible == value)
                return;

            _isResultVisible = value;
            OnPropertyChanged();
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}