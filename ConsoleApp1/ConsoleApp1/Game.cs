﻿using Enums;
using Structs;

namespace RoguelikeConsoleGame
{
    public class Game
    {
        private ViewField viewField = ViewField.Title;
        private bool isGameOver = false;

        private Player player;
        private Monster monster;
        private BattleAction battleAction;
        private int wyvernKillCount = 0;

        #region Process
        public void Process()
        {
            // 0. 시작 처리 작업
            Init();
            Start();
            while (!isGameOver)
            {
                // 1. 랜더링
                Render();
                // 2. 입력 처리
                ConsoleKey inputKey = Input();
                // 3. 처리 작업
                Update(inputKey, ref viewField);
            }
            // 4. 끝 처리 작업
            End();
        }

        // 값 초기화 관련 처리
        private void Init()
        {
            int[,] mapNum = new int[11, 11]
            {
                { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, }, // 0
                { 2, 4, 5, 5, 5, 1, 2, 5, 5, 4, 2, }, // 1
                { 2, 2, 2, 2, 1, 1, 2, 5, 2, 2, 2, }, // 2
                { 2, 1, 1, 1, 1, 1, 2, 5, 2, 1, 2, }, // 3
                { 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, }, // 4
                { 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 2, }, // 5
                { 2, 4, 5, 1, 1, 1, 1, 1, 1, 1, 2, }, // 6
                { 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 2, }, // 7
                { 2, 1, 1, 1, 1, 1, 1, 1, 5, 2, 2, }, // 8
                { 2, 1, 1, 1, 1, 5, 1, 2, 5, 4, 2, }, // 9
                { 2, 2, 2, 2, 2, 3, 2, 2, 2, 2, 2, }, // 10
            };

            TileInfo errorTile = new TileInfo((int)Tile.Floor, 'ⓧ', ConsoleColor.Red, false);
            TileInfo floor = new TileInfo((int)Tile.Floor, ' ', ConsoleColor.Black, true);
            TileInfo wall = new TileInfo((int)Tile.Wall, '▦', ConsoleColor.White, false);
            TileInfo potal = new TileInfo((int)Tile.Potal, '@', ConsoleColor.Blue, true);
            TileInfo item = new TileInfo((int)Tile.Item, 'i', ConsoleColor.Yellow, true);
            TileInfo monster = new TileInfo((int)Tile.Monster, 'm', ConsoleColor.DarkRed, true);
            TileInfo[] tiles = new TileInfo[]
            {
                errorTile,
                floor,
                wall,
                potal,
                item,
                monster,
            };

            MapManager.Singleton.Init(tiles, mapNum);
            // 플레이어 초기화 부분은 로비에서 직업 선택 시
            // battleAction = BattleAction.None;
        }

        // 값 세팅 관련 처리
        private void Start()
        {
        }

        // UI or 그림
        private void Render()
        {
            Console.Clear();
            switch (viewField)
            {
                case ViewField.Title:
                    PrintTitle(new Position(0, 0));
                    break;
                case ViewField.SelectJob:
                    PrintSelectJob(new Position(0, 0));
                    break;
                case ViewField.Lobby:
                    PrintLobby(new Position(0, 0));
                    break;
                case ViewField.Town:
                    PrintTown(new Position(0, 0));
                    break;
                case ViewField.Field:
                    PrintField(new Position(0, 0));
                    break;
                case ViewField.Battle:
                    PrintBattle(new Position(0, 0));
                    break;
                case ViewField.Inn:
                    PrintInn(new Position(0, 0));
                    break;
            }
        }

        // 단일 입력 처리
        private ConsoleKey Input()
        {
            return Console.ReadKey(true).Key;
        }

        // Update 처리
        private void Update(ConsoleKey inputKey, ref ViewField view)
        {
            InputUpdate(inputKey, ref view);
            LateUpdate(inputKey, ref view);
        }
        // 입력 처리(입력 키 처리 및 씬변화 처리)
        private void InputUpdate(ConsoleKey inputKey, ref ViewField view)
        {
            // 키 처리
            switch (viewField)
            {
                case ViewField.Title:
                    InputTitle(inputKey);
                    break;
                case ViewField.SelectJob:
                    InputSelectJob(inputKey);
                    break;
                case ViewField.Lobby:
                    InputLobby(inputKey);
                    break;
                case ViewField.Town:
                    InputTown(inputKey);
                    break;
                case ViewField.Field:
                    InputField(inputKey);
                    break;
                case ViewField.Battle:
                    InputBattle(inputKey);
                    break;
                case ViewField.Inn:
                    InputInn(inputKey);
                    break;
            }
        }
        // 마지막 업데이트 (모든 처리의 마지막 후처리)
        private void LateUpdate(ConsoleKey inputKey, ref ViewField view)
        {
            // 후 처리
            switch (viewField)
            {
                case ViewField.Title:
                    break;
                case ViewField.SelectJob:
                    ProcessSelectJob();
                    break;
                case ViewField.Lobby:
                    break;
                case ViewField.Field:
                    break;
                case ViewField.Battle:
                    ProcessBattle();
                    break;
            }
        }

