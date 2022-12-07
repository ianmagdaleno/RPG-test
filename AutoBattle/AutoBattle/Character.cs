using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using static AutoBattle.Types;

namespace AutoBattle
{
    public class Character
    {
        public string Name { get; set; }
        public float Health;
        public float BaseDamage;
        public float DamageMultiplier { get; set; }
        public float range = 1;
        public GridBox currentBox;
        public int PlayerIndex;
        public int CharacterClassIndex;
        public bool oneActivation;

        public Character Target { get; set; }
        public Character(CharacterClass characterClass)
        {
            switch (characterClass)
            {
                case CharacterClass.Paladin:
                    CharacterClassIndex = 1;
                    break;
                case CharacterClass.Warrior:
                    CharacterClassIndex = 2;
                    break;
                case CharacterClass.Cleric:
                    CharacterClassIndex = 3;
                    break;
                case CharacterClass.Archer:
                    CharacterClassIndex = 4;
                    break;
            }
        }
        public void PassiveHability(int characterClass, Character character)
        {
            switch (characterClass)
            {
                case 1:
                    character.oneActivation = false;
                    character.Health = 40;
                    Console.WriteLine("Paladin is Revive !!");
                    break;
                case 2:
                    Console.WriteLine("warrior passiva ativada");
                    break;
                case 3:
                    Console.WriteLine("cleric passiva ativada");
                    break;
                case 4:
                    Console.WriteLine("archer passiva ativada");
                    break;
            }
        }
        public void HabilityActive(int characterClass)
        {
            switch (characterClass)
            {
                case 1:
                    Console.WriteLine("Paladino ativada");
                    break;
                case 2:
                    Console.WriteLine("warrior ativada");
                    break;
                case 3:
                    Console.WriteLine("cleric ativada");
                    break;
                case 4:
                    Console.WriteLine("archer ativada");
                    break;
            }
        }
        public bool TakeDamage(float amount, GridBox currentBox, Grid battlefield, Character target)
        {
            if ((Health -= BaseDamage) <= 0)
            {
                if(target.CharacterClassIndex == 1 && target.oneActivation)//if target a paladin
                {
                    PassiveHability(target.CharacterClassIndex,target);
                }
                else
                {
                    Die(currentBox, battlefield);
                    return true;
                }
            }
            return false;
        }

        public void Die(GridBox currentBox, Grid battlefield)
        {
            currentBox.ocupied = false;
            currentBox.isOwner = false;
            battlefield.grids[GetIndex(currentBox, battlefield)] = currentBox;
            battlefield.drawBattlefield();
        }

        public void WalkTO(string command, Grid battlefield, bool doubleWalk)
        {
            switch (command)
            {
                case "w":
                    if (!battlefield.grids.Find(x => x.Index == currentBox.Index - 10).ocupied)
                    {
                        currentBox.ocupied = false;
                        currentBox.isOwner = false;
                        battlefield.grids[GetIndex(currentBox, battlefield)] = currentBox;
                        currentBox = (battlefield.grids.Find(x => x.Index == currentBox.Index - 10));
                        currentBox.ocupied = true;
                        currentBox.isOwner = true;
                        battlefield.grids[GetIndex(currentBox, battlefield)] = currentBox;
                        battlefield.drawBattlefield();
                        Console.WriteLine("player - up");
                    }
                   else
                    {
                        Console.WriteLine("blocked action, please insert a valid command");
                        StartTurn(battlefield);
                    }
                    break;
                case "a":
                    if (!battlefield.grids.Find(x => x.Index == currentBox.Index - 1).ocupied)
                    {
                        currentBox.ocupied = false;
                        currentBox.isOwner = false;
                        battlefield.grids[GetIndex(currentBox, battlefield)] = currentBox;
                        currentBox = (battlefield.grids.Find(x => x.Index == currentBox.Index - 1));
                        currentBox.ocupied = true;
                        currentBox.isOwner = true;
                        battlefield.grids[GetIndex(currentBox, battlefield)] = currentBox;
                        battlefield.drawBattlefield();
                        Console.WriteLine("player - left");
                    }
                    else
                    {
                        Console.WriteLine("blocked action, please insert a valid command");
                        StartTurn(battlefield);
                    }
                    break;
                case "s":
                    if (!battlefield.grids.Find(x => x.Index == currentBox.Index + 10).ocupied)
                    {
                        currentBox.ocupied = false;
                        currentBox.isOwner = false;
                        battlefield.grids[GetIndex(currentBox, battlefield)] = currentBox;
                        currentBox = (battlefield.grids.Find(x => x.Index == currentBox.Index + 10));
                        currentBox.ocupied = true;
                        currentBox.isOwner = true;
                        battlefield.grids[GetIndex(currentBox, battlefield)] = currentBox;
                        battlefield.drawBattlefield();
                        Console.WriteLine("player - down");
                    }
                    else
                    {
                        Console.WriteLine("blocked action, please insert a valid command");
                        StartTurn(battlefield);
                    }
                    break;
                case "d":
                    if (!battlefield.grids.Find(x => x.Index == currentBox.Index + 1).ocupied)
                    {
                        currentBox.ocupied = false;
                        currentBox.isOwner = false;
                        battlefield.grids[GetIndex(currentBox, battlefield)] = currentBox;
                        currentBox = (battlefield.grids.Find(x => x.Index == currentBox.Index + 1));
                        currentBox.ocupied = true;
                        currentBox.isOwner = true;
                        battlefield.grids[GetIndex(currentBox, battlefield)] = currentBox;
                        battlefield.drawBattlefield();
                        Console.WriteLine("player - right ");
                    }
                    else
                    {
                        Console.WriteLine("blocked action, please insert a valid command");
                        Console.WriteLine();
                        StartTurn(battlefield);
                    }
                    break;
                default:
                    Console.WriteLine("please insert a valid command");
                    //method input behind
                    break;
            }
            if (doubleWalk)
            {
                //special case to double moviment, increase her the options for the second move
                WalkTO(command, battlefield, false);
            }
        }

