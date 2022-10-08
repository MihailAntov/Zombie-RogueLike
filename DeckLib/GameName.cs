namespace DeckLib
{
    public class GameName
    {
        public static String name1 = "A Grave New World";
        public static String name2 = "The Grave Expectations";
        public static String name3 = "DawnLoad of the Dead";
        public static String name4 = "totally_awesome_zombie_game.exe";
        public static String name5 = "Assassin's Creed : ZombiesIGuess";
        public static String name6 = "Note: Put game name here";
        public static String name7 = "Dead Dead Redeadmption";
        public static String name8 = "StabShootRun 2: Electric Boogaloo";
        public static List<String> listOfGameNames = new List<string>{name1, name2, name3, name4, name5, name6, name7, name8};
        public static String GenerateGameName()
        {
            listOfGameNames.Shuffle();
            return listOfGameNames[0];
        }
    }
}