using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NecroDancer
{
    public class Monster : Unit
    {
        bool isAttack = false;
        int movePoint = 1;

        public Monster()
        {
            _atk = 0;
            _life = 0;
            _def = 0;
            _lange = 0;
            _image = "M";

        }

        public void Move(Unit target)
        {
            bool isMove = true;

            //공격할 조건
            if (target.point.X == _point.X - 1
                && target.point.Y == _point.Y)// 플레이어가 왼쪽 옆에있을때.
            {
                isAttack = true;
                isMove = false;
            }
            else if (target.point.X == _point.X + 1
                && target.point.Y == _point.Y)// 플레이어가 오른쪽 옆에있을때.
            {
                isAttack = true;
                isMove = false;
            }
            else if (target.point.X == _point.X
                && target.point.Y == _point.Y + 1)//아래있을때.
            {
                isAttack = true;
                isMove = false;
            }
            else if (target.point.X == _point.X
            && target.point.Y == _point.Y - 1)//위에있을때
            {
                isAttack = true;
                isMove = false;
            }

            if (isMove)
            {
                //깊이탐색으로 플레이어한테 이동.
                //당장은 테스트이니 플레이어 좌표로 직진
                if (target.point.X < _point.X - movePoint)
                {
                    _point.X -= movePoint;
                }
                else if (target.point.X > _point.X + movePoint)
                {
                    _point.X += movePoint;
                }

                else if (target.point.Y < _point.Y - movePoint)
                {
                    _point.Y -= movePoint;
                }
                else if (target.point.Y > _point.Y + movePoint)
                {
                    _point.Y += movePoint;
                }

            }


            if (isAttack)
            {
                //공격
                Attack(target);
            }

        }
        public void Attack(Unit target)
        {


        }



        public void Die()
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
                DropGold(_point, gold);
            }

        }
        public void Spawn(Point point)
        {
            _point = point;


        }


        public Item DropGold(Point point, int gold)
        {
            //골드 드롭
            Gold dropgold = new Gold(point, gold);

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
    }
}
