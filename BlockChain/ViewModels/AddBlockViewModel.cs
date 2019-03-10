﻿using BlockChain.Models;
using BlockChain.Models.BlockChain;
using BlockChain.UtilityObjects;
using System;
using System.Windows;
using System.Windows.Input;

namespace BlockChain.ViewModels
{
    public class AddBlockViewModel : BindableBase
    {
        private BlockChainObj model;

        public string SendingFrom
        {
            get { return sendingFrom; }
            set
            {
                SetProperty(ref sendingFrom, value);
            }
        }
        private string sendingFrom;

        public string SendingTo
        {
            get { return sendingTo; }
            set
            {
                SetProperty(ref sendingTo, value);
            }
        }
        private string sendingTo;

        public string Amount
        {
            get { return amount; }
            set
            {
                SetProperty(ref amount, value);
            }
        }
        private string amount;

        public string Status
        {
            get { return status; }
            set
            {
                SetProperty(ref status, value);
            }
        }
        private string status;


        public ICommand AddBlockButton
        {
            get { return addBlockButton ?? (addBlockButton = new DelegateCommand(AddBlock)); }
        }
        private ICommand addBlockButton;

        public Visibility IsVisible
        {
            get { return isVisible; }
            set { SetProperty(ref isVisible, value); }
        }
        private Visibility isVisible;

        public AddBlockViewModel(BlockChainObj blockChainModel)
        {
            model = blockChainModel;
            model.StatusUpdate += StatusUpdateFromBlockChain;
        }


        /// <summary>
        /// Method reponds to Add Block button
        /// </summary>
        public void AddBlock()
        {
            model.ProcessTransaction(new Transaction(sendingFrom, sendingTo, amount, DateTime.Now));
            ClearUI();
            Status = "Ready";
        }

        /// <summary>
        /// Clears each UI element
        /// </summary>
        private void ClearUI()
        {
            SendingFrom = "";
            SendingTo = "";
            Amount = "";
        }

        /// <summary>
        /// Receives status update strings from the BitCoin model
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StatusUpdateFromBlockChain(object sender, string e)
        {
            Status = e;
        }
    }
}