        // 게임 종료 시 후 처리
        static void End()
        {

        }
        #endregion

        #region Print or RenderDraw
        // Print or RenderDraw ViewField
        private void PrintTitle(Position pos)
        {
            Console.SetCursorPosition(pos.x, pos.y);
            Console.WriteLine("===== 엄양의 루피랜드 =====");
            Console.WriteLine("1. 게임 시작");
            Console.WriteLine("2. 종료");
            Console.Write("메뉴를 선택하세요: ");
        }
        private void PrintSelectJob(Position pos)
        {
            Console.SetCursorPosition(pos.x, pos.y);
            Console.WriteLine("직업을 선택하세요: 1. 전사, 2. 마법사, 3. 도적, 4. 궁수");
        }
        private void PrintLobby(Position pos)
        {
            Console.SetCursorPosition(pos.x, pos.y);
            Console.WriteLine("===== 로비 =====");
            Console.WriteLine("1. 마을로 이동");
            Console.WriteLine("2. 필드로 이동");
            Console.WriteLine("3. 여관으로 이동");
            Console.WriteLine("4. 게임 종료");
            Console.Write("메뉴를 선택하세요: ");
        }
        private void PrintTown(Position pos)
        {
            Console.SetCursorPosition(pos.x, pos.y);
            Console.WriteLine("===== 마을 =====");
            Console.WriteLine("1. 상점에서 아이템 구매");
            Console.WriteLine("2. 퀘스트 수락");
            Console.WriteLine("3. 로비로 돌아가기");
            Console.Write("메뉴를 선택하세요: ");
        }
        private void PrintField(Position pos)
        {
            Console.SetCursorPosition(pos.x, pos.y);
            Console.WriteLine("===== 필드 =====");
            Console.WriteLine("1. 몬스터와 싸우기");
            Console.WriteLine("2. 도망가기");
            Console.WriteLine("3. 로비로 돌아가기");
            Console.Write("메뉴를 선택하세요: ");
        }
        private void PrintBattle(Position pos)
        {
            DrawMap(pos);
            DrawPlayerInfo();
            DrawLogs();

            Console.WriteLine("\n행동을 선택하세요:");
            Console.WriteLine("1. 공격");
            Console.WriteLine("2. 도망가기");
        }
        private void PrintInn(Position pos)
        {
            Console.SetCursorPosition(pos.x, pos.y);
            Console.WriteLine("===== 여관 =====");
            Console.WriteLine("1. 휴식 (HP 회복)");
            Console.WriteLine("2. 로비로 돌아가기");
            Console.Write("메뉴를 선택하세요: ");
        }

        // ETC
        private void DrawMap(Position pos)
        {
            MapManager.Singleton.DrawMapTile(pos);
        }
        private void DrawPlayerInfo()
        {
            Console.SetCursorPosition(Console.WindowWidth - 20, 0);
            Console.WriteLine($"PlayerGold: {player.HaveMoney}");
        }
        private void DrawLogs()
        {
            Console.SetCursorPosition(0, Console.WindowHeight - 5);
            Console.WriteLine("로그 출력하면 될것같아요.");
        }
        #endregion

