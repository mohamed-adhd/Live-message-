using CommunityToolkit.Mvvm.Input;

namespace live_message_app.ViewModels;

public partial class LoginViewModel : ViewModelBase
{
    private readonly MainWindowViewModel _main;

    public LoginViewModel(MainWindowViewModel main)
    {
        _main = main;
    }
    
    
    [RelayCommand]
    private void GoToRegister()
    {
        _main.Currentpage = new RegisterViewModel();
    }
}


