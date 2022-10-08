using static System.Console;
namespace DeckLib
{
    public class CombatVisual
    {
        public static int ZombieVisualCount = Combat.ZombieStrength;
        public static List<string> zombieList = new List<string>();
        public static void DrawBattleField()
        {
            SetCursorPosition(0, 0);
            Write(new string('-', WindowWidth));
            SetCursorPosition(0, 1);
            Write(new string(' ', 1));
            SetCursorPosition(0, 2);
            Write(new string(' ', 1));
            SetCursorPosition(0, 3);
            Write(new string('O', 1));
            SetCursorPosition(0, 4);
            Write(new string(' ', 1));
            SetCursorPosition(0, 5);
            Write(new string(' ', 1));
            SetCursorPosition(0, 6);
            Write(new string(' ', 1));
            SetCursorPosition(0, 7);
            Write(new string('-', WindowWidth));
        }
        public static void UpdateBattleField(int ZombieVisualDistance)
        {
            ZombieVisualCount = Combat.ZombieStrength;
            if (ZombieVisualDistance < 10)
            {


                zombieList.Clear();

                for (int i = 0; i < (10 - ZombieVisualDistance) * 5; i++)
                {
                    if (i < ZombieVisualCount)
                    {
                        zombieList.Add("Z");
                    }
                    else
                    {
                        zombieList.Add(" ");
                    }
                }


                zombieList.Shuffle();
                if (zombieList[0] != "Z" && zombieList[1] != "Z" && zombieList[2] != "Z" && zombieList[3] != "Z" && zombieList[4] != "Z")
                {
                    zombieList[2] = "Z";
                    for (int i = 3; i < zombieList.Count(); i++)
                    {
                        if (zombieList[i] == "Z")
                        {
                            zombieList[i] = " ";
                            break;
                        }
                    }
                }


                SetCursorPosition(3, 1);
                Write(new string(' ', WindowWidth));
                SetCursorPosition(3 + 3 * ZombieVisualDistance, 1);
                for (int i = 0; i < (10 - ZombieVisualDistance); i++)
                {
                    ForegroundColor = ConsoleColor.DarkRed;
                    Write(" " + zombieList[5 * i] + " ");
                    ForegroundColor = ConsoleColor.Gray;
                }
                SetCursorPosition(3, 2);
                Write(new string(' ', WindowWidth));
                SetCursorPosition(3 + 3 * ZombieVisualDistance, 2);
                for (int i = 0; i < (10 - ZombieVisualDistance); i++)
                {
                    ForegroundColor = ConsoleColor.DarkRed;
                    Write(" " + zombieList[5 * i + 1] + " ");
                    ForegroundColor = ConsoleColor.Gray;
                }
                SetCursorPosition(3, 3);
                Write(new string(' ', WindowWidth));
                SetCursorPosition(2, 3);
                Write("O");
                SetCursorPosition(3 + 3 * ZombieVisualDistance, 3);
                for (int i = 0; i < (10 - ZombieVisualDistance); i++)
                {
                    ForegroundColor = ConsoleColor.DarkRed;
                    Write(" " + zombieList[5 * i + 2] + " ");
                    ForegroundColor = ConsoleColor.Gray;
                }
                SetCursorPosition(3, 4);
                Write(new string(' ', WindowWidth));
                SetCursorPosition(3 + 3 * ZombieVisualDistance, 4);
                for (int i = 0; i < (10 - ZombieVisualDistance); i++)
                {
                    ForegroundColor = ConsoleColor.DarkRed;
                    Write(" " + zombieList[5 * i + 3] + " ");
                    ForegroundColor = ConsoleColor.Gray;
                }
                SetCursorPosition(3, 5);
                Write(new string(' ', WindowWidth));
                SetCursorPosition(3 + 3 * ZombieVisualDistance, 5);
                for (int i = 0; i < (10 - ZombieVisualDistance); i++)
                {
                    ForegroundColor = ConsoleColor.DarkRed;
                    Write(" " + zombieList[5 * i + 4] + " ");
                    ForegroundColor = ConsoleColor.Gray;
                    
                }
            }
        }
    }
}