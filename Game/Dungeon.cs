using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Xml.Linq;

namespace Game
{
    class Dungeon
    {
        public static void Main(string[] args)
        {

            Console.WriteLine("\n----WELCOME ADVENTURER----\n");

            Console.WriteLine("WHAT IS YOUR NAME?");
            string adventurerName = Console.ReadLine();

            Character adventurer = new Character()
            {
                Name = adventurerName,
                HitChance = 8,
                MaxHealth = 75,
                Health = 50,
                Block = 0
            };



            Console.WriteLine("WELCOME, " + adventurer.Name + "\n");
            bool helpChoice = false;

            Console.WriteLine("IT HAS BEEN SAID A BRAVE HERO WILL COME TO OUR LAND TO RID US OF THE MONSTERS THAT TERRORIZE OUR PEOPLE, WILL YOU HELP US? ");
            do
            {

                Console.WriteLine("Y.) YES\nN.) No\n");
                ConsoleKey userHelp = Console.ReadKey(intercept: true).Key;

                switch (userHelp)
                {
                    case ConsoleKey.Y:

                        Console.WriteLine("HUZZAH! THEN I SHALL BESTOW UPON YOU A WEAPON TO AID YOU ON YOUR QUEST\n");
                        helpChoice = true;
                        break;
                    case ConsoleKey.N:
                        Console.WriteLine("THEN WE WILL WAIT FOR A BRAVE ADVENTURER, UNTIL THEN, WE SUFFER.");
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("PLEASE KIND STRANGER, WILL YOU HELP US?");

                        break;

                }
            } while (!helpChoice);

            bool chooseWeapon = false;

            //CHOOSE WEAPON
            #region
            do
            {
                Console.WriteLine("HOW WILL YOU REACH GLORY? \n A.) BY WAY OF THE AXE \n B.) BY WAY OF THE BOW \n S.) BY WAY OF THE SWORD\n X.) MAYHAPS ADVENTURE CALLS ANOTHER DAY...\n");
                ConsoleKey userChoice = Console.ReadKey(intercept: true).Key;

                switch (userChoice)
                {
                    case ConsoleKey.A:
                        Console.WriteLine("THE WAY OF THE AXE, AN HONORABLE CHOICE");
                        Weapon axe = new Weapon()
                        {
                            Name = "GOBLIN CLEAVER",
                            MinDamage = 10,
                            MaxDamage = 18,
                            IsTwoHanded = true,
                            BonusHitChance = 3

                        };
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("*****YOU HAVE RECEIVED " + axe.Name + "*****\n");
                        adventurer.charWeapon = axe;
                        chooseWeapon = true;
                        break;
                    case ConsoleKey.B:
                        Console.WriteLine("THE WAY OF THE BOW, AN HONORABLE CHOICE");
                        Weapon bow = new Weapon()
                        {
                            Name = "DRAGON SEEKER",
                            MinDamage = 12,
                            MaxDamage = 14,
                            IsTwoHanded = true,
                            BonusHitChance = 7

                        };
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("*****YOU HAVE RECEIVED " + bow.Name + "*****\n");
                        adventurer.charWeapon = bow;

                        chooseWeapon = true;
                        break;
                    case ConsoleKey.S:
                        Console.WriteLine("THE WAY OF THE SWORD, AN HONORABLE CHOICE");
                        Weapon sword = new Weapon()
                        {
                            Name = "ORC BANE",
                            MinDamage = 13,
                            MaxDamage = 15,
                            IsTwoHanded = false,
                            BonusHitChance = 5

                        };
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("*****YOU HAVE RECEIVED " + sword.Name + "*****\n");
                        adventurer.charWeapon = sword;

                        chooseWeapon = true;
                        break;
                    case ConsoleKey.X:
                        Console.WriteLine("THERE IS HONOR IS SELF PRESERVATION\n");
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("CHOOSE AN HONORABLE PATH\n");
                        break;
                }
            } while (!chooseWeapon);
            Console.ForegroundColor = ConsoleColor.White;
            #endregion

            int chamber = 1;
            Console.WriteLine("Your Journey Begins...");
            Console.Title = "DUNGEON QUEST\n";
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n*********YOU ARE ENTERING THE DUNGEON OF DOOM*********\n");
            Console.ForegroundColor = ConsoleColor.White;

            //DUNGEON 1
            #region
            do
            {
                int monsterLvl = chamber;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("-----------------\nDUNGEON CHAMBER {0}\n-----------------\n", chamber);




                string[] monsterNames = { "DANGBORN THE UNHOLY", "GARBAGE THE DESTROYER", "PRIMUS THE DAMNED", "DAVE FROM CLEVELAND", "AGBONTHORN THE ABSOLUTE" };

                Monster monster = new Monster()
                {
                    Name = monsterNames[chamber - 1],
                    HitChance = monsterLvl,
                    Health = monsterLvl * 10,
                    Block = monsterLvl + 5,
                    Damage = monsterLvl + 5
                };
                Console.WriteLine("MONSTER DETECTED: {0}", monster.Name);
                Console.ForegroundColor = ConsoleColor.White;
                bool exit = false;

                do
                {
                    Console.WriteLine("\nChoose your Path \n A.) Attack \n R.) Run Away \n P.) Player Info \n M.) Monster Info \n X.) Exit \n");
                    ConsoleKey userChoice = Console.ReadKey(intercept: true).Key;

                    switch (userChoice)
                    {
                        case ConsoleKey.A:
                            bool toHit = (hitChance(adventurer.charWeapon.BonusHitChance, adventurer.HitChance, monster.Block));
                            if (toHit == true)
                            {
                                int hit = damage(adventurer.charWeapon.MinDamage, adventurer.charWeapon.MaxDamage);
                                Console.WriteLine("YOU STRUCK YOUR MARK");
                                Console.WriteLine("YOU DEAL {0} DAMAGE\n", hit);
                                monster.Health = monster.Health - hit;
                            }
                            else
                            {
                                Console.WriteLine("YOU MISSED YOUR MARK");
                            }


                            if (monster.Health < 1)
                            {
                                Console.ForegroundColor = ConsoleColor.Green;

                                Console.WriteLine("---------------\nTHE MONSTER HAS BEEN DEFEATED\n---------------\n");
                                Console.ForegroundColor = ConsoleColor.White;

                                bool choice = false;
                                do
                                {
                                    Console.WriteLine("\nCLAIM YOUR REWARD:\n H.) HEALTH POTION\n D.) BOLSTER DEFENSE\n A.) BOLSTER ATTACK\n");
                                    ConsoleKey userReward = Console.ReadKey(intercept: true).Key;
                                    Console.ForegroundColor = ConsoleColor.Green;

                                    switch (userReward)
                                    {
                                        case ConsoleKey.H:
                                            if (adventurer.Health == adventurer.MaxHealth)
                                            {
                                                Console.ForegroundColor = ConsoleColor.White;
                                                Console.WriteLine("YOU ARE AT MAX HEALTH, PERHAPS ANOTHER REWARD WILL BE MORE USEFUL");
                                                break;
                                            }
                                            else {
                                                adventurer.Health = adventurer.Health + 5;
                                                Console.WriteLine("HEALTH POTION CONSUMED, YOU GAIN +5 HEALTH\n");
                                                choice = true;
                                                break;
                                            }
                                        case ConsoleKey.D:
                                            if (adventurer.Block < 1)
                                            {
                                                Console.ForegroundColor = ConsoleColor.Green;
                                                Console.WriteLine("YOU HAVE RECIEVED A WOODEN SHILED, DEFENSE INCREASED\n");
                                                adventurer.Block = 2;
                                            }
                                            else
                                            {
                                                adventurer.Block = adventurer.Block + 1;
                                                Console.WriteLine("DEFENSE INCREASED\n");
                                            }
                                            choice = true;
                                            break;
                                        case ConsoleKey.A:
                                            adventurer.charWeapon.MaxDamage = adventurer.charWeapon.MaxDamage + 1;
                                            adventurer.HitChance = adventurer.HitChance + 1;
                                            Console.WriteLine("ATTACK INCREASED\n");
                                            choice = true;
                                            break;
                                        default:
                                            Console.ForegroundColor = ConsoleColor.White;

                                            Console.WriteLine("CHOOSE A REWARD");
                                            break;
                                    }
                                    Console.ForegroundColor = ConsoleColor.White;

                                } while (!choice);

                                chamber++;
                                exit = true;

                                if (chamber < 5)
                                {
                                    Console.WriteLine("YOU SALLY FORTH, DEEPER INTO THE DUNGEON");

                                }
                                else
                                {
                                    Console.WriteLine("THE FOG LIFTS, THE MADDENING SPIRITS IN THE AIR SUBSIDE");
                                    Console.WriteLine("A CHEST AT THE BACK OF THE ROOM APPEARS, A MAGICAL WAVE SURGES THE BOX OPEN AND A BRIGHT LIGHT JUMPS FROM THE BOX INTO YOUR WEAPON");
                                    Console.ForegroundColor = ConsoleColor.Green;

                                    Console.WriteLine("\n-------------------\n{0} IS GLOWING WITH ENERGY, YOUR WEAPON HAS BEEN UPGRADED TO LEVEL 2!\n-------------------\n", adventurer.charWeapon.Name);
                                    adventurer.charWeapon.MinDamage = adventurer.charWeapon.MinDamage + 2;
                                    adventurer.charWeapon.MaxDamage = adventurer.charWeapon.MaxDamage + 2;
                                    adventurer.charWeapon.BonusHitChance = adventurer.charWeapon.BonusHitChance + 3;
                                    adventurer.charWeapon.Name = adventurer.charWeapon.Name + " (Level 2)";
                                    Console.ForegroundColor = ConsoleColor.White;

                                    Console.WriteLine("THE GLOWING LIGHT SEEPS FROM YOUR UPGRADED WEAPON AND INTO YOUR HANDS... YOU HAVE REACHED LEVEL 2!");
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine("\n-------------------\nYOU HAVE REACHED LEVEL 2! MAX HEALTH, ATTACK, AND DEFENSE INCREASED!");
                                    Console.ForegroundColor = ConsoleColor.White;

                                    adventurer.MaxHealth = 100;
                                    adventurer.Health = 75;
                                    if (adventurer.Block < 1)
                                    {
                                        Console.ForegroundColor = ConsoleColor.Green;
                                        Console.WriteLine("YOU HAVE RECIEVED A WOODEN SHILED, DEFENSE INCREASED\n-------------------\n");
                                        Console.ForegroundColor = ConsoleColor.White;
                                        adventurer.Block = 5;
                                    }
                                    else
                                    {
                                        adventurer.Block = adventurer.Block + 3;
                                    }
                                    adventurer.HitChance = adventurer.HitChance + 3;







                                }

                            }
                            else
                            {
                                Console.WriteLine("THE MONSTER STILL STANDS, THEY ARE POISED TO STRIKE\n");


                                bool MonstertoHit = (hitChance(monster.HitChance, 0, adventurer.Block));
                                if (MonstertoHit == true)
                                {
                                    int monsterHit = damage(monster.Damage, monster.Damage);
                                    adventurer.Health = adventurer.Health - monsterHit;
                                    Console.WriteLine("{0} LANDED THEIR BLOW", monster.Name);
                                    Console.WriteLine("YOU TAKE {0} DAMAGE\n", monsterHit);
                                }
                                else
                                {
                                    Console.WriteLine("YOU BLOCKED {0}'S STRIKE", monster.Name);
                                }






                                if (adventurer.Health < 1)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("\n*********HERE LIES {0}, VANQUISHED AT THE HANDS OF {1}*********\n", adventurer.Name, monster.Name);
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Environment.Exit(0);
                                }
                                else
                                {
                                    Console.WriteLine("THE BATTLE RAGES ON");
                                }
                            }
                            break;

                        case ConsoleKey.R:
                            Console.WriteLine("THERE IS HONOR IS SELF PRESERVATION\n");
                            Environment.Exit(0);
                            break;

                        case ConsoleKey.P:
                            Console.WriteLine(adventurer.Name);
                            Console.Write("{0} Health remaning\n", adventurer.Health);
                            Console.Write("You Weild {0}\n", adventurer.charWeapon.Name);

                            break;

                        case ConsoleKey.M:
                            Console.WriteLine("Level {0} Monster Detected", monsterLvl);
                            Console.WriteLine(monster.Name);
                            Console.Write("{0} Health remaning\n", monster.Health);
                            break;

                        case ConsoleKey.X:
                            Console.WriteLine("QUEST COMPLETE\n");
                            exit = true;
                            break;

                        default:
                            Console.WriteLine("CHOOSE AN HONORABLE PATH\n");
                            break;
                    }
                } while (!exit);


            } while (chamber < 5);
            #endregion

