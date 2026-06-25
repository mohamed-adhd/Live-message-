namespace live_message_app.ViewModels;
using System.Collections.Generic;
using System.Net.Sockets;
using Tmds.DBus.Protocol;
using live_message_app.Services;
using static System.Console;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel;
using live_message_app.Views;
using Avalonia.Threading;
public partial class RecoverViewModel : ViewModelBase
{
    private readonly MainWindowViewModel _main;

    [ObservableProperty] private string username = "";
    [ObservableProperty] private string info = "";

    public RecoverViewModel(MainWindowViewModel main)
    {
        _main = main;
    }

    [RelayCommand]
    private void SendRecovery()
    {
        
    }

    [RelayCommand]
    private void GoToLogin()
    {
        _main.Currentpage = new LoginViewModel(_main);
    }
}