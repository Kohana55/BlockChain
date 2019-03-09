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
    public class MainViewModel : BindableBase
    {

        private Main main = new Main();

        public AddBlockViewModel addBlockViewModel;
        public BlockChainViewModel blockChainViewModel;

        public string Port
        {
            get { return port; }
            set
            {
                SetProperty(ref port, value);
            }
        }
        public string port;

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
            main.server.OnPortOpen += OnPortOpenMessageFromServer;
            main.server.OnConnectionSuccessful += OnConnectionSuccessfulMessageFromServer;
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

        /// <summary>
        /// Server is listening, update UI
        /// </summary>
        /// <param name="sender"></param>
        private void OnPortOpenMessageFromServer()
        {
            Port = $"Listening on: {main.server.server.LocalEndpoint.ToString()}";
        }

        /// <summary>
        /// Server has recieved an active TCP connection, update UI
        /// </summary>
        private void OnConnectionSuccessfulMessageFromServer()
        {
            Port = $"Client connected on: {main.server.server.LocalEndpoint.ToString()}";

            // TEST CODE - client connected, try sending a message to the client
            main.server.Send("Hello");
        }
    }
}
