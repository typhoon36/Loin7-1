using System;
using System.Linq;
using System.Text;

namespace TRPG_RE
{
    /* File: MainGame.cs
    * Desc : 메인게임 관리
    * <Functions>
    * SetData() : 컨턴츠에 필요한 초기데이터 설정
    * DisplayMainUI() : 메인화면 출력(UTF-8 이모지 출력)
    * DisplayStatUI() : 캐릭터 상태 창 출력(0번누르면 메인화면으로)
    * EnterDungeon() : 몬스터 스폰 및 배틀매니저에서 전투시작받아오기
    * DisplayInventory() : 인벤토리 출력
    * CheckInput() : 입력값 확인
    */

    public class MainGame
    {
        static Player player;
        static Item[] itemDb;
        static Monster[] monsterDb;

        public static void GameStart()
        {
            SetData();
            DisplayMainUI();
        }

        //데이터 설정(플레이어,몬스터,아이템)
        static void SetData()
        {
            player = new Player(1, 10, "Character", "전사", 10, 5, 50, 1000, 0, 20);

            itemDb = new Item[]
            {
                new Item("회색 원라인 티셔츠", 1, 5,"평범한 티셔츠입니다.",1000),
                new Item("브론즈 갑옷", 1, 9,"동으로 만들어져 내구도가 불안한 갑옷입니다.",2000),
                new Item("스틸 갑옷",1,15,"이제야 갑옷이라 불릴만한 철제 갑옷입니다.",3500),
                new Item("검사의 양손검", 0, 2,"수련용 양손 검입니다.",500),
                new Item("왕푸", 0, 5,"어디선가 본 느낌의 양손 검입니다.",1500),
                new Item("발키리소드", 0, 7,"이름에서처럼 강한 검입니다.",2500),
                new Item("빨간 물약", 2 , 0,"HP를 약 10% 회복시켜줍니다.", 100 ),
                new Item("하얀 물약", 2 , 0,"HP를 약 20% 회복시켜줍니다.", 200 )
            };

            monsterDb = new Monster[]
            {
                new Monster("슬라임", 1, 3, 1, 8, 5, 100),
                new Monster("고블린", 3, 5, 2, 15, 10, 200),
                new Monster("스켈레톤", 5, 7, 2, 25, 20, 300),
                new Monster("오우거", 8, 12, 5, 50, 60, 500)
            };
        }

        //메인 화면
        static void DisplayMainUI()
        {
            //처음 화면 출력전에 클리어
            Console.Clear();
            
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;

            Console.WriteLine("★ Text-RPG(TRPG)에 오신것을 환영합니다.★\n");
            Console.WriteLine("👤 1. 상태보기");
            Console.WriteLine("\U0001f9be 2. 인벤토리");
            Console.WriteLine("🙌 3. 상점");
            Console.WriteLine("😈 4. 던전가기");
            Console.WriteLine("⛩️ 5. 게임종료\n");
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            int input = CheckInput(1, 5);

            switch (input)
            {
                case 1:
                    DisplayStatUI(); //스탯창
                    break;

                case 2:
                    DisplayInventory(); //인벤토리
                    break;

                case 3:
                    DisplayShopUI(); //상점
                    break;

                case 4:
                    EnterDungeon(); //던전 입장
                    break;

                case 5:
                    Environment.Exit(0); //게임종료
                    break;
            }
        }


        //스탯창
        static void DisplayStatUI()
        {
            Console.Clear();
            Console.WriteLine("[상태 보기]");
            Console.WriteLine("캐릭터의 정보가 표시됩니다.\n");

            player.DispalyPlayerInfo();

            Console.WriteLine();
            Console.WriteLine("0 : 나가기");
            Console.WriteLine();

            Console.WriteLine("원하시는 행동을 입력해주세요.");

            int input = CheckInput(0, 0);

            switch (input)
            {
                case 0:
                    DisplayMainUI();
                    break;
            }

        }

        //던전 입장
        static void EnterDungeon()
        {
            //몬스터 랜덤 등장
            Random rnd = new Random();
            Monster spawn = monsterDb[rnd.Next(monsterDb.Length)];

            //매니저에서 배틀시작
            BattleManager battle = new BattleManager(player, spawn);
            battle.StartBattle();

            //메인 UI
            DisplayMainUI();
        }