        public void StartTurn(Grid battlefield)
        {
            if (this.currentBox.isOwner)
            {
                InputMove(battlefield);
            }
            else
            {
                // automatic moviment is just for the bots, player have choice
                // if there is no target close enough, calculates in wich direction this character should move to be closer to a possible target
                if (CheckCloseTargets(battlefield,this))
                {
                    Attack(Target, battlefield);
                    return;
                }
                else
                {
                    if (this.currentBox.xIndex > Target.currentBox.xIndex)
                    {
                        if ((battlefield.grids.Exists(x => x.Index == currentBox.Index - 10)))
                        {
                            currentBox.ocupied = false;
                            battlefield.grids[GetIndex(currentBox,battlefield)] = currentBox;
                            currentBox = (battlefield.grids.Find(x => x.Index == currentBox.Index - 10));
                            currentBox.ocupied = true;
                            battlefield.grids[GetIndex(currentBox, battlefield)] = currentBox;
                            Console.WriteLine($"Player {PlayerIndex} walked up\n");
                            battlefield.drawBattlefield();
                            return;
                        }
                    }
                    else if (currentBox.xIndex < Target.currentBox.xIndex)
                    {
                        if ((battlefield.grids.Exists(x => x.Index == currentBox.Index + 10)))
                        {
                            currentBox.ocupied = false;
                            battlefield.grids[GetIndex(currentBox, battlefield)] = currentBox;
                            currentBox = (battlefield.grids.Find(x => x.Index == currentBox.Index + 10));
                            currentBox.ocupied = true;
                            battlefield.grids[GetIndex(currentBox, battlefield)] = currentBox;
                            Console.WriteLine($"Player {PlayerIndex} walked down\n");
                            battlefield.drawBattlefield();
                            return;
                        }
                    }
                    else if (currentBox.yIndex > Target.currentBox.yIndex)
                    {
                        if ((battlefield.grids.Exists(y => y.Index == currentBox.Index - 1)))
                        {
                            currentBox.ocupied = false;
                            battlefield.grids[GetIndex(currentBox, battlefield)] = currentBox;
                            currentBox = (battlefield.grids.Find(x => x.Index == currentBox.Index - 1));
                            currentBox.ocupied = true;
                            battlefield.grids[GetIndex(currentBox, battlefield)] = currentBox;
                            Console.WriteLine($"Player {PlayerIndex} walked left\n");
                            battlefield.drawBattlefield();
                            return;
                        }
                    }
                    else if (currentBox.yIndex < Target.currentBox.yIndex)
                    {
                        if ((battlefield.grids.Exists(y => y.Index == currentBox.Index + 1)))
                        {
                            currentBox.ocupied = false;
                            battlefield.grids[GetIndex(currentBox, battlefield)] = currentBox;
                            currentBox = (battlefield.grids.Find(x => x.Index == currentBox.Index + 1));
                            currentBox.ocupied = true;
                            battlefield.grids[GetIndex(currentBox, battlefield)] = currentBox;
                            Console.WriteLine($"Player {PlayerIndex} walked right\n");
                            battlefield.drawBattlefield();
                            return;
                        }
                    }
                }
            }
        }
        // Check in x and y directions if there is any character close enough to be a target.
        bool CheckCloseTargets(Grid battlefield, Character character)
        {

            bool left = battlefield.grids.Find(x => x.Index == currentBox.Index - 1 * character.range).ocupied;
            bool right = battlefield.grids.Find(x => x.Index == currentBox.Index + 1 * character.range).ocupied;
            bool up = battlefield.grids.Find(x => x.Index == currentBox.Index - 10 * character.range).ocupied;
            bool down = battlefield.grids.Find(x => x.Index == currentBox.Index + 10 * character.range).ocupied;
            
            
            if (left || right || up || down)
            {
                return true;
            }
            return false;
        }
        int GetIndex(GridBox current, Grid battlefield)
        {
            //Find the true index of the list
            for(int i = 0; i < battlefield.grids.Count; i++)
            {
                if (battlefield.grids[i].Index == current.Index)
                {
                    return i;
                }
            }
            return 0;
        }
        public void Attack(Character target, Grid battlefield)
        {
            var rand = new Random();
            target.TakeDamage(rand.Next(0, (int)BaseDamage),target.currentBox, battlefield, target);
            Console.WriteLine($"Player {PlayerIndex} is attacking the player {Target.PlayerIndex} and did {BaseDamage} damage\n");
        }
        void InputMove(Grid battlefield)
        {
            Console.WriteLine("[W] Up  ,[A] Left ,[S] Down ,[D] Right ,[Q]Attack");
            string choice = Console.ReadLine();
            
            if(choice == "w" || choice == "a" || choice == "s" || choice == "d")
            {
                WalkTO(choice, battlefield, false);
            }
            else if (choice == "x"){
                Console.WriteLine("skipped");
            }
            else if(choice == "q")
            {
                if (CheckCloseTargets(battlefield,this))
                {
                    Attack(Target, battlefield);
                }
            }
        }
    }
}
