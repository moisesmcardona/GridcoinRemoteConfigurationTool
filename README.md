# GridcoinRemoteConfigurationTool
This is a tool that will modify your Gridcoin Research Configuration File in order to enable Remote RPC access

This software is written in Visual Basic .NET and targets the .NET Frameworks 4.5.2.

The software is easy to use. You just launch it and it will attempt to find your Gridcoin Research configuration file located in the AppData directory in your PC. 

If the software finds the configuration file, it will load the RPC values so you can edit them as you wish. If you never enabled or added any RPC information to the file, you can fill out the details and press "Save", in which the software will write the new information to the config file

If the software doesn't finds the configuration file, you can browse for it manually, and it will load the RPC details currently in the config if they are available. If not, you can specify the values in the software and save them.

The software makes a backup copy of the config file as "gridcoinresearch.conf.rpcbak" in case something happens, but nothing bad should happen. Please note that this software is released as is and I'm not liable for any damage done to your wallet config file, gridcoin, machine, etc.

If you find any bugs, or want to add something to the app, please contact me or raise an issue :)

Have a nice day and enjoy this tool!
