﻿using SimpleTrader.WPF.Commands;
using SimpleTrader.WPF.State.Authentificators;
using SimpleTrader.WPF.State.Navigators;
using System.Windows.Input;

namespace SimpleTrader.WPF.VVM.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly IRenavigator _renavigator;

        private string _username = string.Empty;
        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public MessageViewModel ErrorMessageViewModel { get; }
        public string SetErrorMessageViewModel
        {
            set => ErrorMessageViewModel.Message = value;
        }
        public bool HasErrorMessage 
            => ErrorMessageViewModel.HasMessage;
        public ICommand LoginCommand { get; }

        public LoginViewModel(IAuthenticator authenticator, IRenavigator renavigator)
        {

            ErrorMessageViewModel = new MessageViewModel();

            _renavigator = renavigator;
            LoginCommand = new LoginCommand(this, authenticator, _renavigator);
        }
    }
}
