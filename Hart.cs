using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NecroDancer
{
    class Hart
    {
        Point _point;
        int _len;
        Queue<Bit> bits;
        string _image = "[     ]"; 

 
        public Hart(Point point , int difficulty)
        {
            
            bits = new Queue<Bit>();
            
            int size = _image.Length;

            _point = point;

            size = size / 2;

            //_point.PosX -= size;

            _len = difficulty;
        }

        public void AddBit(Bit bit)
        {
            bits.Enqueue(bit);
        }

        public void RemoveBits()
        {
            bits.Dequeue();
            bits.Dequeue();
        }

        public void BitMove()
        {
            foreach (var bit in bits)
            {
                bit.Move();
            }
        }
        public bool IsBits()
        {
            return bits.Count > 0;
        }
        public bool IsHit()
        {
            //left right 순서대로 비트 넣을거니까 0 2 4 같은 짝수는 전부다 왼쪽 비트 이다.
            //고로 맨앞에는 무조건 왼쪽 비트가 들어온다.
            Point point = bits.Peek().Point;
            if (point.PosX >= _point.PosX +_len )
            {
                return true;
            }
            
            //왼쪽 비트가 속도만큼 더해진 위치가 하트 위치와 같다면 
            //이러는 이유는 조금 빠른것 정도는 허용해주기 위해서
            
            //비트의 위치가 하트보다 크거나 같지 않으면 어차피 안와서 false
            return false;
        }

        public void Print()
        {
            Console.SetCursorPosition(_point.PosX, _point.PosY);
            Console.WriteLine(_image);

            foreach (var bit in bits) {
                Console.SetCursorPosition(bit.Point.PosX, bit.Point.PosY);
                Console.Write(bit.Image);
            }

         


        }

    }

}