        #region Inventory
        // 인벤토리 UI
        static void DisplayInventory()
        {
            Console.Clear();
            Console.WriteLine("[인벤토리]");
            Console.WriteLine("보유중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("[ 아이템 목록 ]");

            player.DisplayInventoryInfo(false);

            Console.WriteLine();
            Console.WriteLine("1 : 장착 관리");
            Console.WriteLine("2 : 포션 관리");
            Console.WriteLine("0 : 나가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            int result = CheckInput(0, 2);

            switch (result)
            {
                case 0:
                    DisplayMainUI();
                    break;

                case 1:
                    DisplayEquipUI();
                    break;
                case 2:
                    DisplayPotionUI();
                    break;
            }

        }

        // 인벤토리 장착 UI
        static void DisplayEquipUI()
        {
            Console.Clear();
            Console.WriteLine("인벤토리");
            Console.WriteLine("장착 할 아이템을 선택해 주세요.");
            Console.WriteLine();
            Console.WriteLine("[ 아이템 목록 ]");

            player.DisplayInventoryInfo(true);

            Console.WriteLine();
            Console.WriteLine("0 : 나가기");
            Console.WriteLine();
            Console.WriteLine("장착할 아이템의 번호를 적어주세요.");

            int result = CheckInput(0, player.InventoryCount);

            switch (result)
            {
                case 0:
                    DisplayInventory();
                    break;

                default:
                    {
                        int itemIdx = result - 1;
                        Item targetItem = player.returnInventory[itemIdx];
                        player.EquipItem(targetItem);


                        DisplayEquipUI();

                    }
                    break;

            }
        }

        //포션 관리 UI
        static void DisplayPotionUI()
        {
            Console.Clear();
            Console.WriteLine("포션 관리");
            Console.WriteLine("사용할 포션을 선택해주세요.");
            Console.WriteLine();

            //where.ToList로 인벤 받아오기
            var potions = player.returnInventory
                                 .Where(i => i.Type == 2)
                                 .ToList();

            //포션이 없을경우
            if (potions.Count == 0)
            {
                Console.WriteLine("사용할 수 있는 포션이 없습니다.");
                Console.WriteLine("0 : 나가기");
                Console.ReadLine();

                DisplayInventory();
                return;
            }

            //포션을 가지고있으면 가지고있는만큼 출력
            for (int i = 0; i < potions.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {potions[i].ItemInfoText()}");
            }

            Console.WriteLine();
            Console.WriteLine("0 : 나가기");

            int input = CheckInput(0, potions.Count);

            if (input == 0)
            {
                DisplayInventory();
                return;
            }

            player.UseConsumable(potions[input - 1]);
            Console.ReadLine();
            DisplayPotionUI();
        }
        #endregion

        #region 상점
        // 상점 UI
        static void DisplayShopUI()
        {
            Console.Clear();
            Console.WriteLine("상점");
            Console.WriteLine("아이템을 구매할 수 있습니다.");
            Console.WriteLine();

            Console.WriteLine("[ 보유골드 ]");
            Console.WriteLine($"{player.Gold} G");
            Console.WriteLine();

            Console.WriteLine("[ 아이템 목록 ]");

            for (int i = 0; i < itemDb.Length; i++)
            {
                Item curItem = itemDb[i];

                string displayPrice = player.HasItem(curItem) ? "구매완료" : $"{curItem.Price}G";
                Console.WriteLine($"- {curItem.ItemInfoText()} | {displayPrice}");
            }

            Console.WriteLine();
            Console.WriteLine("1 : 아이템 구매");
            Console.WriteLine("0 : 나가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            int result = CheckInput(0, 1);

            switch (result)
            {
                case 0:
                    DisplayMainUI();
                    break;

                case 1:
                    DisplayBuyUI();
                    break;
            }

        }

        // 상점 구매 UI
        static void DisplayBuyUI()
        {
            Console.Clear();
            Console.WriteLine("상점 - 아이템 구매");
            Console.WriteLine("아이템을 구매할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("[ 보유골드 ]");
            Console.WriteLine($"{player.Gold}G");
            Console.WriteLine();
            Console.WriteLine("[ 아이템 목록 ]");


            for (int i = 0; i < itemDb.Length; i++)
            {
                //아이템목록을 아이템데이터에서 불러오기
                Item curItem = itemDb[i];

                //만약 플레이어가 해당 아이템을 구매했을시 가격대신 구매완료를 붙여주도록
                string displayPrice = player.HasItem(curItem) ? "구매완료" : $"{curItem.Price}G";
                Console.WriteLine($"{i + 1} {curItem.ItemInfoText()} | {displayPrice}");
            }

            Console.WriteLine();

            Console.WriteLine("0 : 나가기");
            Console.WriteLine();

            Console.WriteLine("원하시는 행동을 입력해주세요.");

            int result = CheckInput(0, itemDb.Length);

            switch (result)
            {
                case 0:
                    DisplayShopUI();
                    break;

                default:

                    int itemIdx = result - 1;
                    Item targetItem = itemDb[itemIdx];

                    if (player.HasItem(targetItem))
                    {
                        Console.WriteLine(" ===  구매 완료된 아이템입니다. === ");
                        Console.WriteLine("enter를 입력해주세요.");
                        Console.ReadLine();
                    }
                    else
                    {
                        //골드가 구매금액에 충족하면
                        if (player.Gold >= targetItem.Price)
                        {
                            Console.WriteLine(" === 구매를 완료했습니다. === ");
                            Console.WriteLine("enter를 입력해주세요.");
                            Console.ReadLine();

                            player.BuyItem(targetItem);
                        }
                        //골드가 부족하면
                        else
                        {
                            Console.WriteLine(" === 골드가 부족합니다. === ");
                            Console.WriteLine("enter를 입력해주세요.");
                            Console.ReadLine();

                        }
                    }

                    //여기까지 오면 구매 UI 재출력
                    DisplayBuyUI();
                    break;
            }
        }

        #endregion

        //입력값 확인
        public static int CheckInput(int min, int max)
        {
            int result;

            while (true)
            {
                bool isNumber = int.TryParse(Console.ReadLine(), out result);

                if (isNumber && result >= min && result <= max)
                    return result;

                Console.WriteLine("잘못된 입력입니다.");
            }
        }
    }
}
