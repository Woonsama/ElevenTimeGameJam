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

        [Header("�����Ҷ� �׷��� Ground ����")]
        public MinAndMaxInt beginGroundCount;

        [Header("�׷��� �ִ� X, �ִ� Y")]
        public Vector2Int MaxDrawBound;

        [Header("�������� ũ��� ������")]
        public MinAndMaxInt holeXPossibleCount;

        [Header("�������� ������ ����")]
        public MinAndMaxInt nextYPossibleCount;

        [Header("�ѹ��� �׷��� �� �ִ� X")]
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

            //1. ó�� �׷��� Ground
            int bgCount = beginGroundCount.GetRandomOne();
            x = DrawGround(x, y, bgCount + 1);

            tileHeight.AddRange(Enumerable.Repeat(y, bgCount));

            //2. �������ʹ� �� �Ǵ� �Ʒ��� �׸�
            while(x < MaxDrawBound.x)
            {
                // 2.1. Ȧ ũ�� 
                int holeXCnt = holeXPossibleCount.GetRandomOne();
                tileHeight.AddRange(Enumerable.Repeat(-1, holeXCnt));
                x += holeXCnt;

                //2.2 Y ��ġ ����(���̿� ���� Ȯ�� ������ �ʿ���)..���� �� ���� ���

                // y MaxDrawBound.y, 0
                // y MaxDrawBound.y - y == 1, 1
                // y MaxDrawBound.y - y == 2~, 2
                int possibleMaxY = Mathf.Clamp(MaxDrawBound.y - y, 0, nextYPossibleCount.Max);

                // y 0, 0
                // y 1, -1
                // y 2~, -2
                int possibleMinY = Mathf.Clamp(0 - y, nextYPossibleCount.Min, 0);
                int nextHeightY = UnityEngine.Random.Range(possibleMinY, possibleMaxY + 1);

                //y�� ���� limit ���
                y = Mathf.Clamp(y + nextHeightY, 0, MaxDrawBound.y);                

                //3.3 ���� �� �׸���                               
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