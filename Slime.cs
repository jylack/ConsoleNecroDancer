using System;

namespace NecroDancer
{
    class Slime : Monster
    {
        Fword fword = new Fword();
        Random random = new Random();

        bool isAtkMonster;

        Point tempPos;
        
        public Slime(Point point)
        {
            tempPos = new Point();

            _point = point;

            tempPos = _point;//처음좌표 알아야함. 왓다갓다만할거라서.


            _image = "ⓢ";
            _atk = 1;
            _lange = 1;
            _def = 0;
            _life = 1;
            isAtkMonster = random.Next(0, 2) == 0 ? true : false; // 0이면 true 1이면 false 공격가능 불가능 설정.
            //슬라임 하나로 두종류 공격가능한애 못한애 사용


            fword = (Fword)random.Next(0, 4);//0~3까지 행동값있음. 아무방향으로 정해줌

        }

        public override void Move()
        {
            int tempY = 0;
            int tempX = 0;

            //공격가능한애
            if (isAtkMonster)
            {

                switch (fword)
                {
                    case Fword.Up:
           
                        if (tempPos == _point)
                        {

                            tempY = _point.Y - movePoint;

                        }
                        else
                        {

                            tempY = _point.Y + movePoint;

                        }

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


                        break;

                    case Fword.Down:


                        if (tempPos == _point)
                        {

                            tempY = _point.Y + movePoint;

                        }
                        else
                        {

                            tempY = _point.Y - movePoint;

                        }
                        if (TileManager.tiles[tempY, _point.X].GetTileType() == TileType.Player)
                        {
                            Attack();
                        }
                        //플레이어나 벽이나 뭔가 특수한게 아니면 이동.
                        else
                        {
                            _point.Y = tempY;
                        }
                        break;

                    case Fword.Left:

                        if (tempPos == _point)
                        {
                            tempX = _point.X - movePoint;
                            
                        }
                        else
                        {
                            tempX = _point.X + movePoint;

                        }

                        if (TileManager.tiles[_point.Y, tempX].GetTileType() == TileType.Player)
                        {
                            Attack();
                        }
                        //플레이어나 벽이나 뭔가 특수한게 아니면 이동.
                        else
                        {
                            _point.X = tempX;
                        }

                        break;

                    case Fword.Right:

                        if(tempPos ==  _point)
                        {
                            tempX = _point.X + movePoint;
                        }
                        else
                        {
                            tempX = _point.X - movePoint;
                        }


                        if (TileManager.tiles[_point.Y, tempX].GetTileType() == TileType.Player)
                        {
                            Attack();
                        }
                        //플레이어나 벽이나 뭔가 특수한게 아니면 이동.
                        else
                        {
                            _point.X = tempX;
                        }

                        break;
                }


            }
            //불가능한애. 제자리에서 점프만함. 안움직임.
            else
            {

            }

        }


    }
}
