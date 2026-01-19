using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRPG_RE
{

    /*
     * File: Monster.cs
     * Desc : 몬스터
     * <Functions>
     * GetRandAttack() : 랜덤 공격력 반환
     * TakeDamage() : 플레이어로부터 데미지
     * DisplayMonsterInfo(): 몬스터 정보 출력
     * IsDead() : 죽었는지 확인할 bool
     */
    public class Monster
    {
        #region Stat
        public string Name { get; }
        public int MinAtt { get; } //최소 공격력
        public int MaxAtt { get; } //최대공격력
        public int Def { get; }
        public int Hp { get; set; }
        public int Exp { get; }
        public int Gold { get; }
        #endregion

        static Random rand = new Random(); //몬스터의 랜덤공격력을 위한 변수


        public Monster(string name, int minAtt, int maxAtt, int def, int hp, int exp, int gold)
        {
            Name = name;
            MinAtt = minAtt;
            MaxAtt = maxAtt;
            Def = def;
            Hp = hp;
            Exp = exp;
            Gold = gold;
        }

        //최대-최소 공격을 랜덤하게
        public int GetRandomAttack() { return rand.Next(MinAtt, MaxAtt + 1); }

        //몬스터 설명
        public void DisplayMonsterInfo()
        {
            Console.WriteLine($"몬스터 : {Name}");
            Console.WriteLine($"공격력 : {MinAtt} ~ {MaxAtt}");
            Console.WriteLine($"방어력 : {Def}");
            Console.WriteLine($"체력 : {Hp}");
            Console.WriteLine($"경험치 : {Exp}");
            Console.WriteLine($"골드 : {Gold} G");
        }

        //플레이어로부터 피해
        public void TakeDamage(int dmg)
        {
            Hp -= dmg;
            if (Hp < 0) Hp = 0;
        }

        //몬스터가 죽었는지 체크(죽으면 HP 0)
        public bool IsDead { get { return Hp <= 0; } }
    }
}
