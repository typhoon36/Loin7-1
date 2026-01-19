using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRPG_RE
{

    /*
     * File: Player.cs
     * Desc : 플레이어 관리
     * <Functions>
     * DispalyPlayerInfo() : 
     * DisplayInventoryInfo
     * 
     */
    public class Player
    {
        #region Stat
        public int Level { get; set; } //레벨
        public int TotalLevel { get; set; } //최대 레벨
        public string Name { get; } //이름
        public string Job { get; } //직업
        public int Att { get; } //공격력
        public int Def { get; } //방어력
        public int MaxHp { get; } // 최대 체력
        public int Hp { get; private set; } //현재 체력
        public int Gold { get; private set; } //골드

        public int Exp { get; private set; } //현재 경험치
        public int TotalExp { get; private set; } //최대 경험치


        public int ExtraAtt { get; set; } //추가 공격력
        public int ExtraDef { get; set; } //추가 방어력
        #endregion

        //인벤토리 & 장비 리스트
        List<Item> Inventory = new List<Item>();
        List<Item> EquipList = new List<Item>();

        //인벤에 몇개가 있는지
        public int InventoryCount { get { return Inventory.Count; } }

        //인벤토리 반환함수
        public List<Item> returnInventory { get { return Inventory; } }


        public Player(int level, int totallevel, string name, string job, int att, int def, int hp, int gold, int exp, int totalexp)
        {
            Level = level;
            TotalLevel = totallevel;
            Name = name;
            Job = job;
            Att = att;
            Def = def;
            MaxHp = hp;
            Hp = hp;
            Gold = gold;
            Exp = exp;
            TotalExp = totalexp;
        }

        //플레이어UI 출력(MainGame호출)
        public void DispalyPlayerInfo()
        {
            Console.WriteLine($"LV.{Level:D2}");
            Console.WriteLine($"{Name} {{ {Job} }}");

            Console.WriteLine(ExtraAtt == 0 ? $"공격력 : {Att}" : $"공격력 : {ExtraAtt + Att} (+{ExtraAtt})");
            Console.WriteLine(ExtraDef == 0 ? $"방어력 : {Def}" : $"방어력 : {ExtraDef + Def} (+{ExtraDef})");

            Console.WriteLine($"체력 : {Hp}");
            Console.WriteLine($"골드 : {Gold}G");
            Console.WriteLine($"경험치 : {Exp} / {TotalExp}");
        }


        //인벤토리 출력
        public void DisplayInventoryInfo(bool showIdx)
        {

            for (int i = 0; i < Inventory.Count; i++)
            {
                Item targetItem = Inventory[i];

                string displayIdx = showIdx ? $"{i + 1}  " : "";

                //장착한 아이템에 대한 연산(삼항으로 [E]를 표시)
                string displayEquip = IsEquipItem(targetItem) ? "[E]" : "";

                Console.WriteLine($"-{displayIdx}{displayEquip} {targetItem.ItemInfoText()}");
            }

        }

        public void EquipItem(Item item)
        {
            //예외처리
            if (item.Type == 2)
            {
                Console.WriteLine("소모 아이템은 장착할 수 없습니다.");
                Console.ReadLine();
                return;
            }

            //아이템을 장착하지않았을시
            if (IsEquipItem(item))
            {
                EquipList.Remove(item);

                if (item.Type == 0) { ExtraAtt -= item.Value; }
                else if (item.Type == 1) { ExtraDef -= item.Value; }
            }

            //실제 장착했으면 장착리스트에 추가(IsEquipItem이 true)
            else
            {
                //장착리스트 추가
                EquipList.Add(item);

                //타입이 0번이면 추가 공격력을 올리게 하기
                if (item.Type == 0) ExtraAtt += item.Value;
                //1번이면 추가 방어력
                else if (item.Type == 1) ExtraDef += item.Value;
            }
        }


        //장착한 아이템이 있는지(Contains로 있는지 확인)
        public bool IsEquipItem(Item item) { return EquipList.Contains(item); }

        //아이템 구매
        public void BuyItem(Item item)
        {
            Gold -= item.Price;
            Inventory.Add(item);
        }

        //아이템을 보유하고있는지(Contains로 있는지 확인)
        public bool HasItem(Item item) { return Inventory.Contains(item); }


        public void FullHeal() { Hp = MaxHp; }

        //레벨업
        public void LevelUp()
        {
            if (Exp >= TotalExp && Level < TotalLevel)
            {
                Exp = 0;
                Level += 1;
                Console.WriteLine("Level UP!");
                FullHeal();//레벨업했으니 풀피
            }
            //현재 레벨과 만렙과 같아지면 만렙이라 표시
            else if (Level >= TotalLevel)
            {
                Level = TotalLevel;
                Exp = TotalExp;
                Console.WriteLine("만렙입니다!!");
            }
        }

        //아이템 사용시 호출(amount에따라  HP 회복)
        public void Heal(int amount)
        {
            Hp += amount;
            if (Hp > MaxHp) Hp = MaxHp;
        }

        //아이템 사용(포션)
        public void UseConsumable(Item item)
        {
            //아이템타입이 2가 아니면 리턴
            if (item.Type != 2) return;

            //사용
            item.UseItem(this);

            //인벤에서 제거
            Inventory.Remove(item);
        }

        //몬스터한테서 데미지를 받음
        public void TakeDamage(int dmg)
        {
            Hp -= dmg;

            if (Hp < 0 && IsDead == true) Hp = 0;
        }

        //플레이어가 죽었는지 체크(죽으면 체력을 0으로 리턴)
        public bool IsDead { get { return Hp <= 0; } }

        //공격력 총합(공격력 + 추가공격력으로 계산)
        public int GetTotalAttack() { return Att + ExtraAtt; }

        //방어력 총합(방어력 + 추가 방어력)
        public int GetTotalDef() { return Def + ExtraDef; }

        //몬스터 처치후 획득 골드
        public void AddGold(int amount) { Gold += amount; }

        //레벨업이나 몬스터 처치후 획득 경험치에 대한 함수
        public void AddExp(int amount)
        {
            Exp += amount;
            LevelUp();
        }

    }
}
