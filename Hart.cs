using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace NecroDancer
{
    class Hart
    {
        Point _point;
        int _len;
        Queue<Beat> beats;//들어온 순서대로 나가야해서 큐씀
        string _image = "[     ]";
        int size;
        int width;//너비 위치


        bool isHartStart = true;


        public int combo = 0;


        public Hart(Point point, int difficulty)
        {

            beats = new Queue<Beat>();


            _point = point;
            size = _image.Length;

            size = size / 2 + 1;

            _len = difficulty;

            width = Console.WindowWidth / 4;
        }

        public int Size { get { return size; } }

        public Point GetPos()
        {
            return _point;
        }

        public void Addbeat(int posY)
        {
            //왼쪽
            beats.Enqueue(new Beat(new Point(width, posY), true));
            //오른쪽
            beats.Enqueue(new Beat(new Point(Console.WindowWidth - width, posY), false));
        }

        public void Removebeats()
        {
            beats.Dequeue();
            beats.Dequeue();
        }

        public void beatMove()
        {
            foreach (var beat in beats)
            {
                beat.Move();
            }
        }

        public Beat Getbeat()
        {
            return beats.Peek();
        }

        public bool Isbeats()
        {
            return beats.Count > 0;
        }
        public bool IsNonCheckHit()
        {
            //left right 순서대로 비트 넣을거니까 0 2 4 같은 짝수는 전부다 왼쪽 비트 이다.
            //고로 맨앞에는 무조건 왼쪽 비트가 들어온다.
            Point point = beats.Peek().Point;

            //
            if (point.X >= _point.X + size)
            {
                return true;
            }
            return false;
        }

        public bool IsCheckHit()
        {
            //left right 순서대로 비트 넣을거니까 0 2 4 같은 짝수는 전부다 왼쪽 비트 이다.
            //고로 맨앞에는 무조건 왼쪽 비트가 들어온다.
            Point point = beats.Peek().Point;
            if (point.X >= _point.X &&
                point.X <= _point.X + _len)
            {
                return true;
            }

            //왼쪽 비트가 속도만큼 더해진 위치가 하트 위치와 같다면 
            //이러는 이유는 조금 빠른것 정도는 허용해주기 위해서

            //비트의 위치가 하트보다 크거나 같지 않으면 어차피 안와서 false
            return false;
        }




        public void Update()
        {


            if (isHartStart)
            {
                
                Addbeat(_point.Y);

                if (Isbeats())
                {
                    beatMove();

                    //여기는 비트가 하트에 맞았고, 플레이어가 누르기전에 맞았을때
                    if (IsNonCheckHit())
                    {
                        Removebeats();
                        combo = 0;

                    }
                }


            }

        }

        public void Render()
        {
            Console.SetCursorPosition(_point.X, _point.Y);
            Console.WriteLine(_image);
            Console.SetCursorPosition(_point.X, _point.Y +1);
            Console.Write($"combo : {combo}");

            foreach (var beat in beats)
            {
                Console.SetCursorPosition(beat.Point.X, beat.Point.Y);
                Console.Write(beat.Image);
            }
        }

    }

}
