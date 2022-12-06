using System;
using static AutoBattle.Character;
using static AutoBattle.Grid;
using System.Collections.Generic;
using System.Linq;
using static AutoBattle.Types;

namespace AutoBattle
{
    class Program
    {
        public static int sizeC;
        public static int sizeL;
        public static int TeamSize;
        static void Main(string[] args)
        {
            GetGridChoice();
            Grid grid = new Grid(sizeL, sizeC);
            CharacterClass playerCharacterClass;
            GridBox PlayerCurrentLocation;
            GridBox EnemyCurrentLocation;
            Character PlayerCharacter;
            Character EnemyCharacter;
            List<Character> AllPlayers = new List<Character>();
            int currentTurn = 0;
            int numberOfPossibleTiles = grid.grids.Count;
            Setup();


            void Setup()
            {
                GetPlayerChoice();
            }
            
            void GetGridChoice()
            {
                Console.WriteLine("Choose the size of the grid for the table:\n");
                Console.WriteLine("*Table size influences gameplay and team size\n");
                Console.WriteLine("[1](5x5) Small/ 1 Player    [2](8x8) Mid/ 2 Players    [3](8x10) Large/ 3 Players");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        sizeC = 5;
                        sizeL = 5;
                        TeamSize = 1;
                        break;
                    case "2":
                        sizeC = 8;
                        sizeL = 8;
                        TeamSize = 2;
                        break;
                    case "3":
                        sizeC = 8;
                        sizeL = 10;
                        TeamSize = 3;
                        break;
                    default:
                        Console.WriteLine("Insert a accepted value");
                        Console.WriteLine();
                        GetGridChoice();
                        break;
                }
            }

            void GetPlayerChoice()
            {
                //asks for the player to choose between for possible classes via console.
                Console.WriteLine("Choose Between One of this Classes:\n");
                Console.WriteLine("[1] Paladin, [2] Warrior, [3] Cleric, [4] Archer");
                //store the player choice in a variable
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        CreatePlayerCharacter(Int32.Parse(choice));
                        break;
                    case "2":
                        CreatePlayerCharacter(Int32.Parse(choice));
                        break;
                    case "3":
                        CreatePlayerCharacter(Int32.Parse(choice));
                        break;
                    case "4":
                        CreatePlayerCharacter(Int32.Parse(choice));
                        break;
                    default:
                        GetPlayerChoice();
                        break;
                }
            }

            void CreatePlayerCharacter(int classIndex)
            {
                for(int i = 0; i < TeamSize; i++)
                {
                    CharacterClass characterClass = (CharacterClass)classIndex;
                    Console.WriteLine($"Player Class Choice: {characterClass}");
                    PlayerCharacter = new Character(characterClass);
                    PlayerCharacter.Health = 100;
                    PlayerCharacter.BaseDamage = 20;
                    PlayerCharacter.PlayerIndex = i;
                    CreateEnemyCharacter(i);
                }
            }

            void CreateEnemyCharacter(int i)
            {
                //randomly choose the enemy class and set up vital variables
                var rand = new Random();
                int randomInteger = rand.Next(1, 4);
                CharacterClass enemyClass = (CharacterClass)randomInteger;
                Console.WriteLine($"Enemy Class Choice: {enemyClass}");
                EnemyCharacter = new Character(enemyClass);
                EnemyCharacter.Health = 100;
                PlayerCharacter.BaseDamage = 20;
                PlayerCharacter.PlayerIndex = 1 + i;
                StartGame();
            }

            void StartGame()
            {
                Console.WriteLine(AllPlayers.Count);
                //populates the character variables and targets
                EnemyCharacter.Target = PlayerCharacter;
                PlayerCharacter.Target = EnemyCharacter;
                for (int i = 0; i < TeamSize; i++)
                {
                    AllPlayers.Add(PlayerCharacter);
                    AllPlayers.Add(EnemyCharacter);
                }
                AlocatePlayers();
                StartTurn();

            }

            void StartTurn()
            {
                foreach (Character character in AllPlayers)
                {   
                    character.StartTurn(grid);
                }
                currentTurn++;
                HandleTurn();
            }

            void HandleTurn()
            {
                if (PlayerCharacter.Health == 0)
                {
                    Console.WriteLine("fim da linha amigao, tente novamente");//TO DO modo de reiniciar
                    return;
                }
                else if (EnemyCharacter.Health == 0)
                {
                    Console.Write(Environment.NewLine + Environment.NewLine);

                    // endgame?

                    Console.Write(Environment.NewLine + Environment.NewLine);
                    return;
                }
                else
                {
                    Console.Write(Environment.NewLine + Environment.NewLine);
                    Console.WriteLine("Click on any key to start the next turn...\n");
                    Console.Write(Environment.NewLine + Environment.NewLine);

                    ConsoleKeyInfo key = Console.ReadKey();
                    StartTurn();
                }
            }

            int GetRandomInt(int max)
            {
                System.Random range = new System.Random();
                int random = range.Next(0, max);
                return random;
            }

            void AlocatePlayers()
            {
                AlocatePlayerCharacter();

            }

            void AlocatePlayerCharacter()
            {
                for (int i = 0; i < TeamSize; i++)
                {
                    int random = GetRandomInt(grid.grids.Count);

                    GridBox RandomLocation = (grid.grids.ElementAt(random));
                    if (!RandomLocation.ocupied)
                    {
                        GridBox PlayerCurrentLocation = RandomLocation;
                        RandomLocation.ocupied = true;
                        RandomLocation.isOwner = true;//teste
                        grid.grids[random] = RandomLocation;
                        PlayerCharacter.currentBox = grid.grids[random];
                        AlocateEnemyCharacter();
                    }
                    else
                    {
                        AlocatePlayerCharacter();
                    }
                }
            }

            void AlocateEnemyCharacter()
            {
                int random = GetRandomInt(grid.grids.Count);

                GridBox RandomLocation = (grid.grids.ElementAt(random)); 
                if (!RandomLocation.ocupied)
                {
                    EnemyCurrentLocation = RandomLocation;
                    RandomLocation.ocupied = true;
                    grid.grids[random] = RandomLocation;
                    EnemyCharacter.currentBox = grid.grids[random];
                    grid.drawBattlefield();
                }
                else
                {
                    AlocateEnemyCharacter();
                }
            }
        }
    }
}

