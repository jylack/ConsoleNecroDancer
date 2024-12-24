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


        public Tile()
        {
            _type = TileType.Floor;
            _hp = 0;
            _atk = 0;
            _def = 0;
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
        public Tile[,] tiles; //맵타일

        string[] tileImage = { "　", "■", "▥", "▦", "□", "▣", "◎", "△", "▼" ,"M" , "P"};//전부 표기

        public TileManager()
        {
            //테스트용 10x10 맵생성
            tiles = new Tile[10, 10];

            for (int y = 0; y < 10; y++)
            {
                for (int x = 0; x < 10; x++)
                {
                    tiles[y, x] = new Tile(TileType.Floor);
                } 
            }
        }

        public void SetTile(Point point, TileType type)
        {
            tiles[point.Y, point.X] = new Tile(type);
        }

        public void Update()
        {
        }

        public void Init()
        {

        }

        public void Render()
        {
            Console.SetCursorPosition(0, 0);

            for (int y = 0; y < 10; y++)
            {
                for (int x = 0; x < 10; x++)
                {
                    Console.Write ( tileImage[(int)tiles[y, x].GetTileType()] );
                }
                Console.WriteLine();
            }
        }

    }
}