            Console.WriteLine("YOU HAVE CLEARED THE DUNGEON! WELL DONE ADVENTURER, BUT YOU ARENT DONE YET!\nCONTINUE ON YOUR QUEST, FOR A CALAMITY STILL TERRORIZES OUR LANDS!");

            //CHOOSE TO CONTINUE
            #region

            bool dungeonChoice = false;
            do
            {
                Console.WriteLine("-------------------\nWILL YOU CONTINUE TO THE NEXT DUNGEON? \nY.) YES\nN.) No\n");
                ConsoleKey userHelp = Console.ReadKey(intercept: true).Key;

                switch (userHelp)
                {
                    case ConsoleKey.Y:
                        Console.WriteLine("HUZZAH! THEN I SHALL BESTOW UPON YOU A SKILL TO AID YOU ON YOUR QUEST\n");
                        dungeonChoice = true;
                        break;
                    case ConsoleKey.N:
                        Console.WriteLine("THEN WE WILL WAIT FOR A BRAVE ADVENTURER, UNTIL THEN, WE SUFFER.");
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("CHOOSE AN HONORABLE PATH");
                        break;

                }
            } while (!dungeonChoice);
            #endregion

            //CHOOSE SKILL
            #region
            bool chooseSkill = false;
            do
            {
                Console.WriteLine("HOW WILL YOU REACH GLORY? \n R.) ROGUES LUCK \n C.) CLERICS DEVINE TOUCH \n B.) BARBARIAN RAGE \n I.) BARDIC INTIMIDATION \n S.) SKILL INFO\n X.) MAYHAPS ADVENTURE CALLS ANOTHER DAY...\n");
                ConsoleKey userPerk = Console.ReadKey(intercept: true).Key;
                Console.ForegroundColor = ConsoleColor.Green;

                switch (userPerk)
                {
                    case ConsoleKey.R:
                        Console.WriteLine("ROGUES LUCK, A USEFUL SKILL TO BE SURE!");
                        chooseSkill = true;
                        adventurer.HasPerk = "ROGUES LUCK";
                        break;

                    case ConsoleKey.C:
                        Console.WriteLine("CLERICS DEVINE TOUCH, A USEFUL SKILL TO BE SURE!");
                        chooseSkill = true;
                        adventurer.HasPerk = "CLERICS DEVINE TOUCH";
                        break;

                    case ConsoleKey.B:
                        Console.WriteLine("BARBARIAN RAGE, A USEFUL SKILL TO BE SURE!");
                        chooseSkill = true;
                        adventurer.HasPerk = "BARBARIAN RAGE";
                        break;

                    case ConsoleKey.I:
                        Console.WriteLine("BARDIC INTIMIDATION, A USEFUL SKILL TO BE SURE!");
                        chooseSkill = true;
                        adventurer.HasPerk = "BARDIC INTIMIDATION";
                        break;

                    case ConsoleKey.X:
                        Console.WriteLine("THERE IS HONOR IS SELF PRESERVATION\n");
                        Environment.Exit(0);
                        break;

                    case ConsoleKey.S:
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine(
                            "\nSKILL INFO:\n ROGUES LUCK: YOU FLIP A COIN - HEADS, YOUR FOE DIES. TAILS, YOU AND YOUR FOE TAKE 10 DAMAGE.\n CLERICS DEVINE TOUCH: MAGICAL LIGHTS DANCE AROUND YOU, GRANTS +10 HEALTH TO THE USER.\n BARBARIAN RAGE: YOU ATTACK IN A BLIND RAGE, +10 DAMAGE IF YOUR ATTACK LANDS.\n BARDIC INTIMIDATION: YOU PLAY A SONG THAT WEAKENS YOUR MONSTERS RESOLVE, -4 TO THIER DEFENSE. \n"
                            );
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.White;

                        Console.WriteLine("CHOOSE A SKILL TO USE\n");
                        break;
                }
            } while (!chooseSkill);

