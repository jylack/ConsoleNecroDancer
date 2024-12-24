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


        public Player()
        {
            Life = 4;
            Atk = 1;
            Def = 0;
            Lange = 1;
            _image = "P";

            

        }

        public void Move(Fword fword) 
        {
            if( _point.Y + movePoint < 9  && _point.X + movePoint < 9 &&                                 
                _point.Y - movePoint >= 0 && _point.X - movePoint >= 0 )
            {

                switch (fword)
                {
                    case Fword.Up:
                        //위로 이동
                        _point.Y -= movePoint;

                        break;
                    case Fword.Down:
                        //아래로 이동
                        _point.Y += movePoint;
                        
                        break;
                    case Fword.Left:
                        //왼쪽으로 이동
                        _point.X -= movePoint;
                        break;
                    case Fword.Right:
                        //오른쪽으로 이동
                        _point.X += movePoint;
                        break;
                }
            }

        }
        public void Attack(Point target)
        {

        }

        public void Attack(Unit target) 
        {

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
            if(_inventory.Contains(item))
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
