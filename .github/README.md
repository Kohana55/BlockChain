# BlockChain
A C# BlockChain client

This client will send out transactions across the network. 
The miner will pool these transactions and attempt to create a block. Once made the miner will propose the block back to this client.
If this client is happy, the block is added to the chain and an update is sent out to all other nodes. 

# Note
A confusing sidenote: This application is the "BlockChain Client", but the TCP connection part of it acts as the server.
Meaning the "BlockChain Client" (this) is the "TCP Server" & the "BlockChain Miner" also available on this GIT, is the "TCP Client".
