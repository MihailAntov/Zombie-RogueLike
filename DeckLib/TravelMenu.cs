using static System.Console;
namespace DeckLib
{
    public class TravelMenu : Travel
    {
        public static void EnterTravelMenu()
        {
            Menu.Travelling = true;
            Menu TravelMenu = new Menu();
            TravelMenu.header.Add("The zombie horde following you is growing. The towns behind you are lost. You must move forward.");

            for (int i = OtherLocations.Count(); i > 0; i--)
            {
                if (OtherLocations[i - 1].YValue < CurrentWorldMapY || OtherLocations[i - 1].Visited == true)
                {
                    OtherLocations.Remove(OtherLocations[i - 1]);
                }
            }
            foreach (PointOnWorldMap point in OtherLocations)
            {
                point.FoodCostOfTravel = CalculateFoodCostOfTravel(CurrentWorldMapX, CurrentWorldMapY, point.XValue, point.YValue);
            }
            List<PointOnWorldMap> SortedOtherLocations = OtherLocations.OrderBy(o => o.FoodCostOfTravel).ToList();
            for (int i = 0; i < SortedOtherLocations.Count(); i++)
            {
                if (SortedOtherLocations[i].FoodCostOfTravel <= Player.Food)
                {
                    TravelMenuList.Add(SortedOtherLocations[i]);
                    if (Player.Tools == "Detailed map of the area")
                    {
                        TravelMenu.addOption($"{SortedOtherLocations[i].Name} : {SortedOtherLocations[i].FoodCostOfTravel} food.  (Population: {SortedOtherLocations[i].populationEstimate()}   Ammo: {SortedOtherLocations[i].ammoEstimate()}   Food: {SortedOtherLocations[i].foodEstimate()};)");
                    }
                    else
                    {
                        TravelMenu.addOption($"{SortedOtherLocations[i].Name} : {SortedOtherLocations[i].FoodCostOfTravel} food.");
                    }
                    
                }
                if (TravelMenu.options.Count() == 3)
                {
                    break;
                }
            }
            if (TravelMenu.options.Count < 1)
            {
                WriteLine("You continue east, even though you are running low on food.");
                WriteLine("Before you can reach another settlement, you run out of food and begin to starve.");
                WriteLine("You have been walking for hours, and you are barely able to lift your feet.");
                WriteLine("You stumble and fall. You don't have to strength to get up. You hear growling behind you.");
                WriteLine("Press any key to continue.");
                ReadKey();
                Player.Dead = true;
                Menu.Travelling = false;


            }
            else
            {
                TravelMenu.header.Add($"Select your next destination:             Current Food: {Player.Food}");
            choosingDestination:
                TravelMenu.goIntoMenu();
                switch (TravelMenu.finalSelection)
                {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                    case 7:
                    case 8:
                    case 9:
                    case 10:
                    case 11:
                    case 12:

                        DeckLib.Player.Food = DeckLib.Player.Food - SortedOtherLocations[TravelMenu.finalSelection].FoodCostOfTravel;
                        if (CurrentLocation.Type == "City")
                        {
                            CurrentLocation.Symbol = "O";
                        }
                        else if (CurrentLocation.Type == "Town")
                        {
                            CurrentLocation.Symbol = "o";
                        }
                        else if (CurrentLocation.Type == "Small Town")
                        {
                            CurrentLocation.Symbol = ".";
                        }

                        TravelMenuList[TravelMenu.finalSelection].Symbol = "X";
                        TravelMenuList[TravelMenu.finalSelection].Visited = true;
                        CurrentLocation = TravelMenuList[TravelMenu.finalSelection];
                        CurrentWorldMapX = TravelMenuList[TravelMenu.finalSelection].XValue;
                        CurrentWorldMapY = TravelMenuList[TravelMenu.finalSelection].YValue;
                        break;

                    case 99:
                        goto choosingDestination;
                }


                if (CurrentLocation.Type == "End")
                {
                    Menu.Travelling = false;
                    Player.Won = true;
                }
                else
                {
                    newMap.AmmoRichness = CurrentLocation.AmmoRichness;
                    newMap.FoodRichness = CurrentLocation.FoodRichness;
                    newMap.TreeDensity = CurrentLocation.TreeDensity;
                    newMap.HouseDensity = CurrentLocation.HouseDensity;
                    newMap.Size = CurrentLocation.Size;
                    newMap.ZombieDensity = CurrentLocation.ZombieDensity;
                    TravelMenuList.Clear();

                    for (int i = TravelMenu.options.Count(); i > 0; i--)
                    {
                        TravelMenu.removeOption(TravelMenu.options[i - 1]);
                    }
                    Menu.Travelling = false;
                    newMap.GenerateMap();
                    Movement.currentMap = newMap;
                    newMap.PlayerPosition = newMap.listOfPoints.IndexOf(newMap.listOfPoints[newMap.Size * (newMap.Size - 1)]);
                    Movement.ExploreMap();
                }
            }
        }
        public static int CalculateFoodCostOfTravel(int XOrigin, int YOrigin, int XDestination, int YDestination)
        {
            double xOriginDouble = (double)XOrigin;
            double yOriginDouble = (double)YOrigin;
            double xDestinationDouble = (double)XDestination;
            double yDestinationDouble = (double)YDestination;
            return Convert.ToInt32(2*Math.Sqrt((Math.Abs(xDestinationDouble - xOriginDouble) * Math.Abs(xDestinationDouble - xOriginDouble)) + (Math.Abs(yDestinationDouble - yOriginDouble) * Math.Abs(yDestinationDouble - yOriginDouble))));

        }
    }
}