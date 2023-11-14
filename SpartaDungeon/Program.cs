using ConsoleTables;

namespace SpartaDungeon
{
    internal class Program
    {
        private static Character player;
        private static List<Item> Inventory = new List<Item>(); // 인벤토리
        private static List<Item> Armor = new List<Item>(); // 방어구
        private static List<Item> Weapon = new List<Item>(); // 무기 
        private static List<Item> Shop = new List<Item>(); // 상점

        static void Main(string[] args)
        {
            GameDataSetting();
            DisplayGameIntro();
        }

        static int CheckValidInput(int min, int max)
        {
            while (true)
            {
                string input = Console.ReadLine();

                bool parseSuccess = int.TryParse(input, out var ret);
                if (parseSuccess)
                {
                    if (ret >= min && ret <= max)
                        return ret;
                }

                Console.WriteLine("잘못된 입력입니다.");
            }
        }

        private static void ShowHighlightedText(string text)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        static void GameDataSetting()
        {
            // 캐릭터 정보 
            player = new Character("Zelda", "전사", 1, 10, 5, 100, 1500);

            // 아이템 정보
            Item ironarmor = new Item("무쇠갑옷(A)", 0, 5, "방어력 +5", 0, 100, "무쇠로 만들어져 튼튼한 갑옷입니다.");
            Armor.Add(ironarmor);

            Item thornmail = new Item("가시갑옷(A)", 0, 10, "방어력 +10", 0, 300, "날카로운 가시들의 부드러운 춤.");
            Armor.Add(thornmail);

            Item thunderstorm = new Item("번개질주(A)", 0, 15, "방어력 +15", 0, 550, "누구보다 빛나고 싶은 자들의 우상.");
            Armor.Add(thunderstorm);

            Item goldenplate  = new Item("황금갑옷(A)", 0, 20, "방어력 +20", 0, 800, "번뜩이는 흉갑에 적의 눈동자가 스칩니다.");
            Armor.Add(goldenplate);

            Item oldsword = new Item("낡은 검(W)", 2, 0, "공격력 +2", 0, 50, "쉽게 볼 수 있는 낡은 검입니다.");
            Weapon.Add(oldsword);

            Item pinkvenom = new Item("핑크 베놈(W)", 6, 0, "공격력 +6", 0, 150, "진분홍 살모사의 독이 서린 단검입니다.");
            Weapon.Add(pinkvenom);

            Item bloodyspear = new Item("핏빛 창(W)", 9, 0, "공격력 +9", 0, 250, "전장의 핏물이 이룬 살기로 가득합니다.");
            Weapon.Add(bloodyspear);

            Item lunarblade = new Item("월식(W)", 12, 0, "공격력 +12", 0, 400, "황혼이 머문 자리에 깃든 만월의 축복.");
            Weapon.Add(lunarblade);

            Inventory.Add(Armor[0]);   
            Inventory.Add(Weapon[0]);

            for (int i = 1; i < Armor.Count; i++)
            {
                Shop.Add(Armor[i]);
            }
            for (int i = 1; i < Weapon.Count; i++)
            {
                Shop.Add(Weapon[i]);
            }

        }

        // 인트로 화면
        static void DisplayGameIntro()
        {
            Console.Clear();

            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.");
            Console.WriteLine();
            ShowHighlightedText("1. 상태 보기");
            ShowHighlightedText("2. 인벤토리");
            ShowHighlightedText("3. 상점");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">>");

            int input = CheckValidInput(1, 3);
            switch (input)
            {
                case 1:
                    DisplayMyInfo();
                    break;

                case 2:
                    DisplayInventory();
                    break;
                case 3:
                    DisplayShop();
                    break;
            }
        }

