using static System.Console;
using System.Collections.Generic;
namespace DeckLib
{
    public class Map
    {
        public Map(int ammoRichness, int foodRichness, int size, int houseDensity, int treeDensity, int zombieDensity)
        {
            AmmoRichness = ammoRichness;
            FoodRichness = foodRichness;
            Size = size;
            HouseDensity = houseDensity;
            TreeDensity = treeDensity;
            ZombieDensity = zombieDensity;
        }

        public List<PointOnMap> listOfPoints = new List<PointOnMap>();
        public PointOnMap nextPoint;
        public string MapMessage;
        public int AmmoRichness;
        public int FoodRichness;
        public int HouseDensity;


        public int TreeDensity;
        public int Size;
        public int PlayerPosition;
        public int ZombieDensity;
        public int Exit;
        private List<int> zombieCountList = new List<int>();
        private List<int> zombieGenerator = new List<int>();

        //methods

        public static void SetColors(PointOnMap point)
        {
            switch (point.Type)
            {
                case "Empty":
                case "Zombie":
                    switch (Travel.CurrentLocation.Type)
                    {
                        case "City":
                            ForegroundColor = ConsoleColor.Gray;
                            break;
                        case "Town":
                            ForegroundColor = ConsoleColor.Gray;
                            break;
                        case "Small Town":
                            ForegroundColor = ConsoleColor.DarkGreen;
                            break;
                    }
                    break;

                case "Tree":
                    switch (Travel.CurrentLocation.Type)
                    {
                        case "City":
                            ForegroundColor = ConsoleColor.Green;
                            break;
                        case "Town":
                            ForegroundColor = ConsoleColor.Green;
                            break;
                        case "Small Town":
                            ForegroundColor = ConsoleColor.DarkGreen;
                            break;
                    }

                    break;
                case "House":
                    switch (Travel.CurrentLocation.Type)
                    {
                        case "City":
                            ForegroundColor = ConsoleColor.White;
                            break;
                        case "Town":
                            ForegroundColor = ConsoleColor.DarkRed;
                            break;
                        case "Small Town":
                            ForegroundColor = ConsoleColor.DarkRed;
                            break;
                    }
                    break;
            }
        }
        public void GenerateMap()
        {
            listOfPoints.Clear();

            //generate trees
            for (int i = 0; i < 10; i++)
            {
                if (i < TreeDensity)
                {
                    PointGenerator.typeList.Add("Tree");
                }
                else
                {
                    PointGenerator.typeList.Add("Empty");
                }
            }
            //generate houses
            for (int i = 0; i < 10; i++)
            {
                if (i < HouseDensity)
                {
                    PointGenerator.typeList.Add("House");
                }
                else
                {
                    PointGenerator.typeList.Add("Empty");
                }
            }

            //generate zombies
            for (int i = 0; i < ZombieDensity; i++)
            {
                zombieGenerator.Add(1 + (i / 2));
            }
            for (int i = 0; i < 10; i++)
            {
                if (i < ZombieDensity)
                {
                    zombieGenerator.Shuffle();
                    zombieCountList.Add(zombieGenerator[0]);
                    zombieGenerator.Shuffle();
                    zombieCountList.Add(zombieGenerator[0]);
                    zombieGenerator.Shuffle();
                    zombieCountList.Add(zombieGenerator[0]);
                }
                else
                {
                    zombieCountList.Add(0);
                    zombieCountList.Add(0);
                    zombieCountList.Add(0);
                }
            }
            zombieGenerator.Clear();

            for (int x = 0; x < Size; x++)
            {
                for (int y = 0; y < Size; y++)
                {
                    ShuffleThings.Shuffle(PointGenerator.typeList);
                    nextPoint = new PointOnMap(PointGenerator.typeList[0], x, y);
                    if (nextPoint.Type == "Tree")
                    {
                        if ((y == Size - 1) || (x == Size - 1))
                        {
                            nextPoint.Type = "Empty";
                            nextPoint.symbolOnMap = " ";
                        }
                        else
                        {
                            nextPoint.symbolOnMap = "Ÿ";
                            nextPoint.passable = false;
                        }

                    }
                    else if (nextPoint.Type == "House")
                    {
                        nextPoint.symbolOnMap = "■";
                        nextPoint.searchable = true;
                        zombieCountList.Shuffle();
                        nextPoint.zombieCount = zombieCountList[0];
                    }
                    else
                    {
                        zombieCountList.Shuffle();
                        nextPoint.zombieCount = zombieCountList[0];
                        //REMOVE NEXT LINE TO HIDE ZOMBIES 
                        if (nextPoint.zombieCount != 0)
                        {
                            //nextPoint.symbolOnMap = $"{nextPoint.zombieCount}";
                            nextPoint.Type = "Zombie";
                        }
                        else
                        {
                            nextPoint.symbolOnMap = " ";
                        }


                    }
                    listOfPoints.Add(nextPoint);
                }
            }

            PointGenerator.typeList.Clear();
            zombieCountList.Clear();

            PlayerPosition = listOfPoints.IndexOf(listOfPoints[(Size * Size) - Size]);
            listOfPoints[PlayerPosition].zombieCount = 0;
            Exit = listOfPoints.IndexOf(listOfPoints[Size - 1]);
            listOfPoints[Exit].passable = true;

        }

