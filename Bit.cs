using System.Runtime.CompilerServices;

namespace NecroDancer
{
    public struct Point
    {

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X
        {
            get; set;
        }

        public int Y
        {
            get; set;
        }

        //일일이 비교하는거 쓰기 귀찮아져서 만듬.
        public static bool operator ==(Point a, Point b)
        {
            return a.X == b.X && a.Y == b.Y;
        }
        public static bool operator !=(Point a, Point b)
        {
            return a.X != b.X && a.Y != b.Y;
        }

    }

    class Beat
    {
        Point _point;
        int _speed;
        bool _isHart = false;
        bool _isLeft;//true면 왼쪽     false이면 오른쪽        
        string _image = "|";



        public Beat(Point point, int speed, bool isLeft)
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
                _point.X += 1;
            }
            else
            {
                _point.X -= 1;
            }
        }

        public void IsHit()
        {
            _isHart = true;
        }



    }
}
