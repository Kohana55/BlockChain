﻿using BlockChain.Models;
using BlockChain.Models.BlockChain;
using DigiCard.UtilityObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BlockChain.ViewModels
{
    public class MainViewModel : BindableBase
    {
        private Main main = new Main();

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

<<<<<<< HEAD
        public string BlockCreationTime
        {
            get { return blockCreationTime; }
            set
            {
                SetProperty(ref blockCreationTime, value);
            }
        }
        private string blockCreationTime;
=======
        public int Difficulty
        {
            get { return difficulty; }
            set
            {
                SetProperty(ref difficulty, value);
            }
        }
        private int difficulty;
>>>>>>> 698344a556c4f7616a4402174a2e4c5504174aca

        public ICommand AddBlockButton
        {
            get { return addBlockButton ?? (addBlockButton = new DelegateCommand(AddBlock)); }
        }
        private ICommand addBlockButton;

        public MainViewModel() { }

        public void AddBlock()
        {
            main.lewCoins.difficulty = difficulty;
            main.lewCoins.AddBlock(new Block(DateTime.Now, $"s:{sendingFrom},r:{sendingTo},n:{amount}"));
            ClearUI();
        }

        private void ClearUI()
        {
            SendingFrom = "";
            SendingTo = "";
            Amount = "";
        }
    }
}
