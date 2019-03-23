using BlockChain.Models.BlockChain;
using BlockChain.UtilityObjects;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Data;

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

        public object __BlocksLock = new object();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="blockChainModel"></param>
        public BlockChainViewModel(BlockChainObj blockChainModel)
        {
            model = blockChainModel;
            BindingOperations.EnableCollectionSynchronization(Blocks, __BlocksLock);
            model.ChainUpdated += ProcessChainUpdateFromModel;
            PopulateBlockEntryCollection();
        }

        /// <summary>
        /// 
        /// </summary>
        private void ProcessChainUpdateFromModel()
        {
            PopulateBlockEntryCollection();
        }

        /// <summary>
        /// Populates the observable collection with data from each 
        /// block in the chain
        /// </summary>
        public void PopulateBlockEntryCollection()
        {
            Blocks.Clear();
            for (int i = 0; i < model.chain.Count; i++)
            {
                Blocks.Add(new BlockEntryViewModel(model.chain[i]));
            }
        }
    }
}
