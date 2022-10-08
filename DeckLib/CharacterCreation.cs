using static System.Console;
namespace DeckLib
{
    public class CharacterClass
    {
        public static Menu ClassMenu = new Menu();
        
        public static Menu FightMenu = new Menu();
        public static Menu NotFightMenu = new Menu();
        public static Menu StabMenu = new Menu();
        public static Menu ShootMenu = new Menu();
        public static Menu SneakMenu = new Menu();
        public static Menu FleeMenu = new Menu();
        public static Menu ToolsMenu = new Menu();
        public static Menu TraitMenu = new Menu();
        public static Menu ConfirmChoice = new Menu();
        public static bool menusLoaded = false;
        public static bool Started = false;

                
                
 
        public static void LoadMenuQuestions()
        {
            //class menu
            ClassMenu.header.Add("Would you rather fight the zombies or not fight them?");
            ClassMenu.header.Add("");
            ClassMenu.addOption("Fight");
            ClassMenu.addOption("Not Fight");
            
            //fight menu
            FightMenu.header.Add("Would you rather stab or shoot a zombie?");
            FightMenu.header.Add("");
            FightMenu.addOption("Stab");
            FightMenu.addOption("Shoot");

            //notfight menu
            NotFightMenu.header.Add("Would you rather sneak past them or flee from them?");
            NotFightMenu.header.Add("");
            NotFightMenu.addOption("Sneak");
            NotFightMenu.addOption("Flee");

            //stab menu
            StabMenu.header.Add("Would you wait for an opportunity to attack or would you prefer to charge in?");
            StabMenu.header.Add("");
            StabMenu.addOption("Wait");
            StabMenu.addOption("Charge");

            //shoot menu
            ShootMenu.header.Add("Would you rather shoot more accurately or more quickly?");
            ShootMenu.header.Add("");
            ShootMenu.addOption("Accurately");
            ShootMenu.addOption("Quickly");
            
            //sneak menu
            SneakMenu.header.Add("Would you rather be more silent or more deadly?");
            SneakMenu.header.Add("");
            SneakMenu.addOption("Silent");
            SneakMenu.addOption("Deadly");
            
            //flee menu
            FleeMenu.header.Add("Would you prefer speed or endurance?");
            FleeMenu.header.Add("");
            FleeMenu.addOption("Speed");
            FleeMenu.addOption("Endurance");
            
            //tools menu
            ToolsMenu.header.Add("On the day the world came crumbling down, you left your home in a hurry.");
            ToolsMenu.header.Add("Before going out the door, what was the one item you grabbed?");
            ToolsMenu.header.Add("");
            ToolsMenu.addOption("Detailed map of the area");
            ToolsMenu.addOption("Survival kit");
            ToolsMenu.addOption("Bag of ammo");
            
            //trait menu
            TraitMenu.header.Add("You've always been:");
            TraitMenu.header.Add("");
            TraitMenu.addOption("Gifted");
            TraitMenu.addOption("A Fast Learner");
            TraitMenu.addOption("Versatile");

            //confirm choice menu
            ConfirmChoice.header.Add("Are you sure?");
            ConfirmChoice.header.Add("");
            ConfirmChoice.addOption("Yes");
            ConfirmChoice.addOption("No");

            menusLoaded = true;
            
        }
        public static void CharacterCreation()
        {
            //narrative
            Clear();
            if (Started == false)
            {
                WriteLine("You stop to catch your breath. You've been on the run for hours. You wipe the sweat from your face, and look at your hands.");
                WriteLine("They are covered in blood. Your heart skips a beat, but then you realize it's dry. It's not yours.");
                WriteLine("It must be Frank's. You think of Frank, with his round, friendly face. He seemed like a nice guy when you met him this morning.");
                WriteLine("");
                WriteLine("(Press any key to continue.)");
                ReadKey();
                Clear();

                WriteLine("<Five Hours Earlier>");
                WriteLine(" \"Put your weapon on the ground, and walk forward. Slowly.\", said the guard at the gate. The newcomer complied without saying a word.");
                WriteLine("The gate opened slowly, and the lone wanderer walked into the small camp.");
                WriteLine("After searching him, the guards sent him to Frank's tent. \"It's orientation, you see\", they explained.");
                WriteLine("In the tent, Frank was waiting patiently, holding a ledger. He waved the new arrival in.");
                WriteLine("\"Come in, mister... ?\"");
                WriteLine("The man answered:");
                WriteLine("");
                WriteLine("(type in your name and press \"Enter\")");

            }
            else
            {
                WriteLine("Please enter your character's name:");
            }
            //ask for name
            Player.Name = ReadLine();
            if (Player.Name.Length < 1)
            {
                Player.Name = "Wanderer";
            }
            Clear();

            //class questions
            if (Started == false)
            {
                WriteLine($"\"Have a seat then, {Player.Name}.\"");
                WriteLine("Frank opened the ledger, clicked his pen, and said:");
                WriteLine($"\"Everyone here pulls their weight, {Player.Name}. If you are to stay with us, that means you too.\"");
                WriteLine("He smiled. \"Don't worry, you'll be fine. Just answer these questions, so that I can put you down for a position.\"");
                WriteLine("");



            }
            else
            {
                WriteLine("In order to choose a class, you will need to answer a few questions.");
            }
            
            WriteLine("(Use the \"up\" and \"down\" arrows to navigate through the options.");
            WriteLine("Press \"enter\" to select your answer, and \"backspace\" to return to the previous question.");
            WriteLine("Press any key to continue to the questions.)");
            ReadKey();
            Clear();

            //class menu
            EnterClassMenu:
            ClassMenu.goIntoMenu();
            switch(ClassMenu.finalSelection)
            {
                case 0:
                    EnterFightMenu:
                    FightMenu.goIntoMenu();
                    switch(FightMenu.finalSelection)
                    {
                        case 0:
                            EnterStabMenu:
                            StabMenu.goIntoMenu();
                            switch(StabMenu.finalSelection)
                            {
                                case 0:
                                    ConfirmChoice.goIntoMenu();
                                    switch(ConfirmChoice.finalSelection)
                                    {
                                        case 0:
                                        Player.CharacterClass = "Cold Blooded";
                                        break;
                                        case 1:
                                        case 99:
                                        goto EnterStabMenu; 
                                    }    
                                break;
                                case 1:
                                    ConfirmChoice.goIntoMenu();
                                    switch(ConfirmChoice.finalSelection)
                                    {
                                        case 0:
                                        Player.CharacterClass = "Reckless";
                                        break;
                                        case 1:
                                        case 99:
                                        goto EnterStabMenu; 
                                    } 
                                break;
                                case 99:
                                goto EnterFightMenu;
                            }
                        break;
                        case 1:
                            EnterShootMenu:
                            ShootMenu.goIntoMenu();
                            switch(ShootMenu.finalSelection)
                            {
                                case 0:
                                    ConfirmChoice.goIntoMenu();
                                    switch(ConfirmChoice.finalSelection)
                                    {
                                        case 0:
                                        Player.CharacterClass = "Precise";
                                        break;
                                        case 1:
                                        case 99:
                                        goto EnterShootMenu; 
                                    }    
                                break;
                                case 1:
                                    ConfirmChoice.goIntoMenu();
                                    switch(ConfirmChoice.finalSelection)
                                    {
                                        case 0:
                                        Player.CharacterClass = "Trigger-happy";
                                        break;
                                        case 1:
                                        case 99:
                                        goto EnterShootMenu; 
                                    } 
                                break;
                                case 99:
                                goto EnterFightMenu;
                            }
                        break;
                        case 99:
                        goto EnterClassMenu;
                    }
                break;
                case 1:
                    EnterNotFightMenu:
                    NotFightMenu.goIntoMenu();
                    switch(NotFightMenu.finalSelection)
                    {
                        case 0:
                            EnterSneakMenu:
                            SneakMenu.goIntoMenu();
                            switch(SneakMenu.finalSelection)
                            {
                                case 0:
                                    ConfirmChoice.goIntoMenu();
                                    switch(ConfirmChoice.finalSelection)
                                    {
                                        case 0:
                                        Player.CharacterClass = "Silent";
                                        break;
                                        case 1:
                                        case 99:
                                        goto EnterSneakMenu; 
                                    }    
                                break;
                                case 1:
                                    ConfirmChoice.goIntoMenu();
                                    switch(ConfirmChoice.finalSelection)
                                    {
                                        case 0:
                                        Player.CharacterClass = "Deadly";
                                        break;
                                        case 1:
                                        case 99:
                                        goto EnterSneakMenu; 
                                    } 
                                break;
                                case 99:
                                goto EnterNotFightMenu;
                            }
                        break;
                        case 1:
                            EnterFleeMenu:
                            FleeMenu.goIntoMenu();
                            switch(FleeMenu.finalSelection)
                            {
                                case 0:
                                    ConfirmChoice.goIntoMenu();
                                    switch(ConfirmChoice.finalSelection)
                                    {
                                        case 0:
                                        Player.CharacterClass = "Swift";
                                        break;
                                        case 1:
                                        case 99:
                                        goto EnterFleeMenu; 
                                    }    
                                break;
                                case 1:
                                    ConfirmChoice.goIntoMenu();
                                    switch(ConfirmChoice.finalSelection)
                                    {
                                        case 0:
                                        Player.CharacterClass = "Tireless";
                                        break;
                                        case 1:
                                        case 99:
                                        goto EnterFleeMenu; 
                                    } 
                                break;
                                case 99:
                                goto EnterNotFightMenu;
                            }
                        break;
                        case 99:
                        goto EnterClassMenu;
                    }
                break;
                case 99:
                goto EnterClassMenu;
            }

            // tools question
            entertoolsmenu:
            ToolsMenu.goIntoMenu();
            switch(ToolsMenu.finalSelection)
            {
                case 0:
                    Player.Tools = "Detailed map of the area";
                    break;
                case 1:
                    Player.Tools = "Backpack";
                    break;
                case 2:
                    Player.Tools = "Bag of ammo";
                    break;
                case 99:
                goto entertoolsmenu;
            }

            //trait question
            entertraitmenu:
            TraitMenu.goIntoMenu();
            switch(TraitMenu.finalSelection)
            {
                case 0:
                    Player.Trait = "Gifted";
                    break;
                case 1:
                    Player.Trait = "Fast learner";
                    break;
                case 2:
                    Player.Trait = "Versatile";
                    break;
                case 99:
                goto entertraitmenu;
            }
            Clear();
            if (Started == false)
            {
                WriteLine("\"That's enough, thanks\", Frank said while writing in the ledger. \"You'll fit right in, as soon as we...\"");
                WriteLine("He was interrupted by a scream outside. He darted out of the tent, rifle in hand.");
                WriteLine("The newcomer followed him out only to witness utter chaos.");
                WriteLine("The gates, so sturdy mere minutes before, were down. A sea of bodies was pouring through the gaping hole in the walls.");
                WriteLine("Frank was shotting again and again, but there was no end to the zombie horde. One of them appeared from the side of the tent.");
                WriteLine("Before the wanderer could react, the zombie had sunk its teeth into Frank's neck. A gush of blood came out, covering the wanderer's face.");
                WriteLine();
                WriteLine("He had seen this before. He knew how it ended. He had hoped this one would be different, but deep down he knew better.");
                WriteLine("He knew what to do. He ran.");
                WriteLine("YOU ran.");
                WriteLine("");
                WriteLine("(Press any key to continue.)");
                ReadKey();
            }
            

            Clear();
            WriteLine("Character created successfully.");
            ClassProcedures.LoadClass();
            WriteLine();
            WriteLine($"Welcome, {Player.Name}.");
            WriteLine($"Your class is {Player.CharacterClass}.");
            foreach (string line in Player.ClassDescription)
            {
                WriteLine(line);
            }
            WriteLine();
            WriteLine($"Your starting tool is the {Player.Tools}.");
            foreach (string line in Player.ToolsDescription)
            {
                WriteLine(line);
            }
            WriteLine();
            WriteLine($"Your trait is {Player.Trait}.");
            foreach (string line in Player.TraitDescription)
            {
                WriteLine(line);
            }
            WriteLine();
            WriteLine("Press any key to continue.");
            ReadKey();
            Clear();

        }

    }
}