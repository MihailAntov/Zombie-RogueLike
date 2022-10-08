using static System.Console;
namespace DeckLib
{
    public class LevellingUp
    {
        public static Menu levelUpMenu = new Menu();
        public static void LevelUp()
        {
            Combat.levellingUp = true;
            Player.Level = Player.Level + 1;
            Player.XP = Player.XP - Player.XPToNextLevel;
            Player.XPToNextLevel = Player.XPToNextLevel + DefaultValues.DefaultXPToNextLevelIncrement;
            levelUpMenu.header.Add($"You have reached level {Player.Level}!");
            levelUpMenu.header.Add("Your class abilities improve.");
            levelUpMenu.header.Add("Please select an option.");
            levelUpMenu.addOption("Learn a new action");
            foreach (Card action in Deck.cardList)
            {
                if (action.Value < 3)
                {
                    levelUpMenu.addOption("Improve a random action you already know.");
                    break;
                }
            }
            levelUpMenu.goIntoMenu();
            switch (levelUpMenu.finalSelection)
            {
                case 0:
                    Deck.DeckSize = Deck.DeckSize + 1;
                    PickACard.PickOneOfThree();
                    Clear();
                    break;
                case 1:
                    Deck.cardList.Shuffle();
                    foreach (Card action in Deck.cardList)
                    {
                        if (action.Value < 3)
                        {
                            action.Value++;
                            string oldName = action.Name;
                            if (action.Type == "Stab")
                            {
                                switch (action.Value)
                                {
                                    case 1:
                                        action.Name = "Stab a lot";
                                        break;
                                    case 2:
                                        action.Name = "Stab a whole lot";
                                        break;
                                    case 3:
                                        action.Name = "Stab like a madman";
                                        break;
                                }
                            }
                            else if (action.Type == "Shoot")
                            {
                                switch (action.Value)
                                {
                                    case 1:
                                        action.Name = "Shoot a lot";
                                        break;
                                    case 2:
                                        action.Name = "Shoot a whole lot";
                                        break;
                                    case 3:
                                        action.Name = "Shoot like crazy";
                                        break;
                                }
                            }
                            else
                            {
                                switch (action.Value)
                                {
                                    case 1:
                                        action.Name = "Run";
                                        break;
                                    case 2:
                                        action.Name = "Run Fast";
                                        break;
                                    case 3:
                                        action.Name = "Run Like Hell";
                                        break;
                                }
                            }
                            Clear();
                            WriteLine($"{oldName} has been upgraded to {action.Name}.");
                            break;
                        }
                    }
                    break;
            }
            levelUpMenu.header.Clear();
            levelUpMenu.options.Clear();
            Combat.levellingUp = false;
            WriteLine("Press any key to return to exploring.");
            ReadKey();

        }
    }
}