using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace NecroDancer
{
    internal class BattleManager : IManagerInterFace
    {
        TileManager _tileManager;
        Player player;
        Monster monster;
        List<Item> dropItemList;//바닥에 아이템 떨군거 표시
        List<Unit> units;
        Point monsterSpawnPos;//나중에 랜덤으로 몬스터 생성할때 재활용 할 좌표
        Point tempPos;
        Point playerTempPos;
        TileType tempTile; // 멥에 변동이 생겼을때 원래 가지고있던 타일값 저장하는 용도.
        TileType tempTile2; // 멥에 변동이 생겼을때 원래 가지고있던 타일값 저장하는 용도.

        Queue<Point> monsterPosQueue;//몬스터들 이동좌표 미리저장해둘곳.
        Queue<TileType> tempTileTypes;//몬스터들 이동할 타일 미리저장해둠.

        bool isPlayerMove = false;

        public void Init()
        {
            monsterPosQueue = new Queue<Point>();
            tempTileTypes = new Queue<TileType>();


            _tileManager = new TileManager();
            _tileManager.Init();

            player = new Player();

            monster = new Monster();

            dropItemList = new List<Item>();

            tempPos = new Point(0, 0);

            playerTempPos = new Point(0, 3);

            monsterSpawnPos = new Point(8, 0);

            tempTileTypes.Enqueue(_tileManager.tiles[monsterSpawnPos.Y, monsterSpawnPos.X].GetTileType());
            tempTile2 = _tileManager.tiles[playerTempPos.Y, playerTempPos.X].GetTileType();

            monster.Spawn(monsterSpawnPos);
            player.Spawn(playerTempPos);

            //tempPos = monster.point;
            playerTempPos = monster.point;
        }
        public void Update()
        {
            GameManager.isGameStart = true;

            #region 플레이어이동


            if (isPlayerMove)
            {
                playerTempPos = player.point;
            }

            switch (GameManager.input.Key)
            {
                //방향 이동.
                case ConsoleKey.A:
                case ConsoleKey.LeftArrow:

                    isPlayerMove = player.Move(Fword.Left);

                    break;
                case ConsoleKey.RightArrow:
                case ConsoleKey.D:
                    isPlayerMove = player.Move(Fword.Right);


                    break;
                case ConsoleKey.UpArrow:
                case ConsoleKey.W:
                    isPlayerMove = player.Move(Fword.Up);


                    break;
                case ConsoleKey.S:
                case ConsoleKey.DownArrow:
                    isPlayerMove = player.Move(Fword.Down);

                    break;
            }

            if (isPlayerMove)
            {
                _tileManager.SetTile(playerTempPos, tempTile2);//지나간자리 초기화


                _tileManager.SetTile(player.point, TileType.Player);
                tempTile2 = _tileManager.tiles[playerTempPos.Y, playerTempPos.X].GetTileType();
            }

            #endregion

            #region 몬스터이동


      
            //임시 좌표로 몬스터 이동 가능한 지역인가 탐색

            //해당 위치가 플레이어가 아닌가?
            if(_tileManager.tiles[tempPos.Y, tempPos.X].GetTileType() != TileType.Player)
            {
                //임시 좌표에 몬스터 좌표 넣어둠.
                tempPos = monster.point;

                //임시 좌표에 이동할 좌표 미리옮겨봄. 옮긴거 큐에 넣어둠.
                monsterPosQueue.Enqueue(monster.TempMovePos(player, tempPos));

                //이동할 좌표의 타일 저장
                tempTileTypes.Enqueue(_tileManager.tiles[tempPos.Y, tempPos.X].GetTileType());

                //이동하기 전 좌표에 이전 타일 배치.
                _tileManager.SetTile(monster.point, tempTileTypes.Dequeue());

                //임시 좌표로 몬스터 이동.
                monster.Move(player, monsterPosQueue.Dequeue());

                //몬스터 이동한 위치에 몬스터 그려주기.
                _tileManager.SetTile(monster.point, TileType.Monster);

            }
            //이동 불가지역임. 나중에 몬스터별로 행동다르게 할지도
            else
            {

 
            }
            #endregion







            //if (tempPos != player.point)
            //{
            //    //이동할 좌표 저장해둔거 넘김.
            //    monster.Move(player, tempPos);



            //    _tileManager.SetTile(tempPos, tempTile);//지나간자리 초기화
            //                                            //_tileManager.SetTile(tempPos, TileType.Floor);//지나간자리 초기화

            //    _tileManager.SetTile(monster.point, TileType.Monster);

            //    tempTile = _tileManager.tiles[tempPos.Y, tempPos.X].GetTileType();
            //    //_tileManager.SetTile(tempPos, tempTile);//지나간자리 초기화

            //}
        }

        public void Render()
        {
            _tileManager.Render();
            Console.SetCursorPosition(20, 0);
            Console.Write($"Monster X : {monster.point.X} Y : {monster.point.Y}");
            Console.SetCursorPosition(20, 1);
            Console.Write($"tempPos X : {tempPos.X} Y : {tempPos.Y}");
            Console.SetCursorPosition(20, 2);
            Console.Write($"Player  X : {player.point.X} Y : {player.point.Y}");
            Console.SetCursorPosition(20, 3);
            Console.Write($"tempPosTile: {_tileManager.tiles[tempPos.Y,tempPos.X].GetTileType()}");
        }


    }
}
