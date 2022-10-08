using static System.Console;
namespace DeckLib
{
    public class HelpMenu
    {
        public static void HelpGeneralInformation()
        {
            Clear();
            WriteLine("This is a procedurally generated game of the rogue-like genre. Each playthrough will be different, and not all of them will result in success.");
            WriteLine("You are playing as a survivor in a post-apocalyptic world infested with mindless walking corpses.");
            WriteLine();
            WriteLine("Your goal is to travel to a safe zone. To do that, you will need to traverse several zones.");
            WriteLine("In them you will have the chance to explore, loot, and fight for your life against the undead hordes.");
            WriteLine();
            WriteLine("You will also choose a class, which provides unique abilities, and collect experience as you go along, improving those abilities.");
            WriteLine();
            WriteLine("At the start of the game, you will select a number of actions that your character possesses. Those are needed in combat.");
            WriteLine();
            WriteLine("You will have control over your path, but make no mistake - you will not succeed every time.");
            WriteLine();
            WriteLine("Press any key to continue.");
            WriteLine();
            ReadKey();
        }
        public static void HelpExploration()
        {
            Clear();
            WriteLine("While exploring, you see a represenation of your character on a map. Using the arrow keys, you navigate the area, with the goal of reaching the exit.");
            WriteLine();
            WriteLine("Beware! Although the map might appear nearly empty, every inch of it has the potential to house the dreaded zombies.");
            WriteLine("When you stumble upon zombies, you will be automatically moved into the combat screen.");
            WriteLine("Note that when you run into zombies, you don't just fight them, but nearby ones too.");
            WriteLine();
            WriteLine("The rest of the time you will be travelling around the map. You may be tempted to rush for the exit, but that might not always be the best choice.");
            WriteLine();
            WriteLine("Green \"Y\"s on the map are trees. They block your movement, so you need to go around them.");
            WriteLine("Marked as squares on the exploration map are buildings. Upon visiting those, you can loot their contents (if any). ");
            WriteLine("Those buildings are the only source of your two resources - food and ammo. Both of those are important to each class, albeit to varying degrees.");
            WriteLine();
            WriteLine("Food is needed to move - every step on the exploration map costs a small fraction of your food.");
            WriteLine("Additionally, food is needed to travel to a new location - the cost depends on the distance you need to travel.");
            WriteLine();
            WriteLine("Ammo is needed in combat.");
            WriteLine();
            WriteLine("Press any key to continue.");
            ReadKey();

        }
        public static void HelpCombat()
        {
            Clear();
            WriteLine("When combat begins, you will be presented with a list of actions that you can take, and a visual represenation of the enemy.");
            WriteLine("You select an action with the arrow keys, and press enter to use it.");
            WriteLine();
            WriteLine("Below your actions is where you will find the combat messages. Every action you take, and every action your enemies take, is reflected there.");
            WriteLine("Important factors such as range, behavior, the effectiveness of your actions, and so on, can be easily determined by keeping an eye on these messages.");
            WriteLine();        
            WriteLine("The actions your character can learn are Stab, Shoot, and Run.");
            WriteLine("Each of these actions comes in several versions. The general rule is, the better the version, the better the effect.");
            WriteLine("Better Runs net you more distance, better Stabs deal more damage, and better Shoots deal more damage but at the expense of more ammo.");
            WriteLine();
            WriteLine("You also automatically get basic versions of Stab and Shoot, as well as a Wait action.");
            WriteLine("Those are weaker than regular actions, but can be used any number of times per fight, and you always have them at the start.");
            WriteLine("At the start of each combat, the list of actions you can take includes the basic ones (stab, shoot and wait) as well as two of the ones you know.");
            WriteLine();
            WriteLine("Once used, a non-basic action is unavailable for the rest of the fight, but it will be available for your next encounter.");
            WriteLine("Every round during combat you gain access to an additional action out of the ones you know, if there are any left.");
            WriteLine();
            WriteLine("An important factor in combat is distance. The relentless zombie hordes are trying to reach you, so that they can devour you.");
            WriteLine("If they succeed, you instantly die.");
            WriteLine();
            WriteLine("To help with that, the Run action puts some distance between you and the zombies. Run enough times, and you will escape the fight altogether.");
            WriteLine("Running helps manipulate distance to your advantage.");
            WriteLine();
            WriteLine("The Stab action is also related to distance. It represents using your melee weapon to take out one or more zombies.");
            WriteLine("It is therefore only available when the zombies are close to you. The combat messages will help you know if that's the case.");
            WriteLine();
            WriteLine("The final action is Shoot. Unlike stab, it does not have any distance limitations. It does, however, consume Ammo with each shot.");
            WriteLine();
            WriteLine();
            WriteLine("Press any key to continue.");
            ReadKey();
        }
    }
}