using System.Reflection.Metadata.Ecma335;

namespace live_message_app.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
public partial class RegisterViewModel: ViewModelBase
{
    private readonly MainWindowViewModel _main;
    [ObservableProperty] string username,name,passwrd,cpasswd,gmail,message;
    public RegisterViewModel(MainWindowViewModel main)
    {
        _main = main;
    }
    [RelayCommand]
    private void GoToLogin()
    {
        _main.Currentpage = new LoginViewModel(_main);
    }

    [RelayCommand]
    public bool rgister()
    {
        if (Cpasswd != Passwrd)
        {
            Message = "passwords arent matching";
        }else if (!Gmail.Contains("@gmail.com"))
        {
            Message = "not a valid ";
        }
    }
}