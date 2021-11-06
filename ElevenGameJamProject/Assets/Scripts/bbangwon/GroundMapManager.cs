using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;
using System.Linq;

namespace eleven.game
{
    [Serializable]
    public struct MinAndMaxInt
    {
        public int Min;
        public int Max;

        public int GetRandomOne()
        {
            return UnityEngine.Random.Range(Min, Max + 1);
        }
    };

    public class GroundMapManager : MonoBehaviour
    {       
        Tilemap tileMap;

        [SerializeField]
        Tile[] Ground;

        [SerializeField]
        Tile[] Floor;

        List<int> tileHeight;

        [Header("시작할때 그려질 Ground 개수")]
        public MinAndMaxInt beginGroundCount;

        [Header("그려질 최대 X, 최대 Y")]
        public Vector2Int MaxDrawBound;

        [Header("낭떠러지 크기로 가능한")]
        public MinAndMaxInt holeXPossibleCount;

        [Header("낭떠러지 가능한 높이")]
        public MinAndMaxInt nextYPossibleCount;

        [Header("한번에 그려질 수 있는 X")]
        public MinAndMaxInt drawXPossibleCount;
        

        private void Start()
        {
            tileMap = GetComponentInChildren<Tilemap>();
            InitGround();           
        }         

        public void InitGround()
        {
            tileHeight = new List<int>();
            int x = -1;
            int y = 0;

            //1. 처음 그려질 Ground
            int bgCount = beginGroundCount.GetRandomOne();
            x = DrawGround(x, y, bgCount + 1);

            tileHeight.AddRange(Enumerable.Repeat(y, bgCount));

            //2. 다음부터는 위 또는 아래에 그림
            while(x < MaxDrawBound.x)
            {
                // 2.1. 홀 크기 
                int holeXCnt = holeXPossibleCount.GetRandomOne();
                tileHeight.AddRange(Enumerable.Repeat(-1, holeXCnt));
                x += holeXCnt;

                //2.2 Y 위치 조정(높이에 따라 확률 재계산이 필요함)..다음 땅 높이 계산

                // y MaxDrawBound.y, 0
                // y MaxDrawBound.y - y == 1, 1
                // y MaxDrawBound.y - y == 2~, 2
                int possibleMaxY = Mathf.Clamp(MaxDrawBound.y - y, 0, nextYPossibleCount.Max);

                // y 0, 0
                // y 1, -1
                // y 2~, -2
                int possibleMinY = Mathf.Clamp(0 - y, nextYPossibleCount.Min, 0);
                int nextHeightY = UnityEngine.Random.Range(possibleMinY, possibleMaxY + 1);

                //y축 랜덤 limit 계산
                y = Mathf.Clamp(y + nextHeightY, 0, MaxDrawBound.y);                

                //3.3 다음 땅 그리기                               
                int drawXCnt = drawXPossibleCount.GetRandomOne();
                x = DrawGround(x, y, drawXCnt);
                tileHeight.AddRange(Enumerable.Repeat(y, holeXCnt));
            }
        }


        int DrawGround(int beginX, int y, int count)
        {
            Tile[] tileGround = null;
            if (y == 0)
            {
                tileGround = Ground;                
            }
            else
            {
                tileGround = Floor;
            }

            int nextCount = beginX + count;
            for (int i = beginX; i < nextCount; i++)
            {
                if (i == beginX)
                    tileMap.SetTile(new Vector3Int(i, y, 0), tileGround[0]);

                else if (i == nextCount - 1)
                    tileMap.SetTile(new Vector3Int(i, y, 0), tileGround[2]);

                else
                    tileMap.SetTile(new Vector3Int(i, y, 0), tileGround[1]);
            }

            return nextCount;
        }
    }

}