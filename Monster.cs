using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NecroDancer
{
    public class Monster : Unit
    {
        public Monster()
        {
            _point = new Point(0, 0);
            _atk = 0;
            _life = 0;
            _def = 0;
            _lange = 0;
        }

        public void Move(Point target) 
        {
            //깊이탐색으로 플레이어한테 이동.
        }
        public void Attack(Unit target) { }



        public void Die() 
        {
            Random rnd = new Random();

            int gold = rnd.Next(1, 100);

            int itemdrop = rnd.Next(1, 100);

            if(itemdrop > 50)
            {
                DropItem(_point);
            }
            else
            {
                //골드 드롭
                DropGold(_point,gold);
            }

        }
        public void Spawn() { }     
        

        public Item DropGold(Point point,int gold)
        {
            //골드 드롭
             Gold dropgold = new Gold(point,gold);

            return dropgold;
        }

        public void DropItem(Point point)
        {
            //아이템 드롭


        }

        public void Patattrn()
        {
            // 패턴
        }

        public void Serch()
        {
            // 탐색
        }
    }
}
