using BlockChain.Models;
using BlockChain.UtilityObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using BlockChain.Models.Networking;

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

        public MainViewModel()
        {
            addBlockViewModel = new AddBlockViewModel(main.lewCoins);
            blockChainViewModel = new BlockChainViewModel(main.lewCoins);
            main.server.OnPortOpen += OnPortOpenMessageFromServer;
            main.server.OnConnectionSuccessful += OnConnectionSuccessfulMessageFromServer;
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
        private void OnConnectionSuccessfulMessageFromServer(ClientFromServerEventArgs e)
        {
            Port = $"Client connected on: {main.server.server.LocalEndpoint.ToString()}";
        }
    }
}
