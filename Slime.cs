using System;

namespace NecroDancer
{
    class Slime : Monster
    {

        Random random = new Random();

        //Point startPos;

        public Fword fword = new Fword();
        bool isAtkMonster;

        public Slime(Point point)
        {
            //startPos = new Point();

            _point = point;

            //결국 필요없어짐.
            //startPos = point;//처음좌표 알아야함. 왓다갓다만할거라서.

            _image = "ⓢ";
            _atk = 1;
            _lange = 1;
            _def = 0;
            _life = 1;
            isAtkMonster = random.Next(0, 2) == 0 ? true : false; // 0이면 true 1이면 false 공격가능 불가능 설정.
                                                                  //슬라임 하나로 두종류 공격가능한애 못한애 사용
            isAtkMonster = true;

            //이런식으로 생성자에서 같은몬스터 여러타입으로 나눌수있나 테스트
            if (isAtkMonster)
            {
                _life = 2;//움직이는 녀석 체력 2
            }

            RandomFword();

        }

        //어딘가 벽에서 생성되고 벽으로 이동하라 할때 호출
        public void RandomFword()
        {
            //0~3까지 행동값있음. 아무방향으로 정해줌
            fword = (Fword)random.Next((int)Fword.start + 1, (int)Fword.end);
        }

        //방향 역방향으로만 바꿈 
        public void TunFword()
        {
            switch (fword)
            {
                case Fword.Up:
                    fword = Fword.Down;
                    break;

                case Fword.Down:
                    fword = Fword.Up;
                    break;

                case Fword.Left:
                    fword = Fword.Right;
                    break;

                case Fword.Right:
                    fword = Fword.Left;
                    break;

            }
        }


        public Point NextMove()
        {
            int tempY = 0;
            int tempX = 0;

            bool isFword = true;

            //공격가능한애

            if (isAtkMonster)
            {
                switch (fword)
                {
                    case Fword.Up:

                        tempY = _point.Y - movePoint;
                        tempX = _point.X;

                        if (tempY < 0)
                        {
                            RandomFword();
                            tempY = 0;
                            isFword = false;
                        }

                        if (isFword)
                        {

                            if (TileManager.tiles[tempY, tempX].GetTileType() == TileType.Player)
                            {
                                //몬스터가 공격.
                                Attack();
                            }

                            //다시 방향 돌아오기 위한 함수.
                            TunFword();

                        }

                        break;

                    case Fword.Down:

                        tempY = _point.Y + movePoint;
                        tempX = _point.X;

                        if (tempY >= TileManager.tileSize)
                        {
                            RandomFword();
                            tempY = TileManager.tileSize - 1;
                            isFword = false;

                        }

                        if (isFword)
                        {
                            if (TileManager.tiles[tempY, tempX].GetTileType() == TileType.Player)
                            {
                                Attack();
                            }
                            TunFword();
                        }

                        break;


                    case Fword.Right:
                        tempX = _point.X + movePoint;
                        tempY = _point.Y;

                        if (tempX >= TileManager.tileSize)
                        {
                            RandomFword();
                            tempX = TileManager.tileSize - 1;
                            isFword = false;
                        }

                        if (isFword)
                        {
                            if (TileManager.tiles[tempY, tempX].GetTileType() == TileType.Player)
                            {
                                Attack();
                            }
                            TunFword();
                        }

                        break;

                    case Fword.Left:
                        tempX = _point.X - movePoint;
                        tempY = _point.Y;

                        if (tempX < 0)
                        {
                            RandomFword();
                            tempX = 0;
                            isFword = false;
                        }

                        if (isFword)
                        {
                            if (TileManager.tiles[tempY, tempX].GetTileType() == TileType.Player)
                            {
                                Attack();
                            }
                            TunFword();
                        }
                        break;

                }
            }

            return new Point(tempX, tempY);

        }

        //음 결국 안쓰게됬음.
        public override void Move()
        {
            //int tempY;
            //int tempX;



            ////공격가능한애
            //if (isAtkMonster)
            //{

            //    switch (fword)
            //    {
            //        case Fword.Up:
            //            if (_point.Y - movePoint < 0)
            //            {
            //                fword = Fword.Down;
            //            }

            //            if (startPos == _point)
            //            {
            //                tempY = _point.Y - movePoint;
            //            }
            //            else if (startPos.Y == _point.Y + movePoint)
            //            {
            //                //여기가 지금 문제는 맞는듯. 흠 어케해야하지?
            //                tempY = _point.Y + movePoint;
            //            }
            //            else
            //            {
            //                tempY = _point.Y;
            //            }

            //            if (tempY < TileManager.tileSize && tempY >= 0)
            //            {
            //                if (TileManager.tiles[tempY, _point.X].GetTileType() == TileType.Player)
            //                {
            //                    //몬스터가 공격.
            //                    Attack();
            //                }
            //                //플레이어나 벽이나 뭔가 특수한게 아니면 이동.
            //                else
            //                {
            //                    _point.Y = tempY;
            //                }
            //            }

            //            break;

            //        case Fword.Down:

            //            if (_point.Y + movePoint > TileManager.tileSize - 1)
            //            {
            //                fword = Fword.Up;

            //            }



            //            if (startPos == _point)
            //            {
            //                tempY = _point.Y + movePoint;
            //            }
            //            else
            //            {
            //                tempY = _point.Y - movePoint;
            //            }


            //            if (tempY < TileManager.tileSize && tempY >= 0)
            //            {

            //                if (TileManager.tiles[tempY, _point.X].GetTileType() == TileType.Player)
            //                {
            //                    Attack();
            //                }
            //                //플레이어나 벽이나 뭔가 특수한게 아니면 이동.
            //                else
            //                {
            //                    _point.Y = tempY;
            //                }
            //            }
            //            break;

            //        case Fword.Left:
            //            if (_point.X - movePoint < 0)
            //            {
            //                fword = Fword.Right;

            //            }


            //            if (startPos == _point)
            //            {
            //                tempX = _point.X - movePoint;
            //            }
            //            else
            //            {
            //                tempX = _point.X + movePoint;
            //            }
            //            if (tempX < TileManager.tileSize && tempX >= 0)
            //            {
            //                if (TileManager.tiles[_point.Y, tempX].GetTileType() == TileType.Player)
            //                {
            //                    Attack();
            //                }
            //                //플레이어나 벽이나 뭔가 특수한게 아니면 이동.
            //                else
            //                {
            //                    _point.X = tempX;
            //                }
            //            }


            //            break;

            //        case Fword.Right:
            //            if (_point.X + movePoint > TileManager.tileSize - 1)
            //            {
            //                fword = Fword.Left;

            //            }


            //            if (startPos == _point)
            //            {
            //                tempX = _point.X + movePoint;
            //            }
            //            else
            //            {
            //                tempX = _point.X - movePoint;
            //            }

            //            if (tempX < TileManager.tileSize && tempX >= 0)
            //            {
            //                if (TileManager.tiles[_point.Y, tempX].GetTileType() == TileType.Player)
            //                {
            //                    Attack();
            //                }
            //                //플레이어나 벽이나 뭔가 특수한게 아니면 이동.
            //                else
            //                {
            //                    _point.X = tempX;
            //                }
            //            }


            //            break;
            //    }


            //}
            ////불가능한애. 제자리에서 점프만함. 안움직임. 공격도 안함.

        }
    }
}
