using static System.Console;
namespace DeckLib
{
    public static class Combat
    {
        public static bool ranAway;
        public static int ZombieStrength;
        public static int ZombieMovement = 1;
        public static List<string> zombieBehavior = new List<string> { "Passive", "Passive", "Extra Tough", "Tough", "Rush", "Tough" };
        public static int CurrentRoundAttack;
        public static int ZombieStartingCount;
        public static bool gotAttention;
        public static int Distance;
        public static int oldDistance;
        //verify that a card successfully resolves, if not - keep choosing
        public static bool chosenCard = false;
        public static bool inCombat = false;
        public static bool levellingUp = false;
        public static int XPreward;
        public static int AmmoSpent;
        public static int CurrentColdBloodedBonus;
        public static int ColdBloodedCounter;
        public static int ColdBloodCap;
        public static int CurrentRecklessBonus;
        public static int RecklessCounter;
        public static int SniperBonus;
        public static int SneakAttack;
        public static int RunAndGun;
        public static int TiredCounter;
        public static int FoodLost;
        public static int AmmoLost;
        public static int FastXP;
        public static string Action;
        public static string ActionType;
        public static Map currentMap = Movement.currentMap;
        public static Menu combatMenu = new Menu();
        public static List<Card> handList = new List<Card>();
        public static List<string> zombieList = new List<string>();


