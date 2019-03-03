using BlockChain.Models;
using BlockChain.UtilityObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BlockChain.ViewModels
{
    public class MainViewModel
    {

        private Main main = new Main();

        public AddBlockViewModel addBlockViewModel;
        public BlockChainViewModel blockChainViewModel;

        public ICommand TransactionScreenCommand
        {
            get { return transactionScreenCommand ?? (transactionScreenCommand = new DelegateCommand(ShowTransactionScreen)); }
        }
        private ICommand transactionScreenCommand;

        public ICommand LedgerScreenCommand
        {
            get { return ledgerScreenCommand ?? (ledgerScreenCommand = new DelegateCommand(ShowLedgerScreen)); }
        }
        private ICommand ledgerScreenCommand;

        public MainViewModel()
        {
            addBlockViewModel = new AddBlockViewModel(main.lewCoins);
            blockChainViewModel = new BlockChainViewModel(main.lewCoins);
        }

        private void ShowTransactionScreen()
        {
            addBlockViewModel.IsVisible = Visibility.Visible;
            blockChainViewModel.IsVisible = Visibility.Collapsed;
        }

        private void ShowLedgerScreen()
        {
            blockChainViewModel.PopulateBlockEntryCollection();
            blockChainViewModel.IsVisible = Visibility.Visible;
            addBlockViewModel.IsVisible = Visibility.Collapsed;
        }
    }
}
