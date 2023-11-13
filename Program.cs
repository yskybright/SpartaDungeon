using ConsoleTables;

namespace SpartaDungeon
{
    internal class Program
    {
        private static Character player;
        private static List<Item> Inventory = new List<Item>(); //인벤토리
        private static List<Item> Armor = new List<Item>(); //방어구 아이템 목록
        private static List<Item> Weapon = new List<Item>(); //무기 아이템 목록

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
            // 캐릭터 정보 세팅
            player = new Character("Chad", "전사", 1, 10, 5, 100, 1500);

            // 아이템 정보 세팅
            Item ironarmor = new Item("무쇠갑옷", 1, 0, 5, "방어력 +5", 0, 100, "무쇠로 만들어져 튼튼한 갑옷입니다.");
            Armor.Add(ironarmor);

            Item thornmail = new Item("가시갑옷", 3, 0, 10, "방어력 +10", 0, 300, "날카로운 가시들의 부드러운 춤.");
            Armor.Add(thornmail);

            Item goldenplate  = new Item("황금갑옷", 5, 0, 15, "방어력 +15", 0, 600, "번뜩이는 흉갑에 적의 눈이 스칩니다.");
            Armor.Add(goldenplate);

            Item oldsword = new Item("낡은 검", 1, 2, 0, "공격력 +2", 0, 50, "쉽게 볼 수 있는 낡은 검입니다.");
            Weapon.Add(oldsword);

            Item bloodyspear = new Item("핏빛 창", 3, 7, 0, "공격력 +7", 0, 100, "핏물이 이룬 살기로 가득합니다.");
            Weapon.Add(bloodyspear);

            Item lunarblade = new Item("월식", 5, 12, 0, "공격력 +12", 0, 300, "황혼이 머문 자리에 깃든 만월의 축복.");
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

        static void DisplayGameIntro()
        {
            Console.Clear();

            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 전전으로 들어가기 전 활동을 할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("1. 상태보기");
            Console.WriteLine("2. 인벤토리");
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

        static void DisplayMyInfo()
        {
            Console.Clear();

            Console.WriteLine("상태보기");
            Console.WriteLine("캐릭터의 정보를 표시합니다.");
            Console.WriteLine();
            Console.WriteLine($"Lv.{player.Level}");
            Console.WriteLine($"{player.Name}({player.Job})");
            Console.WriteLine($"공격력 :{player.Atk}");
            Console.WriteLine($"방어력 : {player.Def}");
            Console.WriteLine($"체력 : {player.Hp}");
            Console.WriteLine($"Gold : {player.Gold} G");
            Console.WriteLine();
            Console.WriteLine("0. 나가기");

            int input = CheckValidInput(0, 0);
            switch (input)
            {
                case 0:
                    DisplayGameIntro();
                    break;
            }
        }

        static void DisplayInventory()
        {
            ConsoleTable table = new ConsoleTable("아이템 이름", "효과", "설명");
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("인벤토리");
            Console.ResetColor();
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine("\n[아이템 목록]"); ;
            for (int i = 0; i < Inventory.Count; i++)
            {
                table.AddRow(Inventory[i].Name, Inventory[i].Effect, Inventory[i].Descriptions);
            }
            table.Write();
            Console.WriteLine();
            Console.WriteLine("1. 장착 관리");
            Console.WriteLine("0. 나가기");
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

            static void DisplayItemManagement()
            {
                ConsoleTable table = new ConsoleTable("아이템 이름", "효과", "설명");
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("인벤토리 - 아이템 장착");
                Console.ResetColor();
                Console.WriteLine("보유 중인 아이템을 장착할 수 있습니다.");
                Console.WriteLine("\n[아이템 목록]");
                for (int i = 0; i < Inventory.Count; i++)
                {
                    table.AddRow(i + 1 + ". " + Inventory[i].Name, Inventory[i].Effect, Inventory[i].Descriptions);
                }
                table.Write();
                Console.WriteLine();
                Console.WriteLine("0. 나가기");
                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write(">>");
                int input = CheckValidInput(0, Inventory.Count);
                if (input == 0)
                {
                    DisplayGameIntro();
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
            }   //아이템 장착 관리



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
            public int Level { get; }
            public int Atk { get; }
            public int Def { get; }
            public string Effect { get; }
            public int Hp { get; }
            public int Gold { get; }
            public string Descriptions { get; }
            public Item(string name, int level, int atk, int def, string effect, int hp, int gold, string descriptions)
            {
                Name = name;
                Level = level;
                Atk = atk;
                Def = def;
                Effect = effect;
                Hp = hp;
                Gold = gold;
                Descriptions = descriptions;
            }

            public void Equip(Character character, List<Item> inventory, List<Item> weapon, List<Item> armor)
            {
                if (weapon.Contains(this))//this는 장착할 아이템, 장착 아이템이 웨폰인지 확인
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
            }//아이템 장착 메서드

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
            }//아이템 장착 해제 메서드

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
            }//아이템 장착 해제 메서드 오버로딩
        }

    }
}