        // 상태 보기
        static void DisplayMyInfo()
        {
            Console.Clear();

            ShowHighlightedText("상태 보기");
            Console.WriteLine("캐릭터의 정보를 표시합니다.");
            Console.WriteLine();
            Console.WriteLine($"Lv.{player.Level}");
            Console.WriteLine($"{player.Name} ({player.Job})");
            Console.WriteLine($"공격력 : {player.Atk}");
            Console.WriteLine($"방어력 : {player.Def}");
            Console.WriteLine($"체력 : {player.Hp}");
            Console.WriteLine($"Gold : {player.Gold} G");
            Console.WriteLine();
            Console.WriteLine("0. 메인 화면");

            int input = CheckValidInput(0, 0);
            switch (input)
            {
                case 0:
                    DisplayGameIntro();
                    break;
            }
        }

        // 인벤토리
        static void DisplayInventory()
        {
            ConsoleTable table = new ConsoleTable("아이템 이름", "효과", "설명", "Gold");
            Console.Clear();
            ShowHighlightedText("인벤토리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine("\n[아이템 목록]"); ;
            for (int i = 0; i < Inventory.Count; i++)
            {
                table.AddRow(Inventory[i].Name, Inventory[i].Effect, Inventory[i].Descriptions, Inventory[i].Gold);
            }
            table.Write();
            Console.WriteLine();
            Console.WriteLine("1. 장착 관리");
            Console.WriteLine("0. 메인 화면");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">>");

            int input = CheckValidInput(0, 1);
            switch (input)
            {
                case 0:
                    DisplayGameIntro();
                    break;
                case 1:
                    DisplayItemManagement();
                    break;

            }

            // 장착 관리
            static void DisplayItemManagement()
            {
                ConsoleTable table = new ConsoleTable("아이템 이름", "효과", "설명", "Gold");
                Console.Clear();
                ShowHighlightedText("인벤토리 - 아이템 장착");
                Console.WriteLine("보유 중인 아이템을 장착할 수 있습니다.");
                Console.WriteLine("\n[아이템 목록]");
                for (int i = 0; i < Inventory.Count; i++)
                {
                    table.AddRow(i + 1 + ". " + Inventory[i].Name, Inventory[i].Effect, Inventory[i].Descriptions, Inventory[i].Gold);
                }
                table.Write();
                Console.WriteLine();
                Console.WriteLine("0. 인벤토리");
                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write(">>");
                int input = CheckValidInput(0, Inventory.Count);
                if (input == 0 )
                {
                    DisplayInventory();
                }
                else
                {
                    if (Inventory[input - 1].Name.Contains("[E]"))
                    {
                        Inventory[input - 1].UnEquip(player, Weapon, Armor);
                        DisplayItemManagement();
                    }
                    else
                    {
                        Inventory[input - 1].Equip(player, Inventory, Weapon, Armor);
                        DisplayItemManagement();
                    }
                }
            }   
        }

