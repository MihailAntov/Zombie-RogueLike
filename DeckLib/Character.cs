using static System.Console;
namespace DeckLib
{
    public class ClassProcedures
    {

        public static Menu CharacterScreenMenu = new Menu();
        public static Menu HelpSubMenu = new Menu();
        public static void CharacterScreen()
        {
            Clear();
            CharacterScreenMenu.header.Add($"Name: {Player.Name}       Level: {Player.Level}");
            CharacterScreenMenu.header.Add($"XP: {Player.XP} / {Player.XPToNextLevel}");
            CharacterScreenMenu.header.Add($"");
            CharacterScreenMenu.header.Add($"Class: {Player.CharacterClass}");
            foreach (string line in Player.ClassDescription)
            {
                CharacterScreenMenu.header.Add(line);
            }
            CharacterScreenMenu.header.Add($"");
            CharacterScreenMenu.header.Add($"Item:  {Player.Tools}");
            foreach (string line in Player.ToolsDescription)
            {
                CharacterScreenMenu.header.Add(line);
            }
            CharacterScreenMenu.header.Add($"");
            CharacterScreenMenu.header.Add($"Trait: {Player.Trait}");
            foreach (string line in Player.TraitDescription)
            {
                CharacterScreenMenu.header.Add(line);
            }
            CharacterScreenMenu.header.Add($"");
            CharacterScreenMenu.header.Add("Actions currently known:");
            foreach (Card action in Deck.cardList)
            {
                CharacterScreenMenu.header.Add($"{action.Name}");
            }
            CharacterScreenMenu.header.Add($"");
            CharacterScreenMenu.options.Add("Return to game");
            CharacterScreenMenu.options.Add("Help");
            HelpSubMenu.header.Add("Select a topic");
            HelpSubMenu.footer.Add("(use ↑ and ↓ to navigate the menu, and \"backspace\" to return to the previous menu.)" );
            HelpSubMenu.addOption("General Information");
            HelpSubMenu.addOption("Exploration");
            HelpSubMenu.addOption("Combat");
            CharacterScreenMenu.options.Add("Exit to main menu");
            CharacterScreenMenu:
            CharacterScreenMenu.goIntoMenu();
            switch (CharacterScreenMenu.finalSelection)
            {
                case 0:
                case 99:
                CharacterScreenMenu.header.Clear();
                CharacterScreenMenu.options.Clear();
                Movement.ExploreMap();
                break;
                case 1:
                HelpSubMenu:
                HelpSubMenu.goIntoMenu();
                switch (HelpSubMenu.finalSelection)
                {
                    case 0:
                    HelpMenu.HelpGeneralInformation();
                    goto HelpSubMenu;

                    case 1:
                    HelpMenu.HelpExploration();
                    goto HelpSubMenu;

                    case 2:
                    HelpMenu.HelpCombat();
                    goto HelpSubMenu;

                    case 99:
                    Clear();
                    goto CharacterScreenMenu;
                }
                break;
                case 2:
                Movement.Exploring = false;
                CharacterScreenMenu.header.Clear();
                CharacterScreenMenu.options.Clear();
                HelpSubMenu.header.Clear();
                HelpSubMenu.options.Clear();
                HelpSubMenu.footer.Clear();
                Unload.UnloadAssets();
                break;              
            }

        }
        public static void LoadClass()
        {
            
            //meleee
            if (Player.CharacterClass == "Cold Blooded" || Player.CharacterClass == "Reckless")
            {
                DefaultValues.StabBonus = 1;
                
            }
            else
            {
                DefaultValues.StabBonus = 0;
            }
            //Cold Blooded
            if (Player.CharacterClass == "Cold Blooded")
            {
                DefaultValues.ColdBloodedBonus = 1;
                Player.ClassDescription.Clear();
                Player.ClassDescription.Add("(Your stabs deal more damage. Each time you wait, the extra damage increases, up to a cap determined by your level.)");
            }
            else
            {
                DefaultValues.ColdBloodedBonus = 0;
            }

            //Reckless
            if (Player.CharacterClass == "Reckless")
            {
                DefaultValues.StabRange = 5;
                DefaultValues.RecklessMultiplier = 1;
                Player.ClassDescription.Clear();
                Player.ClassDescription.Add("(Your stabs deal more damage based on your level, and you can stab from further away.");
                Player.ClassDescription.Add("Each time you stab, the bonus increases.");
                Player.ClassDescription.Add("Each time you don't stab, the bonus decreases.)");
            }
            else
            {
                DefaultValues.StabRange = 3;
                DefaultValues.RecklessMultiplier = 0;
            }

            //Precise
            if (Player.CharacterClass == "Precise")
            {
                //diretly implemented in combat screen
                Player.ClassDescription.Clear();
                Player.ClassDescription.Add("(If the zombies are not close to you, your shots never miss and sometimes deal bonus damage.");
                Player.ClassDescription.Add("The bonus is based on your level and does not increase the ammo cost - you can kill several zombies with one shot.)");
            }
            else
            {
                //directly implement in combat screen
            }

            //Trigger-happy
            if (Player.CharacterClass == "Trigger-happy")
            {
                DefaultValues.ShootBonus = 2;
                Player.ClassDescription.Clear();
                Player.ClassDescription.Add("(Your shots deal more damage and cost more ammo, based on your level.");
                Player.ClassDescription.Add("You find much more ammo in buildings.)");
                //bonus ammo implemented in Loot
            }
            else
            {
                DefaultValues.ShootBonus = 0;
            }

            if (Player.CharacterClass == "Silent")
            {
                // aggroing less zombies directly implemented in Combat
                Player.ClassDescription.Clear();
                Player.ClassDescription.Add("(You attract much fewer zombies when you engage them.");
                Player.ClassDescription.Add("Running away does not attract more zombies.)");
            }
            else
            {

            }

            if (Player.CharacterClass == "Deadly")
            {
                //directly implemented in Combat
                Player.ClassDescription.Clear();
                Player.ClassDescription.Add("(You attract fewer zombies when you engage them. ");
                Player.ClassDescription.Add("At the start of each fight, you take out a number of zombies depending on your level.)");
            }
            else
            {

            }

            if (Player.CharacterClass == "Swift")
            {
                DefaultValues.RunBonus = 1;
                //implemented running and attention in Combat
                Player.ClassDescription.Clear();
                Player.ClassDescription.Add("(Your running is more effective.");
                Player.ClassDescription.Add("If you run away after taking out a certain number of enemies, you permanently draw away a portion of the zombies.)");
            }
            else
            {
                DefaultValues.RunBonus = 0;
            }

            if (Player.CharacterClass == "Tireless")
            {
                Player.ClassDescription.Clear();
                Player.ClassDescription.Add("(You can reuse run actions a certain number of times per fight, based on your level.");
                Player.ClassDescription.Add("Stabbing or shooting after you run is slightly more effective, based on your level.)");
                //implemented directly in Combat
            }
            else
            {
                //implemented directly in Combat
            }

            if(Player.Tools == "Detailed map of the area")
            {
                //implemented in worldmap
                Player.ToolsDescription.Clear();
                Player.ToolsDescription.Add("(Provides additional information on food, ammo and population of potential travel destinations.)");
            }
            else
            {

            }

            if(Player.Tools == "Backpack")
            {
                Player.Food = 20;
                Loot.BackPackMultiplier = 2;
                Player.ToolsDescription.Clear();
                Player.ToolsDescription.Add("(Increases your starting food. You find more food in buildings.)");
            }
            else
            {
                Player.Food = DefaultValues.defaultStartingFood;
                Loot.BackPackMultiplier = 0;
            }

            if(Player.Tools == "Bag of ammo")
            {
                Player.Ammo = 30;
                Loot.AmmoBagModifier = 1;
                Player.ToolsDescription.Clear();
                Player.ToolsDescription.Add("(Increases starting ammo. You find more ammo in buildings.)");
            }
            else
            {
                Player.Ammo = DefaultValues.defaultStartingAmmo;
                Loot.AmmoBagModifier = 0;
            }

            if(Player.Trait == "Gifted")
            {
                Deck.DeckSize = 6;
                Player.TraitDescription.Clear();
                Player.TraitDescription.Add("(You start the game with two additional actions.)");
            }
            else
            {
                Deck.DeckSize = 4;
            }

            if(Player.Trait == "Fast learner")
            {
                DefaultValues.DefaultXPToNextLevel = 15;
                Player.XPToNextLevel = DefaultValues.DefaultXPToNextLevel;
                DefaultValues.DefaultXPToNextLevelIncrement = 1;
                Player.TraitDescription.Clear();
                Player.TraitDescription.Add("(You level up faster.)");
            }
            else
            {
                DefaultValues.DefaultXPToNextLevel = 20;
                Player.XPToNextLevel = DefaultValues.DefaultXPToNextLevel;
                DefaultValues.DefaultXPToNextLevelIncrement = 2;
            }

            if(Player.Trait == "Versatile")
            {
                //directly implemented in PickACard
                Player.TraitDescription.Clear();
                Player.TraitDescription.Add("(When picking a new action, choose from two additional options.)");
            }
            else
            {
                //directly implemented in PickACard
            }
        }

        //cold blooded:
        //reckless
        //precise
        //trigger happy
        //silent
        //deadly
        //fast
        //tireless


        //map of the area
        //survival kit
        //bag of ammo


        //gifted
        //fastLearner
        //versatile
    }


}
