using ConsoleNecroDancer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace NecroDancer
{
    internal class BattleManager : IManagerInterFace
    {

        Player player;

        List<Item> dropItemList;//바닥에 아이템 떨군거 표시 
        List<Monster> monsters;//나중에 몬스터들 여기다 다 넣을거임.

        Point playerTempPos;

        Point monsterNextPos;//몬스터 다음좌표 가지고있음.
        Point monsterPrevPos;//몬스터 이전좌표 가지고있음.

        List<Point> tempSpawnPos;//스폰할떄 위치 저장해둔데 안가기 만들려고 씀.

        bool isPlayerMove = false;
        bool isAction = false;

        int killCount = 0;


        //들어온 값이랑 다른값이 나올때 까지 랜덤
        Random random1 = new Random();
        Random random2 = new Random();

        Stopwatch monsterWatch = new Stopwatch();

        public Point RandomPos()//pos랑 안에서 연산해줄애랑 달라야 나옴. 
        {


            int rndX;
            int rndY;

            bool isRnd = true;

            Point rndPos = new Point();

            while (isRnd == true)
            {
                rndX = random1.Next( TileManager.tileSize);
                rndY = random2.Next(TileManager.tileSize);

                rndPos = new Point(rndY, rndX);

                isRnd = tempSpawnPos.Contains(rndPos);
            }

            tempSpawnPos.Add(rndPos);

            return rndPos;
        }
        public void SetAction(bool Action)
        {
            isAction = Action;
        }

        public void SetTimer(Stopwatch stopwatch)
        {
            monsterWatch = stopwatch;
        }

        public void Init()
        {

            monsters = new List<Monster>();
            tempSpawnPos = new List<Point>(); //첫 생성 좌표 다가지고있음. 몬스터 생성후 안씀.

            TileManager.TileSetTing();

            player = new Player();


            monsterNextPos = new Point();
            playerTempPos = new Point(0, 3);

            dropItemList = new List<Item>();

            //
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
                monsters[i].Spawn(monsters[i].point);
            }

            player.Spawn(playerTempPos);



        }

        public void Update()
        {
            GameManager.isGameStart = true;

            //몬스터가 없거나 플레이어 피가 없으면 게임종료.
            if (killCount >= monsters.Count || Player.Life <= 0)
            {
                Program.isGame = false;
            }


            #region 플레이어이동

            //하트 인식 잘들어왔나 디버깅용
            //Console.SetCursorPosition(15,15);
            //Console.WriteLine(isAction);



            if (isPlayerMove)//이동해서 플레이어 좌표가 변하기 전의 정보를 넣어줘야함. 안그럼 타일에 정보남음.
            {
                playerTempPos = player.point;
            }



            if (isAction)
            {

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


            }


            if (isPlayerMove)
            {
                //if(isPlyAtk) 
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

                //연산용 세팅
                TileManager.SetTile(player.point, TileType.Player);
            }

            #endregion



            #region 몬스터이동

            if (monsterWatch.ElapsedMilliseconds > 1200)
            {
                monsterWatch.Restart();
                string image;

                


                for (int i = 0; i < monsters.Count; i++)
                {
                    //죽은 몬스터 리스트에서 지울까 생각했었음.
                    if (monsters[i].isAlive == false)//몬스터 재활용 바로바로 할거면 이게나음.
                    {
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
                                //slime.Move();
                                //다음좌표 저장
                                monsterNextPos = slime.NextMove();

                                //tempMonsterPos = slime.point;
                                break;

                            case "ⓚ"://스켈 -주변만 돌다가 근처면 따라오는녀석. 플레이어 인식범위

                                break;

                        }

                        //if (monsterNextPos.X == -1 && monsterNextPos.Y == -1)
                        //{
                        //    monsterNextPos = monsters[i].point;
                        //}

                        //임시 좌표로 몬스터 이동 가능한 지역인가 탐색

                        //해당 위치가 플레이어가 아닌가? 그리고 바닥인가? 에대한 체크
                        if (monsterNextPos != player.point &&
                        TileManager.tiles[monsterNextPos.Y, monsterNextPos.X].GetTileType() == TileType.Floor)
                        {
                            //다음 좌표로 몬스터 이동.


                            monsters[i].Move(monsterNextPos);


                            //어차피 타일정보는 초기화될것이지만
                            //다음 몬스터들이 이전 몬스터들이 어디로 이동했는지 알기 위해
                            //사용한 타일변경
                            TileManager.SetTile(monsters[i].point, TileType.Monster);
                            //posList.Add(monsters[i].point);

                        }
                        //이동 불가지역임. 나중에 몬스터별로 행동다르게 할지도
                        else if (TileManager.tiles[monsterNextPos.Y, monsterNextPos.X].GetTileType() == TileType.Player)
                        {
                            monsters[i].Attack();
                            monsters[i].point = monsterPrevPos;
                        }
                        else if (TileManager.tiles[monsterNextPos.Y, monsterNextPos.X].GetTileType() == TileType.Monster)
                        {
                            Slime slime = monsters[i] as Slime;
                            slime.fword = (Fword)random1.Next((int)Fword.start + 1, (int)Fword.end);
                        }

                    }
                    else
                    {
                        monsters[i].Die();
                        killCount++;

                    }
                }

            }
            #endregion

        }



        public void Render()
        {

            ////디버그 코드
            Slime temp = new Slime(new Point(0, 0));

            
            for (int i = 0; i < monsters.Count; i++)
            {
                if (monsters[i].isAlive)
                {
                    temp = monsters[i] as Slime;
                    Console.SetCursorPosition(20, i);
                    Console.Write($"{i}번 X : {temp.point.X}, Y : {temp.point.Y}");
                    Console.Write($"\t방향 : {temp.fword} \t HP : {temp.Life} ");
                }
                
            }



            Console.SetCursorPosition(20, 5);
            Console.Write($"player X :{player.point.X} Y : {player.point.Y} \t HP : {Player.Life}");


            //여기부터 본진코드            
            TileManager.TileSetTing();


            for (int i = 0; i < monsters.Count; i++)
            {
                if (monsters[i].isAlive == true)
                {
                    TileManager.SetTile(monsters[i].point, monsters[i].type);
                }
            }
            TileManager.SetTile(player.point, player.type);

            /*
            //for(int i = 1;i < player.viewPoint; i++)
            //{
            //    if (player.point.Y + i < TileManager.tileSize - 1)
            //    {
            //        TileManager.tiles[player.point.Y + i, player.point.X].isView = true;
            //    }
            //    if (player.point.X + i < TileManager.tileSize - 1)
            //    {
            //        TileManager.tiles[player.point.Y, player.point.X + i].isView = true;
            //    }
            //    if(player.point.X - i >= 0)
            //    {
            //        TileManager.tiles[player.point.Y, player.point.X - i].isView = true;
            //    }
            //    if (player.point.Y - i >= 0)
            //    {
            //        TileManager.tiles[player.point.Y - i, player.point.X].isView = true;
            //    }                
            //}
            */


            //시야 제한코드
            for (int viewX = -player.viewPoint; viewX < player.viewPoint; viewX++)
            {
                for (int viewY = -player.viewPoint; viewY < player.viewPoint; viewY++)
                {
                    int newX = player.point.X + viewX;
                    int newY = player.point.Y + viewY;

                    if (Math.Abs(viewX) + Math.Abs(viewY) < player.viewPoint)
                    {
                        if (newX >= 0 && newX < TileManager.tileSize &&
                            newY >= 0 && newY < TileManager.tileSize)
                        {
                            TileManager.tiles[newY, newX].isView = true;
                        }
                    }
                }
            }



            TileManager.Render();



            //posList.Clear();

        }


    }
}
