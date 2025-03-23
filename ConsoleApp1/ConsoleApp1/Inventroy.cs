using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Inventory
    {

        // 초기화
        private Dictionary<string, int> items = new Dictionary<string, int>();


        // 인벤토리 아이템 추가 
        public void AddItem(string itemName, int quantity = 1)
        {
            if (items.ContainsKey(itemName))
            {
                items[itemName] += quantity;
            }
            else
            {
                items[itemName] = quantity;
            }
        }

        // 인벤토리 아이템제거 기본값 1설정
        public void RemoveItem(string itemName, int quantity = 1)
        {
            if (items.ContainsKey(itemName))
            {   //인벤토리에 해당아이템 존재시
                if (items[itemName] > quantity)
                {   // 제거할 수량이 현재수량보다 작으면 수량감소
                    items[itemName] -= quantity;
                }   // 제거할 수량이 현재 수량과 같거나 많으면 아이템 완전히 제거 
                else
                {
                    items.Remove(itemName);
                }
            }
        }
        //특정아이템 수량 반환
        public int GetItemQuantity(string itemName)
        {
            return items.ContainsKey(itemName) ? items[itemName] : 0;
        }

        //인벤 ui
        public void DisplayInventory()
        {
            Console.WriteLine("======인벤토리======");
            foreach (var item in items)
            {
                Console.WriteLine($"{item.Key}: {item.Value}개");
            }
            Console.WriteLine("====================");
        }
    }
    // 인벤 관려해서 아이템추가시 playerInventory.addItem("HP 포션", 3); 이렇게 하기! 
    // 아이템제거는 RemoveItem
    // 아이템 수량 확인은 예시로 int hpPotionCount = playerInventory.GetItemQuantity("HP 포션");
    //Console.WriteLine($"HP 포션 수량: {hpPotionCount}");
    // 인벤 확인 playerInventory.DisplayInventory();
}
