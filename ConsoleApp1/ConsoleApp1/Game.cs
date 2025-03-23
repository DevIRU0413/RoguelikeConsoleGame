using ConsoleApp1;
using Enums;
using Structs;

namespace RoguelikeConsoleGame
{
    public class Game
    {
        private ViewField viewField = ViewField.Title;
        private bool isGameOver = false;
        private Logger battleLoger = null;

        private Player player;
        private Monster monster;
        private BattleAction battleAction;
        private int wyvernKillCount = 0;
        private Shop shop;

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
            int[,] mapNum = new int[21, 21]
            {
                //0  1  2  3  4  5  6  7  8  9  10 11 12 13 14 15 16 17 18 19 20
                { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, }, // 0
                { 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, }, // 1
                { 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, }, // 2
                { 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, }, // 3
                { 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, }, // 4
                { 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, }, // 5
                { 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, }, // 6
                { 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, }, // 7
                { 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, }, // 1
                { 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, }, // 1
                { 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, }, // 1
                { 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, }, // 1
                { 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, }, // 1
                { 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, }, // 1
                { 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, }, // 1
                { 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, }, // 1
                { 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, }, // 1
                { 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, }, // 1
                { 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, }, // 1
                { 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, }, // 1
                { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, }, // 0
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
            battleLoger = new Logger(19);
            shop = new Shop();
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
            ViewField currentView = view;
            InputUpdate(inputKey, ref view);
            LateUpdate(inputKey, currentView);

        }
        // 입력 처리(입력 키 처리 및 씬변화 처리)
        private void InputUpdate(ConsoleKey inputKey, ref ViewField view)
        {
            // 키 처리
            switch (view)
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
            }
        }
        // 마지막 업데이트 (모든 처리의 마지막 후처리)
        private void LateUpdate(ConsoleKey inputKey, ViewField view)
        {
            // 후 처리
            switch (view)
            {
                case ViewField.Title:
                    break;
                case ViewField.SelectJob:
                    ProcessSelectJob();
                    break;
                case ViewField.Lobby:
                    break;
                case ViewField.Field:
                    ProcessBattle();
                    break;
                //마을에 들어갈때 와이번 킬카운트 초기화
                case ViewField.Town:
                    wyvernKillCount = 0;
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

            DrawMap(pos);
            DrawPlayerPos(player.Position);
            DrawPlayerInfo(new Position(Console.WindowWidth - 20, pos.y));
            battleLoger.DrawLoger(new Position(44, pos.y));

            // DrawPlayerInfo(new Position(44, 0));
            // DrawKeyInfo(new Position(44, 3));
            // battleLoger.DrawLoger(new Position(66, pos.y));
            Console.WriteLine("1. 몬스터와 싸우기");
            Console.WriteLine("2. 도망가기");
            Console.WriteLine("3. 로비로 돌아가기");
            Console.Write("메뉴를 선택하세요: ");
        }

        // ETC
        private void DrawMap(Position pos)
        {
            MapManager.Singleton.DrawMapTile(pos);
        }
        private void DrawPlayerInfo(Position pos)
        {
            Console.SetCursorPosition(pos.x, pos.y);
            Console.WriteLine("*=====Status=======*");
            Console.SetCursorPosition(pos.x, pos.y + 1);
            Console.WriteLine($"*Gold: {player.HaveMoney} / ATK: {player.AttackPower}");
            Console.SetCursorPosition(pos.x, pos.y + 2);
            Console.WriteLine("*==================*");
        }
        private void DrawPlayerPos(Position pos)
        {
            Console.SetCursorPosition(player.Position.x * 2, player.Position.y);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($" ▼");
            Console.ResetColor();
        }
        private void DrawKeyInfo(Position pos)
        {
            Console.SetCursorPosition(pos.x, pos.y);
            Console.WriteLine("*==================*");
            Console.SetCursorPosition(pos.x, pos.y + 1);
            Console.WriteLine("*【이동 】[W.A.S.D]*");
            Console.SetCursorPosition(pos.x, pos.y + 2);
            Console.WriteLine("*【인벤토리 】[I]  *");
            Console.SetCursorPosition(pos.x, pos.y + 3);
            Console.WriteLine("*【능력치 】[J]    *");
            Console.SetCursorPosition(pos.x, pos.y + 4);
            Console.WriteLine("*==================*");
        }
        #endregion

        #region Input
        // Input ViewField
        private void InputTitle(ConsoleKey inputKey)
        {
            switch (inputKey)
            {
                case ConsoleKey.D1:
                    viewField = ViewField.SelectJob;
                    break;
                case ConsoleKey.D2:
                    isGameOver = true;
                    break;
            }
        }
        private void InputSelectJob(ConsoleKey inputKey)
        {
            switch (inputKey)
            {
                case ConsoleKey.D1:
                    player = new Warrior();
                    break;
                case ConsoleKey.D2:
                    player = new Mage();
                    break;
                case ConsoleKey.D3:
                    player = new Rogue();
                    break;
                case ConsoleKey.D4:
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
                case ConsoleKey.D1:
                    viewField = ViewField.Town;
                    break;
                case ConsoleKey.D2:
                    viewField = ViewField.Field;
                    break;

                case ConsoleKey.D3:
                    isGameOver = true;
                    break;
            }
        }
        private void InputTown(ConsoleKey inputKey)
        {
            switch (inputKey)
            {
                case ConsoleKey.D1:
                    shop.OpenShop(player);
                    break;
                case ConsoleKey.D2:
                    // 퀘스트 수락 로직 추가
                    Console.WriteLine("와이번 5번 잡으면 그때 몬스터.cs 에서 드래곤 어때요 >?");
                    break;
                case ConsoleKey.D3:
                    viewField = ViewField.Lobby;
                    break;
            }
        }
        private void InputField(ConsoleKey inputKey)
        {
            switch (inputKey)
            {
                case ConsoleKey.D1:
                    if (monster == null || monster.HP <= 0)
                    {
                        monster = Monster.GenerateMonster(wyvernKillCount);
                        viewField = ViewField.Battle;
                        battleAction = BattleAction.None;
                    }
                    break;
                case ConsoleKey.D2:
                    Console.WriteLine("플레이어가 도망쳤습니다!");
                    viewField = ViewField.Lobby;
                    break;
                case ConsoleKey.D3:
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

            Console.WriteLine($"선택한 직업: {player.Job}, HP: {player.HaveMoney}, 공격력: {player.AttackPower}");
            viewField = ViewField.Lobby;
            player.SetPosition(new Position(10, 10));
        }
        private void ProcessBattle()
        {
            switch (battleAction)
            {
                case BattleAction.Attack:
                    if (monster == null)
                    {
                        break;
                    }
                    player.Attack(monster);

                    if (monster.HP > 0)
                    {
                        MonsterAttack();
                    }
                    else
                    {
                        battleLoger.AddLog($"{monster.Name}을(를) 처치했습니다!");
                        if (monster.Name == "와이번")
                        {
                            wyvernKillCount++;
                            if (wyvernKillCount == 3)
                            {
                                battleLoger.AddLog("음산한 기운이 감싸든다");
                            }
                            else if (wyvernKillCount == 4)
                            {
                                battleLoger.AddLog("싸늘한 기운이 감싸온다.");
                            }
                            else if (wyvernKillCount == 5)
                            {
                                battleLoger.AddLog("멀리서 표효 소리가 들려온다.");
                            }
                        }
                        monster = null;
                    }
                    break;
                case BattleAction.RunAway:
                    Console.WriteLine("플레이어가 도망쳤습니다!");
                    viewField = ViewField.Lobby;
                    break;
                default:
                    Console.WriteLine("잘못된 입력입니다.");
                    break;
            }

            if (player.HaveMoney <= 0)
            {
                battleLoger.AddLog("플레이어가 사망했습니다. 게임 오버!");
                isGameOver = true;
            }
        }

        // ETC
        private void InGameUpdate()
        {
            int currentTileNum = MapManager.Singleton.MapTiles[player.Position.y, player.Position.x];
            // 현재 위치의 타일 검색
            TileInfo currenTileInfo = MapManager.Singleton.TileInfos[currentTileNum];

            if (player.Position.y == player.BeforePosition.y && player.Position.x == player.BeforePosition.x)
                return;
            else
                battleLoger.AddLog($"[MOVE] [x {player.BeforePosition.x}, y {player.BeforePosition.y}] > [x {player.Position.x}, y {player.Position.y}]");

            // 검색된 타일을 가지고 처리
            if (currenTileInfo.tileID == 3) // 포탈
            {
                battleLoger.AddLog("!!!!!!!!!! = Potal = !!!!!!!!");
            }
            else if (currenTileInfo.tileID == 4) // 아이템
            {
                battleLoger.AddLog("!!!!!!!!!! = ITEM = !!!!!!!!");

            }
            else if (currenTileInfo.tileID == 5) // 몬스터
            {
                battleLoger.AddLog("!!!!!!!!!! = MONSTER = !!!!!!!!");
            }
            else if (currenTileInfo.tileID == 0)
            {
                Console.WriteLine("!!!!!!!!!!ERROR!!!!!!!!");
                Console.WriteLine("!!!!!!!!!!ERROR!!!!!!!!");
                Console.WriteLine("!!!!!!!!!!ERROR!!!!!!!!");
                Console.WriteLine("!!!!!!!!!!ERROR!!!!!!!!");
                Console.WriteLine("!!!!!!!!!!ERROR!!!!!!!!");
                Console.WriteLine("!!!!!!!!!!ERROR!!!!!!!!");
                Console.WriteLine("!!!!!!!!!!ERROR!!!!!!!!");
            }
        }
        private void Move(ConsoleKey key)
        {
            Position direction = new Position();
            Position movePos = new Position();

            switch (key)
            {
                // 왼
                case ConsoleKey.A:
                case ConsoleKey.LeftArrow:
                    direction.x--;
                    break;
                // 위
                case ConsoleKey.W:
                case ConsoleKey.UpArrow:
                    direction.y--;
                    break;
                // 오
                case ConsoleKey.D:
                case ConsoleKey.RightArrow:
                    direction.x++;
                    break;
                // 아래
                case ConsoleKey.S:
                case ConsoleKey.DownArrow:
                    direction.y++;
                    break;
                default:
                    return;
            }

            movePos.y = player.Position.y + direction.y;
            movePos.x = player.Position.x + direction.x;

            // 위치 음수 값 처리
            // 위치 최대 값 처리
            // 위치 불가 처리
            if (!MapManager.Singleton.IsInMap(movePos) || !MapManager.Singleton.IsPosMovable(movePos))
                player.SetPosition(player.Position);
            else
                player.SetPosition(movePos);
        }
        private void MonsterAttack()
        {
            int damage = new Random().Next(monster.AttackPower - 5, monster.AttackPower + 5);

            // 전사의 자동 방어 시스템 적용유무는 모르겠음 머쓱 
            if (player is Warrior warrior && warrior.BlockAttack())
            {
                battleLoger.AddLog($"전사가 몬스터의 공격을 막았습니다!");
                return;
            }

            player.HaveMoney -= damage;
            battleLoger.AddLog($"{monster.Name}이(가) 플레이어에게 {damage}의 피해를 입혔습니다! 남은 HP: {player.HaveMoney}");

            if (player.HaveMoney <= 0)
            {   // 기회창출의 돌 로직 
                if (player.HasStone)
                {
                    player.HaveMoney += 100;
                    player.HasStone = false;
                    battleLoger.AddLog("알수 없는 힘에 의해 죽음을 면했습니다");
                }
                else
                {
                    battleLoger.AddLog("플레이어가 사망했습니다.");
                    isGameOver = true;
                }
            }
        }
        #endregion
    }
}