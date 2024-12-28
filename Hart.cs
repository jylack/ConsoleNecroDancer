using System;
using System.Collections.Generic;
using System.Diagnostics;

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
        
        int _gameSpeed = 0;

        bool isHartStart = true;

        Stopwatch sw = new Stopwatch();
        Stopwatch beatWatch = new Stopwatch();

        public int combo = 0;


        public Hart(Point point, int difficulty)
        {

            beats = new Queue<Beat>();


            _point = point;
            size = _image.Length;

            size = size / 2 + 1;

            //_point.PosX -= size;

            _len = difficulty;

            width = Console.WindowWidth / 4;
        }

        public int Size { get { return size; } }

        public Point GetPos()
        {
            return _point;
        }
        public void SetGameSpeed(int speed)
        {
            _gameSpeed = speed;
        }
        public void Addbeat(int speed ,int posY)
        {
            //왼쪽
            beats.Enqueue(new Beat(new Point(width, posY), speed, true));
            //오른쪽
            beats.Enqueue(new Beat(new Point(Console.WindowWidth - width, posY), speed, false));
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
            sw.Start();
            beatWatch.Start();

            if (isHartStart)
            {
                //게임 스피드만큼 시간이 지나면 비트를 추가한다.
                if (sw.ElapsedMilliseconds > _gameSpeed)
                {

                    Addbeat(_gameSpeed, _point.Y);//비트가 움직일속도 지정                    

                    sw.Restart();
                }
                
                if (beatWatch.ElapsedMilliseconds > 100)
                {
                    //Console.Clear();
                    //ConSoleClear();

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

                    beatWatch.Restart();

                    
                    //Console.WriteLine();
                    //Console.WriteLine(combo);

                }
            }

            beatWatch.Stop();
            sw.Stop();
            
        }

        public void Render()
        {                           
                Console.SetCursorPosition(_point.X, _point.Y);
                Console.WriteLine(_image);

                foreach (var beat in beats)
                {
                    Console.SetCursorPosition(beat.Point.X, beat.Point.Y);
                    Console.Write(beat.Image);
                }
        }

            
        

    }

}
