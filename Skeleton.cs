using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NecroDancer
{
    class Skeleton : Monster
    {
        Fword fword;
        Random random;

        bool isSerch;

        public Skeleton(Point point)
        {
            random = new Random();
            fword = new Fword();

            isSerch = false;

            _point = point;

            _image = "ⓚ";

            _atk = 2;
            _lange = 1;
            _def = 0;
            _life = 2;

        }
    }
}
