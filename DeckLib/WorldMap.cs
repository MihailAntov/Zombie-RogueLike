using static System.Console;
namespace DeckLib
{
    public static class WorldMap
    {
        public static List<PointOnWorldMap> ListOfWorldPoints = new List<PointOnWorldMap>();
        public static List<int> ListOfXValues = new List<int>();
        public static List<int> ListOfYValues = new List<int>();
        public static List<string> ListOfTypes = new List<string> { "City", "City", "City", "Town", "Town", "Town", "Town", "Small Town", "Small Town", "Small Town", "Small Town", "Small Town" };

        public static List<string> ListOfCityNames = new List<string> { "Rome", "Paris", "London", "Stormwind", "Sofia", "Boston", "New York", "Chicago", "Tokyo", "Shanghai", "Seattle", "Moscow", "Vienna" };
        public static List<string> ListOfTownNames = new List<string> { "Shattrath", "Ruse", "Burgas", "Varna", "Ohrid", "Krakow", "Split", "Belfast", "Canterbury", "Minsk", "Pripyat", "Pliska", "Petrich" };
        public static List<string> ListOfSmallTownNames = new List<string> { "Godech", "Dirtville", "Honeywood", "Buckland", "Dover", "Deal", "Svoge", "Lakeshire", "Goldshire", "Krapec", "Brusnik", "Peturch", "Razlog" };
        public static List<int> CityAmmoRichness = new List<int> { 4,5,6,7 };
        public static List<int> TownAmmoRichness = new List<int> { 3,4,5,6 };
        public static List<int> SmallTownAmmoRichness = new List<int> { 1,2,3 };
        public static List<int> CityFoodRichness = new List<int> { 2, 3, 4, 5, 6 };
        public static List<int> TownFoodRichness = new List<int> { 3,4,5 };
        public static List<int> SmallTownFoodRichness = new List<int> { 1,2};
        public static List<int> CityTreeDensity = new List<int> { 0, 1, 2 };
        public static List<int> TownTreeDensity = new List<int> {2,3,4  };
        public static List<int> SmallTownTreeDensity = new List<int> {2, 3, 4, 5 };
        public static List<int> CityHouseDensity = new List<int> { 3,4,5};
        public static List<int> TownHouseDensity = new List<int> { 2,3,4 };
        public static List<int> SmallTownHouseDensity = new List<int> { 1, 2 };
        public static List<int> CityZombieDensity = new List<int> { 4,5,6,7 };
        public static List<int> TownZombieDensity = new List<int> {3,4,5};
        public static List<int> SmallTownZombieDensity = new List<int> { 2, 3 };
        public static List<int> CitySize = new List<int> { 19, 20, 21, 22, 23, 24 };
        public static List<int> TownSize = new List<int> { 15, 16, 17, 18, 19, 20 };
        public static List<int> SmallTownSize = new List<int> {11, 12, 13, 14, 15, 16 };
        public static int CurrentXValue = 0;
        public static int CurrentYValue = 0;
        public static int ColumnCounter = 0;
        public static int SpaceCounter = 0;
        public static int LocationCounter = 0;
        public static PointOnWorldMap NextPoint;
        public static void GenerateWorldMap()
        {
            for (int i = 0; i < ListOfCityNames.Count; i++)
            {
            checkAgain:
                if (ListOfCityNames[i].Length < 10)
                {
                    ListOfCityNames[i] = ListOfCityNames[i] + " ";
                    goto checkAgain;
                }
            }
            for (int i = 0; i < ListOfTownNames.Count; i++)
            {
            checkAgain:
                if (ListOfTownNames[i].Length < 10)
                {
                    ListOfTownNames[i] = ListOfTownNames[i] + " ";
                    goto checkAgain;
                }
            }
            for (int i = 0; i < ListOfSmallTownNames.Count; i++)
            {
            checkAgain:
                if (ListOfSmallTownNames[i].Length < 10)
                {
                    ListOfSmallTownNames[i] = ListOfSmallTownNames[i] + " ";
                    goto checkAgain;
                }
            }
            ListOfCityNames.Shuffle();
            ListOfTownNames.Shuffle();
            ListOfSmallTownNames.Shuffle();


            for (int i = 0; i < 10; i++)
            {
                ListOfXValues.Add(i);
                ListOfXValues.Add(i);
                ListOfXValues.Add(i);
                ListOfXValues.Add(i);
            }
            for (int i = 0; i < 15; i++)
            {
                ListOfYValues.Add(i);
                ListOfYValues.Add(i);
                ListOfYValues.Add(i);
                ListOfYValues.Add(i);
                ListOfYValues.Add(i);
                ListOfYValues.Add(i);

            }
            ListOfXValues.Shuffle();
            ListOfYValues.Shuffle();
            ListOfTypes.Shuffle();

            for (int i = 0; i < 10; i++)
            {
                CurrentXValue = i;
                for (int j = 0; j < 15; j++)
                {
                    CurrentYValue = j;
                    NextPoint = new PointOnWorldMap(CurrentXValue, CurrentYValue);
                    NextPoint.Name = "";
                    NextPoint.Symbol = "           ";
                    ListOfWorldPoints.Add(NextPoint);
                }
            }
            WriteLine($"Successfully generated {ListOfWorldPoints.Count} points.");
            WriteLine();
        PlaceNextLocation:
            if (LocationCounter < 11)
            {

                foreach (PointOnWorldMap point in ListOfWorldPoints)
                {
                    if (point.XValue == ListOfXValues[LocationCounter] && point.YValue == ListOfYValues[LocationCounter])
                    {
                        point.Type = ListOfTypes[LocationCounter];
                        if (point.Type == "City")
                        {
                            point.Symbol = "O";
                            point.Name = ListOfCityNames[LocationCounter];
                            CityAmmoRichness.Shuffle();
                            point.AmmoRichness = Convert.ToInt32(CityAmmoRichness[0] * DefaultValues.defaultAmmoRichnessMultiplier);
                            CityFoodRichness.Shuffle();
                            point.FoodRichness = Convert.ToInt32(CityFoodRichness[0] * DefaultValues.defaultFoodRichnessMultiplier);
                            CityTreeDensity.Shuffle();
                            point.TreeDensity = CityTreeDensity[0];
                            CityHouseDensity.Shuffle();
                            point.HouseDensity = CityHouseDensity[0];
                            CityZombieDensity.Shuffle();
                            point.ZombieDensity = Convert.ToInt32(CityZombieDensity[0] * DefaultValues.defaultZombieDensityMultiplier);
                            CitySize.Shuffle();
                            point.Size = CitySize[0];
                            Travel.OtherLocations.Add(point);
                        }
                        if (point.Type == "Town")
                        {
                            point.Symbol = "o";
                            point.Name = ListOfTownNames[LocationCounter];
                            TownAmmoRichness.Shuffle();
                            point.AmmoRichness = Convert.ToInt32(TownAmmoRichness[0] * DefaultValues.defaultAmmoRichnessMultiplier);
                            TownFoodRichness.Shuffle();
                            point.FoodRichness = Convert.ToInt32(TownFoodRichness[0] * DefaultValues.defaultFoodRichnessMultiplier);
                            TownTreeDensity.Shuffle();
                            point.TreeDensity = TownTreeDensity[0];
                            TownHouseDensity.Shuffle();
                            point.HouseDensity = TownHouseDensity[0];
                            TownZombieDensity.Shuffle();
                            point.ZombieDensity = Convert.ToInt32(TownZombieDensity[0] * DefaultValues.defaultZombieDensityMultiplier);
                            TownSize.Shuffle();
                            point.Size = TownSize[0];
                            Travel.OtherLocations.Add(point);
                        }
                        if (point.Type == "Small Town")
                        {
                            point.Symbol = "o";
                            point.Name = ListOfSmallTownNames[LocationCounter];
                            SmallTownAmmoRichness.Shuffle();
                            point.AmmoRichness = Convert.ToInt32(SmallTownAmmoRichness[0] * DefaultValues.defaultAmmoRichnessMultiplier);
                            SmallTownFoodRichness.Shuffle();
                            point.FoodRichness = Convert.ToInt32(SmallTownFoodRichness[0] * DefaultValues.defaultFoodRichnessMultiplier);
                            SmallTownTreeDensity.Shuffle();
                            point.TreeDensity = SmallTownTreeDensity[0];
                            SmallTownHouseDensity.Shuffle();
                            point.HouseDensity = SmallTownHouseDensity[0];
                            SmallTownZombieDensity.Shuffle();
                            point.ZombieDensity = Convert.ToInt32(SmallTownZombieDensity[0] * DefaultValues.defaultZombieDensityMultiplier);
                            SmallTownSize.Shuffle();
                            point.Size = SmallTownSize[0];
                            Travel.OtherLocations.Add(point);
                        }
                        LocationCounter++;
                        goto PlaceNextLocation;
                    }
                }
            }
            if (LocationCounter == 11)
            {
                foreach (PointOnWorldMap point in ListOfWorldPoints)
                {
                    if (point.XValue == ListOfXValues[LocationCounter] & point.YValue == 0)
                    {
                        point.Type = "Small Town";
                        point.Symbol = "X";
                        point.Name = ListOfSmallTownNames[LocationCounter];
                        SmallTownAmmoRichness.Shuffle();
                        point.AmmoRichness = Convert.ToInt32(SmallTownAmmoRichness[0] * DefaultValues.defaultAmmoRichnessMultiplier);
                        SmallTownFoodRichness.Shuffle();
                        point.FoodRichness = Convert.ToInt32(SmallTownFoodRichness[0] * DefaultValues.defaultFoodRichnessMultiplier);
                        SmallTownTreeDensity.Shuffle();
                        point.TreeDensity = SmallTownTreeDensity[0];
                        SmallTownHouseDensity.Shuffle();
                        point.HouseDensity = SmallTownHouseDensity[0];
                        SmallTownZombieDensity.Shuffle();
                        point.ZombieDensity = Convert.ToInt32(SmallTownZombieDensity[0] * DefaultValues.defaultZombieDensityMultiplier);
                        SmallTownSize.Shuffle();
                        point.Size = SmallTownSize[0];
                        Travel.CurrentLocation = point;
                        Travel.CurrentWorldMapX = point.XValue;
                        Travel.CurrentWorldMapY = point.YValue;
                        point.Visited = true;
                        LocationCounter++;
                        goto PlaceEnd;
                    }
                }
            }
        PlaceEnd:
            if (LocationCounter == 12)
            {
                foreach (PointOnWorldMap point in ListOfWorldPoints)
                {
                    if (point.XValue == ListOfXValues[LocationCounter] & point.YValue == 14)
                    {
                        point.Type = "End";
                        point.Symbol = "G";
                        point.Name = "Safe Zone";
                        Travel.OtherLocations.Add(point);
                        //LocationCounter++;
                    }
                }
            }

        }
        public static void ViewWorldMap()
        {
            WriteLine("--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
            for (int i = 0; i < 150; i++)
            {

                switch(ListOfWorldPoints[i].Type)
                {
                    case "City":
                    ForegroundColor = ConsoleColor.DarkRed;
                    break;
                    case "Town":
                    ForegroundColor = ConsoleColor.DarkYellow;
                    break;
                    case "Small Town":
                    ForegroundColor = ConsoleColor.Green;
                    break;
                    case "End":
                    ForegroundColor = ConsoleColor.DarkGreen;
                    break;
                }
                Write(ListOfWorldPoints[i].Symbol);
                ForegroundColor = ConsoleColor.Gray;
                Write(" " + ListOfWorldPoints[i].Name);
                SpaceCounter++;
                ColumnCounter++;
                if (ColumnCounter == 15)
                {
                    WriteLine();
                    WriteLine();
                    WriteLine();
                    ColumnCounter = 0;
                    SpaceCounter = 0;

                }


            }
            WriteLine();
            WriteLine("--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
            ForegroundColor = ConsoleColor.DarkRed;
            Write("      O");
            ForegroundColor = ConsoleColor.Gray;
            Write(" : Large City           ");
            ForegroundColor = ConsoleColor.DarkYellow;
            Write("o");
            ForegroundColor = ConsoleColor.Gray;
            Write(" : Town         ");
            ForegroundColor = ConsoleColor.Green;
            Write("o");
            ForegroundColor = ConsoleColor.Gray;
            Write(" : Small Town     ");
            ForegroundColor = ConsoleColor.Gray;
            Write("X");
            ForegroundColor = ConsoleColor.Gray;
            Write(" : You are here     G : Goal");
            WriteLine();
            WriteLine();
        }
    }
    public class PointOnWorldMap
    {
        public string Name;
        public bool Visited = false;
        public int XValue;
        public int YValue;
        public string Symbol;
        public string Type;
        public string WorldPointType;
        public string ammoEstimate()
        {
            if(AmmoRichness < 1)
            {
                return "None";
            }
            else if (AmmoRichness < 3)
            {
                return "Not much";
            }
            else if (AmmoRichness < 5)
            {
                return "Some";
            }
            else if (AmmoRichness < 7)
            {
                return "A lot";
            }
            else
            {
                return "Abundant";
            }
        }
        public string populationEstimate()
        {
            if(ZombieDensity < 1)
            {
                return "None";
            }
            else if (ZombieDensity < 3)
            {
                return "Little";
            }
            else if (ZombieDensity < 5)
            {
                return "Medium";
            }
            else if (ZombieDensity < 7)
            {
                return "Large";
            }
            else
            {
                return "Huge";
            }
        }
        public string foodEstimate()
        {
            if(FoodRichness < 1)
            {
                return "None";
            }
            else if (FoodRichness < 3)
            {
                return "Not much";
            }
            else if (FoodRichness < 5)
            {
                return "Some";
            }
            else if (FoodRichness < 7)
            {
                return "A lot";
            }
            else
            {
                return "Abundant";
            }
        }
        public int AmmoRichness;
        public int FoodRichness;
        public int TreeDensity;
        public int HouseDensity;
        public int ZombieDensity;
        public int FoodCostOfTravel;
        public int Size;
        public PointOnWorldMap(int xvalue, int yvalue)
        {
            XValue = xvalue;
            YValue = yvalue;
        }
    }
    public class Travel
    {
        public static PointOnWorldMap CurrentLocation;
        public static int CurrentWorldMapX;
        public static int CurrentWorldMapY;
        public static Map newMap = new Map(3, 3, 10, 5, 5, 5);
        public static List<PointOnWorldMap> OtherLocations = new List<PointOnWorldMap> { };
        public static List<PointOnWorldMap> TravelMenuList = new List<PointOnWorldMap> { };
        





    }
}
