﻿using System;
using System.Windows.Input;
using SafeMessages.Helpers;
using SafeMessages.Models;
using Xamarin.Forms;

namespace SafeMessages.ViewModels {
  internal class AddIdViewModel : BaseViewModel {
    private string _userId;

    public DataModel AppData => DependencyService.Get<DataModel>();

    public ICommand CreateIdCommand { get; }
    public string UserId { get => _userId; set => SetProperty(ref _userId, value); }

    public AddIdViewModel() {
      IsUiEnabled = true;
      UserId = string.Empty;
      CreateIdCommand = new Command(OnCreateIdCommand);
    }

    private async void OnCreateIdCommand() {
      IsUiEnabled = false;
      try {
        await SafeApp.AddIdAsync(UserId);
        AppData.Accounts.Add(new UserId(UserId));
        AppData.Accounts.Sort();
        MessagingCenter.Send(this, MessengerConstants.NavUserIdsPage);
      } catch (Exception ex) {
        await Application.Current.MainPage.DisplayAlert("Error", $"Create Id Failed: {ex.Message}", "OK");
        IsUiEnabled = true;
      }
    }
  }
}
