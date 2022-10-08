namespace DeckLib;
using static System.Console;

public static class Player
{
    public static string Name;
    public static string CharacterClass;
    public static List<string> ClassDescription = new List<string>();
    public static List<string> ToolsDescription = new List<string>();
    public static List<string> TraitDescription = new List<string>();
    //cold blooded:
    //reckless
    //precise
    //trigger happy
    //silent
    //deadly
    //fast
    //tireless
    public static string Tools;
    //map of the area
    //survival kit
    //bag of ammo
    public static string Trait;
    //gifted
    //fastLearner
    //versatile
    public static int Ammo;
    public static int Food;
    public static int FoodCounter = 0;
    public static bool Dead = false;
    public static int XP = 0;
    public static int XPToNextLevel;
    public static int Level = 1;
    public static bool ZombieSense = false;
    public static bool Won = false;
    
    
}
public static class Deck
{
    public static string deckName;
    public static int DeckSize;
    public static List<Card> cardList = new List<Card>();

    //methods
    public static void AddCard(Card newCard)
    {
        cardList.Add(newCard);
    }
    public static void RemoveCard(Card oldCard)
    {
        cardList.Add(oldCard);
    }
    public static void DrawCard()
    {
        
        cardList.Shuffle();
        foreach (Card card in cardList)
        {
            if(card.InHand == false && card.InDiscard == false)
            {
                card.InHand = true;
                break;
            }
        }
        
    }
    

    public static void View()
    {
        Clear();
        if (cardList == null)
        {
            WriteLine("No cards in your deck.");
        }
        else
        {
            WriteLine($"Contents of {deckName}:");
            WriteLine();
            foreach(Card card in cardList)
            {
                WriteLine($"{card.Name}: +{card.Value} {card.Type}");
            }
        }
    }
    public static void Shuffle()
    {
        ShuffleThings.Shuffle<Card>(cardList);
    }
}
public class Card
{
    public List<int> valueList = new List<int>{1, 2, 3};
    public List<string> typeList = new List<string>{"Stab","Shoot","Run"};
    public int cardCounter = 0;
    public string Name;
    public string ID;
    public int Value;
    public string Type;
    public int AttValue;
    public int DefValue;
    public int TacValue;
    public int StratValue;
    public bool InHand;
    public bool InDiscard;
    public Card()
    {
        Name = "Unknown";
    }
    public Card(string id, string type, int value)
    {
        id = ID;
        Type = type;
        Value = value;
        InHand = false;
        if(type == "Attack")
        {
            AttValue = value;
        }
        else
        {
            AttValue = 0;
        }

        if(type == "Defense")
        {
            DefValue = value;
        }
        else
        {
            DefValue = 0;
        }

        if(type == "Tactical")
        {
            TacValue = value;
        }
        else
        {
            TacValue = 0;
        }
    }
}