            #endregion




            /**************DUNGEON TWO******************/



            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine("\n*********YOU ARE ENTERING THE DUNGEON OF TERROR*********\n");

            int dungeonTwoChamber = 1;

            //DUNGEON 2
            #region
            do
            {
                int monsterLvl = chamber;
                Console.WriteLine("-----------------\nDUNGEON CHAMBER {0}\n-----------------\n", dungeonTwoChamber);


                monsterLvl = monsterMaker();

                string[] monsterNames = { "TICKLES THE SILLY CAT", "RAINBOW THE HAPPY RACCOON", "WADDLE THE PLAYFUL PONY", "POPPY THE SLEEPY PANDA", "COCOA THE HUGGABLE PUPPY" };

                Monster monster = new Monster()
                {
                    Name = monsterNames[dungeonTwoChamber - 1],
                    HitChance = monsterLvl,
                    Health = monsterLvl * 10,
                    Block = monsterLvl + 7,
                    Damage = monsterLvl + 7
                };

                Console.WriteLine("MONSTER DETECTED: {0}", monster.Name);
                Console.ForegroundColor = ConsoleColor.White;

                bool exit = false;

                do
                {
                    Console.WriteLine("\nChoose your Path \n A.) Attack \n R.) Run Away \n S.) Skill \n P.) Player Info \n M.) Monster Info \n X.) Exit \n");
                    ConsoleKey userChoice = Console.ReadKey(intercept: true).Key;

                    switch (userChoice)
                    {
                        case ConsoleKey.A:
                            bool toHit = (hitChance(adventurer.charWeapon.BonusHitChance, adventurer.HitChance, monster.Block));
                            if (toHit == true)
                            {
                                int hit = damage(adventurer.charWeapon.MinDamage, adventurer.charWeapon.MaxDamage);
                                Console.WriteLine("YOU STRUCK YOUR MARK");
                                Console.WriteLine("YOU DEAL {0} DAMAGE\n", hit);
                                monster.Health = monster.Health - hit;
                            }
                            else
                            {
                                Console.WriteLine("YOU MISSED YOUR MARK");
                            }


                            if (monster.Health < 1)
                            {
                                Console.ForegroundColor = ConsoleColor.Green;

                                Console.WriteLine("---------------\nTHE MONSTER HAS BEEN DEFEATED\n---------------\n");
                                Console.ForegroundColor = ConsoleColor.White;

                                bool choice = false;
                                do
                                {
                                    Console.WriteLine("\nCLAIM YOUR REWARD:\n H.) HEALTH POTION\n D.) BOLSTER DEFENSE\n A.) BOLSTER ATTACK\n");
                                    ConsoleKey userReward = Console.ReadKey(intercept: true).Key;
                                    Console.ForegroundColor = ConsoleColor.Green;

                                    switch (userReward)
                                    {
                                        case ConsoleKey.H:
                                            if (adventurer.Health == adventurer.MaxHealth)
                                            {
                                                Console.ForegroundColor = ConsoleColor.White;
                                                Console.WriteLine("YOU ARE AT MAX HEALTH, PERHAPS ANOTHER REWARD WILL BE MORE USEFUL");
                                                break;
                                            }
                                            else
                                            {
                                                adventurer.Health = adventurer.Health + 10;
                                                Console.WriteLine("HEALTH POTION CONSUMED, YOU GAIN +10 HEALTH\n");
                                                choice = true;
                                                break;
                                            }
                                        case ConsoleKey.D:
                                            if (adventurer.Block < 1)
                                            {
                                                Console.WriteLine("YOU HAVE RECIEVED A WOODEN SHILED, DEFENSE INCREASED\n");
                                                adventurer.Block = 2;
                                            }
                                            else
                                            {
                                                adventurer.Block = adventurer.Block + 1;
                                                Console.WriteLine("DEFENSE INCREASED\n");
                                            }
                                            choice = true;
                                            break;
                                        case ConsoleKey.A:
                                            adventurer.charWeapon.MaxDamage = adventurer.charWeapon.MaxDamage + 1;
                                            adventurer.HitChance = adventurer.HitChance + 1;
                                            Console.WriteLine("ATTACK INCREASED\n");
                                            choice = true;
                                            break;
                                        default:
                                            Console.ForegroundColor = ConsoleColor.White;
                                            Console.WriteLine("CHOOSE A REWARD");
                                            break;
                                    }
                                    Console.ForegroundColor = ConsoleColor.White;

                                } while (!choice);

                                dungeonTwoChamber++;
                                exit = true;

                                if (dungeonTwoChamber < 5)
                                {
                                    Console.WriteLine("YOU SALLY FORTH, DEEPER INTO THE DUNGEON");

                                }
                                else
                                {
                                    Console.WriteLine("THE FOG LIFTS, THE MADDENING SPIRITS IN THE AIR SUBSIDE");
                                    Console.WriteLine("A CHEST AT THE BACK OF THE ROOM APPEARS, A MAGICAL WAVE SURGES THE BOX OPEN AND A BRIGHT LIGHT JUMPS FROM THE BOX INTO YOUR HANDS");
                                    Console.ForegroundColor = ConsoleColor.Green;

                                    Console.WriteLine("\n---------------\nYOUR HANDS ARE GLOWING WITH ENERGY, YOUR SKILL HAS BEEN UPGRADED TO LEVEL 2!\n---------------\n", adventurer.HasPerk);

                                    adventurer.HasPerk = adventurer.HasPerk + "Lvl 2";


                                }

                            }
                            else
                            {
                                Console.WriteLine("THE MONSTER STILL STANDS, THEY ARE POISED TO STRIKE\n");
                                bool MonstertoHit = (hitChance(monster.HitChance, 0, adventurer.Block));
                                if (MonstertoHit == true)
                                {
                                    int monsterHit = damage(monster.Damage, monster.Damage);
                                    adventurer.Health = adventurer.Health - monsterHit;
                                    Console.WriteLine("{0} LANDED THEIR BLOW", monster.Name);
                                    Console.WriteLine("YOU TAKE {0} DAMAGE\n", monsterHit);
                                }
                                else
                                {
                                    Console.WriteLine("YOU BLOCKED {0}'S STRIKE", monster.Name);
                                }

                                if (adventurer.Health < 1)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("\n*********HERE LIES {0}, VANQUISHED AT THE HANDS OF {1}*********\n", adventurer.Name, monster.Name);
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Environment.Exit(0);
                                }
                                else
                                {
                                    Console.WriteLine("THE BATTLE RAGES ON");
                                }
                            }
                            break;
                        case ConsoleKey.S:
                            if (adventurer.HasPerk == "ROGUES LUCK")
                            {
                                int coinFlip = roguesLuck();
                                if (coinFlip == 1)
                                {
                                    Console.WriteLine("HEADS, YOUR OPPONENT FEELS THE CUNNING ROGUES POWER");
                                    monster.Health = 0;
                                }
                                else
                                {
                                    Console.WriteLine("BOTH YOU AND {0} FEEL THE CUNNING ROGUES POWER, YOU BOTH TAKE 5 DAMAGE", monster.Name);
                                    monster.Health = monster.Health - 10;
                                    adventurer.Health = adventurer.Health - 10;
                                }
                            }
                            else if (adventurer.HasPerk == "CLERICS DEVINE TOUCH")
                            {
                                if (adventurer.Health == adventurer.MaxHealth)
                                {
                                    Console.WriteLine("YOU ARE AT MAX HEALTH");
                                }
                                else
                                {
                                    adventurer.Health = adventurer.Health + 10;
                                    Console.WriteLine("YOU FEEL THE CLERICS DEVINE TOUCH, YOU GAIN +10 HEALTH\n");
                                }
                            }
                            else if (adventurer.HasPerk == "BARBARIAN RAGE")
                            {
                                Console.WriteLine("ATTACK IN A BARBARIC RAGE, CHARGING WILDLY");
                                toHit = (hitChance(adventurer.charWeapon.BonusHitChance, adventurer.HitChance, monster.Block));
                                if (toHit == true)
                                {
                                    int hit = damage(adventurer.charWeapon.MinDamage, adventurer.charWeapon.MaxDamage) + 10;
                                    Console.WriteLine("YOU STRUCK YOUR MARK");
                                    Console.WriteLine("YOU DEAL {0} DAMAGE\n", hit);
                                    monster.Health = monster.Health - hit;
                                }
                                else
                                {
                                    Console.WriteLine("YOU MISSED YOUR MARK");
                                }
                            }
                            else if (adventurer.HasPerk == "BARDIC INTIMIDATION")
                            {
                                Console.WriteLine("YOU STRUM AN INTENSE SONG, ONE OF ALL YOUR GREATIST TRIUMPHS, YOUR FOE IS INTIMIDATED");
                                Console.WriteLine("{0}'S DEFENSE HAS BEEN LOWERED!", monster.Name);
                                monster.Block = monster.Block - 4;
                            }

                            if (monster.Health < 1)
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("---------------\nTHE MONSTER HAS BEEN DEFEATED\n---------------\n");
                                Console.ForegroundColor = ConsoleColor.White;
                                bool choice = false;
                                do
                                {
                                    Console.WriteLine("\nCLAIM YOUR REWARD:\n H.) HEALTH POTION\n D.) BOLSTER DEFENSE\n A.) BOLSTER ATTACK\n");
                                    ConsoleKey userReward = Console.ReadKey(intercept: true).Key;

                                    switch (userReward)
                                    {
                                        case ConsoleKey.H:
                                            if (adventurer.Health == adventurer.MaxHealth)
                                            {
                                                Console.ForegroundColor = ConsoleColor.White;
                                                Console.WriteLine("YOU ARE AT MAX HEALTH, PERHAPS ANOTHER REWARD WILL BE MORE USEFUL");
                                                break;
                                            }
                                            else
                                            {
                                                adventurer.Health = adventurer.Health + 10;
                                                Console.WriteLine("HEALTH POTION CONSUMED, YOU GAIN +10 HEALTH\n");
                                                choice = true;
                                                break;
                                            }
                                        case ConsoleKey.D:
                                            if (adventurer.Block < 1)
                                            {
                                                Console.WriteLine("YOU HAVE RECIEVED A WOODEN SHILED, DEFENSE INCREASED\n");
                                                adventurer.Block = 2;
                                            }
                                            else
                                            {
                                                adventurer.Block = adventurer.Block + 1;
                                                Console.WriteLine("DEFENSE INCREASED\n");
                                            }
                                            choice = true;
                                            break;
                                        case ConsoleKey.A:
                                            adventurer.charWeapon.MaxDamage = adventurer.charWeapon.MaxDamage + 1;
                                            adventurer.HitChance = adventurer.HitChance + 1;
                                            Console.WriteLine("ATTACK INCREASED\n");
                                            choice = true;
                                            break;
                                        default:
                                            Console.ForegroundColor = ConsoleColor.White;
                                            Console.WriteLine("CHOOSE A REWARD");
                                            break;
                                    }
                                    Console.ForegroundColor = ConsoleColor.White;

                                } while (!choice);

                                dungeonTwoChamber++;
                                exit = true;

                                if (dungeonTwoChamber < 5)
                                {
                                    Console.WriteLine("YOU SALLY FORTH, DEEPER INTO THE DUNGEON");

                                }
                                else
                                {
                                    Console.WriteLine("THE FOG LIFTS, THE MADDENING SPIRITS IN THE AIR SUBSIDE");
                                    Console.WriteLine("A CHEST AT THE BACK OF THE ROOM APPEARS, A MAGICAL WAVE SURGES THE BOX OPEN AND A BRIGHT LIGHT JUMPS FROM THE BOX INTO YOUR HANDS");
                                    Console.ForegroundColor = ConsoleColor.Green;

                                    Console.WriteLine("\n---------------\nYOUR HANDS ARE GLOWING WITH ENERGY, YOUR SKILL HAS BEEN UPGRADED TO LEVEL 2!\n---------------\n", adventurer.HasPerk);

                                    adventurer.HasPerk = adventurer.HasPerk + "Lvl 2";
                                    Console.ForegroundColor = ConsoleColor.White;


                                }

                            }
                            else
                            {
                                Console.WriteLine("THE MONSTER STILL STANDS, THEY ARE POISED TO STRIKE\n");
                                bool MonstertoHit = (hitChance(monster.HitChance, 0, adventurer.Block));
                                if (MonstertoHit == true)
                                {
                                    int monsterHit = damage(monster.Damage, monster.Damage);
                                    adventurer.Health = adventurer.Health - monsterHit;
                                    Console.WriteLine("{0} LANDED THEIR BLOW", monster.Name);
                                    Console.WriteLine("YOU TAKE {0} DAMAGE\n", monsterHit);
                                }
                                else
                                {
                                    Console.WriteLine("YOU BLOCKED {0}'S STRIKE", monster.Name);
                                }

                                if (adventurer.Health < 1)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("\n*********HERE LIES {0}, VANQUISHED AT THE HANDS OF {1}*********\n", adventurer.Name, monster.Name);
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Environment.Exit(0);

                                }
                                else
                                {
                                    Console.WriteLine("THE BATTLE RAGES ON");
                                }
                            }

