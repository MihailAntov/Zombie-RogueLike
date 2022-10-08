namespace DeckLib
{
    public class Unload
    {
        public static void UnloadAssets()
        {
            Player.Food = DefaultValues.defaultStartingFood;
            Player.Ammo = DefaultValues.defaultStartingAmmo;
            Player.XP = 0;
            Player.Level = 1;
            Player.XPToNextLevel = DefaultValues.DefaultXPToNextLevel;
            Player.Dead = false;
            Player.Won = false;
            Menu.Travelling = false;
            Deck.cardList.Clear();
            PickACard.cardMenu.footer.Clear();
            WorldMap.ListOfWorldPoints.Clear();
            WorldMap.LocationCounter = 0;
            Travel.OtherLocations.Clear();
            Travel.TravelMenuList.Clear();
            CharacterClass.Started = false;
            
        }
        
    }
}