        public static void PlayCard(Card cardToBePlayed)
        {
            if (cardToBePlayed.Type == "Stab")
            {
                if (Distance <= DefaultValues.StabRange)
                {
                    Action = cardToBePlayed.Name;
                    ActionType = cardToBePlayed.Type;
                    CurrentRoundAttack = CurrentRoundAttack + cardToBePlayed.Value + 1 + DefaultValues.StabBonus + CurrentColdBloodedBonus + CurrentRecklessBonus + RunAndGun;
                    ColdBloodedCounter = 0;
                    RecklessCounter++;
                    RunAndGun = 0;
                    cardToBePlayed.InHand = false;
                    cardToBePlayed.InDiscard = true;
                    chosenCard = true;
                }
                else
                {
                    combatMenu.footer.Add("You are too far away to stab. Select a different action.");
                }
            }
            if (cardToBePlayed.Type == "Shoot")
            {
                if (CurrentRoundAttack + 1 + cardToBePlayed.Value + DefaultValues.ShootBonus + RunAndGun > Player.Ammo)
                {
                    combatMenu.footer.Add("You don't have enough ammo for that. Select a different action.");

                }
                else
                {
                    Action = cardToBePlayed.Name;
                    ActionType = cardToBePlayed.Type;
                    CurrentRoundAttack = CurrentRoundAttack + cardToBePlayed.Value + 1 + DefaultValues.ShootBonus + RunAndGun;
                    AmmoSpent = CurrentRoundAttack;
                    Player.Ammo = Player.Ammo - AmmoSpent;
                    RecklessCounter--;
                    RunAndGun = 0;
                    cardToBePlayed.InHand = false;
                    cardToBePlayed.InDiscard = true;
                    chosenCard = true;
                }
            }
            if (cardToBePlayed.Type == "Run")
            {
                Action = cardToBePlayed.Name.ToLower();
                ActionType = cardToBePlayed.Type;
                if (Distance + 1 + cardToBePlayed.Value + DefaultValues.RunBonus < 10)
                {
                    Distance = Distance + 1 + cardToBePlayed.Value + DefaultValues.RunBonus;
                }
                else
                {
                    Distance = 10;
                }
                
                RecklessCounter--;
                
                if (Player.CharacterClass == "Tireless" && TiredCounter < Player.Level)
                {
                    RunAndGun = 1 + (Player.Level / 3);
                    TiredCounter++;
                }
                else
                {
                    cardToBePlayed.InHand = false;
                    cardToBePlayed.InDiscard = true;
                }
                
                chosenCard = true;
            }
        }
        public static void StartCombat(Map currentMap)
        {
            currentMap = Movement.currentMap;

            //checks a 5x5 square around the player's position for zombies and adds them to the zombieStrength value.
            if (Player.CharacterClass == "Silent")
            {
                if (currentMap.listOfPoints[currentMap.PlayerPosition].Scaled == false)
                {
                    currentMap.listOfPoints[currentMap.PlayerPosition].zombieCount = currentMap.listOfPoints[currentMap.PlayerPosition].zombieCount + (Player.Level / 4);
                    currentMap.listOfPoints[currentMap.PlayerPosition].Scaled = true;
                }
                ZombieStrength = currentMap.listOfPoints[currentMap.PlayerPosition].zombieCount;
                //only fight the zombies at the location
            }
            else if (Player.CharacterClass == "Deadly")
            {
                foreach (PointOnMap point in currentMap.listOfPoints)
                {
                    if ((
                       point.XPosition == currentMap.listOfPoints[currentMap.PlayerPosition].XPosition - 1 ||
                       point.XPosition == currentMap.listOfPoints[currentMap.PlayerPosition].XPosition ||
                       point.XPosition == currentMap.listOfPoints[currentMap.PlayerPosition].XPosition + 1) &&
                       (point.YPosition == currentMap.listOfPoints[currentMap.PlayerPosition].YPosition - 1 ||
                       point.YPosition == currentMap.listOfPoints[currentMap.PlayerPosition].YPosition ||
                       point.YPosition == currentMap.listOfPoints[currentMap.PlayerPosition].YPosition + 1))
                    {
                        if (point.zombieCount != 0)
                        {
                            if (point.Scaled == false)
                            {
                                point.zombieCount = point.zombieCount + (Player.Level / 4);
                                point.Scaled = true;
                            }
                            //calculates zombie strength in a 3x3 grid
                            ZombieStrength = ZombieStrength + point.zombieCount;
                        }
                    }

                }
            }
            else
            {
                foreach (PointOnMap point in currentMap.listOfPoints)
                {
                    if ((point.XPosition == currentMap.listOfPoints[currentMap.PlayerPosition].XPosition - 2 ||
                       point.XPosition == currentMap.listOfPoints[currentMap.PlayerPosition].XPosition - 1 ||
                       point.XPosition == currentMap.listOfPoints[currentMap.PlayerPosition].XPosition ||
                       point.XPosition == currentMap.listOfPoints[currentMap.PlayerPosition].XPosition + 1 ||
                       point.XPosition == currentMap.listOfPoints[currentMap.PlayerPosition].XPosition + 2) &&
                       (point.YPosition == currentMap.listOfPoints[currentMap.PlayerPosition].YPosition - 2 ||
                       point.YPosition == currentMap.listOfPoints[currentMap.PlayerPosition].YPosition - 1 ||
                       point.YPosition == currentMap.listOfPoints[currentMap.PlayerPosition].YPosition ||
                       point.YPosition == currentMap.listOfPoints[currentMap.PlayerPosition].YPosition + 1 ||
                       point.YPosition == currentMap.listOfPoints[currentMap.PlayerPosition].YPosition + 2))
                    {
                        if (point.zombieCount != 0)
                        {
                            if (point.Scaled == false)
                            {
                                point.zombieCount = point.zombieCount + (Player.Level / 4);
                                point.Scaled = true;
                            }
                            //calculates zombie strength in a 5x5 grid
                            ZombieStrength = ZombieStrength + point.zombieCount;
                        }
                    }

                }
            }

            //messages for combat start
            Clear();
            ZombieStartingCount = ZombieStrength;
            if (ZombieStrength == 1)
            {
                WriteLine("You hear the sound of shuffling feet nearby. A lone zombie is slowly shuffling toward you.");
            }
            if (ZombieStrength == 2)
            {
                WriteLine("You hear a low moan. Two zombies are approaching you. ");
            }
            else if (ZombieStrength > 2)
            {
                WriteLine("You hear a growl. A zombie has noticed you. ");
                WriteLine($"Attracted by the noise, others draw near. A total of {ZombieStrength} zombies are slowly approaching you.");
            }


            
            if (Player.CharacterClass == "Deadly")
            {
                SneakAttack = 1+(Player.Level/2);
                
                if(ZombieStrength == 2)
                {

                    WriteLine($"Using the element of surprise, you manage to take out one of them before the other one has a chance to react.");
                    ZombieStrength = 1;
                }
                else if (ZombieStrength > 2)
                {
                    if (SneakAttack > ZombieStrength)
                    {
                        WriteLine("Using the element of surprise, you manage to take out all but one of the zombies before they have a chance to react.");
                        ZombieStrength = 1;
                    }
                    else
                    {
                        WriteLine($"Using the element of surprise, you manage to take out {SneakAttack} zombies before they have a chance to react. ");
                        ZombieStrength = ZombieStrength - SneakAttack;
                    }
                    
                }
                
            }
            WriteLine("Press any key to begin combat.");
            ReadKey();
            CombatLoop(ZombieStrength);

            //Cleanup at end of fight:
            ZombieStrength = 0;
        }
        public static void CombatLoop(int zombieStrength)
        {
            //precombat setting of values
            currentMap = Movement.currentMap;
            XPreward = zombieStrength;
            

            if (currentMap.listOfPoints[currentMap.PlayerPosition].searchable == true)
            {
                Distance = DefaultValues.defaultIndoorDistance;
            }
            else
            {
                Distance = DefaultValues.defaultOutdoorDistance;
            }
            CurrentRoundAttack = DefaultValues.defaultAttack;
            SniperBonus = Player.Level / 2;
            ColdBloodedCounter = 0;
            ColdBloodCap = Player.Level + 1;
            RunAndGun = 0;
            TiredCounter = 0;
            CurrentColdBloodedBonus = DefaultValues.ColdBloodedBonus * ColdBloodedCounter;

            RecklessCounter = 0;
            CurrentRecklessBonus = DefaultValues.RecklessMultiplier * RecklessCounter;

            gotAttention = false;
            DeckLib.Deck.DrawCard();
            DeckLib.Deck.DrawCard();

            //actual combat loop
            while (inCombat == true)
            {

                Console.CursorVisible = false;
                //populate the menu with options
                //CombatVisual.UpdateBattleField(Distance, zombieStrength);
                ZombieStrength = zombieStrength;
                CurrentColdBloodedBonus = ColdBloodedCounter * DefaultValues.ColdBloodedBonus;
                if (CurrentColdBloodedBonus >= ColdBloodCap)
                {
                    combatMenu.footer.Add("You see an opportunity to strike. There's no need to wait any longer.");
                    CurrentColdBloodedBonus = ColdBloodCap;
                }
                if (RecklessCounter < 0)
                {
                    RecklessCounter = 0;
                }
                CurrentRecklessBonus = DefaultValues.RecklessMultiplier * RecklessCounter * (Player.Level / 2 + 1);
                combatMenu.addOption("Stab");
                combatMenu.addOption("Wait");
                combatMenu.addOption("Shoot");
                foreach (Card card in Deck.cardList)
                {
                    if (card.InHand == true)
                    {
                        handList.Add(card);
                    }
                }
                foreach (Card card in handList)
                {
                    combatMenu.addOption($"{card.Name}");
                }
            //picking an action starts here
            choosingOption:
                //combatMenu.header.Add($"Zombie Count: {zombieStrength}   Ammo: {Player.Ammo}   Distance: {Distance}   Reckless: {CurrentRecklessBonus} Sniper{SniperBonus} Coldblooded{CurrentColdBloodedBonus} ");
                combatMenu.header.Add($"   Ammo: {Player.Ammo}");
                combatMenu.header.Add($"");
                combatMenu.footer.Add(DisplayDistance(zombieStrength));
                if (Distance <= DefaultValues.StabRange && Player.CharacterClass == "Reckless")
                {
                    combatMenu.footer.Add("You are close enough to charge in.");
                }
                Console.CursorVisible = false;
                combatMenu.goIntoMenu();
                Console.CursorVisible = false;
                Combat.combatMenu.header.Clear();
                Combat.combatMenu.footer.Clear();
                switch (combatMenu.finalSelection)
                {

                    case 0:
                        if (Distance <= DefaultValues.StabRange)
                        {
                            Action = "stab";
                            ActionType = "Stab";
                            CurrentRoundAttack = CurrentRoundAttack + 1 + DefaultValues.StabBonus + CurrentColdBloodedBonus + CurrentRecklessBonus + RunAndGun;
                            ColdBloodedCounter = 0;
                            RecklessCounter++;
                            RunAndGun = 0;
                            chosenCard = true;
                            break;
                        }
                        else
                        {
                            combatMenu.footer.Add("You are too far away to stab. Select a different action.");
                            goto choosingOption;
                        }
                    case 1:
                        Action = "wait";
                        ActionType = "Wait";
                        ColdBloodedCounter++;
                        RecklessCounter--;
                        RunAndGun = 0;
                        chosenCard = true;
                        break;
                    case 2:
                        if (CurrentRoundAttack + 1 + DefaultValues.ShootBonus + RunAndGun > Player.Ammo)
                        {
                            combatMenu.footer.Add("You don't have enough ammo for that. Select a different action.");
                            goto choosingOption;
                        }
                        else
                        {
                            Action = "shoot";
                            ActionType = "Shoot";
                            CurrentRoundAttack = CurrentRoundAttack + 1 + DefaultValues.ShootBonus + RunAndGun;
                            AmmoSpent = CurrentRoundAttack;
                            RecklessCounter--;
                            RunAndGun = 0;
                            Player.Ammo = Player.Ammo - AmmoSpent;
                            chosenCard = true;
                            break;
                        }

                    case 3:
                        PlayCard(handList[0]);
                        if (chosenCard == false)
                        {
                            goto choosingOption;
                        }
                        break;
                    case 4:
                        PlayCard(handList[1]);
                        if (chosenCard == false)
                        {
                            goto choosingOption;
                        }
                        break;
                    case 5:
                        PlayCard(handList[2]);
                        if (chosenCard == false)
                        {
                            goto choosingOption;
                        }
                        break;
                    case 6:
                        PlayCard(handList[3]);
                        if (chosenCard == false)
                        {
                            goto choosingOption;
                        }
                        break;
                    case 7:
                        PlayCard(handList[4]);
                        if (chosenCard == false)
                        {
                            goto choosingOption;
                        }
                        break;
                    case 8:
                        PlayCard(handList[5]);
                        if (chosenCard == false)
                        {
                            goto choosingOption;
                        }
                        break;
                    case 9:
                        PlayCard(handList[6]);
                        if (chosenCard == false)
                        {
                            goto choosingOption;
                        }
                        break;
                    case 10:
                        PlayCard(handList[7]);
                        if (chosenCard == false)
                        {
                            goto choosingOption;
                        }
                        break;
                    case 11:
                        PlayCard(handList[8]);
                        if (chosenCard == false)
                        {
                            goto choosingOption;
                        }
                        break;
                    case 12:
                        PlayCard(handList[9]);
                        if (chosenCard == false)
                        {
                            goto choosingOption;
                        }
                        break;
                    case 99:
                        goto choosingOption;
                }
                if (chosenCard == false)
                {
                    goto choosingOption;
                }
                //zombie behavior starts here
                ShuffleThings.Shuffle(zombieBehavior);
                switch (zombieBehavior[0])
                {
                    case "Passive":
                        if (zombieStrength < 2)
                        {
                            combatMenu.footer.Add("The zombie is moving toward you.");
                        }
                        else
                        {
                            combatMenu.footer.Add("The zombies are moving toward you.");
                        }
                        break;
                    case "Rush":
                        if (zombieStrength < 2)
                        {
                            combatMenu.footer.Add("The zombie is moving toward you quickly.");
                        }
                        else
                        {
                            combatMenu.footer.Add("The zombies are moving toward you quickly.");
                        }
                        if (Distance != 10)
                        {
                            Distance = Distance - 1;
                        }
                        
                        break;
                    case "Tough":
                        if (zombieStrength < 2)
                        {
                            combatMenu.footer.Add("The zombie is moving toward you with odd, jerky motions, making it harder to hit.");
                        }
                        else
                        {
                            combatMenu.footer.Add("The zombies are moving toward you, tightly packed together, making them harder to hit.");
                        }

                        if (ActionType == "Stab" || ActionType == "Shoot")
                        {
                            if (Distance > 3 && Player.CharacterClass == "Precise")
                            {
                                CurrentRoundAttack = CurrentRoundAttack + SniperBonus;
                            }
                            else
                            {
                                CurrentRoundAttack = CurrentRoundAttack - 1;
                            }
                        }
                        break;
                    case "Extra Tough":
                        if (zombieStrength < 2)
                        {
                            combatMenu.footer.Add("The zombie is making unpredictable movements, making it much harder to hit.");
                        }
                        else
                        {
                            combatMenu.footer.Add("The zombies are moving toward you from several directions, making them much harder to hit.");
                        }

                        if (ActionType == "Stab" || ActionType == "Shoot")
                        {
                            if (Distance > 3 && Player.CharacterClass == "Precise")
                            {
                                CurrentRoundAttack = CurrentRoundAttack + SniperBonus;
                            }
                            else
                            {
                                CurrentRoundAttack = CurrentRoundAttack - 2;
                            }

                        }
                        break;
                }
                //combat resolution starts here

                if (ActionType == "Shoot")
                {
                    if (AmmoSpent < 2)
                    {
                        combatMenu.footer.Add($"You {Action.ToLower()}, spending a bullet.");
                    }
                    else
                    {
                        if (RunAndGun > 0)
                        {
                            combatMenu.footer.Add($"You {Action.ToLower()} from a new angle, spending {AmmoSpent} bullets.");
                        }
                        else
                        {
                            combatMenu.footer.Add($"You {Action.ToLower()}, spending {AmmoSpent} bullets.");
                        }
                        
                    }

                    //WriteLine($"You {Action.ToLower()}, spending {AmmoSpent} bullets.");
                }
                else if (ActionType == "Wait")
                {

                    if (CurrentColdBloodedBonus > 0)
                    {
                        combatMenu.footer.Add($"You patiently wait for the perfect moment to strike.");
                    }
                    else
                    {
                        combatMenu.footer.Add($"You wait for a better moment to act.");
                    }
                }
                else if (ActionType == "Run" && CurrentColdBloodedBonus > 0)
                {
                    combatMenu.footer.Add($"You reposition yourself, looking for a better angle.");
                }
                else if (ActionType == "Stab" && CurrentColdBloodedBonus > 0)
                {
                    combatMenu.footer.Add($"Your patience has paid off. You've lured the zombies to a narrow opening, where you can dispatch many of them one at a time.");
                }
                else if (ActionType == "Stab" && CurrentRecklessBonus > 0)
                {
                    combatMenu.footer.Add($"You hack and slash with no regard for your safety.");
                }
                else if (ActionType == "Run" && Player.CharacterClass == "Tireless")
                {
                    combatMenu.footer.Add("You move to a better position. Your next attack will be stronger. ");
                } 
                else
                {
                    combatMenu.footer.Add($"You {Action.ToLower()}.");
                }

                if (ActionType == "Stab" || ActionType == "Shoot")
                {
                    //custom message for zombies dying - singular and plural - depending on number of Zombies
                    if (CurrentRoundAttack == 1 && zombieStrength > 1)
                    {
                        combatMenu.footer.Add("One zombie dies.");
                    }
                    else if (CurrentRoundAttack >= zombieStrength && zombieStrength > 1)
                    {
                        if (zombieStrength == ZombieStartingCount)
                        {
                            combatMenu.footer.Add($"All {zombieStrength} zombies die.");
                        }
                        else
                        {
                            combatMenu.footer.Add($"The remaining {zombieStrength} zombies die.");
                        }
                        //WriteLine($"The remaining {zombieStrength} zombies die.");
                    }
                    else if (CurrentRoundAttack >= zombieStrength && zombieStrength == 1)
                    {
                        if (zombieStrength == ZombieStartingCount)
                        {
                            combatMenu.footer.Add("The zombie dies.");
                        }
                        else
                        {
                            combatMenu.footer.Add($"The last zombie dies.");
                        }

                    }
                    else if (CurrentRoundAttack < 1)
                    {
                        combatMenu.footer.Add("No zombies die.");
                    }
                    else
                    {
                        combatMenu.footer.Add($"{CurrentRoundAttack} zombies die.");
                    }
                }
                //zombie losses
                if (ActionType == "Stab" || ActionType == "Shoot")
                {
                    if (CurrentRoundAttack > 0)
                    {
                        zombieStrength = zombieStrength - CurrentRoundAttack;
                    }
                }
                CombatVisual.ZombieVisualCount = zombieStrength;

                if (zombieStrength == 1)
                {
                    combatMenu.footer.Add("There is a single zombie remaining.");
                }
                else if (zombieStrength > 1)
                {
                    combatMenu.footer.Add($"There are {zombieStrength} zombies remaining.");
                }
                else
                {
                    combatMenu.footer.Add("No zombies remain.");
                }
                
                
                if(zombieStrength*5 <= ZombieStartingCount*4 + Player.Level && zombieStrength > 0 && Player.CharacterClass == "Swift")
                {
                    if (Distance != 10)
                    {
                        combatMenu.footer.Add("You've attracted the attention of the zombies. If you flee now, you will be able to draw some of them away.");
                    }
                    gotAttention = true;
                }
                
                //distance change and resulting message
                if (Distance != 10)
                {
                    Distance = Distance - ZombieMovement;
                }
                




                //clear the card chocie from the menu, so that another can be chosen
                //clear the menu options to repopulate with new ones
                //clear attack value
                //draw for turn
                chosenCard = false;
                for (int i = combatMenu.options.Count; i > 0; i--)
                {
                    combatMenu.removeOption(combatMenu.options[i - 1]);
                }

                CurrentRoundAttack = DefaultValues.defaultAttack;
                handList.Clear();
                Deck.DrawCard();
                ActionType = "Empty";
                AmmoSpent = 0;
                
                //outcome 0 : dead
                if (Distance < 1 && zombieStrength > 0)
                {
                    inCombat = false;
                    Player.Dead = true;

                    break;
                }
                
                //outcome 1 : won
                if (zombieStrength < 1)
                {
                    Clear();
                    combatMenu.footer.Add("Press any key to continue.");
                    for (int i = 0; i < combatMenu.footer.Count(); i++)
                    {
                        SetCursorPosition(5, combatMenu.menuStartingPosition + combatMenu.header.Count() + 3 + combatMenu.options.Count() + 3 + i);
                        Write(combatMenu.footer[i]);
                    }
                    ReadKey();
                    Player.XP = Player.XP + XPreward;
                    Clear();
                    WriteLine("You have won. The surrounding area seems to be clear.");
                    WriteLine($"You gained {XPreward} experience!");
                    WriteLine();
                    if (Player.XP >= Player.XPToNextLevel)
                    {
                        LevellingUp.LevelUp();

                    }
                    else
                    {
                        WriteLine("Press any key to continue exploring.");
                        ReadKey();
                    }
                    //clear zombies
                    if (Player.CharacterClass == "Silent")
                    {
                        currentMap.listOfPoints[currentMap.PlayerPosition].zombieCount = 0;
                        //currentMap.listOfPoints[currentMap.PlayerPosition].symbolOnMap = " ";
                    }
                    else if (Player.CharacterClass == "Deadly")
                    {
                        foreach (PointOnMap point in currentMap.listOfPoints)
                        {
                            if ((
                               point.XPosition == currentMap.listOfPoints[currentMap.PlayerPosition].XPosition - 1 ||
                               point.XPosition == currentMap.listOfPoints[currentMap.PlayerPosition].XPosition ||
                               point.XPosition == currentMap.listOfPoints[currentMap.PlayerPosition].XPosition + 1) &&
                               (point.YPosition == currentMap.listOfPoints[currentMap.PlayerPosition].YPosition - 1 ||
                               point.YPosition == currentMap.listOfPoints[currentMap.PlayerPosition].YPosition ||
                               point.YPosition == currentMap.listOfPoints[currentMap.PlayerPosition].YPosition + 1))
                            {
                                if (point.zombieCount != 0)
                                {
                                    point.zombieCount = 0;
                                    //point.symbolOnMap = " ";
                                }
                            }

                        }
                    }
                    else
                    {


                        foreach (PointOnMap point in currentMap.listOfPoints)
                        {
                            if ((point.XPosition == currentMap.listOfPoints[currentMap.PlayerPosition].XPosition - 2 ||
                               point.XPosition == currentMap.listOfPoints[currentMap.PlayerPosition].XPosition - 1 ||
                               point.XPosition == currentMap.listOfPoints[currentMap.PlayerPosition].XPosition ||
                               point.XPosition == currentMap.listOfPoints[currentMap.PlayerPosition].XPosition + 1 ||
                               point.XPosition == currentMap.listOfPoints[currentMap.PlayerPosition].XPosition + 2) &&
                               (point.YPosition == currentMap.listOfPoints[currentMap.PlayerPosition].YPosition - 2 ||
                               point.YPosition == currentMap.listOfPoints[currentMap.PlayerPosition].YPosition - 1 ||
                               point.YPosition == currentMap.listOfPoints[currentMap.PlayerPosition].YPosition ||
                               point.YPosition == currentMap.listOfPoints[currentMap.PlayerPosition].YPosition + 1 ||
                               point.YPosition == currentMap.listOfPoints[currentMap.PlayerPosition].YPosition + 2))
                            {
                                if (point.zombieCount != 0)
                                {
                                    point.zombieCount = 0;
                                    //point.symbolOnMap = " ";
                                }
                            }

                        }
                    }
                    inCombat = false;
                    //break;

                }
                
                //outcome 2 : ran away
                if (Distance > 9)
                {
                    Clear();
                    combatMenu.footer.Add("Press any key to continue.");
                    for (int i = 0; i < combatMenu.footer.Count(); i++)
                    {
                        SetCursorPosition(5, combatMenu.menuStartingPosition + combatMenu.header.Count() + 3 + combatMenu.options.Count() + 3 + i);
                        Write(combatMenu.footer[i]);
                    }
                    ReadKey();
                    Clear();
                    if (Player.CharacterClass == "Swift" && gotAttention == true)
                    {
                        WriteLine("You run away, drawing some of the zombies around with you. You then circle back and lose them.");
                        WriteLine("The area seems safer.");
                    }
                    else
                    {
                        WriteLine("You flee. You can hear growling in the distance, but no zombies seem to be following you.");
                    }
                    
                    if (Player.CharacterClass != "Silent" && Player.CharacterClass != "Deadly" && Player.CharacterClass != "Swift")
                    {
                        WriteLine("The commotion has probably attracted more of them.");
                        WriteLine("You are not safe here.");
                    }
                    
                    if (gotAttention == false)
                    {
                        WriteLine($"You seem to have dropped some of your supplies while running.");
                        if(Player.Food < 10)
                        {
                            FoodLost = 2;
                        }
                        else
                        {
                            FoodLost = Player.Food / 3;
                        }

                        if (Player.Ammo < 20)
                        {
                            AmmoLost = 5;
                        }
                        else
                        {
                            AmmoLost = Player.Food /3;
                        }
                        WriteLine($"You are missing {FoodLost} food and {AmmoLost} ammo.");
                        Player.Food = Player.Food - FoodLost;
                        Player.Ammo = Player.Ammo - AmmoLost;
                        FoodLost = 0;
                        AmmoLost = 0;
                    }
                    else
                    {
                        WriteLine($"You seem to have dropped some of your supplies while running.");
                        if (Player.Ammo <= 3)
                        {
                            WriteLine($"You are missing 1 food and all of your ammo.");
                            
                            Player.Food = Player.Food -1;
                            if (Player.Food < 0)
                            {
                                Player.Food = 0;
                            }
                            Player.Ammo = 0;
                        }
                        else
                        {
                            WriteLine($"You are missing 1 food and 3 ammo.");
                            Player.Food = Player.Food -1;
                            if (Player.Food < 0)
                            {
                                Player.Food = 0;
                            }
                            Player.Ammo = Player.Ammo - 3;
                        }
                        
                    }
                    
                    
                    if (Player.CharacterClass == "Swift")
                    {
                        if (gotAttention == true)
                        {
                            //fix Fast running
                            foreach (PointOnMap point in currentMap.listOfPoints)
                        {
                            if ((point.XPosition == currentMap.listOfPoints[currentMap.PlayerPosition].XPosition - 2 ||
                               point.XPosition == currentMap.listOfPoints[currentMap.PlayerPosition].XPosition - 1 ||
                               point.XPosition == currentMap.listOfPoints[currentMap.PlayerPosition].XPosition ||
                               point.XPosition == currentMap.listOfPoints[currentMap.PlayerPosition].XPosition + 1 ||
                               point.XPosition == currentMap.listOfPoints[currentMap.PlayerPosition].XPosition + 2) &&
                               (point.YPosition == currentMap.listOfPoints[currentMap.PlayerPosition].YPosition - 2 ||
                               point.YPosition == currentMap.listOfPoints[currentMap.PlayerPosition].YPosition - 1 ||
                               point.YPosition == currentMap.listOfPoints[currentMap.PlayerPosition].YPosition ||
                               point.YPosition == currentMap.listOfPoints[currentMap.PlayerPosition].YPosition + 1 ||
                               point.YPosition == currentMap.listOfPoints[currentMap.PlayerPosition].YPosition + 2))
                            {
                                if (point.zombieCount != 0)
                                {
                                    FastXP = FastXP + (point.zombieCount - (point.zombieCount / (1 + Player.Level))) / 2;
                                    point.zombieCount = point.zombieCount / (1 + Player.Level);

                                }
                            }

                        }
                        WriteLine($"You have gained {FastXP} XP!");
                        Player.XP = Player.XP + FastXP;
                        FastXP = 0;
                        if (Player.XP >= Player.XPToNextLevel)
                    {
                        LevellingUp.LevelUp();
                    }
                            //fix Fast running
                        }
                    }
                    else if (Player.CharacterClass != "Silent" && Player.CharacterClass != "Deadly")
                    {


                        foreach (PointOnMap point in currentMap.listOfPoints)
                        {
                            if ((point.XPosition == currentMap.listOfPoints[currentMap.PlayerPosition].XPosition - 2 ||
                               point.XPosition == currentMap.listOfPoints[currentMap.PlayerPosition].XPosition - 1 ||
                               point.XPosition == currentMap.listOfPoints[currentMap.PlayerPosition].XPosition ||
                               point.XPosition == currentMap.listOfPoints[currentMap.PlayerPosition].XPosition + 1 ||
                               point.XPosition == currentMap.listOfPoints[currentMap.PlayerPosition].XPosition + 2) &&
                               (point.YPosition == currentMap.listOfPoints[currentMap.PlayerPosition].YPosition - 2 ||
                               point.YPosition == currentMap.listOfPoints[currentMap.PlayerPosition].YPosition - 1 ||
                               point.YPosition == currentMap.listOfPoints[currentMap.PlayerPosition].YPosition ||
                               point.YPosition == currentMap.listOfPoints[currentMap.PlayerPosition].YPosition + 1 ||
                               point.YPosition == currentMap.listOfPoints[currentMap.PlayerPosition].YPosition + 2))
                            {
                                if (point.zombieCount != 0)
                                {
                                    point.zombieCount = point.zombieCount + 1;
                                    //point.symbolOnMap = Convert.ToString(point.zombieCount);
                                    //spawn more zombies after you run
                                }
                            }

                        }
                    }
                    WriteLine();
                    WriteLine("Press any key to continue exploring.");
                    ReadKey();

                    ranAway = true;
                    inCombat = false;

                }
                


            }
            foreach (Card card in Deck.cardList)
            {
                card.InDiscard = false;
                card.InHand = false;

            }

        }
        public static string DisplayDistance(int zombieStrength)
        {
            if (zombieStrength > 1)
            {
                switch (Distance)
                {
                    case -2:
                    case -1:
                    case 0:
                        return ("The zombies are upon you.");

                    case 1:
                        return ("The zombies are VERY close.");

                    case 2:
                    case 3:
                        return ("The zombies are close.");

                    case 4:
                    case 5:
                    case 6:
                        return ("The zombies are at a moderate distance.");

                    default:
                        return ("The zombies are far away.");

                }

            }
            if (zombieStrength == 1)
            {
                switch (Distance)
                {
                    case -2:
                    case -1:
                    case 0:
                        return ("The zombie is upon you.");

                    case 1:
                        return ("The zombie is VERY close.");

                    case 2:
                    case 3:
                        return ("The zombie is close.");

                    case 4:
                    case 5:
                    case 6:
                        return ("The zombie is at a moderate distance.");

                    default:
                        return ("The zombie is far away.");

                }
            }
            else
            {
                return "";
            }
        }

    }
}