                            break;
                        case ConsoleKey.R:
                            Console.WriteLine("THERE IS HONOR IS SELF PRESERVATION\n");
                            Environment.Exit(0);
                            break;

                        case ConsoleKey.P:
                            Console.WriteLine(adventurer.Name);
                            Console.Write("{0} Health remaning\n", adventurer.Health);
                            Console.Write("You Weild {0}\n", adventurer.charWeapon.Name);
                            Console.WriteLine("Skill Enabled: {0}", adventurer.HasPerk);

                            break;

                        case ConsoleKey.M:
                            Console.WriteLine("Level {0} Monster Detected", monsterLvl);
                            Console.WriteLine(monster.Name);
                            Console.Write("{0} Health remaning\n", monster.Health);
                            break;

                        case ConsoleKey.X:
                            Console.WriteLine("QUEST COMPLETE\n");
                            exit = true;
                            break;

                        default:
                            Console.WriteLine("CHOOSE AN HONORABLE PATH\n");
                            break;
                    }
                } while (!exit);


            } while (dungeonTwoChamber < 5);
            #endregion
            Console.ForegroundColor = ConsoleColor.White;


            Console.WriteLine("THE GROUND BENEATH YOU BEGINS TO QUAKE.......\n");
            Console.WriteLine("THE DUNGEON SPLITS OPEN WITH A CRACK AND OUT POURS A DISGUSTINGLY EVIL FOE\n");



            Villan villian = new Villan()
            {
                Name = "TORMONT: DEATH INCARNATE",
                Health = 200,
                HitChance = 10,
                Block = 17,
                Damage = 17
            };



            Console.WriteLine("{0} RISES FROM HIS SLUMBER.\n", villian.Name);
            Console.WriteLine("{0} YOU ARE A FOOL, YOU HAVE DEFEATED THOSE WHO GAURD MY TOMB, AND SET ME FREE.\n", adventurer.Name);
            Console.WriteLine("NOW YOU TOO SHALL DIE.\n", adventurer.Name);


            bool victory = false;
                do
                {
                    Console.WriteLine("\nChoose your Path \n A.) Attack \n R.) Run Away \n S.) Skill \n P.) Player Info \n M.) Monster Info \n");
                    ConsoleKey userChoice = Console.ReadKey(intercept: true).Key;

                    switch (userChoice)
                    {
                        case ConsoleKey.A:
                            bool toHit = (hitChance(adventurer.charWeapon.BonusHitChance, adventurer.HitChance, villian.Block));
                            if (toHit == true)
                            {
                                int hit = damage(adventurer.charWeapon.MinDamage, adventurer.charWeapon.MaxDamage);
                                Console.WriteLine("YOU STRUCK YOUR MARK");
                                Console.WriteLine("YOU DEAL {0} DAMAGE\n", hit);
                                villian.Health = villian.Health - hit;
                            }
                            else
                            {
                                Console.WriteLine("YOU MISSED YOUR MARK");
                            }


                            if (villian.Health < 1)
                            {
                                Console.ForegroundColor = ConsoleColor.Green;

                                Console.WriteLine("---------------\nTHE MONSTER HAS BEEN DEFEATED\n---------------\n");
                                Console.ForegroundColor = ConsoleColor.White;

                                bool choice = false;
                                do
                                {
                                    Console.WriteLine("\nCLAIM YOUR REWARD:\n H.) HEALTH POTION\n D.) BOLSTER DEFENSE\n A.) BOLSTER ATTACK\n");
                                    ConsoleKey userReward = Console.ReadKey(intercept: true).Key;
                                    Console.ForegroundColor = ConsoleColor.Green;

                                    switch (userReward)
                                    {
                                        case ConsoleKey.H:
                                            if (adventurer.Health == adventurer.MaxHealth)
                                            {
                                                Console.ForegroundColor = ConsoleColor.White;
                                                Console.WriteLine("YOU ARE AT MAX HEALTH, PERHAPS ANOTHER REWARD WILL BE MORE USEFUL");
                                                break;
                                            }
                                            else
                                            {
                                                adventurer.Health = adventurer.Health + 15;
                                                Console.WriteLine("HEALTH POTION CONSUMED, YOU GAIN +10 HEALTH\n");
                                                choice = true;
                                                break;
                                            }
                                        case ConsoleKey.D:
                                                adventurer.Block = adventurer.Block + 2;
                                                Console.WriteLine("DEFENSE INCREASED\n");
                                            break;
                                        case ConsoleKey.A:
                                            adventurer.charWeapon.MaxDamage = adventurer.charWeapon.MaxDamage + 2;
                                            adventurer.HitChance = adventurer.HitChance + 2;
                                            Console.WriteLine("ATTACK INCREASED\n");
                                            choice = true;
                                            break;
                                        default:
                                            Console.ForegroundColor = ConsoleColor.White;
                                            Console.WriteLine("CHOOSE A REWARD");
                                            break;
                                    }
                                    Console.ForegroundColor = ConsoleColor.White;

                                } while (!choice);

                                victory = true;

                            }
                            else
                            {
                                Console.WriteLine("THE MONSTER STILL STANDS, THEY ARE POISED TO STRIKE\n");
                                bool MonstertoHit = (hitChance(villian.HitChance, 0, adventurer.Block));
                                if (MonstertoHit == true)
                                {
                                    int monsterHit = damage(villian.Damage, villian.Damage);
                                    adventurer.Health = adventurer.Health - monsterHit;
                                    Console.WriteLine("{0} LANDED THEIR BLOW", villian.Name);
                                    Console.WriteLine("YOU TAKE {0} DAMAGE\n", monsterHit);
                                }
                                else
                                {
                                    Console.WriteLine("YOU BLOCKED {0}'S STRIKE", villian.Name);
                                }

                                if (adventurer.Health < 1)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("\n*********HERE LIES {0}, VANQUISHED AT THE HANDS OF {1}*********\n", adventurer.Name, villian.Name);
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Environment.Exit(0);
                                }
                                else
                                {
                                    Console.WriteLine("THE BATTLE RAGES ON");
                                }
                            }
                            break;
                        case ConsoleKey.S:
                            if (adventurer.HasPerk == "ROGUES LUCK")
                            {
                                int coinFlip = roguesLuck();
                                if (coinFlip == 1)
                                {
                                    Console.WriteLine("HEADS, YOUR OPPONENT FEELS THE CUNNING ROGUES POWER");
                                    villian.Health = 0;
                                }
                                else
                                {
                                    Console.WriteLine("BOTH YOU AND {0} FEEL THE CUNNING ROGUES POWER, YOU BOTH TAKE 5 DAMAGE", villian.Name);
                                    villian.Health = villian.Health - 10;
                                    adventurer.Health = adventurer.Health - 10;
                                }
                            }
                            else if (adventurer.HasPerk == "CLERICS DEVINE TOUCH")
                            {
                                if (adventurer.Health == adventurer.MaxHealth)
                                {
                                    Console.WriteLine("YOU ARE AT MAX HEALTH");
                                }
                                else
                                {
                                    adventurer.Health = adventurer.Health + 10;
                                    Console.WriteLine("YOU FEEL THE CLERICS DEVINE TOUCH, YOU GAIN +10 HEALTH\n");
                                }
                            }
                            else if (adventurer.HasPerk == "BARBARIAN RAGE")
                            {
                                Console.WriteLine("ATTACK IN A BARBARIC RAGE, CHARGING WILDLY");
                                toHit = (hitChance(adventurer.charWeapon.BonusHitChance, adventurer.HitChance, villian.Block));
                                if (toHit == true)
                                {
                                    int hit = damage(adventurer.charWeapon.MinDamage, adventurer.charWeapon.MaxDamage) + 10;
                                    Console.WriteLine("YOU STRUCK YOUR MARK");
                                    Console.WriteLine("YOU DEAL {0} DAMAGE\n", hit);
                                     villian.Health = villian.Health - hit;
                                }
                                else
                                {
                                    Console.WriteLine("YOU MISSED YOUR MARK");
                                }
                            }
                            else if (adventurer.HasPerk == "BARDIC INTIMIDATION")
                            {
                                Console.WriteLine("YOU STRUM AN INTENSE SONG, ONE OF ALL YOUR GREATIST TRIUMPHS, YOUR FOE IS INTIMIDATED");
                                Console.WriteLine("{0}'S DEFENSE HAS BEEN LOWERED!", villian.Name);
                                villian.Block = villian.Block - 4;
                            }

                            if (villian.Health < 1)
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("---------------\nTHE MONSTER HAS BEEN DEFEATED\n---------------\n");
                                Console.ForegroundColor = ConsoleColor.White;
                                bool choice = false;
                                do
                                {
                                    Console.WriteLine("\nCLAIM YOUR REWARD:\n H.) HEALTH POTION\n D.) BOLSTER DEFENSE\n A.) BOLSTER ATTACK\n");
                                    ConsoleKey userReward = Console.ReadKey(intercept: true).Key;

                                    switch (userReward)
                                    {
                                        case ConsoleKey.H:
                                            if (adventurer.Health == adventurer.MaxHealth)
                                            {
                                                Console.ForegroundColor = ConsoleColor.White;
                                                Console.WriteLine("YOU ARE AT MAX HEALTH, PERHAPS ANOTHER REWARD WILL BE MORE USEFUL");
                                                break;
                                            }
                                            else
                                            {
                                                adventurer.Health = adventurer.Health + 10;
                                                Console.WriteLine("HEALTH POTION CONSUMED, YOU GAIN +10 HEALTH\n");
                                                choice = true;
                                                break;
                                            }
                                        case ConsoleKey.D:
                                            if (adventurer.Block < 1)
                                            {
                                                Console.WriteLine("YOU HAVE RECIEVED A WOODEN SHILED, DEFENSE INCREASED\n");
                                                adventurer.Block = 2;
                                            }
                                            else
                                            {
                                                adventurer.Block = adventurer.Block + 1;
                                                Console.WriteLine("DEFENSE INCREASED\n");
                                            }
                                            choice = true;
                                            break;
                                        case ConsoleKey.A:
                                            adventurer.charWeapon.MaxDamage = adventurer.charWeapon.MaxDamage + 1;
                                            adventurer.HitChance = adventurer.HitChance + 1;
                                            Console.WriteLine("ATTACK INCREASED\n");
                                            choice = true;
                                            break;
                                        default:
                                            Console.ForegroundColor = ConsoleColor.White;
                                            Console.WriteLine("CHOOSE A REWARD");
                                            break;
                                    }
                                    Console.ForegroundColor = ConsoleColor.White;

                                } while (!choice);

                                
                                victory = true;

                                if (dungeonTwoChamber < 5)
                                {
                                    Console.WriteLine("YOU SALLY FORTH, DEEPER INTO THE DUNGEON");

                                }
                                else
                                {
                                    Console.WriteLine("THE FOG LIFTS, THE MADDENING SPIRITS IN THE AIR SUBSIDE");
                                    Console.WriteLine("A CHEST AT THE BACK OF THE ROOM APPEARS, A MAGICAL WAVE SURGES THE BOX OPEN AND A BRIGHT LIGHT JUMPS FROM THE BOX INTO YOUR HANDS");
                                    Console.ForegroundColor = ConsoleColor.Green;

                                    Console.WriteLine("\n---------------\nYOUR HANDS ARE GLOWING WITH ENERGY, YOUR SKILL HAS BEEN UPGRADED TO LEVEL 2!\n---------------\n", adventurer.HasPerk);

                                    adventurer.HasPerk = adventurer.HasPerk + "Lvl 2";
                                    Console.ForegroundColor = ConsoleColor.White;


                                }

                            }
                            else
                            {
                                Console.WriteLine("THE MONSTER STILL STANDS, THEY ARE POISED TO STRIKE\n");
                                bool MonstertoHit = (hitChance(villian.HitChance, 0, adventurer.Block));
                                if (MonstertoHit == true)
                                {
                                    int monsterHit = damage(villian.Damage, villian.Damage);
                                    adventurer.Health = adventurer.Health - monsterHit;
                                    Console.WriteLine("{0} LANDED THEIR BLOW", villian.Name);
                                    Console.WriteLine("YOU TAKE {0} DAMAGE\n", monsterHit);
                                }
                                else
                                {
                                    Console.WriteLine("YOU BLOCKED {0}'S STRIKE", villian.Name);
                                }

                                if (adventurer.Health < 1)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("\n*********HERE LIES {0}, VANQUISHED AT THE HANDS OF {1}*********\n", adventurer.Name, villian.Name);
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Environment.Exit(0);

                                }
                                else
                                {
                                    Console.WriteLine("THE BATTLE RAGES ON");
                                }
                            }

                            break;
                        case ConsoleKey.R:
                            Console.WriteLine("THERE IS NO ESCAPE\n");
                            Environment.Exit(0);
                            break;

                        case ConsoleKey.P:
                            Console.WriteLine(adventurer.Name);
                            Console.Write("{0} Health remaning\n", adventurer.Health);
                            Console.Write("You Weild {0}\n", adventurer.charWeapon.Name);
                            Console.WriteLine("Skill Enabled: {0}", adventurer.HasPerk);

                            break;

                        case ConsoleKey.M:
                            Console.WriteLine("{0} Detected", villian.Name);
                            Console.WriteLine(villian.Name);
                            Console.Write("{0} Health remaning\n", villian.Health);
                            break;

                        

                        default:
                            Console.WriteLine("CHOOSE AN HONORABLE PATH\n");
                            break;
                    }
                } while (!victory);











































        }   

        static bool hitChance(int weaponBonus, int playerBonus, int monsterAC)
        {
            int bonusSum = weaponBonus + playerBonus;
            Random rand = new Random();
            int hitRoll = rand.Next(1, 21);
            int toHit = bonusSum + hitRoll;
            if (toHit > monsterAC)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        static int damage(int minDamage, int maxDamage)
        {
            Random rand = new Random();
            int damage = rand.Next(minDamage, maxDamage);
            return damage;
            
        }

        static int monsterMaker()
        {
            Random rand = new Random();
            int level = rand.Next(5, 9);
            return level;
        }


        static int roguesLuck()
        {
            Random rand = new Random();
            int level = rand.Next(1, 3);
            return level;
        }
        }

}














