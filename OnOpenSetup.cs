using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightMasters
{
    public abstract class OnOpenSetup
    {

        public static ICard[] BaseDeck { get; private set; }//In order to properly check for corruption, all
                                                            //neutral card objects are created once and
                                                            //stored in this array.

        public static bool Setup()
        {

            bool isAllGood = true;

            //U stands for unknown, M for missing, C for corrupt and G for good

            Dictionary<string, string> FileCondition = new() { 
                
                { "ICard.cs", "U"}, { "ICharClass.cs", "U" }, { "IToken.cs", "U"}, { "IMinion.cs", "U"}, 
                { "Damage.cs", "U"}, { "CharacterClasses.cs", "U"}, { "Player.cs", "U"}, 
                { "PlayHelper.cs", "U"}, { "TokenHandler.cs", "U"}, { "ClassCards.cs", "U"}, 
                { "NeutralCards.cs", "U"}, { "ClassMinions.cs", "U"}, { "NeutralMinions.cs", "U"}, 
                { "MainMenu.cs", "U"}

            };

            FileCondition = FileCorruptionCheck(FileExistenceCheck(FileCondition));

            if(FileCondition.ContainsValue("M"))
            {

                isAllGood = false;

                Console.WriteLine("The following files are missing:");

                Console.WriteLine(FileCondition.Keys.Where(Key => FileCondition[Key] == "M"));

            }

            if (FileCondition.ContainsValue("C"))
            {

                isAllGood = false;

                Console.WriteLine("The following files are corrupted:");

                Console.WriteLine(FileCondition.Keys.Where(Key => FileCondition[Key] == "C"));

            }

            if (FileCondition.ContainsValue("U"))
            {

                isAllGood = false;

                Console.WriteLine("The following files may be either missing or corrupted:");

                Console.WriteLine(FileCondition.Keys.Where(Key => FileCondition[Key] == "U"));

            }


            return isAllGood; //If all checks passed, return true
        }

        private static Dictionary<string,string> FileExistenceCheck(Dictionary<string,string> FileCondition) {

            foreach (KeyValuePair<string,string> file in FileCondition)
            {

                if (!File.Exists(Path.Combine(Directory.GetCurrentDirectory(), file.Key)))
                {
                    FileCondition[file.Key] = "M";
                }

            }

            return FileCondition;

        }

        private static Dictionary<string, string> FileCorruptionCheck(Dictionary<string, string> FileCondition)
        {

            //TO DO: CHECK FOR CORRUPTED FILES

            





            return FileCondition;

        }

    }

}
