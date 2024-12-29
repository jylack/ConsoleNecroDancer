using System;

namespace NecroDancer
{
    public class Monster : Unit
    {

        protected int movePoint = 1;
        int count = 0;

        public bool isSerch = false;
        public bool isMove = true;


        public Monster()
        {
            _atk = 0;
            _life = 0;
            _def = 0;
            _lange = 0;
            _image = "ⓜ";
            type = TileType.Monster;
            count++;
        }


        //몬스터 좌표 이동안시키고 이동시킬 좌표 템프에 넣어둠.
        public Point TempMovePos(Unit target, Point tempPos)
        {

            //tempPos 임시 몬스터 좌표



            //target 플레이어 유닛정보
            bool isMove = true;
            if (isMove)
            {
                Random random = new Random();
                Fword fword = new Fword();

                if (target.point.X < tempPos.X)
                {
                    fword = Fword.Left;
                }
                else if (target.point.X > tempPos.X)
                {
                    fword = Fword.Right;
                }
                else
                {
                    if (target.point.Y < tempPos.Y)
                    {
                        fword = Fword.Up;
                    }
                    else if (target.point.Y > tempPos.Y)
                    {
                        fword = Fword.Down;
                    }
                    else
                    {
                        fword = Fword.Down;
                    }
                }


                switch (fword)
                {
                    case Fword.Up:
                        tempPos.Y = _point.Y - movePoint;

                        if (tempPos.Y < 0)
                        {
                            tempPos.Y = _point.Y + movePoint;

                        }

                        //if (target.point.Y >= tempPos.Y)
                        //{
                        //    tempPos.Y = _point.Y + movePoint;
                        //    isAttack = true;
                        //}
                        break;

                    case Fword.Down:

                        tempPos.Y = _point.Y + movePoint;

                        if (tempPos.Y > TileManager.tileSize - 1)
                        {
                            tempPos.Y = _point.Y - movePoint;

                        }

                        //if (target.point.Y <= tempPos.Y)
                        //{
                        //    tempPos.Y = _point.Y - movePoint;
                        //    isAttack = true;
                        //}
                        break;

                    case Fword.Left:
                        tempPos.X = _point.X - movePoint;

                        if (tempPos.X < 0)
                        {
                            tempPos.X = _point.X + movePoint;

                        }


                        //if (target.point.X >= tempPos.X)
                        //{
                        //    tempPos.X = _point.X + movePoint;
                        //    isAttack = true;

                        //}
                        break;

                    case Fword.Right:
                        tempPos.X = _point.X + movePoint;

                        if (tempPos.X > TileManager.tileSize - 1)
                        {
                            tempPos.X = _point.X - movePoint;

                        }


                        //if (target.point.X <= tempPos.X)
                        //{
                        //    tempPos.X = _point.X - movePoint;
                        //    isAttack = true;

                        //}
                        break;
                }


                //여기코드들 다 수정해야함.
                /*
                //if (target.point.X < _point.X - movePoint)
                //{
                //    _point.X -= movePoint;
                //}
                //else if (target.point.X > _point.X + movePoint)
                //{
                //    _point.X += movePoint;
                //}
                //else if (target.point.Y < _point.Y - movePoint)
                //{
                //    _point.Y -= movePoint;
                //}
                //else if (target.point.Y > _point.Y + movePoint)
                //{
                //    _point.Y += movePoint;
                //}




                //Console.SetCursorPosition(20, 0);
                //Console.Write($"X : {tempPos.X} Y : {tempPos.Y}");

                //Fword fword = (Fword)random.Next(0, 4);//동서남북

                //switch (fword)
                //{
                //    case Fword.Up:
                //        tempPos.Y = _point.Y - movePoint;

                //        if (target.point.Y >= tempPos.Y)
                //        {
                //            tempPos.Y = _point.Y + movePoint;
                //            isAttack = true;
                //        }
                //        break;

                //    case Fword.Down:
                //        tempPos.Y = _point.Y = movePoint;

                //        if (target.point.Y <= tempPos.Y)
                //        {
                //            tempPos.Y = _point.Y - movePoint;
                //            isAttack = true;
                //        }
                //        break;
                //    case Fword.Left:
                //        tempPos.X = _point.X - movePoint;

                //        if (target.point.X >= tempPos.X)
                //        {
                //            tempPos.X = _point.X + movePoint;
                //            isAttack = true;

                //        }
                //        break;
                //    case Fword.Right:
                //        tempPos.X = _point.X + movePoint;

                //        if (target.point.X <= tempPos.X)
                //        {
                //            tempPos.X = _point.X - movePoint;
                //            isAttack = true;

                //        }
                //        break;
                //}


                //Console.SetCursorPosition(20, 0);
                //Console.Write(fword.ToString());


                //깊이탐색으로 플레이어한테 이동.
                //당장은 테스트이니 플레이어 좌표로 직진
                */
            }

            return tempPos;

        }


