using static System.Console;
namespace DeckLib

{
    public class PickACard
    {
        public static Menu cardMenu = new Menu();
        public static Card choiceOne;
        public static Card choiceTwo;
        public static Card choiceThree;
        public static Card choiceFour;
        public static Card choiceFive;
        public static bool Selecting;


        public static void PickOneOfThree()
        {
            Selecting = true;

        cardMenu:
            if (Deck.cardList.Count() < Deck.DeckSize)
            {
                cardMenu.header.Add("Select one of the following actions to permanently learn:");
                cardMenu.header.Add("");
                choiceOne = CardGenerator.GenerateCard();
                choiceTwo = CardGenerator.GenerateCard();
                choiceThree = CardGenerator.GenerateCard();
                choiceFour = CardGenerator.GenerateCard();
                choiceFive = CardGenerator.GenerateCard();
                
            secondCard:
                if (choiceTwo.Name == choiceOne.Name)
                {
                    choiceTwo = CardGenerator.GenerateCard();
                    goto secondCard;
                }
            thirdCard:
                if (choiceThree.Name == choiceOne.Name || choiceThree.Name == choiceTwo.Name)
                {
                    choiceThree = CardGenerator.GenerateCard();
                    goto thirdCard;
                }
            fourthCard:
                if (choiceFour.Name == choiceOne.Name || choiceFour.Name == choiceTwo.Name || choiceFour.Name == choiceThree.Name)
                {
                    choiceFour = CardGenerator.GenerateCard();
                    goto fourthCard;
                }
            fifthCard:    
                if (choiceFive.Name == choiceOne.Name || choiceFive.Name == choiceTwo.Name || choiceFive.Name == choiceThree.Name || choiceFive.Name == choiceFour.Name)
                {
                    choiceFive = CardGenerator.GenerateCard();
                    goto fifthCard;
                }    
                cardMenu.addOption($"{choiceOne.Name}");
                cardMenu.addOption($"{choiceTwo.Name}");
                cardMenu.addOption($"{choiceThree.Name}");
                if(Player.Trait == "Versatile")
                {
                    cardMenu.addOption($"{choiceFour.Name}");
                    cardMenu.addOption($"{choiceFive.Name}");
                }
                if (Deck.cardList.Count() > 0)
                {
                    cardMenu.footer.Add("Actions currently known:");
                    foreach(Card action in Deck.cardList)
                    {
                        cardMenu.footer.Add($"{action.Name}");
                    }
                }
                cardMenu.goIntoMenu();
                cardMenu.header.Clear();
                cardMenu.footer.Clear();
                switch (cardMenu.finalSelection)
                {
                    case 0:
                        Deck.AddCard(choiceOne);
                        cardMenu.options.Clear();
                        goto cardMenu;
                    case 1:
                        Deck.AddCard(choiceTwo);
                        cardMenu.options.Clear();
                        goto cardMenu;
                    case 2:
                        Deck.AddCard(choiceThree);
                        cardMenu.options.Clear();
                        goto cardMenu;
                    case 3:
                        Deck.AddCard(choiceFour);
                        cardMenu.options.Clear();
                        goto cardMenu;
                    case 4:
                        Deck.AddCard(choiceFive);
                        cardMenu.options.Clear();
                        goto cardMenu;        
                    
                }
            }
            else
            {
                Selecting = false;
            }
            
        }
    }
}