using CommunityToolkit.Mvvm.Input;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using live_message_app.Services;
using live_message_app.Views;
namespace live_message_app.ViewModels;
public partial class MainWindowViewModel : ObservableObject
{
    [ObservableProperty] private object currentpage;

    [ObservableProperty] private int id;
    [ObservableProperty] private bool connected = false;
    Services.Network network=new();
    

    public MainWindowViewModel()
    {
        currentpage = new LoginViewModel(this);
        connected=network.Connect("127.0.0.1", 8000);

    }
}