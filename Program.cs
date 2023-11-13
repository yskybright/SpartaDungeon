using ConsoleTables;

namespace SpartaDungeon
{
    internal class Program
    {
        private static Character player;
        private static List<Item> Inventory = new List<Item>(); // 인벤토리
        private static List<Item> Armor = new List<Item>(); // 방어구
        private static List<Item> Weapon = new List<Item>(); // 무기 

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

        static void GameDataSetting()
        {
            // 캐릭터 정보 
            player = new Character("Zelda", "전사", 1, 10, 5, 100, 1500);

            // 아이템 정보
            Item ironarmor = new Item("무쇠갑옷", 0, 5, "방어력 +5", 0, 100, "무쇠로 만들어져 튼튼한 갑옷입니다.");
            Armor.Add(ironarmor);

            Item thornmail = new Item("가시갑옷", 0, 10, "방어력 +10", 0, 300, "날카로운 가시들의 부드러운 춤.");
            Armor.Add(thornmail);

            Item goldenplate  = new Item("황금갑옷", 0, 15, "방어력 +15", 0, 600, "번뜩이는 흉갑에 적의 눈동자가 스칩니다.");
            Armor.Add(goldenplate);

            Item oldsword = new Item("낡은 검", 2, 0, "공격력 +2", 2, 50, "쉽게 볼 수 있는 낡은 검입니다.");
            Weapon.Add(oldsword);

            Item bloodyspear = new Item("핏빛 창", 7, 0, "공격력 +7", 7, 100, "전장의 핏물이 이룬 살기로 가득합니다.");
            Weapon.Add(bloodyspear);

            Item lunarblade = new Item("월식", 12, 0, "공격력 +12", 12, 300, "황혼이 머문 자리에 깃든 만월의 축복.");
            Weapon.Add(lunarblade);

            for (int i = 0; i < Armor.Count; i++)
            {
                Inventory.Add(Armor[i]);
            }
            for (int i = 0; i < Weapon.Count; i++)
            {
                Inventory.Add(Weapon[i]);
            }
        }

        // 인트로 화면
        static void DisplayGameIntro()
        {
            Console.Clear();

            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("1. 상태 보기");
            Console.WriteLine("2. 인벤토리");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">>");

            int input = CheckValidInput(1, 2);
            switch (input)
            {
                case 1:
                    DisplayMyInfo();
                    break;

                case 2:
                    DisplayInventory();
                    break;
            }
        }

        // 상태 보기
        static void DisplayMyInfo()
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("상태 보기");
            Console.ResetColor();
            Console.WriteLine("캐릭터의 정보를 표시합니다.");
            Console.WriteLine();
            Console.WriteLine($"Lv.{player.Level}");
            Console.WriteLine($"{player.Name}({player.Job})");
            Console.WriteLine($"공격력 :{player.Atk}");
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
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("인벤토리");
            Console.ResetColor();
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
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("인벤토리 - 아이템 장착");
                Console.ResetColor();
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


        public class Character
        {
            public string Name { get; }
            public string Job { get; }
            public int Level { get; }
            public int Atk { get; set; }
            public int Def { get; set; }
            public int Hp { get; set; }
            public int Gold { get; }
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
                if (weapon.Contains(this))// this는 장착할 아이템, 장착 아이템이 웨폰인지 확인
                {
                    character.EquipWeapon = true;
                    Name = "[E]" + Name;
                    character.Atk += Atk;
                    character.Def += Def;
                }
                else if (armor.Contains(this))
                {
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




