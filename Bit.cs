using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NecroDancer
{
    struct Point
    {
        int x, y;
        

        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public int PosX
        {
            get { return x; }
            set { x = value; }
        }

        public int PosY
        {
            get { return y; }
            set { y = value; }
        }

        public Point GetPos()
        {
            return new Point(x, y);
        }

        //일일이 비교하는거 쓰기 귀찮아져서 만듬.
        public static bool operator == (Point a, Point b)
        {
            return a.x == b.x && a.y == b.y;
        }
        public static bool operator !=(Point a, Point b)
        {
            return a.x != b.x && a.y != b.y;
        }

        public static bool operator >(Point a, Point b)
        {
            return a.x > b.x;
        }

        public static bool operator <(Point a, Point b)
        {
            return a.x < b.x;
        }   


    }

    class Bit
    {
        Point _point;
        int _speed;
        bool _isHart = false;
        bool _isLeft;//true면 왼쪽     false이면 오른쪽        
        string _image = "|";



        public Bit(Point point, int speed, bool isLeft)
        {
            _point = point;
            _speed = speed;
            _isLeft = isLeft;
        }

        public string Image
        {
            get { return _image; }
        }

        public Point Point
        {
            get { return _point; }
        }

        public bool IsLeft
        {
            get { return _isLeft; }
        }

        public void SetSpeed(int speed)
        {
            _speed = speed;
        }

        public void Move()
        {
            if (_isLeft)
            {
                _point.PosX += 1;
            }
            else
            {
                _point.PosX -= 1; 
            }
        }

        public void IsHit()
        {
            _isHart = true;
        }



    }
}
