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



        public Player(Point point)
        {
            _point = point;

        }

        public void Move(Fword fword) 
        {
            switch(fword)
            {
                case Fword.Up:
                    //위로 이동
                    _point.Y--;

                    break;
                case Fword.Down:
                    //아래로 이동
                    _point.Y++;
                    break;
                case Fword.Left:
                    //왼쪽으로 이동
                    _point.X--;
                    break;
                case Fword.Right:
                    //오른쪽으로 이동
                    _point.X++;
                    break;
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

        public void Spawn() { }


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
