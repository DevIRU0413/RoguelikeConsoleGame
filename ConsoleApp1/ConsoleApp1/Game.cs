using ConsoleApp1;
using Enums;
using Structs;

namespace RoguelikeConsoleGame
{
    public class Game
    {
        private ViewField viewField = ViewField.Title;
        private bool isGameOver = false;
        private Logger fieldLogger = null;
        private Logger battleLogger = null;

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
            Dictionary<int, int[,]> mapNums = new Dictionary<int, int[,]>
            {
                {
                    1, new int[21,21]
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
                        { 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 5, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, }, // 1
                        { 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, }, // 1
                        { 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, }, // 1
                        { 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, }, // 1
                        { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, }, // 0
                    }
                },
                {
                    2, new int[21, 21]
                    {
                        //0  1  2  3  4  5  6  7  8  9  10 11 12 13 14 15 16 17 18 19 20
                        { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, }, // 0
                        { 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, }, // 1
                        { 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, }, // 2
                        { 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, }, // 3
                        { 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, }, // 4
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
                        { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, }, // 0
                    }
                },
                {
                    3, new int[21, 21]
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
                        { 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, }, // 1
                        { 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, }, // 1
                        { 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, }, // 1
                        { 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, }, // 1
                        { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, }, // 0
                    }
                }
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

            MapManager.Singleton.Init(tiles, mapNums, 1);
            fieldLogger = new Logger(19);
            battleLogger = new Logger(20);
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
                case ViewField.Town:
                    PrintTown(new Position(0, 0));
                    break;
                case ViewField.Field:
                    PrintField(new Position(0, 0));
                    break;
                case ViewField.Battle:
                    PrintBattle(new Position(0, 0));
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
                case ViewField.Town:
                    InputTown(inputKey);
                    break;
                case ViewField.Field:
                    InputField(inputKey);
                    break;
                case ViewField.Battle:
                    InputBattle(inputKey);
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
                    ProcessField();
                    break;
                case ViewField.Battle:
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
            fieldLogger.DrawLoger(new Position(44, pos.y));

