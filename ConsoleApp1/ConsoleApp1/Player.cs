namespace RoguelikeConsoleGame;

public abstract class Player
{
    public string Job { get; }
    public int HP { get; set; }
    public int AttackPower { get; }
    public int HaveMoney { get; }
    protected Random random = new Random();

    protected Player(string job, int hp, int attackPower, int haveMoney)
    {
        Job = job;
        HP = hp;
        AttackPower = attackPower;
        HaveMoney = haveMoney;
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
