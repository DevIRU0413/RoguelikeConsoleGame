using RoguelikeConsoleGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Shop
    {
        private Dictionary<string, (int cost, Action<Player> effect)> items = new Dictionary<string, (int cost, Action<Player> effect)>();

        public Shop()
        {
            InitializeItems();
        }

        private void InitializeItems()
        {
            items["HP 포션"] = (50, (player) =>
            {
                player.Inventory.AddItem("HP 포션");

            }
            );

            items["복권"] = (150, (player) =>
            {
                int gold = new Random().Next(0, 301);
                player.AddMoney(gold);
                Console.WriteLine($"복권을 구매하여 {gold}골드를 획득했습니다!");
            }
            );

            items["기회창출의 돌"] = (1000, (player) =>
            {
                player.Inventory.AddItem("기회창출의 돌");
                // 부활 로직 필요 
            }
            );

            items["강화의 돌(1회 구매가능)"] = (500, (player) =>
            {
                player.AttackPower += 3;
                
            }
            );
        }

        public void OpenShop(Player player)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("===== 상점 =====");
                Console.WriteLine("구매할 아이템을 선택하세요:");
                int index = 1;
                foreach (var item in items)
                {
                    if (item.Key != "강화의 돌(1회구매가능)" || !player.usedStone)
                    {
                        Console.WriteLine($"{index}. {item.Key} - {item.Value.cost}골드");
                        index++;
                    }
                }
                Console.WriteLine($"{index}. 나가기");
                Console.Write("선택: ");

                string input = Console.ReadLine();
                if (int.TryParse(input, out int choice) && choice >= 1 && choice <= items.Count + 1)
                {
                    if (choice == items.Count + 1)
                    {
                        break;
                    }
                    else
                    {
                        string selectedItem = GetItemNameByIndex(choice);
                        if (items.TryGetValue(selectedItem, out var itemInfo))
                        {
                            if (player.HaveMoney >= itemInfo.cost)
                            {
                                player.AddMoney(-itemInfo.cost);
                                itemInfo.effect(player);
                            }
                            else
                            {
                                Console.WriteLine("골드가 부족합니다!");
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                }
                Console.WriteLine("계속하려면 아무 키나 누르세요...");
                Console.ReadKey();
            }
        }

        private string GetItemNameByIndex(int index)
        {
            int count = 1;
            foreach (var item in items)
            {
                if (count == index)
                {
                    return item.Key;
                }
                count++;
            }
            return null;
        }
    }
}