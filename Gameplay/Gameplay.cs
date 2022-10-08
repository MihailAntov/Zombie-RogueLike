using static System.Console;
using DeckLib;
Console.CursorVisible = false;
WriteLine("For an optimal experience, maximize the console screen size by holding down the Windows key and pressing the \"Up\" arrow.");
WriteLine("Press any key to continue.");
ReadKey();
//main menu
CharacterClass.LoadMenuQuestions();
Menu mainMenu = new Menu();
mainMenu.header.Add(GameName.GenerateGameName());
mainMenu.header.Add(" ");
mainMenu.addOption("Start a new game.");
mainMenu.addOption("Exit.");
mainMenu.footer.Add(" ");
mainMenu.footer.Add("Use the up and down arrow keys to navigate.");
mainMenu.footer.Add("Press enter to select, and escape to go back to the previous menu.");
mainMenu.footer.Add(" ");
mainMenu.footer.Add("Mishko Prodakshans. Some rights reserved.");
Menu helpMenu = new Menu();

//help menu
helpMenu.header.Add("Select a topic");
helpMenu.header.Add("");
helpMenu.footer.Add("(use ↑ and ↓ to navigate the menu, and \"backspace\" to return to the previous menu.)" );
helpMenu.addOption("General information");
helpMenu.addOption("Exploration");
helpMenu.addOption("Combat");
mainMenu:
Console.CursorVisible = false;

//start menu
Menu startMenu = new Menu();
startMenu.header.Add("Select an option:");
startMenu.footer.Add("(use ↑ and ↓ to navigate the menu, and \"backspace\" to return to the previous menu.)" );
startMenu.addOption("Begin your adventure.");
startMenu.addOption("Make a new character");
startMenu.addOption("Help");
startMenu.addOption("Exit to main menu");

//begin game
mainMenu.goIntoMenu();
switch (mainMenu.finalSelection)
{
    case 0:
        CharacterClass.CharacterCreation();
        Clear();
        CharacterClass.Started = true;
    startMenu:
        startMenu.goIntoMenu();
        switch (startMenu.finalSelection)
        {
            case 0:
                break;
            case 1:
                CharacterClass.CharacterCreation();
                goto startMenu;
            case 2:
            helpMenu:
                helpMenu.goIntoMenu();
                switch (helpMenu.finalSelection)
                {
                    case 0:
                        HelpMenu.HelpGeneralInformation();
                        goto helpMenu;
                    case 1:
                        HelpMenu.HelpExploration();
                        goto helpMenu;
                    case 2:
                        HelpMenu.HelpCombat();
                        goto helpMenu;
                    case 99:
                        goto startMenu;
                }
                break;
            case 3:
                CharacterClass.ConfirmChoice.goIntoMenu();
                switch (CharacterClass.ConfirmChoice.finalSelection)
                {
                    case 0:
                        
                        
                        mainMenu.header.Clear();
    mainMenu.header.Add(GameName.GenerateGameName());
    mainMenu.header.Add(" ");
                        Unload.UnloadAssets();
                        goto mainMenu;
                    case 1:
                        goto startMenu;
                }
                goto startMenu;
            case 99:
                goto startMenu;
        }
        Clear();
        if (Player.Trait == "Gifted")
        {
            WriteLine("Being gifted, you must choose six starting actions for your character.");
        }
        else
        {
            WriteLine("You must choose four starting actions for your character.");
        }

        WriteLine("Press any key to continue.");
        ReadKey();
        Clear();
        PickACard.PickOneOfThree();
        WorldMap.GenerateWorldMap();
        Movement.currentMap = new Map(Travel.CurrentLocation.AmmoRichness, Travel.CurrentLocation.FoodRichness, Travel.CurrentLocation.Size, Travel.CurrentLocation.HouseDensity, Travel.CurrentLocation.TreeDensity, Travel.CurrentLocation.ZombieDensity);

        Movement.currentMap.GenerateMap();
        Movement.ExploreMap();

        break;
    case 1:
        WriteLine("Exitting game.");
        CharacterClass.ConfirmChoice.goIntoMenu();
        switch (CharacterClass.ConfirmChoice.finalSelection)
        {
            case 0:
                Environment.Exit(0);
                break;
            case 1:
                goto mainMenu;
        }
        ReadLine();
        break;
    case 99:
        goto mainMenu;

}
if (Player.Dead == true)
{
    Clear();
    WriteLine("You feel teeth piercing your flesh as everything goes dark.");
    WriteLine("You die.");
    WriteLine("Press any key to return to the main menu.");
    ReadKey();
    mainMenu.header.Clear();
    mainMenu.header.Add(GameName.GenerateGameName());
    mainMenu.header.Add(" ");
    Unload.UnloadAssets();

    goto mainMenu;
}
else if (Player.Won == true)
{
    Clear();
    WriteLine("In the distance you see a fortified wall with spotlights illuminating the area for miles around.");
    WriteLine("You can make out faint shouts coming from within, as a large set of gates opens, showing the well lit interior.");
    WriteLine();
    WriteLine("You made it.");
    ReadKey();
    mainMenu.header.Clear();
    mainMenu.header.Add(GameName.GenerateGameName());
    mainMenu.header.Add(" ");
    Unload.UnloadAssets();
    //roll credits
    goto mainMenu;
}
else
{
    mainMenu.header.Clear();
    mainMenu.header.Add(GameName.GenerateGameName());
    mainMenu.header.Add(" ");
    Unload.UnloadAssets();
    goto mainMenu;
}
