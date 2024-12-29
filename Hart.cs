using System;
using System.Collections.Generic;

namespace NecroDancer
{
    class Hart
    {
        Queue<Beat> beats;//들어온 순서대로 나가야해서 큐씀
        Point _point;
        int _len;
        string _image = "[     ]";
        int size;
        int beatX;//비트 왼쪽의 x값

        int _time;



        public int combo = 0;


        public Hart(Point point, int difficulty)
        {

            beats = new Queue<Beat>();


            _point = point;
            size = _image.Length;

            size = size / 2 + 1;

            _len = difficulty;

            //비트초기 생성위치를 정해줄 녀석.
            beatX = Console.WindowWidth / 4;
        }

        public int Size { get { return size; } }

        public Point GetPos()
        {
            return _point;
        }

        public void Addbeat(int posY)
        {
            //왼쪽 끝에서 생성되는데 적당한 위치로 이동시켜서 생성. true는 왼쪽 false는 오른쪽으로 정해주는값이다.
            beats.Enqueue(new Beat(new Point(beatX, posY), true));
            //오른쪽 끝에서 생성되는데 반대편이랑 같은위치에서 나와야하므로 최대크기에서 빼줌.
            beats.Enqueue(new Beat(new Point(Console.WindowWidth - beatX, posY), false));
        }

        //비트 지울때마다 두번씩 하기엔 보기안 좋아서 따로만들은 함수
        public void Removebeats()
        {
            beats.Dequeue();
            beats.Dequeue();
        }

        //비트들 전체를 움직일 함수
        public void beatMove()
        {
            foreach (var beat in beats)
            {
                beat.Move();
            }
        }

        //비트가 있는지체크
        public bool Isbeats()
        {
            return beats.Count > 0;
        }

        //비트가 하트에 안맞았는지 체크
        public bool IsNonCheckHit()
        {
            //left right 순서대로 비트 넣을거니까 0 2 4 같은 짝수는 전부다 왼쪽 비트 이다.
            //고로 맨앞에는 무조건 왼쪽 비트가 들어온다.
            Point point = beats.Peek().Point;

            //여기서 _point.x는 하트의 왼쪽부분. +size 하면 하트의 범위를 벗어났다.
            if (point.X >= _point.X + size)
            {
                return true;
            }
            return false;
        }

        //비트가 하트에 맞았는지 체크
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
            //비트가있는가.
            if (Isbeats())
            {
                beatMove();

                //여기는 비트가 하트에 맞았고, 플레이어가 누르기전에 맞았을때
                if (IsNonCheckHit())
                {
                    //비트삭제 콤보 초기화
                    Removebeats();
                    combo = 0;
                }
            }



        }

        public void Render()
        {
            Console.SetCursorPosition(_point.X, _point.Y);
            Console.WriteLine(_image);
            Console.SetCursorPosition(_point.X, _point.Y + 1);
            Console.Write($"combo : {combo}");

            foreach (var beat in beats)
            {
                Console.SetCursorPosition(beat.Point.X, beat.Point.Y);
                Console.Write(beat.Image);
            }
        }

    }

}
