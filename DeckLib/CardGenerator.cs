using System.Collections.Generic;
using static System.Console;
namespace DeckLib
{
    public static class CardGenerator
    {

        public static List<int> valueList = new List<int>{1,1,1,2,2,3};
        public static List<string> typeList = new List<string>{"Stab","Shoot","Run"};
        public static int cardCounter = 0;

        
        public static Card GenerateCard()
        {  
            ShuffleThings.Shuffle<int>(valueList);
            ShuffleThings.Shuffle<string>(typeList);
            cardCounter++;
            Card generated = new Card("card"+$"{cardCounter}", typeList[0], valueList[0]);
            if (generated.Type == "Stab")
            {
               switch(generated.Value)
               {
                   case 1:
                   generated.Name = "Stab a lot"; 
                   break;
                   case 2:
                   generated.Name = "Stab a whole lot"; 
                   break;
                   case 3:
                   generated.Name = "Stab like a madman"; 
                   break;
               }
            }
            else if (generated.Type == "Shoot")
            {
               switch(generated.Value)
               {
                   case 1:
                   generated.Name = "Shoot a lot"; 
                   break;
                   case 2:
                   generated.Name = "Shoot a whole lot"; 
                   break;
                   case 3:
                   generated.Name = "Shoot like crazy"; 
                   break;
               }
            }
            else
            {
               switch(generated.Value)
               {
                   case 1:
                   generated.Name = "Run"; 
                   break;
                   case 2:
                   generated.Name = "Run Fast"; 
                   break;
                   case 3:
                   generated.Name = "Run Like Hell"; 
                   break;
               }
            }
            return generated;
        }

    }
}