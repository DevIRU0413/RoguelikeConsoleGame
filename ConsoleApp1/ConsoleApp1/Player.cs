namespace RoguelikeConsoleGame;

public abstract class Player
{
    public string Job { get; }
    public int HP { get; set; }
    public int AttackPower { get; set; }
    public int HaveMoney { get; set; }
    public Inventory Inventory { get; set; }
    public bool usedStone { get; set; }
    protected Random random = new Random();

    protected Player(string job, int hp, int attackPower, int haveMoney)
    {
        Job = job;
        HP = hp;
        AttackPower = attackPower;
        HaveMoney = haveMoney;
        Inventory = new Inventory();
        usedStone = false;
    }

    public abstract void Attack(Monster monster);
}

public class Warrior : Player
{
    public Warrior() : base("전사", 100, 10, 10) { }

    public override void Attack(Monster monster)
    {
        monster.HP -= AttackPower;
        Console.WriteLine($"{Job}가 {monster.Name}에게 {AttackPower}의 피해를 입혔습니다!");
    }

    // 40% 확률로 몬스터 공격 차단
    public bool BlockAttack()
    {
        return random.Next(0, 100) < 40;
    }
}

public class Mage : Player
{
    public Mage() : base("마법사", 50, 15, 10) { }

    public override void Attack(Monster monster)
    {
        monster.HP -= AttackPower;
        Console.WriteLine($"{Job}가 {monster.Name}에게 {AttackPower}의 피해를 입혔습니다!");
    }
}
public class Archer : Player
{
    public Archer() : base("궁수", 75, 15, 10) { }

    public override void Attack(Monster monster)
    {
        // 80% 확률로 공격 성공
        if (random.Next(0, 100) < 80)
        {
            monster.HP -= AttackPower;
            Console.WriteLine($"{Job}가 {monster.Name}에게 {AttackPower}의 피해를 입혔습니다!");
        }
        else
        {
            Console.WriteLine($"{Job}의 공격이 빗나갔습니다!");
        }
    }
}

public class Rogue : Player
{
    public Rogue() : base("도적", 75, 12, 10) { }

    public override void Attack(Monster monster)
    {
        monster.HP -= AttackPower;
        Console.WriteLine($"{Job}가 {monster.Name}에게 {AttackPower}의 피해를 입혔습니다!");

        // 10% 확률로 추가 공격
        if (random.Next(0, 100) < 10)
        {
            monster.HP -= AttackPower;
            Console.WriteLine($"{Job}가 추가 공격을 성공시켜 {AttackPower}의 추가 피해를 입혔습니다!");
        }
    }
}
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