using ConsoleNecroDancer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NecroDancer
{
    public class Player : Unit
    {
        int movePoint = 1;
        bool isMove ;

        public Player()
        {
            Life = 4;
            Atk = 1;
            Def = 0;
            Lange = 1;
            _image = "ⓟ";
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
                    if (_point.Y > 9)
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
                    if (_point.X > 9)
                    {
                        _point.X -= movePoint;
                        isMove = false;

                    }
                    break;
            }

            return isMove;
        }
        public void Attack(Tile tile)
        {
            
        }

    
        public void Attack(Unit target)
        {
            int dmg = target.Def - Atk;

            if (dmg > 0)
            {
                dmg = 0;
            }

            target.Life += dmg;
        }

        public void Die()
        {

        }

        public void Spawn(Point point)
        {
            _point = point;
        }


        public void PopItem(Item item)
        {
            if (_inventory.Contains(item))
            {
                _inventory.Remove(item);
            }
        }

        public void GetItme(Item item)
        {
            // 아이템 획득
            _inventory.Add(item);
        }

    }

}
