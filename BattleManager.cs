using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Threading;

namespace NecroDancer
{
    internal class BattleManager : IManagerInterFace
    {
        //TileManager TileManager;
        Player player;
        //Monster monster;
        List<Item> dropItemList;//바닥에 아이템 떨군거 표시
        List<Monster> monsters;//나중에 몬스터들 여기다 다 넣을거임.

        Point monsterSpawnPos;//나중에 랜덤으로 몬스터 생성할때 재활용 할 좌표
        Point playerTempPos;

        Point monsterNextPos;//몬스터 다음좌표 가지고있음.
        Point monsterPrevPos;//몬스터 이전좌표 가지고있음.

        TileType playerTempTile; // 멥에 변동이 생겼을때 원래 가지고있던 타일값 저장하는 용도.

        List<Point> tempSpawnPos;//스폰할떄 위치 저장해둔데 안가기 만들려고 씀.

        Queue<Point> monsterPosQueue;//몬스터들 이동좌표 미리저장해둘곳.
        Queue<TileType> tempTileTypes;//몬스터들 이동할 타일 미리저장해둠.

        bool isPlayerMove = false;

        //들어온 값이랑 다른값이 나올때 까지 랜덤
        Random random1 = new Random();
        Random random2 = new Random();

        public Point RandomPos()//pos랑 안에서 연산해줄애랑 달라야 나옴. 
        {


            int rndX;
            int rndY;

            bool isAdd = true;

            Point rndPos = new Point();

            while (isAdd == true)
            {
                rndX = random1.Next(TileManager.tileSize);
                rndY = random2.Next(TileManager.tileSize);

                rndPos = new Point(rndY, rndX);

                isAdd = tempSpawnPos.Contains(rndPos);
            }

            tempSpawnPos.Add(rndPos);

            return rndPos;
        }

        public void Init()
        {
            //monsterPosQueue = new Queue<Point>();
            tempTileTypes = new Queue<TileType>();
            monsters = new List<Monster>();
            tempSpawnPos = new List<Point>();

            //TileManager = new TileManager();
            TileManager.Init();

            player = new Player();

            //monster = new Monster();

            monsterNextPos = new Point();
            playerTempPos = new Point(0, 3);

            dropItemList = new List<Item>();


            tempSpawnPos.Add(playerTempPos);

            monsters.Add(new Slime(RandomPos()));
            Thread.Sleep(1);
            monsters.Add(new Slime(RandomPos()));
            Thread.Sleep(1);
            monsters.Add(new Slime(RandomPos()));
            Thread.Sleep(1);
            monsters.Add(new Slime(RandomPos()));



            for (int i = 0; i < monsters.Count; i++)
            {
                tempTileTypes.Enqueue(TileManager.tiles[monsters[i].point.Y, monsters[i].point.X].GetTileType());
            }
            for (int i = 0; i < monsters.Count; i++)
            {
                monsters[i].Spawn(monsters[i].point);
            }

            playerTempTile = TileManager.tiles[playerTempPos.Y, playerTempPos.X].GetTileType();

            player.Spawn(playerTempPos);


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
            }

            #endregion

            #region 몬스터이동

            Queue<int> indexs = new Queue<int>();
            string image;


            for (int i = 0; i < monsters.Count; i++)
            {
                //죽은 몬스터 리스트에서 지울까 생각했었음.
                if (monsters[i].isAlive == false)//몬스터 재활용 바로바로 할거면 이게나음.
                {
                    //indexs.Enqueue(i);
                    continue;
                }

                if (monsters[i].Life > 0)
                {
                    //이전좌표 저장
                    monsterPrevPos = monsters[i].point;

                    image = monsters[i].Image;

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
                            //다음좌표 저장
                            monsterNextPos = slime.point;

                            //tempMonsterPos = slime.point;
                            break;

                        case "ⓚ"://스켈 -주변만 돌다가 근처면 따라오는녀석. 플레이어 인식범위

                            break;

                    }



                    //임시 좌표로 몬스터 이동 가능한 지역인가 탐색

                    //해당 위치가 플레이어가 아닌가? 그리고 바닥인가? 에대한 체크
                    if (monsterNextPos != player.point && 
                        TileManager.tiles[monsterNextPos.Y,monsterNextPos.X].GetTileType() == TileType.Floor)
                    {
                        //다음 좌표로 몬스터 이동.
                        monsters[i].Move(monsterNextPos);
                    }
                    //이동 불가지역임. 나중에 몬스터별로 행동다르게 할지도
                    else if (monsterNextPos == player.point)
                    {
                        monsters[i].Attack();
                        monsters[i].point = monsterPrevPos;
                    }
                    

                    //for (int j = 0; j < monsters.Count; j++)
                    //{
                    //    if (monsterNextPos == monsters[j].point)
                    //    {
                    //        switch (image)
                    //        {
                    //            case "ⓜ"://미노일떄 - 따라오는녀석 인식범위 플레이어 인식범위 +2
                    //                     //이런식으로 바꿔서 해주면될듯
                    //                     //Monster temp =  monsters[i] as Monster;
                    //                     //temp.TempMovePos(player, tempPos);

                    //                //임시 좌표에 이동할 좌표 미리옮겨봄. 옮긴거 큐에 넣어둠.
                    //                //monsterPosQueue.Enqueue(monsters[i].TempMovePos(player, tempPos));
                    //                break;
                    //            case "ⓢ"://슬라임 -혼자 노는녀석
                    //                Slime slime = monsters[i] as Slime;
                    //                slime.fword = (Fword)random1.Next((int)Fword.start + 1, (int)Fword.end);

                    //                //tempMonsterPos = slime.point;
                    //                break;

                    //            case "ⓚ"://스켈 -주변만 돌다가 근처면 따라오는녀석. 플레이어 인식범위

                    //                break;

                    //        }

                            
                    //    }
                    //}


                }
                else
                {
                    monsters[i].Die();
                    indexs.Enqueue(i);

                }



            }

        }
            //디버그용으로 주석중
            //if (indexs.Count > 0)
            //{

            //    if (monsters[indexs.Peek()].isAlive == false)
            //    {
            //        monsters.RemoveAt(indexs.Dequeue());
            //    }
            //}


            #endregion
        

        public void Render()
        {

            ////디버깅 코드
            Slime temp = new Slime(new Point(0, 0));

            for (int i = 0; i < 5; i++)
            {
                Console.SetCursorPosition(20, i);
                Console.Write("                                                                                           ");
            }

            for (int i = 0; i < monsters.Count; i++)
            {
                temp = monsters[i] as Slime;
                Console.SetCursorPosition(20, i);
                Console.Write($"{i}번 X : {temp.point.X}, Y : {temp.point.Y}");
                Console.Write($"\t방향 : {temp.fword} \t HP : {temp.Life} \t 생존: {temp.isAlive}");
            }



            Console.SetCursorPosition(20, 5);
            Console.Write($"player X :{player.point.X} Y : {player.point.Y} \t HP : {Player.Life}");
            ///

            //여기부터 본진코드
            TileManager.Init();


            for (int i = 0; i < monsters.Count; i++)
            {
                if (monsters[i].isAlive == true)
                    TileManager.SetTile(monsters[i].point, monsters[i].type);
            }

            TileManager.SetTile(player.point, TileType.Player);

            TileManager.Render();





        }


    }
}
