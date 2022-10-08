using static System.Console;
namespace DeckLib
{
    public static class Movement
    {
        public static bool Exploring = false;

        public static Map currentMap;

        public static void ExploreMap()
        {
        ExploreMap:
            Clear();
            Exploring = true;
            currentMap.NewDrawMap();
            while (Exploring == true)
            {
                // Exploring - Put a goto Exploring; at case 99 in movement switch to remove Esc as a map redrawing tool
                Console.CursorVisible = false;
                if (currentMap.PlayerPosition == currentMap.Exit)
                {
                    Exploring = false;
                    TravelMenu.EnterTravelMenu();
                    break;
                }
                foreach (PointOnMap point in currentMap.listOfPoints)
                {
                    if ((point.XPosition == currentMap.listOfPoints[currentMap.PlayerPosition].XPosition - 1 ||
                           point.XPosition == currentMap.listOfPoints[currentMap.PlayerPosition].XPosition ||
                           point.XPosition == currentMap.listOfPoints[currentMap.PlayerPosition].XPosition + 1) &&
                          (point.YPosition == currentMap.listOfPoints[currentMap.PlayerPosition].YPosition - 1 ||
                           point.YPosition == currentMap.listOfPoints[currentMap.PlayerPosition].YPosition ||
                           point.YPosition == currentMap.listOfPoints[currentMap.PlayerPosition].YPosition + 1))
                    {
                        if (point.zombieCount != 0)
                        {
                            Player.ZombieSense = true;
                        }
                    }
                }

                //Zombie Sense can be turned on from here
                if (Player.ZombieSense == true)
                {
                    //currentMap.MapMessage = (currentMap.MapMessage+"You sense danger nearby."+"\n");
                    Player.ZombieSense = false;
                }
                if (currentMap.listOfPoints[currentMap.PlayerPosition].zombieCount != 0 && Combat.ranAway == false)
                {
                    //INITIATE COMBAT
                    Combat.inCombat = true;
                    Combat.StartCombat(currentMap);
                    if (Player.Dead == true)
                    {
                        Exploring = false;
                        break;
                    }
                    Combat.combatMenu.footer.Clear();
                    Clear();
                    goto ExploreMap;

                }
                if (currentMap.listOfPoints[currentMap.PlayerPosition].searchable == true && Combat.ranAway == false)
                {
                    if (Player.Dead == false)
                    {
                        WriteLine();
                        currentMap.listOfPoints[currentMap.PlayerPosition].searchable = false;
                        currentMap.listOfPoints[currentMap.PlayerPosition].symbolOnMap = "x";
                        Loot.GenerateLoot(currentMap.AmmoRichness, currentMap.FoodRichness);
                        currentMap.MapMessage = currentMap.MapMessage + Loot.message;
                        Loot.message = "";
                    }
                }
                for (int i = 0; i < 10; i++)
                {
                    SetCursorPosition(30, 5 + currentMap.Size + i);
                    Write(new string(' ', WindowWidth));
                }
                //old message
                SetCursorPosition(30, 5 + currentMap.Size);
                Write(currentMap.MapMessage);
                currentMap.MapMessage = "";

                //food and ammo updates
                SetCursorPosition(22 + 3 * currentMap.Size, 2);
                Write($"\b\b\b{Player.Food} ");
                SetCursorPosition(22 + 3 * currentMap.Size, 4);
                Write($"\b\b\b{Player.Ammo} ");
                //SetCursorPosition(22 + 3 * currentMap.Size, 6);
                //Write($"\b\b\bZ:{currentMap.ZombieDensity} ");
                //SetCursorPosition(22 + 3 * currentMap.Size, 7);
                //Write($"\b\b\bF:{currentMap.FoodRichness} ");
                //SetCursorPosition(22 + 3 * currentMap.Size, 8);
                //Write($"\b\b\bA:{currentMap.AmmoRichness} ");

                Combat.ranAway = false;
                var input = ReadKey();
                switch (input.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (currentMap.PlayerPosition > currentMap.Size - 1)
                        {
                            if (currentMap.listOfPoints[currentMap.PlayerPosition - currentMap.Size].passable == true)
                            {

                                SetCursorPosition(7 + 3 * (currentMap.PlayerPosition % currentMap.Size), 4 + (currentMap.PlayerPosition / currentMap.Size));
                                Write("\b");
                                Map.SetColors(currentMap.listOfPoints[currentMap.PlayerPosition]);
                                Write(currentMap.listOfPoints[currentMap.PlayerPosition].symbolOnMap);
                                ForegroundColor = ConsoleColor.Gray;
                                currentMap.PlayerPosition = currentMap.PlayerPosition - currentMap.Size;
                                SetCursorPosition(7 + 3 * (currentMap.PlayerPosition % currentMap.Size), 4 + (currentMap.PlayerPosition / currentMap.Size));
                                Write("\b");
                                Write("O");
                                Player.FoodCounter++;
                                if (Player.FoodCounter > 5)
                                {
                                    Player.Food--;
                                    Player.FoodCounter = 0;
                                    currentMap.MapMessage = (currentMap.MapMessage + "\n" + "You're getting hungry. You eat 1 food." + "\n");
                                }
                            }

                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (currentMap.PlayerPosition < (currentMap.listOfPoints.Count - currentMap.Size))
                        {
                            if (currentMap.listOfPoints[currentMap.PlayerPosition + currentMap.Size].passable == true)
                            {
                                SetCursorPosition(7 + 3 * (currentMap.PlayerPosition % currentMap.Size), 4 + (currentMap.PlayerPosition / currentMap.Size));
                                Write("\b");
                                Map.SetColors(currentMap.listOfPoints[currentMap.PlayerPosition]);
                                Write(currentMap.listOfPoints[currentMap.PlayerPosition].symbolOnMap);
                                ForegroundColor = ConsoleColor.Gray;
                                currentMap.PlayerPosition = currentMap.PlayerPosition + currentMap.Size;
                                SetCursorPosition(7 + 3 * (currentMap.PlayerPosition % currentMap.Size), 4 + (currentMap.PlayerPosition / currentMap.Size));
                                Write("\b");
                                Write("O");
                                Player.FoodCounter++;
                                if (Player.FoodCounter > 5)
                                {
                                    Player.Food--;
                                    Player.FoodCounter = 0;
                                    currentMap.MapMessage = (currentMap.MapMessage + "\n" + "You're getting hungry. You eat 1 food." + "\n");
                                }
                            }

                        }
                        break;
                    case ConsoleKey.LeftArrow:
                        if ((currentMap.PlayerPosition) % currentMap.Size != 0)
                        {
                            if (currentMap.listOfPoints[currentMap.PlayerPosition - 1].passable == true)
                            {
                                SetCursorPosition(7 + 3 * (currentMap.PlayerPosition % currentMap.Size), 4 + (currentMap.PlayerPosition / currentMap.Size));
                                Write("\b");
                                Map.SetColors(currentMap.listOfPoints[currentMap.PlayerPosition]);
                                Write(currentMap.listOfPoints[currentMap.PlayerPosition].symbolOnMap);
                                ForegroundColor = ConsoleColor.Gray;
                                currentMap.PlayerPosition = currentMap.PlayerPosition - 1;
                                SetCursorPosition(7 + 3 * (currentMap.PlayerPosition % currentMap.Size), 4 + (currentMap.PlayerPosition / currentMap.Size));
                                Write("\b");
                                Write("O");
                                Player.FoodCounter++;
                                if (Player.FoodCounter > 5)
                                {
                                    Player.Food--;
                                    Player.FoodCounter = 0;
                                    currentMap.MapMessage = (currentMap.MapMessage + "\n" + "You're getting hungry. You eat 1 food." + "\n");
                                }
                            }

                        }
                        break;
                    case ConsoleKey.RightArrow:
                        if ((currentMap.PlayerPosition + 1) % currentMap.Size != 0)
                        {
                            if (currentMap.listOfPoints[currentMap.PlayerPosition + 1].passable == true)
                            {
                                SetCursorPosition(7 + 3 * (currentMap.PlayerPosition % currentMap.Size), 4 + (currentMap.PlayerPosition / currentMap.Size));
                                Write("\b");
                                Map.SetColors(currentMap.listOfPoints[currentMap.PlayerPosition]);
                                Write(currentMap.listOfPoints[currentMap.PlayerPosition].symbolOnMap);
                                ForegroundColor = ConsoleColor.Gray;
                                currentMap.PlayerPosition = currentMap.PlayerPosition + 1;
                                SetCursorPosition(7 + 3 * (currentMap.PlayerPosition % currentMap.Size), 4 + (currentMap.PlayerPosition / currentMap.Size));
                                Write("\b");
                                Write("O");
                                Player.FoodCounter++;
                                if (Player.FoodCounter > 10)
                                {
                                    Player.Food--;
                                    Player.FoodCounter = 0;
                                    currentMap.MapMessage = (currentMap.MapMessage + "\n" + "You're getting hungry. You eat 1 food." + "\n");
                                }
                            }

                        }
                        break;
                    case ConsoleKey.Backspace:
                        ClassProcedures.CharacterScreen();
                        break;
                }


                if (Player.Food < 1 && Player.FoodCounter > 4)
                {
                    Clear();
                    WriteLine("Starving and tired, you are no longer able to move fast enough to outpace the horrible creatures.");
                    WriteLine("As you're walking, you hear a growl right behind you. You turn around, but in this exhausted state, you are way too slow.");
                    WriteLine("Press any key to continue.");
                    ReadKey();
                    Exploring = false;
                    Player.Dead = true;
                }

            }

        }
    }
}