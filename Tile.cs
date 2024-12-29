using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NecroDancer
{
    public enum TileType
    {
        start,
        Wall,//안부셔지는 벽
        woodenWall,//나무벽
        stoneWall,//돌벽
        Floor,//바닥
        Door, //문
        Stair, //입구 내려가는곳.
        Trap, //함정
        Item,
        Monster,
        Player,
        end
    }


    public class Tile
    {
        TileType _type;//타일타입


        int _hp;//부셔지는 벽같은경우 hp있어야함.
        int _atk;//함정같은 경우 타일자체 데미지있음.
        int _def;//상자같은경우 방어력있어서 몇이상 공격력만 되게 해볼까함.


        public int Hp { get { return _hp; } set { _hp = value; } }
        public int Atk { get { return _atk; }}
        public int Def { get { return _def; }}

        //타일들 보여줄꺼냐 말꺼냐
        public bool isView = true;

        public Tile()
        {
            _type = TileType.Floor;
            _hp = 0;
            _atk = 0;
            _def = 0;
         //   isView = true;
        }

        public Tile(TileType type)
        {
            _hp = 0;
            _atk = 0;
            _def = 0;
            _type = type;

            switch (type)
            {
                case TileType.Wall:
                    _hp = 100;
                    _def = 100;
                    break;
                case TileType.woodenWall:
                    _hp = 1;
                    _def = 0;
                    break;
                case TileType.stoneWall:
                    _hp = 3;
                    _def = 1;
                    break;
                case TileType.Trap:
                    _atk = 1;
                    break;
                case TileType.Door:
                    _hp = 1;
                    break;

            }
        }

        public TileType GetTileType()
        {
            return _type;
        }

    }

    public class TileManager
    {

        public static int tileSize = 10;

        public static Tile[,] tiles; //맵타일
        public static Tile[,] originTiles; //맵타일

        static string[] tileImage = { " ", "■", "▥", "▦", "□", "▣", "◎", "△", "▼" , "ⓜ", "ⓟ" };//전부 표기

        public static string[] GetImage
        {
            get { return  tileImage; }
        }

        public TileManager()
        {
  
        }

        public static void SetTile(Point point, TileType type)
        {
            tiles[point.Y, point.X] = new Tile(type);
          //  originTiles[point.Y, point.X] = new Tile(type);
        }

        public void Update()
        {

        }

        public static void Init()
        {
            //테스트용 tileSize X tileSize 맵생성
            tiles = new Tile[tileSize, tileSize];

            for (int y = 0; y < tileSize; y++)
            {
                for (int x = 0; x < tileSize; x++)
                {
                    tiles[y, x] = new Tile(TileType.Floor);
                   // originTiles[y, x] = new Tile(TileType.Floor);
                }
            }
        }

        public static void Render()
        {
            Console.SetCursorPosition(0, 0);

            for (int y = 0; y < tileSize; y++)
            {
                for (int x = 0; x < tileSize; x++)
                {
                    if (tiles[y, x].isView)
                    {
                        Console.Write(tileImage[(int)tiles[y, x].GetTileType()]);
                    }
                    else
                    {
                        Console.Write(tileImage[0]);//여백 혹은 나중에 다르게표현할지도?
                    }
                }
                Console.WriteLine();
            }
        }

    }
}
