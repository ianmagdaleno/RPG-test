using System;
using System.Collections.Generic;
using System.Text;

namespace AutoBattle
{
    public class Types
    {

        public struct CharacterClassSpecific
        {
            CharacterClass CharacterClass;
            float hpModifier;
            float ClassDamage;
            CharacterSkills[] skills;
        }

        public struct GridBox
        {
            public int xIndex;
            public int yIndex;
            public bool ocupied;
            public bool isOwner;
            public int Index;

            public GridBox(int x, int y, bool ocupied)
            {
                xIndex = x;
                yIndex = y;
                this.ocupied = ocupied;
                isOwner = false;
                this.Index = 0;
                this.Index = GetIndex(x,y);
            }
            int GetIndex(int x, int y)
            {
                string result = x.ToString() + y.ToString(); 
                return Int32.Parse(result);
            }
        }
        public struct CharacterSkills
        {
            string Name;
            float damage;
            float damageMultiplier;
        }

        public enum CharacterClass : uint
        {
            Paladin = 1,
            Warrior = 2,
            Cleric = 3,
            Archer = 4   
        }

    }
}
