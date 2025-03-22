namespace RoguelikeConsoleGame;

public class Monster
{
    public string Name { get; }
    public int HP { get; set; }
    public int AttackPower { get; }

    public Monster(string name, int hp, int attackPower)
    {
        Name = name;
        HP = hp;
        AttackPower = attackPower;
    }

    public static Monster GenerateMonster(int wyvernKillCount)
    {
        Random random = new Random();
        int monsterType = random.Next(0, wyvernKillCount >= 5 ? 4 : 3); // 드래곤은 와이번 처치 5회 이후 등장

        switch (monsterType)
        {
            case 0:
                return new Monster("슬라임", 50, 10);
            case 1:
                return new Monster("스켈레톤", 75, 15);
            case 2:
                return new Monster("와이번", 100, 20);
            case 3:
                return new Monster("드래곤", 150, 30);
            default:
                throw new Exception("몬스터 생성 오류");
        }
    }
}
