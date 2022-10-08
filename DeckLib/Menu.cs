using static System.Console;
using System.Collections;
using System.Collections.Generic;
namespace DeckLib
{
    public class Menu
    {
        public List<string> header = new List<string>();
        public List<string> footer = new List<string>();
        
        public List<string> options = new List<string>(); 
        public int menuStartingPosition = 0;
        public int currentSelection = 0;
        public int finalSelection;
        public static bool inMenu = true;
        public static bool Travelling = false;
        

        //constructors
        //methods
        public void addOption(string newOption)
        {
            options.Add(newOption);
        }
        public void removeOption(string oldOption)
        {
            options.Remove(oldOption);
        }
        public void viewMenu()
        {
           if (Menu.Travelling == true)
           {
               menuStartingPosition = 32;
               WorldMap.ViewWorldMap();
           }
           else if (Combat.inCombat == true && Combat.levellingUp == false)
           {
               menuStartingPosition = 8;
               CombatVisual.DrawBattleField();
               if (Combat.Distance <= 10)
               {
                   CombatVisual.UpdateBattleField(Combat.Distance);  
               }
               else
               {
                   CombatVisual.UpdateBattleField(10);
               }
                            
           }
           else
           {
                menuStartingPosition = 0;
           } 
           for(int i = 0; i < header.Count(); i ++)
            {
                SetCursorPosition(5,menuStartingPosition + 3+i);
                Write(header[i]);
            }
        
            for (int i = 0; i < options.Count; i++)
            {
               SetCursorPosition(8,menuStartingPosition + header.Count() + 3 + i);
               Write($"{options[i]}");
            }
            for(int i = 0; i < footer.Count(); i ++)
            {
                SetCursorPosition(5,menuStartingPosition + header.Count() + 3 + options.Count() + 3 + i);
                Write(footer[i]);
            }
            markCurrentSelection();
            SetCursorPosition(5, header.Count()+3+options.Count()+3+footer.Count()+1);
            
        }
        public void markCurrentSelection()
        {
            SetCursorPosition(6,menuStartingPosition + header.Count() + 3 + currentSelection);
            Write(">");
        }
        public void unMarkPrevioustSelection()
        {
            SetCursorPosition(6,menuStartingPosition + header.Count() + 3 + currentSelection);
            Write(" ");
        }
        public void moveDown()
        {
            if (currentSelection < options.Count-1)
            {
                unMarkPrevioustSelection();
                currentSelection = currentSelection + 1;
                markCurrentSelection();
                Console.CursorVisible = false;

            }
        }
        public void moveUp()
        {
            if (currentSelection > 0)
            {
                unMarkPrevioustSelection();
                currentSelection = currentSelection - 1;
                markCurrentSelection();
                Console.CursorVisible = false;
            }
        }
        public void handleInput()
        {
            while (inMenu == true)
            {
                Console.CursorVisible = false;
                enterAgain:
                var input = ReadKey();
                switch (input.Key)
                {
                    case ConsoleKey.DownArrow:
                        moveDown();
                        Console.CursorVisible = false;
                        break;
                    case ConsoleKey.UpArrow:
                        moveUp();
                        Console.CursorVisible = false;
                        break;
                    case ConsoleKey.Enter:
                        finalSelection = currentSelection;
                        currentSelection = 0;
                        inMenu = false;
                        break;
                    case ConsoleKey.Backspace:
                        if(Combat.inCombat == false && PickACard.Selecting == false)
                        {
                        inMenu = false;
                        finalSelection = 99;
                        break;
                        }
                        else
                        {
                            goto enterAgain;
                        }
                        
                    default:
                    goto enterAgain;          
                }
            }
        }
        public void goIntoMenu()
        {
            
            Clear();
            Console.CursorVisible = false;
            inMenu = true;
            if (options.Count == 0)
            {
                inMenu = false;
            }
            else
            {
                viewMenu();
                handleInput();
            }
        }
    }
}