        // 상점
        static void DisplayShop()
        {
            Console.Clear();
            ShowHighlightedText("상점");
            Console.WriteLine("아이템을 구매할 수 있습니다.");
            Console.WriteLine("[" + player.Name + "의 Gold]" + " : " + player.Gold + " G\n");
            Console.WriteLine("\n[아이템 목록]");
            ConsoleTable table = new ConsoleTable("아이템 이름", "효과", "설명", "Gold");
            for (int i = 0; i < Shop.Count; i++)
            {
                if (Inventory.Contains(Shop[i]))
                {
                    table.AddRow(Shop[i].Name, Shop[i].Effect, Shop[i].Descriptions, "구매 완료");
                }
                else
                {
                    table.AddRow(i + 1 + ". " + Shop[i].Name, Shop[i].Effect, Shop[i].Descriptions, Shop[i].Gold);
                }
            }

            table.Write();
            Console.WriteLine("0. 메인 화면");

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write(">>");
                int input = CheckValidInput(0, Shop.Count);
                if (input == 0)
                {
                    DisplayGameIntro();
                }
                else
                {
                    if (Inventory.Contains(Shop[input - 1]))
                    {
                        Console.WriteLine("이미 구매한 아이템입니다.");
                    }
                    else if (player.Gold >= Shop[input - 1].Gold)
                    {
                        player.Gold -= Shop[input - 1].Gold;
                        Inventory.Add(Shop[input - 1]);
                        Console.WriteLine("구매하는 중.. 잠시만 기다려주세요.");
                        Thread.Sleep(1000);
                        DisplayShop();
                    }
                    else if (player.Gold < Shop[input - 1].Gold)
                    {
                        Console.WriteLine("Gold가 부족합니다.");
                    }
                }
            }
        }


        public class Character
        {
            public string Name { get; }
            public string Job { get; }
            public int Level { get; }
            public int Atk { get; set; }
            public int Def { get; set; }
            public int Hp { get; set; }
            public int Gold { get; set; }
            public bool EquipArmor { get; set; }
            public bool EquipWeapon { get; set; }

            public Character(string name, string job, int level, int atk, int def, int hp, int gold)
            {
                Name = name;
                Job = job;
                Level = level;
                Atk = atk;
                Def = def;
                Hp = hp;
                Gold = gold;
                EquipArmor = false;
                EquipWeapon = false;
            }
        }

        public class Item
        {
            public string Name { get; set; }
            public int Atk { get; }
            public int Def { get; }
            public string Effect { get; }
            public int Hp { get; }
            public int Gold { get; }
            public string Descriptions { get; }
            public Item(string name, int atk, int def, string effect, int hp, int gold, string descriptions)
            {
                Name = name;
                Atk = atk;
                Def = def;
                Effect = effect;
                Hp = hp;
                Gold = gold;
                Descriptions = descriptions;
            }

            // 아이템 장착 시 E 표시하고 효과 반영
            public void Equip(Character character, List<Item> inventory, List<Item> weapon, List<Item> armor)
            {
                if (weapon.Contains(this))// 아이템 종류 확인
                {
                    for (int i = 0; i < inventory.Count; i++)
                    {
                        if (character.EquipWeapon)
                        {
                            if (inventory[i].Name.Contains("[E]") && weapon.Contains(inventory[i]))
                            {
                                UnEquip(character, inventory[i], weapon, armor);
                                break;
                            }
                        }
                    }

                    character.EquipWeapon = true;
                    Name = "[E]" + Name;
                    character.Atk += Atk;
                    character.Def += Def;
                }
                else if (armor.Contains(this))
                {
                    for (int i = 0; i < inventory.Count; i++)
                    {
                        if (character.EquipArmor)
                        {
                            if (inventory[i].Name.Contains("[E]") && armor.Contains(inventory[i]))
                            {
                                UnEquip(character, inventory[i], weapon, armor);
                                break;
                            }
                        }
                    }

                    character.EquipArmor = true;
                    Name = "[E]" + Name;
                    character.Atk += Atk;
                    character.Def += Def;
                }
            }

            // 아이템 탈착 시 E 제거하고 효과 반영
            public void UnEquip(Character character, Item item, List<Item> weapon, List<Item> armor)
            {
                if (weapon.Contains(item))
                {

                    character.EquipWeapon = false;
                    item.Name = item.Name.Replace("[E]", "");
                    character.Atk -= item.Atk;
                    character.Def -= item.Def;
                }
                else if (armor.Contains(item))
                {
                    character.EquipArmor = false;
                    item.Name = item.Name.Replace("[E]", "");
                    character.Atk -= item.Atk;
                    character.Def -= item.Def;
                }
            }

            // Item 오버로드
            public void UnEquip(Character character, List<Item> weapon, List<Item> armor)
            {
                if (weapon.Contains(this))
                {
                    character.EquipWeapon = false;
                    Name = Name.Replace("[E]", "");
                    character.Atk -= Atk;
                    character.Def -= Def;
                }
                else if (armor.Contains(this))
                {
                    character.EquipArmor = false;
                    Name = Name.Replace("[E]", "");
                    character.Atk -= Atk;
                    character.Def -= Def;
                }
            }
        }
    }
}

