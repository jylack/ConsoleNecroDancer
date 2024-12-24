using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace NecroDancer
{
    internal class BattleManager : IManagerInterFace
    {
        TileManager _tileManager;
        Player player;
        Monster monster;
        List<Item> dropItemList;//바닥에 아이템 떨군거 표시
        List<Unit> units;
        Point monsterSpawnPos;//나중에 랜덤으로 몬스터 생성할때 재활용 할 좌표
        Point tempPos;
        Point playerTempPos;
        TileType tempTile; // 멥에 변동이 생겼을때 원래 가지고있던 타일값 저장하는 용도.
        TileType tempTile2; // 멥에 변동이 생겼을때 원래 가지고있던 타일값 저장하는 용도.

        bool isPlayerMove = false;

        public void Init()
        {


            _tileManager = new TileManager();
            _tileManager.Init();

            player = new Player();

            monster = new Monster();

            dropItemList = new List<Item>();

            tempPos = new Point(0, 0);

            playerTempPos = new Point(1, 3);

            monsterSpawnPos = new Point(8, 0);

            tempTile = _tileManager.tiles[monsterSpawnPos.Y, monsterSpawnPos.X].GetTileType();
            tempTile2 = _tileManager.tiles[playerTempPos.Y, playerTempPos.X].GetTileType();

            monster.Spawn(monsterSpawnPos);
            player.Spawn(playerTempPos);

            tempPos = monster.point;
            playerTempPos = monster.point;
        }
        public void Update()
        {
            GameManager.isGameStart = true;

            tempPos = monster.point;


            if (isPlayerMove)
            {
                playerTempPos = player.point;

            }

            monster.Move(player);


            _tileManager.SetTile(tempPos, tempTile);//지나간자리 초기화
                                                    //_tileManager.SetTile(tempPos, TileType.Floor);//지나간자리 초기화

            _tileManager.SetTile(monster.point, TileType.Monster);

            tempTile = _tileManager.tiles[tempPos.Y, tempPos.X].GetTileType();
            _tileManager.SetTile(tempPos, tempTile);//지나간자리 초기화



            switch (GameManager.input.Key)
            {
                //방향 이동.
                case ConsoleKey.A:
                case ConsoleKey.LeftArrow:

                    isPlayerMove = player.Move(Fword.Left);

                    break;
                case ConsoleKey.RightArrow:
                case ConsoleKey.D:
                    isPlayerMove = player.Move(Fword.Right);


                    break;
                case ConsoleKey.UpArrow:
                case ConsoleKey.W:
                    isPlayerMove = player.Move(Fword.Up);


                    break;
                case ConsoleKey.S:
                case ConsoleKey.DownArrow:
                    isPlayerMove = player.Move(Fword.Down);

                    break;
            }

            if (isPlayerMove)
            {
                _tileManager.SetTile(playerTempPos, tempTile);//지나간자리 초기화


                _tileManager.SetTile(player.point, TileType.Player);
                tempTile2 = _tileManager.tiles[playerTempPos.Y, playerTempPos.X].GetTileType();


                _tileManager.SetTile(playerTempPos, tempTile);//지나간자리 초기화|
            }


        }

        public void Render()
        {
            _tileManager.Render();
        }


    }
}
