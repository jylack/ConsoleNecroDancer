using System.Collections.Generic;

namespace NecroDancer
{
    //유닛들이 공통적으로 가지고있을 것들
    public abstract class Unit
    {
        protected Point _point;

        protected List<Item> _inventory;

        protected int _atk;
        protected int _life;
        protected int _def;
        protected int _lange;
        public string _image;
        public bool isAlive;



        public Unit()
        {
            _point = new Point(0, 0);
            _atk = 0;
            _life = 0;
            _def = 0;
            _lange = 0;
            _image = "　";
            isAlive = true;
        }

        public Point point { get { return _point; } set { _point = value; } }
        public int Atk { get { return _atk; } set { _atk = value; } }
        public int Life { get { return _life; } set { _life = value; } }
        public int Def { get { return _def; } set { _def = value; } }
        public int Lange { get { return _lange; } set { _lange = value; } }

        public string Image { get { return _image; } }

        public abstract void Move();     
        public abstract void Move(Point point);

        public abstract void Attack();
        public abstract void Attack(Unit target);
        public abstract void Die();
        public abstract void Spawn(Point point);
        public abstract void PopItem(Item item);

    }


}
