# C&#35;
If you plan to develop your entry in C&#35; you need to download and install [Visual Studio Express 2013](http://www.microsoft.com/en-us/download/details.aspx?id=44914).

## Get The Code
Download the [zip file](https://github.com/EntelectChallenge/2015-SpaceInvaders-Bot-CSharp/archive/master.zip) from Github or use Git to clone the repository:
* Start Git Bash
* Change to the directory where you want to checkout the sample bot
* Run: `git clone https://github.com/EntelectChallenge/2015-SpaceInvaders-Bot-CSharp.git`

## Compile
The easiest way to compile is to open the solution (`BasicBot.sln`) in Visual Studio and to select Build -> Build Solution from the menus.

You will need to test that your project compiles from the command prompt as well and for this you'll need a Visual Studio Command Prompt. If this was not added to your start menu during the Visual Studio installation follow the instructions in this [stackoverflow post](http://stackoverflow.com/questions/21476588/where-is-developer-command-prompt-for-vs2013) to add it to your Visual Studio tools menu.

Once you have a working Visual Studio Command Prompt simply start it, change to the project directory and run `compile.bat`.

## Run
Once you have compiled you can do a basic test of the bot by simply launching a Command Prompt, changing to the bot directory and running `run.bat`. This will use the example state and map files in the output folder.

## NuGet
If your bot depends on any libraries you can install them with the [NuGet Package Manager](https://docs.nuget.org/consume/installing-nuget).

Be sure to test that [package restore](https://docs.nuget.org/consume/package-restore) is working so that running your compile.bat will fetch your dependencies before your bot is run.