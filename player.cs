using System;
using System.Collections.Generic;

namespace NecroDancer
{
    public class Player : Unit
    {
        int movePoint = 1;
        bool isMove;

        public static new int Life = 0;
        public static new int Def = 0;

        public int viewPoint;

        public Player()
        {
            Life = 4;
            Atk = 1;
            Def = 0;
            Lange = 1;
            _image = "ⓟ";
            
            type = TileType.Player;
            
            viewPoint = 3;


            _inventory = new List<Item>();
        }

       
        public void Attack(Tile tile)
        {

        }



        public void GetItme(Item item)
        {
            // 아이템 획득
            _inventory.Add(item);
        }

        public override void Move()
        {
            throw new System.NotImplementedException();
        }
        public override void Move(Point point)
        {
            throw new System.NotImplementedException();
        }
        public bool Move(Fword fword)
        {

            switch (fword)
            {
                case Fword.Up:
                    //위로 이동
                    _point.Y -= movePoint;
                    isMove = true;
                    if (_point.Y < 0)
                    {
                        _point.Y += movePoint;
                        isMove = false;
                    }

                    break;
                case Fword.Down:
                    //아래로 이동
                    _point.Y += movePoint;
                    isMove = true;
                    if (_point.Y > TileManager.tileSize - 1)
                    {
                        _point.Y -= movePoint;
                        isMove = false;

                    }

                    break;
                case Fword.Left:
                    //왼쪽으로 이동
                    _point.X -= movePoint;
                    isMove = true;
                    if (_point.X < 0)
                    {
                        _point.X += movePoint;
                        isMove = false;

                    }
                    break;
                case Fword.Right:
                    //오른쪽으로 이동
                    _point.X += movePoint;
                    isMove = true;
                    if (_point.X > TileManager.tileSize - 1)
                    {
                        _point.X -= movePoint;
                        isMove = false;

                    }
                    break;
            }

            return isMove;
        }

        public override void Attack(Unit target)
        {
            int dmg = target.Def - Atk;

            if (dmg > 0)
            {
                dmg = 0;
            }

            target.Life += dmg;
            Console.SetCursorPosition(20, 10);
            Console.WriteLine($"dmg : {dmg} 몬스터 잔여hp : {target.Life}");
        }

        public override void Die()
        {
            
        }

        public override void Spawn(Point point)
        {
            _point = point;

        }

        public override void PopItem(Item item)
        {
            if (_inventory.Contains(item))
            {
                _inventory.Remove(item);
            }
        }

        public override void Attack()
        {
            
        }

    
    }

}
