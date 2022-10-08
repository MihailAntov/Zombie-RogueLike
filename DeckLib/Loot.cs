using static System.Console;
using System.Collections.Generic;
namespace DeckLib
{
    public class Loot
    {
        
        public static string loot1Type;
        public static string loot2Type;
        public static string loot3Type;
        public static int loot1Amount;
        public static int loot2Amount;
        public static int loot3Amount;
        public static int mapSize;
        public static string message;
        public static int triggerHappyModifier;
        public static int AmmoBagModifier;
        public static int BackPackMultiplier;
        public static void addLoot()
        {
            
        }
        public static void GenerateLoot(int AmmoRichness, int FoodRichness)
        {
            
            if (Player.CharacterClass == "Trigger-happy")
            {
                triggerHappyModifier = Player.Level;
            }
            else
            {
                triggerHappyModifier = 0;
            }

            for(int i = 0; i<10; i++)
            {
                if (i < AmmoRichness+triggerHappyModifier)
                {
                    lootTypeList.Add("Ammo");
                }
                else
                {
                    lootTypeList.Add("Empty");
                }
            }
            for(int i = 0; i<10; i++)
            {
                if (i < FoodRichness)
                {
                    lootTypeList.Add("Food");
                }
                else
                {
                    lootTypeList.Add("Empty");
                }
            }

            
            //ISSUE IS HERE;
            ShuffleThings.Shuffle(lootTypeList);
            ShuffleThings.Shuffle(lootAmountList);
            loot1Type = lootTypeList[0];
            loot2Type = lootTypeList[1];
            loot3Type = lootTypeList[2];
            loot1Amount = lootAmountList[0];
            loot2Amount = lootAmountList[1];
            loot3Amount = lootAmountList[2];
            WriteLine();
            //ISSUE IS HERE
            if(loot1Type == "Empty" & loot2Type == "Empty" & loot3Type == "Empty")
            {
                if (Player.CharacterClass == "Trigger-happy")
                {
                    message = message+"\n"+"You manage to find 6 ammo.";
                    Player.Ammo = Player.Ammo + 6;
                }
                else
                {
                    message = message+"\n"+"You find nothing useful.";
                }
                
            }
            else
            {
                if (loot1Type != "Empty")
                {
                    
                    if (loot1Type == "Food")
                    {
                        message = message+"\n"+$"You find {loot1Amount+BackPackMultiplier} {loot1Type}.";
                        Player.Food = Player.Food + loot1Amount + BackPackMultiplier;
                    }
                    if (loot1Type == "Ammo")
                    {
                        if (Player.CharacterClass == "Trigger-happy")
                        {
                            message = message+"\n"+ $"You find a hidden ammo stash of {(2+triggerHappyModifier)*loot1Amount} {loot1Type}.";
                            Player.Ammo = Player.Ammo + ((2+triggerHappyModifier+AmmoBagModifier)*loot1Amount);
                        }
                        else
                        {
                            message = message+"\n"+ $"You find {(2+ AmmoBagModifier)*loot1Amount} {loot1Type}.";
                            Player.Ammo = Player.Ammo + ((2+AmmoBagModifier)*loot1Amount);
                        }
                        
                        
                    }
                }
                if (loot2Type != "Empty")
                {
                    
                    if (loot2Type == "Food")
                    {
                        message = message+"\n"+ $"You find {loot2Amount+BackPackMultiplier} {loot2Type}.";
                        Player.Food = Player.Food + loot2Amount + BackPackMultiplier;
                    }
                    if (loot2Type == "Ammo")
                    {
                        if (Player.CharacterClass == "Trigger-happy")
                        {
                            message = message+"\n"+ $"You find a hidden ammo stash of {(2+triggerHappyModifier)*loot2Amount} {loot2Type}.";
                            Player.Ammo = Player.Ammo + ((2+triggerHappyModifier + AmmoBagModifier)*loot2Amount);
                        }
                        else
                        {
                            message = message+"\n"+ $"You find {(2+ AmmoBagModifier)*loot2Amount} {loot2Type}.";
                            Player.Ammo = Player.Ammo + ((2+AmmoBagModifier)*loot2Amount);
                        }
                        
                        
                    }
                }
                if (loot3Type != "Empty")
                {
                    
                    if (loot3Type == "Food")
                    {
                        message = message+"\n"+ $"You find {loot3Amount+BackPackMultiplier} {loot3Type}.";
                        Player.Food = Player.Food + loot3Amount+BackPackMultiplier;
                    }
                    if (loot3Type == "Ammo")
                    {
                        if (Player.CharacterClass == "Trigger-happy")
                        {
                            message = message+"\n"+ $"You find a hidden ammo stash of {(2+triggerHappyModifier + AmmoBagModifier)*loot3Amount} {loot3Type}.";
                            Player.Ammo = Player.Ammo + ((2+triggerHappyModifier+AmmoBagModifier)*loot3Amount);
                        }
                        else
                        {
                            message = message+"\n"+ $"You find {(2+AmmoBagModifier)*loot3Amount} {loot3Type}.";
                            Player.Ammo = Player.Ammo + ((2+AmmoBagModifier)*loot1Amount);
                        }
                        
                        
                    }
                }
            }
            lootTypeList.Clear();

        }
        public static List<string> lootTypeList = new List<string>();
        public static List<int> lootAmountList = new List<int>{1,1,1,1,1,2,2,2,2,3,3,4};
    }
}