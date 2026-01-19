using System;
using System.Linq;


namespace TRPG_RE
{
    /*
    * File: BattleManager.cs
    * Desc : 전투관리
    * <Functions>
    * StartBattle() : 던전에 들어가면 출력되는 함수
    */

    public class BattleManager
    {
        Player player;
        Monster monster;

        
        public BattleManager(Player player, Monster monster)
        {
            this.player = player;
            this.monster = monster;
        }

        //전투 시작
        public void StartBattle()
        {
            Console.Clear();
            Console.WriteLine("=== 몬스터 등장 ===");
            monster.DisplayMonsterInfo();

            while (true)
            {
                Console.WriteLine("\n0 : 공격");
                Console.WriteLine("1 : 도망");
                Console.WriteLine("2 : 가방");
                Console.WriteLine("원하시는 행동을 입력해주세요.");

                int action = MainGame.CheckInput(0, 2);

                // 도망
                if (action == 1)
                {
                    Console.WriteLine("무사히 도망쳤습니다.");
                    Console.ReadLine();
                    return;
                }

                // 가방
                if (action == 2)
                {
                    bool usedItem = UseItem();

                    ContinueTurn();
                    continue;
                }

                PlayerAttack();

                if (monster.IsDead)
                {
                    Console.WriteLine("몬스터를 처치했습니다!");
                    player.AddGold(monster.Gold);
                    player.AddExp(monster.Exp);
                    Console.WriteLine($"골드 + {monster.Gold} G");
                    Console.WriteLine($"경험치 + {monster.Exp}");
                    Console.ReadLine();
                    return;
                }

               //몬스터 공격시 리턴되게
                if (MonsterTurn()) return;

                //차례 계속
                ContinueTurn();
            }
        }

        //플레이어 공격
        void PlayerAttack()
        {
            int attack = player.GetTotalAttack();
            int damage = Math.Max(1, attack - monster.Def);
            monster.TakeDamage(damage);

            Console.WriteLine($"플레이어가 {damage}의 데미지를 주었습니다.");
        }

        //몬스터 차례
        bool MonsterTurn()
        {
            int attack = monster.GetRandomAttack();
            int damage = Math.Max(1, attack - player.GetTotalDef());
            player.TakeDamage(damage);

            Console.WriteLine($"몬스터가 {damage}의 데미지를 주었습니다.");


            if (player.IsDead)
            {
                Console.WriteLine("플레이어가 사망했습니다...");
                Console.ReadLine();
                player.FullHeal();
                return true;
            }

            return false;
        }

        //가방에서 포션사용할때
        bool UseItem()
        {
            Console.Clear();
            Console.WriteLine("가방");
            Console.WriteLine("사용할 아이템을 선택해주세요.\n");

            //where 키워드로 인벤(가방)에 타입2의 아이템이 있는지 살펴보기
            var potions = player.returnInventory.Where(i => i.Type == 2).ToList();

            //포션 갯수가 0
            if (potions.Count == 0)
            {
                Console.WriteLine("사용할 수 있는 포션이 없습니다.");
                Console.ReadLine();
                return false;
            }

            for (int i = 0; i < potions.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {potions[i].ItemInfoText()}");
            }

            Console.WriteLine("\n0 : 취소");

            int input = MainGame.CheckInput(0, potions.Count);

            if (input == 0) return false;

            player.UseConsumable(potions[input - 1]);
            Console.ReadLine();
            return true;
        }

        //턴 계속하기
        void ContinueTurn()
        {
            Console.WriteLine("\n엔터를 누르면 계속합니다.");
            Console.ReadLine();
            Console.Clear();

            monster.DisplayMonsterInfo();
            player.DispalyPlayerInfo();
        }
    }
}
