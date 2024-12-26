using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace NecroDancer
{
    internal class BattleManager : IManagerInterFace
    {
        TileManager tileManager;
        Player player;
        Monster monster;
        List<Item> dropItemList;//바닥에 아이템 떨군거 표시
        List<Unit> units;//나중에 몬스터들 여기다 다 넣을거임.
        Point monsterSpawnPos;//나중에 랜덤으로 몬스터 생성할때 재활용 할 좌표
        Point tempPos;
        Point playerTempPos;
        TileType tempTile2; // 멥에 변동이 생겼을때 원래 가지고있던 타일값 저장하는 용도.

        Queue<Point> monsterPosQueue;//몬스터들 이동좌표 미리저장해둘곳.
        Queue<TileType> tempTileTypes;//몬스터들 이동할 타일 미리저장해둠.

        bool isPlayerMove = false;

        public void Init()
        {
            monsterPosQueue = new Queue<Point>();
            tempTileTypes = new Queue<TileType>();


            tileManager = new TileManager();
            tileManager.Init();

            player = new Player();

            monster = new Monster();

            dropItemList = new List<Item>();

            tempPos = new Point(0, 0);

            playerTempPos = new Point(0, 3);

            monsterSpawnPos = new Point(8, 0);

            tempTileTypes.Enqueue(tileManager.tiles[monsterSpawnPos.Y, monsterSpawnPos.X].GetTileType());
            tempTile2 = tileManager.tiles[playerTempPos.Y, playerTempPos.X].GetTileType();

            monster.Spawn(monsterSpawnPos);
            player.Spawn(playerTempPos);

            //tempPos = monster.point;
            playerTempPos = monster.point;
        }
        public void Update()
        {
            GameManager.isGameStart = true;

            #region 플레이어이동


            if (isPlayerMove)//이동해서 플레이어 좌표가 변하기 전의 정보를 넣어줘야함. 안그럼 타일에 정보남음.
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
                if (tileManager.tiles[player.point.Y,player.point.X].GetTileType() == TileType.Monster)
                {
                    //일단 이동부터 취소
                    player.point = playerTempPos;
                    //플레이어가 공격?
                    player.Attack(monster);
                    
                    //그리고 끝?

                }
                //아마 타일이 공격가능한경우도 넣어야할듯.우선순위는 몬스터인듯?
                else
                {
                    tileManager.SetTile(playerTempPos, tempTile2);//지나간자리 초기화


                    tileManager.SetTile(player.point, TileType.Player);
                    tempTile2 = tileManager.tiles[playerTempPos.Y, playerTempPos.X].GetTileType();
                }

            }

            #endregion

            #region 몬스터이동


            if (monster.Life > 0)
            {

                //임시 좌표에 몬스터 좌표 넣어둠.
                tempPos = monster.point;


                //임시 좌표에 이동할 좌표 미리옮겨봄. 옮긴거 큐에 넣어둠.
                monsterPosQueue.Enqueue(monster.TempMovePos(player, tempPos));

                //임시 좌표로 몬스터 이동 가능한 지역인가 탐색

                //해당 위치가 플레이어가 아닌가?
                if (tileManager.tiles[monsterPosQueue.Peek().Y, monsterPosQueue.Peek().X].GetTileType() != TileType.Player)
                {

                    //이동할 좌표의 타일 저장
                    tempTileTypes.Enqueue(tileManager.tiles[monsterPosQueue.Peek().Y, monsterPosQueue.Peek().X].GetTileType());
                    Console.SetCursorPosition(20, 3);
                    Console.Write($"tempPosTile: {tempTileTypes.Peek()}");

                    //이동하기 전 좌표에 이전 타일 배치.
                    tileManager.SetTile(tempPos, tempTileTypes.Dequeue());

                    //다음 좌표로 몬스터 이동.
                    monster.Move(player, monsterPosQueue.Dequeue());

                    //몬스터 이동한 위치에 몬스터 그려주기.                
                    tileManager.SetTile(monster.point, TileType.Monster);

                }
                //이동 불가지역임. 나중에 몬스터별로 행동다르게 할지도
                else if (tileManager.tiles[monsterPosQueue.Peek().Y, monsterPosQueue.Peek().X].GetTileType() == TileType.Player)
                {

                    monster.Attack(player);
                    //나중에 죽은부분여기다 넣어야할지도?

                    monsterPosQueue.Dequeue();

                }
                else
                {
                    //몬스터 미리움직인 좌표 지움 다음을위해서.
                    monsterPosQueue.Dequeue();
                }



            }
            //몬스터 다죽음
            //지금은 몬스터 죽은 뒤처릴 어케할건가에 대한 고민
            else
            {
                monster.Die();
                
            }
            #endregion







            //if (tempPos != player.point)
            //{
            //    //이동할 좌표 저장해둔거 넘김.
            //    monster.Move(player, tempPos);



            //    tileManager.SetTile(tempPos, tempTile);//지나간자리 초기화
            //                                            //tileManager.SetTile(tempPos, TileType.Floor);//지나간자리 초기화

            //    tileManager.SetTile(monster.point, TileType.Monster);

            //    tempTile = tileManager.tiles[tempPos.Y, tempPos.X].GetTileType();
            //    //tileManager.SetTile(tempPos, tempTile);//지나간자리 초기화

            //}
        }

        public void Render()
        {
            tileManager.Render();
            Console.SetCursorPosition(20, 0);
            Console.Write($"Monster Life : {monster.Life}");
            //Console.Write($"Monster X : {monster.point.X} Y : {monster.point.Y}");
            Console.SetCursorPosition(20, 1);
            Console.Write($"tempPos X : {tempPos.X} Y : {tempPos.Y}");
            Console.SetCursorPosition(20, 2);
            Console.Write($"Player Life : {player.Life}");
            Console.SetCursorPosition(20, 3);
            //Console.Write($"tempPosTile: {tileManager.tiles[tempPos.Y,tempPos.X].GetTileType()}");
            //Console.Write($"tempPosTile: {tempTileTypes.Peek()}");
            Console.SetCursorPosition(20, 4);
            Console.WriteLine($"{monsterPosQueue.Count}");

        }


    }
}
