using System;
using System.Linq;
using System.Threading;


namespace ConsoleApp1
{
    
    public class HorseRaceGame
    {
        public void StartRace()
        {
            string[] horses = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };
            int[] positions = new int[horses.Length];
            Random random = new Random();
            bool raceFinished = false;

            Console.Clear();
            Console.WriteLine("경마장에 온걸 환영한다.");
            Console.WriteLine("어느 말에 배팅할꺼야? (1번부터 10번까지 입력)");

            // 플레이어 베팅
            int playerBet = -1;
            while (playerBet < 1 || playerBet > 10)
            {
                Console.Write(">> ");
                if (int.TryParse(Console.ReadLine(), out playerBet) && playerBet >= 1 && playerBet <= 10)
                {
                    Console.WriteLine($"{playerBet}번 말({horses[playerBet - 1]})에 배팅하였다\n");
                }
                else
                {
                    Console.WriteLine("말은 10마리밖에 없다구? ");
                }
            }

            Console.WriteLine("경마가 시작됩니다! 준비하세요!\n");

            while (!raceFinished)
            {
                Console.Clear();
                DrawTrack(horses, positions);
                DrawRanking(horses, positions);

                for (int i = 0; i < horses.Length; i++)
                {
                    positions[i] += random.Next(1, 4); // 말의 랜덤 이동

                    if (positions[i] >= 50) // 우승 조건
                    {
                        raceFinished = true;
                        Console.Clear();
                        DrawTrack(horses, positions);
                        DrawRanking(horses, positions);

                        Console.WriteLine($"\n우승 말은~~~ {horses[i]}!");

                        if (i + 1 == playerBet)
                        {
                            Console.WriteLine("축하 한다 돈은 여기있다.");
                        }
                        else
                        {
                            Console.WriteLine("돈 잃었네 ㅋ ");
                        }
                        break;
                    }
                }

                Thread.Sleep(500); // 실시간 업데이트를 위해 대기
            }

            Console.WriteLine("\n내일도 경마는 열리니 찾아와! ");
        }

        private void DrawTrack(string[] horses, int[] positions)
        {
            Console.WriteLine(new string('-', 50));
            for (int i = 0; i < horses.Length; i++)
            {
                Console.Write($"{horses[i]}: ");
                Console.Write(new string('=', positions[i]));
                Console.WriteLine(">");
            }
        }

        private void DrawRanking(string[] horses, int[] positions)
        {
            // 순위 계산
            var ranking = horses.Select((horse, index) => new { Horse = horse, Position = positions[index] })
                                .OrderByDescending(h => h.Position)
                                .ToList();

            // 오른쪽 UI 출력
            Console.SetCursorPosition(60, 0);
            Console.WriteLine("<실시간 순위>");

            for (int i = 0; i < ranking.Count; i++)
            {
                Console.SetCursorPosition(60, i + 2);
                Console.WriteLine($"{i + 1}위: {ranking[i].Horse} (거리: {ranking[i].Position}km)");
            }
        }
    }
}


