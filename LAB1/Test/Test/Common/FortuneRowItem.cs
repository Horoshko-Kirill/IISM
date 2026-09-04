using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Test.Common;

public class FortuneRowItem : INotifyPropertyChanged
{
    private string _nameGame = "";
    private string _donat = "";

    public string NameGame
    {
        get => _nameGame;
        set
        {
            if (_nameGame == value)
                return;
            
            _nameGame = value;
            OnPropertyChanged();
        }
    }

    public string Donat
    {
        get => _donat;
        set
        {
            if (_donat == value)
                return;
            
            _donat = value;
            OnPropertyChanged();
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}