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
        static void Main(string[] args)
        {
            int size = GetGridChoice();
            Grid grid = new Grid(size, size);
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
            
            int GetGridChoice()
            {
                Console.WriteLine("Choose the size of the grid for the table:\n");
                Console.WriteLine("*The size of the table influence the gameplay\n");
                Console.WriteLine("[1](5x5) Small  [2](8x8) Mid  [3](10x10) Large");
                string choice = Console.ReadLine();

                int result = 0;
                switch (choice)
                {
                    case "1":
                        result = 5;
                        break;
                    case "2":
                        result = 8;
                        break;
                    case "3":
                        result = 10;
                        break;
                    default:
                        Console.WriteLine("Insert a accepted value");
                        Console.WriteLine();
                        GetGridChoice();
                        break;
                }
                return result; 
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
                CharacterClass characterClass = (CharacterClass)classIndex;
                Console.WriteLine($"Player Class Choice: {characterClass}");
                PlayerCharacter = new Character(characterClass);
                PlayerCharacter.Health = 100;
                PlayerCharacter.BaseDamage = 20;
                PlayerCharacter.PlayerIndex = 0;
                CreateEnemyCharacter();
            }

            void CreateEnemyCharacter()
            {
                //randomly choose the enemy class and set up vital variables
                var rand = new Random();
                int randomInteger = rand.Next(1, 4);
                CharacterClass enemyClass = (CharacterClass)randomInteger;
                Console.WriteLine($"Enemy Class Choice: {enemyClass}");
                EnemyCharacter = new Character(enemyClass);
                EnemyCharacter.Health = 100;
                PlayerCharacter.BaseDamage = 20;
                PlayerCharacter.PlayerIndex = 1;
                StartGame();
            }

            void StartGame()
            {
                //populates the character variables and targets
                EnemyCharacter.Target = PlayerCharacter;
                PlayerCharacter.Target = EnemyCharacter;
                AllPlayers.Add(PlayerCharacter);
                AllPlayers.Add(EnemyCharacter);
                AlocatePlayers();
                StartTurn();

            }

            void StartTurn()
            {
                if (currentTurn == 0)
                {
                    //AllPlayers.Sort();
                }
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
                    grid.drawBattlefield(size, size);
                }
                else
                {
                    AlocateEnemyCharacter();
                }
            }
        }
    }
}