        public void NewDrawMap()
        {

            Clear();
            int pointCounter = 0;
            BackgroundColor = ConsoleColor.DarkGray;
            ForegroundColor = ConsoleColor.DarkGray;
            for (int i = -1; i < (3 * Size) + 5; i = i + 3)
            {

                SetCursorPosition(4 + i, 3);
                Write("\b");
                Write("XXX");
            }
            for (int i = 0; i < Size; i++)
            {
                SetCursorPosition(3, 4 + i);
                Write("\b");
                Write("XX");
                SetCursorPosition(7 + 3 * Size, 4 + i);
                Write("\b");
                Write("XX");
            }
            for (int i = -1; i < 3 * Size + 5; i = i + 3)
            {
                SetCursorPosition(4 + i, 4 + Size);
                Write("\b");
                Write("XXX");
            }
            BackgroundColor = ConsoleColor.Black;
            ForegroundColor = ConsoleColor.Gray;
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    SetCursorPosition(7 + 3 * j, 4 + i);
                    Write("\b");
                    SetColors(listOfPoints[pointCounter]);
                    Write(listOfPoints[pointCounter].symbolOnMap);
                    ForegroundColor = ConsoleColor.Gray;
                    if (PlayerPosition == pointCounter)
                    {
                        Write("\b");
                        Write("O");
                    }
                    if (Exit == pointCounter)
                    {
                        Console.Write("\b");
                        Write(">");
                    }
                    pointCounter++;
                }
            }
            SetCursorPosition(12 + 3 * Size, 2);
            WriteLine("Food:  ");
            SetCursorPosition(12 + 3 * Size, 4);
            WriteLine("Ammo:  ");
            SetCursorPosition(12 + 3 * Size, 6);
            WriteLine($"O : {Player.Name}");
            SetCursorPosition(12 + 3 * Size, 7);
            switch (Travel.CurrentLocation.Type)
            {
                case "City":
                    ForegroundColor = ConsoleColor.White;
                    break;
                case "Town":
                    ForegroundColor = ConsoleColor.DarkRed;
                    break;
                case "Small Town":
                    ForegroundColor = ConsoleColor.DarkRed;
                    break;
            }
            Write("■");
            ForegroundColor = ConsoleColor.Gray;
            WriteLine(" : Building ");
            SetCursorPosition(12 + 3 * Size, 8);
            switch (Travel.CurrentLocation.Type)
            {
                case "City":
                    ForegroundColor = ConsoleColor.Green;
                    break;
                case "Town":
                    ForegroundColor = ConsoleColor.Green;
                    break;
                case "Small Town":
                    ForegroundColor = ConsoleColor.DarkGreen;
                    break;
            }
            Write("Ÿ");
            ForegroundColor = ConsoleColor.Gray;
            WriteLine(" : Tree  ");
            SetCursorPosition(12 + 3 * Size, 9);
            WriteLine("> : Exit  ");
            SetCursorPosition(12 + 3 * Size, 11);
            WriteLine("Press \"backspace\" to enter menu.");



        }


    }
    public class PointOnMap
    {
        public int XPosition;
        public int YPosition;
        public bool passable = true;
        public bool occupied = false;
        public int zombieCount = 0;
        public bool searchable = false;
        public int topLeft;
        public int topRight;
        public int bottomLeft;
        public int bottomRight;
        public int topMiddle;
        public int bottomMiddle;
        public int middleLeft;
        public int middleRight;
        public bool Scaled = false;

        public bool wall = false;
        public string Type;
        public string symbolOnMap = " ";

        //constructors
        public PointOnMap(string type, int x, int y)
        {
            XPosition = x;
            YPosition = y;
            Type = type;

        }
    }
    public class PointGenerator
    {
        public static List<string> typeList = new List<string> { };
    }
}