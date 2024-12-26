using System;
using System.Collections.Generic;
using System.Security.Policy;
using System.Threading;

namespace NecroDancer
{
    internal class BattleManager : IManagerInterFace
    {
        //TileManager TileManager;
        Player player;
        Monster monster;
        List<Item> dropItemList;//바닥에 아이템 떨군거 표시
        List<Unit> monsters;//나중에 몬스터들 여기다 다 넣을거임.
        Point monsterSpawnPos;//나중에 랜덤으로 몬스터 생성할때 재활용 할 좌표
        Point tempPos;
        Point playerTempPos;
        TileType playerTempTile; // 멥에 변동이 생겼을때 원래 가지고있던 타일값 저장하는 용도.

        Queue<Point> monsterPosQueue;//몬스터들 이동좌표 미리저장해둘곳.
        Queue<TileType> tempTileTypes;//몬스터들 이동할 타일 미리저장해둠.

        bool isPlayerMove = false;

        public void Init()
        {
            monsterPosQueue = new Queue<Point>();
            tempTileTypes = new Queue<TileType>();
            monsters = new List<Unit>();


            //TileManager = new TileManager();
            TileManager.Init();

            player = new Player();

            //monster = new Monster();



            dropItemList = new List<Item>();

            tempPos = new Point(0, 0);

            playerTempPos = new Point(0, 3);

            Random random = new Random();

            int rndX = random.Next(TileManager.tileSize);
            Thread.Sleep(1);
            int rndY = random.Next(TileManager.tileSize);

            monsterSpawnPos = new Point(rndY, rndX);
            monsters.Add(new Slime(monsterSpawnPos));

            for (int i = 0; i < monsters.Count; i++)
            {
                tempTileTypes.Enqueue(TileManager.tiles[monsters[i].point.Y, monsters[i].point.X].GetTileType());
            }
            for (int i = 0; i < monsters.Count; i++)
            {
                monsters[i].Spawn(monsters[i].point);
            }

            playerTempTile = TileManager.tiles[playerTempPos.Y, playerTempPos.X].GetTileType();

            //monster.Spawn(monsterSpawnPos);
            player.Spawn(playerTempPos);

            //tempPos = monster.point;
            //playerTempPos = monster.point;


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
                if (TileManager.tiles[player.point.Y, player.point.X].GetTileType() == TileType.Monster)
                {

                    //플레이어 이동할 좌표에 있떤 몬스터가 누군지 체킹
                    for (int i = 0; i < monsters.Count; i++)
                    {
                        if (monsters[i].point == player.point)
                        {
                            //플레이어가 공격
                            player.Attack(monsters[i]);
                        }
                    }

                    //이동 취소
                    player.point = playerTempPos;


                }
                //아마 타일이 공격가능한경우도 넣어야할듯.우선순위는 몬스터인듯?
                else
                {
                    TileManager.SetTile(playerTempPos, playerTempTile);//지나간자리 초기화


                    TileManager.SetTile(player.point, TileType.Player);
                    playerTempTile = TileManager.tiles[playerTempPos.Y, playerTempPos.X].GetTileType();
                }

            }

            #endregion

            #region 몬스터이동

