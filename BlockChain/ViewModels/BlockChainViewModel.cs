using BlockChain.Models.BlockChain;
using BlockChain.UtilityObjects;
using System.Collections.ObjectModel;
using System.Windows;

namespace BlockChain.ViewModels
{
    public class BlockChainViewModel : BindableBase
    {
        private BlockChainObj model;

        public ObservableCollection<BlockEntryViewModel> Blocks
        {
            get { return blocks; }
            set { SetProperty(ref blocks, value); }
        }
        private ObservableCollection<BlockEntryViewModel> blocks = new ObservableCollection<BlockEntryViewModel>();

        public Visibility IsVisible
        {
            get { return isVisible; }
            set { SetProperty(ref isVisible, value); }
        }
        private Visibility isVisible;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="blockChainModel"></param>
        public BlockChainViewModel(BlockChainObj blockChainModel)
        {
            model = blockChainModel;
            IsVisible = Visibility.Collapsed;
            PopulateBlockEntryCollection();
        }

        /// <summary>
        /// Populates the observable collection with data from each 
        /// block in the chain
        /// </summary>
        public void PopulateBlockEntryCollection()
        {
            blocks.Clear();
            for (int i = 0; i < model.chain.Count; i++)
            {
                Blocks.Add(new BlockEntryViewModel(model.chain[i]));
            }
        }
    }
}