        #region Input
        // Input ViewField
        private void InputTitle(ConsoleKey inputKey)
        {
            switch (inputKey)
            {
                case ConsoleKey.NumPad1:
                    viewField = ViewField.SelectJob;
                    break;
                case ConsoleKey.NumPad2:
                    isGameOver = true;
                    break;
            }
        }
        private void InputSelectJob(ConsoleKey inputKey)
        {
            switch (inputKey)
            {
                case ConsoleKey.NumPad1:
                    player = new Warrior();
                    break;
                case ConsoleKey.NumPad2:
                    player = new Mage();
                    break;
                case ConsoleKey.NumPad3:
                    player = new Rogue();
                    break;
                case ConsoleKey.NumPad4:
                    player = new Archer();
                    break;
                default:
                    player = null;
                    break;
            }
        }
        private void InputLobby(ConsoleKey inputKey)
        {
            switch (inputKey)
            {
                case ConsoleKey.NumPad1:
                    viewField = ViewField.Town;
                    break;
                case ConsoleKey.NumPad2:
                    viewField = ViewField.Field;
                    break;
                case ConsoleKey.NumPad3:
                    viewField = ViewField.Inn;
                    break;
                case ConsoleKey.NumPad4:
                    isGameOver = true;
                    break;
            }
        }
        private void InputTown(ConsoleKey inputKey)
        {
            switch (inputKey)
            {
                case ConsoleKey.NumPad1:
                    // 상점에서 아이템 구매 로직 추가
                    Console.WriteLine("아이템 구매 기능 여기에 넣으면 될듯 >? ");
                    break;
                case ConsoleKey.NumPad2:
                    // 퀘스트 수락 로직 추가
                    Console.WriteLine("와이번 5번 잡으면 그때 몬스터.cs 에서 드래곤 어때요 >?");
                    break;
                case ConsoleKey.NumPad3:
                    viewField = ViewField.Lobby;
                    break;
            }
        }
        private void InputField(ConsoleKey inputKey)
        {
            switch (inputKey)
            {
                case ConsoleKey.NumPad1:
                    if (monster == null || monster.HP <= 0)
                    {
                        monster = Monster.GenerateMonster(wyvernKillCount);
                        viewField = ViewField.Battle;
                        battleAction = BattleAction.None;
                    }
                    break;
                case ConsoleKey.NumPad2:
                    Console.WriteLine("플레이어가 도망쳤습니다!");
                    viewField = ViewField.Lobby;
                    break;
                case ConsoleKey.NumPad3:
                    viewField = ViewField.Lobby;
                    break;
            }
        }
        private void InputBattle(ConsoleKey inputKey)
        {
            switch (inputKey)
            {
                case ConsoleKey.NumPad1:
                    battleAction = BattleAction.Attack;
                    break;
                case ConsoleKey.NumPad2:
                    battleAction = BattleAction.RunAway;
                    break;
                default:
                    battleAction = BattleAction.None;
                    break;
            }
        }
        private void InputInn(ConsoleKey inputKey)
        {
            switch (inputKey)
            {
                case ConsoleKey.NumPad1:
                    player.HP = 100; // HP를 최대로 회복
                    Console.WriteLine("플레이어의 HP가 완전히 회복되었습니다!");
                    break;
                case ConsoleKey.NumPad2:
                    viewField = ViewField.Lobby;
                    break;
            }
        }

        // ETC
        #endregion

        #region Process
        // Process ViewField
        private void ProcessSelectJob()
        {
            if (player == null)
            {
                Console.WriteLine("잘못된 입력입니다. 기본 직업 '전사'로 설정됩니다.");
                player = new Warrior();
            }

            Console.WriteLine($"선택한 직업: {player.Job}, HP: {player.HP}, 공격력: {player.AttackPower}");
            viewField = ViewField.Lobby;
        }
        private void ProcessBattle()
        {
            switch (battleAction)
            {
                case BattleAction.Attack:
                    player.Attack(monster);

                    if (monster.HP > 0)
                    {
                        MonsterAttack();
                    }
                    else
                    {
                        Console.WriteLine($"{monster.Name}을(를) 처치했습니다!");
                        if (monster.Name == "와이번") wyvernKillCount++;
                        monster = null;
                        break;
                    }
                    break;
                case BattleAction.RunAway:
                    Console.WriteLine("플레이어가 도망쳤습니다!");
                    viewField = ViewField.Field;
                    break;
                default:
                    Console.WriteLine("잘못된 입력입니다.");
                    break;
            }

            if (player.HP <= 0)
            {
                Console.WriteLine("플레이어가 사망했습니다. 게임 오버!");
                isGameOver = true;
            }
        }

        // ETC
        private void MonsterAttack()
        {
            int damage = new Random().Next(monster.AttackPower - 5, monster.AttackPower + 5);

            // 전사의 자동 방어 시스템 적용유무는 모르겠음 머쓱 
            if (player is Warrior warrior && warrior.BlockAttack())
            {
                Console.WriteLine($"전사가 몬스터의 공격을 막았습니다!");
                return;
            }

            player.HP -= damage;
            Console.WriteLine($"{monster.Name}이(가) 플레이어를 공격하여 {damage}의 피해를 입혔습니다! 남은 HP: {player.HP}");
        }
        #endregion
    }
}