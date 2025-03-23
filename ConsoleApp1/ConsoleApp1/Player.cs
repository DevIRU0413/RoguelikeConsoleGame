using ConsoleApp1;
using Enums;
using Structs;

namespace RoguelikeConsoleGame;

public abstract class Player
{
    public string Job { get; }

    public int AttackPower { get; set; }
    public int HaveMoney { get; set; }

    public Position Position { get; private set; }
    public Position BeforePosition { get; private set; }


    public Inventory Inventory { get; set; }
    public bool usedStone { get; set; }

    public bool HasStone { get; set; }
    public Mercenary Mercenary { get; set; }
    protected Random random = new Random();

    protected Player(string job, int attackPower, int haveMoney)
    {
        Job = job;

        AttackPower = attackPower;
        HaveMoney = haveMoney;
        Inventory = new Inventory();
        usedStone = false;
        HasStone = false;
        Mercenary = null;
    }

    public abstract PlayerAttackType Attack();

    public void SetPosition(Position pos)
    {
        BeforePosition = Position;
        Position = pos;
    }

    public void HireMercenary()
    {
        Console.WriteLine("용병에게 얼마를 제안하시겠습니까?");
        string input = Console.ReadLine();
        if (int.TryParse(input, out int offeredGold))
        {
            if (offeredGold < 0)
            {
                return;
            }

            if (HaveMoney >= offeredGold)
            {
                HaveMoney -= offeredGold;
                if (Mercenary == null)
                {
                    Mercenary = new Mercenary();
                }
                Mercenary.HireResponse(offeredGold); // 용병의 반응 출력
            }
        }
        else
        {
            Console.WriteLine("잘못된 입력입니다.");
        }
    }
}

public class Warrior : Player
{
    public Warrior() : base("전사", 10, 100) { }

    public override PlayerAttackType Attack()
    {
        return PlayerAttackType.Attack;
    }

    // 40% 확률로 몬스터 공격 차단
    public bool BlockAttack()
    {
        return random.Next(0, 100) < 40;
    }
}

public class Mage : Player
{
    public Mage() : base("마법사", 15, 50) { }

    public override PlayerAttackType Attack()
    {
        return PlayerAttackType.Attack;
    }
}
public class Archer : Player
{
    public Archer() : base("궁수", 15, 75) { }

    public override PlayerAttackType Attack()
    {
        return (random.Next(0, 100) < 80) ? PlayerAttackType.Attack : PlayerAttackType.CriticalAttack;
    }
}

public class Rogue : Player
{
    public Rogue() : base("도적", 12, 75) { }

    public override PlayerAttackType Attack()
    {
        int attackPercent = random.Next(0, 100);
        if (attackPercent < 30)
            return PlayerAttackType.Attack;
        else
            return PlayerAttackType.DoubleAttack;
    }
}
