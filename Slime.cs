using System;

namespace NecroDancer
{
    class Slime : Monster
    {
        public Fword fword = new Fword();

         Random random = new Random();

        bool isAtkMonster;

        Point startPos;

        public Slime(Point point)
        {
            startPos = new Point();

            _point = point;

            startPos = _point;//처음좌표 알아야함. 왓다갓다만할거라서.

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

            fword = (Fword)random.Next((int)Fword.start +1, (int)Fword.end);//0~3까지 행동값있음. 아무방향으로 정해줌

        }

        public override void Move()
        {

            //int tempUY = 0;//위로
            //int tempDY = 0;//아래로
            //int tempLX = 0;//왼쪽
            //int tempRX = 0;//오른쪽

            //int moveCount = 0;

            //tempUY = (_point.Y - movePoint) > 0 ? (_point.Y - movePoint) : 0;
            //moveCount += tempUY == 0 ? 0 : 1;

            //tempDY = (_point.Y + movePoint) < 9 ? (_point.Y + movePoint) : 9;
            //moveCount += tempDY == 9 ? 0 : 1;

            //tempLX = (_point.X - movePoint) > 0 ? (_point.X - movePoint) : 0;
            //moveCount += tempLX == 0 ? 0 : 1;

            //tempRX = (_point.X + movePoint) < 9 ? (_point.X + movePoint) : 9;
            //moveCount += tempRX == 9 ? 0 : 1;


            //fword = (Fword)random.Next(0, moveCount);

            int tempY;
            int tempX ;



            //공격가능한애
            if (isAtkMonster)
            {

                switch (fword)
                {
                    case Fword.Up:
                        if (_point.Y - movePoint < 0)
                        {
                            fword = Fword.Down;
                            
                        }

                        if (startPos == _point)
                        {                           
                            tempY = _point.Y - movePoint;
                        }
                        else if(startPos.Y == _point.Y + movePoint)
                        {
                            //여기가 지금 문제는 맞는듯. 흠 어케해야하지?
                            //
                            tempY = _point.Y + movePoint;                            
                        }
                        else
                        {
                            tempY = _point.Y;
                        }

                        if (tempY < TileManager.tileSize && tempY >= 0)
                        {
                            if (TileManager.tiles[tempY, _point.X].GetTileType() == TileType.Player)
                            {
                                //몬스터가 공격.
                                Attack();
                            }
                            //플레이어나 벽이나 뭔가 특수한게 아니면 이동.
                            else
                            {
                                _point.Y = tempY;
                            }
                        }

                        break;

                    case Fword.Down:

                        if (_point.Y + movePoint > TileManager.tileSize - 1)
                        {
                            fword = Fword.Up;
                            
                        }



                        if (startPos == _point)
                        {
                            tempY = _point.Y + movePoint;
                        }
                        else
                        {
                            tempY = _point.Y - movePoint;
                        }


                        if (tempY < TileManager.tileSize && tempY >= 0)
                        {

                            if (TileManager.tiles[tempY, _point.X].GetTileType() == TileType.Player)
                            {
                                Attack();
                            }
                            //플레이어나 벽이나 뭔가 특수한게 아니면 이동.
                            else
                            {
                                _point.Y = tempY;
                            }
                        }
                        break;

                    case Fword.Left:
                        if (_point.X - movePoint < 0)
                        {
                            fword = Fword.Right;

                            break;
                        }


                        if (startPos == _point)
                        {
                            tempX = _point.X - movePoint;
                        }
                        else
                        {
                            tempX = _point.X + movePoint;
                        }
                        if (tempX < TileManager.tileSize && tempX >= 0)
                        {
                            if (TileManager.tiles[_point.Y, tempX].GetTileType() == TileType.Player)
                            {
                                Attack();
                            }
                            //플레이어나 벽이나 뭔가 특수한게 아니면 이동.
                            else
                            {
                                _point.X = tempX;
                            }
                        }


                        break;

                    case Fword.Right:
                        if (_point.X + movePoint > TileManager.tileSize - 1)
                        {
                            fword = Fword.Left;
                            break;
                        }


                        if (startPos == _point)
                        {
                            tempX = _point.X + movePoint;
                        }
                        else
                        {
                            tempX = _point.X - movePoint;
                        }

                        if (tempX < TileManager.tileSize && tempX >= 0)
                        {
                            if (TileManager.tiles[_point.Y, tempX].GetTileType() == TileType.Player)
                            {
                                Attack();
                            }
                            //플레이어나 벽이나 뭔가 특수한게 아니면 이동.
                            else
                            {
                                _point.X = tempX;
                            }
                        }

                            
                        break;
                }


            }
            //불가능한애. 제자리에서 점프만함. 안움직임. 공격도 안함.

        }
    }
}