            // DrawPlayerInfo(new Position(44, 0));
            // DrawKeyInfo(new Position(44, 3));
            // battleLoger.DrawLoger(new Position(66, pos.y));
        }
        private void PrintBattle(Position pos)
        {
            Console.SetCursorPosition(pos.x, pos.y);
            Console.WriteLine("*============= < 배틀 > =============*");
            battleLogger.DrawLoger(pos + new Position(0, 1));

            pos += new Position(0, 23);
            Console.SetCursorPosition(pos.x, pos.y);
            Console.WriteLine("*==================*");
            Console.SetCursorPosition(pos.x, pos.y + 1);
            Console.WriteLine("*【공격하기 】[1] *");
            Console.SetCursorPosition(pos.x, pos.y + 2);
            Console.WriteLine("*【도망가기 】[2] *");
            Console.SetCursorPosition(pos.x, pos.y + 3);
            Console.WriteLine("*==================*");
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
            // 이동 키
            Move(inputKey);
        }
        private void InputBattle(ConsoleKey inputKey)
        {
            switch (inputKey)
            {
                // 공격
                case ConsoleKey.D1:
                    battleAction = BattleAction.Attack;
                    break;
                // 도망
                case ConsoleKey.D2:
                    battleAction = BattleAction.RunAway;
                    break;
                default:
                    battleAction = BattleAction.None;
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
            viewField = ViewField.Field;
            player.SetPosition(new Position(10, 10));
        }
        private void ProcessField()
        {
            int currentTileNum = MapManager.Singleton.GetTileInfo(player.Position).tileID;
            Tile currentPosTile = (Tile)currentTileNum;

            // 위치가 이전 위치와 같다면 냅둠.
            if (player.Position.y == player.BeforePosition.y
                && player.Position.x == player.BeforePosition.x)
                return;
            // 다른면 위치 출력
            else
                fieldLogger.AddLog($"[MOVE] [x {player.BeforePosition.x}, y {player.BeforePosition.y}] " +
                    $"> [x {player.Position.x}, y {player.Position.y}]");


            switch (currentPosTile)
            {
                case Tile.Floor:
                    break;
                case Tile.Potal:
                    fieldLogger.AddLog("!!!!!!!!!! = Potal = !!!!!!!!");

                    int newFloor = player.Position.floor + 1;

                    if (MapManager.Singleton.IsInMap(newFloor, player.Position))
                    {
                        player.SetPosition(new Position(player.Position.x, player.Position.y, newFloor));
                        MapManager.Singleton.ChangeFloor(newFloor);
                        viewField = ViewField.Field;
                    }
                    else
                    {
                        fieldLogger.AddLog("오류");
                    }
                    break;
                case Tile.Item:
                    fieldLogger.AddLog("!!!!!!!!!! = ITEM = !!!!!!!!");

                    break;
                case Tile.Monster:
                    fieldLogger.AddLog("!!!!!!!!!! = MONSTER = !!!!!!!!");
                    monster = Monster.GenerateMonster(wyvernKillCount);
                    viewField = ViewField.Battle;
                    battleLogger.Clear();
                    break;
                default:
                    Console.WriteLine("!!!!!!!!!!ERROR!!!!!!!!");
                    Console.WriteLine("!!!!!!!!!!ERROR!!!!!!!!");
                    Console.WriteLine("!!!!!!!!!!ERROR!!!!!!!!");
                    Console.WriteLine("!!!!!!!!!!ERROR!!!!!!!!");
                    Console.WriteLine("!!!!!!!!!!ERROR!!!!!!!!");
                    Console.WriteLine("!!!!!!!!!!ERROR!!!!!!!!");
                    Console.WriteLine("!!!!!!!!!!ERROR!!!!!!!!");
                    break;
            }
        }
        private void ProcessBattle()
        {
            bool monsterKill = false;
            switch (battleAction)
            {
                case BattleAction.Attack:
                    // 몬스터 생존 시
                    if (monster.HP > 0)
                    {
                        // 직업에 따른 공격 타입 정하기
                        PlayerAttackType attackType = player.Attack();
                        // 공격 타입에 따른 데미지 산출
                        int hitDamage = 0;
                        bool isAttack = true;
                        switch (attackType)
                        {
                            case PlayerAttackType.Miss:
                                battleLogger.AddLog($"{player.Job}가 공격 실패!!");
                                isAttack = false;
                                break;
                            case PlayerAttackType.Attack:
                                hitDamage = player.AttackPower;
                                battleLogger.AddLog($"{player.Job}가 공격 성공!!");
                                break;
                            case PlayerAttackType.DoubleAttack:
                                hitDamage = player.AttackPower * 2;
                                battleLogger.AddLog($"{player.Job}가 더블 공격 성공!!");
                                break;
                            case PlayerAttackType.CriticalAttack:
                                hitDamage = (int)Math.Round(player.AttackPower * 1.2d);
                                battleLogger.AddLog($"{player.Job}가 크리티컬 공격 성공!!");
                                break;
                        }

                        // 공격 성공 시
                        if (isAttack)
                        {
                            // 데미지 적용
                            monster.HP -= hitDamage;
                            // 데미지 결과 콘솔 입력
                            battleLogger.AddLog($"{player.Job}가 {monster.Name}에게 {hitDamage}의 피해를 입혔습니다!");
                        }
                        monsterKill = (monster.HP <= 0);
                    }
                    break;
                case BattleAction.RunAway:
                    fieldLogger.AddLog("플레이어가 도망쳤습니다!");
                    viewField = ViewField.Field;
                    break;
            }

            // 몬스터 잡았을 때.
            if (monsterKill)
            {
                fieldLogger.AddLog($"{monster.Name}을(를) 처치했습니다!");
                if (monster.Name == "와이번")
                {
                    wyvernKillCount++;
                    if (wyvernKillCount == 3)
                    {
                        fieldLogger.AddLog("음산한 기운이 감싸든다");
                    }
                    else if (wyvernKillCount == 4)
                    {
                        fieldLogger.AddLog("싸늘한 기운이 감싸온다.");
                    }
                    else if (wyvernKillCount == 5)
                    {
                        fieldLogger.AddLog("멀리서 표효 소리가 들려온다.");
                    }
                }
                monster = null;
                viewField = ViewField.Field;
            }
            // 몬스터 살아있을 때
            else
            {
                MonsterAttack();
                if (player.HaveMoney <= 0)
                {   // 기회창출의 돌 로직 
                    if (player.HasStone)
                    {
                        player.HaveMoney += 100;
                        player.HasStone = false;
                        battleLogger.AddLog("알수 없는 힘에 의해 죽음을 면했습니다");
                    }
                    else
                    {
                        battleLogger.AddLog("플레이어가 사망했습니다.");
                        isGameOver = true;
                    }
                }
            }
        }
        // ETC
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

            movePos = player.Position + direction;

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
                battleLogger.AddLog($"전사가 몬스터의 공격을 막았습니다!");
                return;
            }

            player.HaveMoney -= damage;
            battleLogger.AddLog($"{monster.Name}이(가) 플레이어에게 {damage}의 피해를 입혔습니다! 남은 Money: {player.HaveMoney}");
        }
        #endregion
    }
}