        public override void Move()
        {

        }

        //public void Move(Unit target, Point point)
        //{

        //    isMove = true;
        //    #region
        //    ////공격할 조건
        //    //if (target.point.X == _point.X - 1
        //    //    && target.point.Y == _point.Y)// 플레이어가 왼쪽 옆에있을때.
        //    //{
        //    //    isAttack = true;
        //    //    isMove = false;
        //    //}
        //    //else if (target.point.X == _point.X + 1
        //    //    && target.point.Y == _point.Y)// 플레이어가 오른쪽 옆에있을때.
        //    //{
        //    //    isAttack = true;
        //    //    isMove = false;
        //    //}
        //    //else if (target.point.X == _point.X
        //    //    && target.point.Y == _point.Y + 1)//아래있을때.
        //    //{
        //    //    isAttack = true;
        //    //    isMove = false;
        //    //}
        //    //else if (target.point.X == _point.X
        //    //&& target.point.Y == _point.Y - 1)//위에있을때
        //    //{
        //    //    isAttack = true;
        //    //    isMove = false;
        //    //}
        //    #endregion

        //    //공격할떈 안움직여야함.


        //    if (isMove)
        //    {
        //        _point = point;

        //        #region
        //        //Random random = new Random();

        //        //Fword fword = (Fword)random.Next(0, 4);//동서남북

        //        //switch (fword)
        //        //{
        //        //    case Fword.Up:
        //        //        _point.Y -= movePoint;

        //        //        if (target.point.Y >= _point.Y)
        //        //        {
        //        //            _point.Y += movePoint;
        //        //            isAttack = true;
        //        //        }
        //        //        break;

        //        //    case Fword.Down:
        //        //        _point.Y += movePoint;

        //        //        if (target.point.Y <= _point.Y)
        //        //        {
        //        //            _point.Y -= movePoint;
        //        //            isAttack = true;
        //        //        }
        //        //            break;
        //        //    case Fword.Left:
        //        //        _point.X -= movePoint;

        //        //        if (target.point.X >= _point.X)
        //        //        {
        //        //            _point.X += movePoint;
        //        //            isAttack = true;

        //        //        }
        //        //        break;
        //        //    case Fword.Right:
        //        //        _point.X += movePoint;

        //        //        if (target.point.X <= _point.X)
        //        //        {
        //        //            _point.X -= movePoint;
        //        //            isAttack = true;

        //        //        }
        //        //        break;
        //        //}


        //        //깊이탐색으로 플레이어한테 이동.
        //        //당장은 테스트이니 플레이어 좌표로 직진
        //        #endregion
        //    }



        //    //Console.WriteLine(count);
        //}
        public override void Attack(Unit target)
        {

            int dmg = target.Def - Atk;

            //방어도가 공격에 비해 너무 높아서 오히려 체력체우는거 방지용
            if (dmg > 0)
            {
                dmg = 0;
            }

            target.Life += dmg;

        }


        //자식들이 공용으로 쓸 죽음
        public override void Die()
        {
            Random rnd = new Random();

            int gold = rnd.Next(1, 100);

            int itemdrop = rnd.Next(1, 100);

            if (itemdrop > 50)
            {
                DropItem(_point);
            }
            else
            {
                //골드 드롭
                DropGold(gold);
            }

            TileManager.SetTile(_point, TileType.Floor);

            isAlive = false;

        }

        //자식들이 공용으로 쓸 리스폰
        public override void Spawn(Point point)
        {
            _point = point;
            isAlive = true;

        }


        public Item DropGold(int gold)
        {
            //골드 드롭
            Gold dropgold = new Gold(_point, gold);

            return dropgold;
        }

        public void DropItem(Point point)
        {
            //아이템 드롭


        }

        public void Patattrn(Unit target)
        {
            // 패턴
            Random atkRnd = new Random();
            int patattrn = atkRnd.Next(0, 2);//원래는 스킬 갯수만큼해야함. 테스트니 0과 1만하겠슴

            switch (patattrn)
            {
                case 0:
                    //기본공격
                    Attack(target);
                    break;
                case 1:
                    //스킬1
                    break;
            }

        }

        public void Serch()
        {
            // 탐색
        }



        public override void PopItem(Item item)
        {

        }

        public override void Attack()
        {
            int dmg = 0;

            dmg = Player.Def - Atk;
            if (dmg > 0)
            {
                dmg = 0;
            }

            Player.Life += dmg;
        }

        public override void Move(Point point)
        {
            _point = point;
        }
    }
}
