using Structs;
using System.Diagnostics;

namespace ConsoleApp1
{
    public class Logger
    {
        private string[] logs;
        private int count;

        public Logger(int size)
        {
            logs = new string[size];
            count = 0;
        }

        public void AddLog(string eventLog)
        {
            // 빈 공간 없음
            if (count == this.logs.Length)
            {
                // 한칸씩 앞으로 이동
                for (int i = 0; i < this.logs.Length - 1; i++)
                    this.logs[i] = this.logs[i + 1];

                this.logs[this.logs.Length - 1] = eventLog;
            }
            // 빈 공간 있음
            else
            {
                this.logs[count] = eventLog;
                count++;
            }
        }

        public void Clear()
        {
            count = 0;
            for (int i = 0; i < logs.Length; i++)
                logs[i] = null;
        }
        public int GetLogCount()
        {
            return count;
        }
        public string[] GetLogs()
        {
            return logs;
        }

        public void DrawLoger(Position pos)
        {
            if (logs == null) return;

            Console.SetCursorPosition(pos.x, pos.y);
            Console.WriteLine("*===============LOGGER===============*");
            for (int i = 0; i < logs.Length; i++)
            {
                Console.SetCursorPosition(pos.x, pos.y + 1 + i);
                if (logs[i] == null)
                    Console.WriteLine($"*{i + 1}");
                else
                    Console.WriteLine($"*{logs[i]}");
            }
            Console.SetCursorPosition(pos.x, pos.y + 1 + logs.Length);
            Console.WriteLine("*====================================*");
        }
    }
}