            for (int i = 0; i < monsters.Count; i++)
            {
                if (monsters[i].isAlive)
                {

                    //임시 좌표에 몬스터 좌표 넣어둠.
                    tempPos = monsters[i].point;

                    string image = monsters[i].Image;

                    switch (image)
                    {
                        case "ⓜ"://미노일떄 - 따라오는녀석 인식범위 플레이어 인식범위 +2
                            //이런식으로 바꿔서 해주면될듯
                            //Monster temp =  monsters[i] as Monster;
                            //temp.TempMovePos(player, tempPos);

                            //임시 좌표에 이동할 좌표 미리옮겨봄. 옮긴거 큐에 넣어둠.
                            //monsterPosQueue.Enqueue(monsters[i].TempMovePos(player, tempPos));
                            break;
                        case "ⓢ"://슬라임 -혼자 노는녀석
                            Slime slime = monsters[i] as Slime;
                            slime.Move();
                            monsterPosQueue.Enqueue(slime.point);
                            break;

                        case "ⓚ"://스켈 -주변만 돌다가 근처면 따라오는녀석. 플레이어 인식범위

                            break;

                    }
                    //임시 좌표로 몬스터 이동 가능한 지역인가 탐색

                    //해당 위치가 플레이어가 아닌가?
                    if (TileManager.tiles[monsterPosQueue.Peek().Y, monsterPosQueue.Peek().X].GetTileType() != TileType.Player)
                    {
                        switch (image)
                        {
                            case "ⓜ":
                                //이동할 좌표의 타일 저장
                                tempTileTypes.Enqueue(TileManager.tiles[monsterPosQueue.Peek().Y, monsterPosQueue.Peek().X].GetTileType());
                                Console.SetCursorPosition(20, 3);
                                Console.Write($"tempPosTile: {tempTileTypes.Peek()}");

                                //이동하기 전 좌표에 이전 타일 배치.
                                TileManager.SetTile(tempPos, tempTileTypes.Dequeue());

                                //다음 좌표로 몬스터 이동.
                                monsters[i].Move(monsterPosQueue.Dequeue());

                                //몬스터 이동한 위치에 몬스터 그려주기.                
                                TileManager.SetTile(monsters[i].point, TileType.Monster);
                                break;

                            case "ⓢ":
                                //이동할 좌표의 타일 저장
                                tempTileTypes.Enqueue(TileManager.tiles[monsterPosQueue.Peek().Y, monsterPosQueue.Peek().X].GetTileType());
                                Console.SetCursorPosition(20, 3);
                                Console.Write($"tempPosTile: {tempTileTypes.Peek()}");
                                //이동하기 전 좌표에 이전 타일 배치.
                                TileManager.SetTile(tempPos, tempTileTypes.Dequeue());
                                //다음 좌표로 몬스터 이동.
                                monsters[i].Move(monsterPosQueue.Dequeue());

                                //몬스터 이동한 위치에 몬스터 그려주기.                
                                TileManager.SetTile(monsters[i].point, TileType.Monster);
                                break;

                            case "ⓚ":

                                break;

                        }
                    }
                    //이동 불가지역임. 나중에 몬스터별로 행동다르게 할지도
                    else if (TileManager.tiles[monsterPosQueue.Peek().Y, monsterPosQueue.Peek().X].GetTileType() == TileType.Player)
                    {

                        monsters[i].Attack();
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
                    monsters[i].Die();
                }
            }

            #endregion







            //if (tempPos != player.point)
            //{
            //    //이동할 좌표 저장해둔거 넘김.
            //    monster.Move(player, tempPos);



            //    TileManager.SetTile(tempPos, tempTile);//지나간자리 초기화
            //                                            //TileManager.SetTile(tempPos, TileType.Floor);//지나간자리 초기화

            //    TileManager.SetTile(monster.point, TileType.Monster);

            //    tempTile = TileManager.tiles[tempPos.Y, tempPos.X].GetTileType();
            //    //TileManager.SetTile(tempPos, tempTile);//지나간자리 초기화

            //}
        }

        public void Render()
        {
            TileManager.Render();
            Console.SetCursorPosition(20, 0);
            Console.Write($"Monster Life : {monsters[0].Life}");
            //Console.Write($"Monster X : {monster.point.X} Y : {monster.point.Y}");
            Console.SetCursorPosition(20, 1);
            Console.Write($"tempPos X : {tempPos.X} Y : {tempPos.Y}");
            Console.SetCursorPosition(20, 2);
            Console.Write($"Player Life : {Player.Life}");
            Console.SetCursorPosition(20, 3);
            //Console.Write($"tempPosTile: {TileManager.tiles[tempPos.Y,tempPos.X].GetTileType()}");
            //Console.Write($"tempPosTile: {tempTileTypes.Peek()}");
            Console.SetCursorPosition(20, 4);
            Console.WriteLine($"{monsterPosQueue.Count}");

        }


    }
}
