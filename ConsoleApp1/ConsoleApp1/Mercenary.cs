using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RoguelikeConsoleGame
{
    public class Mercenary
    {
        public int HaveMoney { get; set; }
        public int AttackPower { get; set; }
        public int DamageReduction { get; set; }

        public Mercenary()
        {
            HaveMoney = 200;
            AttackPower = 7;
            DamageReduction = 5;
        }

        public void TakeDamage(int damage)
        {
            HaveMoney -= damage;
            if (HaveMoney < 0) HaveMoney = 0;
        }

        public void Attack(Monster monster)
        {
            monster.HP -= AttackPower;
            Console.WriteLine($"용병이 {monster.Name}에게 {AttackPower}의 피해를 입혔습니다!");
        }
        public void HireResponse(int offeredGold)
        {
            // 애니메이션 효과 추가
            for (int i = 0; i < 3; i++)
            {
                Console.Clear();
                switch (i)
                {
                    case 0:
                        Console.WriteLine("용병: 돈을 세어본다...");
                        break;
                    case 1:
                        Console.WriteLine("용병: 미묘한 표정을 짓는다...");
                        break;
                    case 2:
                        if (offeredGold <= 150)
                        {
                            Console.WriteLine("용병: 하하하... 겨우 그돈으로 뭘 바라는거지?");
                        }
                        else if (offeredGold <= 299)
                        {
                            Console.WriteLine("용병: 조금만 더 성의 있게 제안해봐.");
                        }
                        else
                        {
                            Console.WriteLine("용병: 좋아 거래성사야 내가 따라가주지.");
                        }
                        break;
                }
                Thread.Sleep(1500); 
            }
        }
    }
}
