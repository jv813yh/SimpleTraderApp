﻿using SimpleTrader.Domain.Exceptions;
using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services.Interfaces.TransactionServices;
using SimpleTrader.WPF.State.Accounts;
using SimpleTrader.WPF.VVM.ViewModels;

namespace SimpleTrader.WPF.Commands
{
    public class SellStockCommand : AsyncCommandBase
    {
        private readonly SellViewModel _sellViewModel;
        private readonly ISellStockService _sellStockService;
        private readonly IAccountStore _accountStore;

        public SellStockCommand(SellViewModel sellViewModel, 
            ISellStockService sellStockService,
            IAccountStore accountStore)
        {
            _sellViewModel = sellViewModel;
            _sellStockService = sellStockService;
            _accountStore = accountStore;
        }
        public async override Task ExecuteAsync(object? parameter)
        {
            string sellSymbolToUpper = _sellViewModel.Symbol.ToUpper();

            // Set the status and error messages to empty
            _sellViewModel.SetStatusMessage = string.Empty;
            _sellViewModel.SetErrorMessage = string.Empty;

            try
            {
                // Buy the stock and return the account after the purchase
                Account account = await _sellStockService.SellStockAsync(_accountStore.CurrentAccount, sellSymbolToUpper,
                    _sellViewModel.SharesToSell);

                // Update the current account with the balance after the purchase, transactions ...
                _accountStore.CurrentAccount = account;

                // Show a message box to inform the user that the purchase was successful
                _sellViewModel.SetStatusMessage = $"Successfully sold {_sellViewModel.SharesToSell} shares of {sellSymbolToUpper}";

                _sellViewModel.SearchResultSymbol = string.Empty;
            }
            catch (InvalidSymbolException)
            {
                // Inform the user that the purchase was not successful
                _sellViewModel.SetErrorMessage = "Symbol does not exist. ";
            }
            catch (InsufficientSharesException ex)
            {
                // Inform the user that the purchase was not successful
                _sellViewModel.SetErrorMessage = $"Account has insufficient shares. You only have {ex.AccountShares} shares.";
            }
            catch (Exception ex)
            {
                // Inform the user that the purchase was not successful
                _sellViewModel.SetErrorMessage = ex.Message;
            }
        }
    }
}
