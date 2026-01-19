using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRPG_RE
{
    /*
     * File: Item.cs
     * Desc : 아이템 클래스
     * <Functions>
     * ItemInfoText() : 아이템 설명 
     * UseItem() : 아이템 사용(플레이어 클래스에서 호출)
     */
    public class Item
    {
        public string Name { get; } 
        public int Type { get; } //0:무기 1:갑옷 2: 소비아이템
        public int Value { get; }
        public string Desc { get; } //설명
        public int Price { get; } //가격

        public Item(string name, int type, int value, string desc, int price)
        {
            Name = name;
            Type = type;
            Value = value;
            Desc = desc;
            Price = price;
        }

        //아이템 설명
        public string ItemInfoText() { return $"{Name}  {Desc}"; }

        //아이템 사용
        public void UseItem(Player player)
        {

            if (Type != 2) return;

            int healAmount = 0;

            //설명에 10%-20%가 포함되어있으면 설명만큼 회복
            
            if (Desc.Contains("10%"))
            {
                healAmount = (int)(player.MaxHp * 0.1f);
            }


            else if (Desc.Contains("20%"))
            {
                healAmount = (int)(player.MaxHp * 0.2f);
            }

            player.Heal(healAmount);//회복량에따라 회복

            //회복량만큼 회복했다고 출력
            Console.WriteLine($"{Name}을 사용하여 HP를 {healAmount} 회복했습니다.");
        }
    }
}
