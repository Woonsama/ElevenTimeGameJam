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

        [SerializeField]
        GameObject ItemSquid, ItemTuna;

        public int SquidCount, TunaCount;

        [Header("아이템 생성될 +Y 위치(점프고려)")]
        public MinAndMaxInt possibleItemYPos;

        [SerializeField]
        GameObject[] ObstaclePrefab;
        
        public int possibleItemBatchStart;

        [SerializeField]
        Transform objectTransform;

        [SerializeField]
        GameObject ObstacleBanana, ObstacleSealion, ObstaclePuddle1, ObstaclePuddle2, ObstaclePuddle3;

        public int ObstacleBananaCount, ObstacleSealionCount, ObstaclePuddle1Count, ObstaclePuddle2Count, ObstaclePuddle3Count;

        public GameObject Penguin, DeadZone;

        private void Awake()
        {
            tileMap = GetComponentInChildren<Tilemap>();
        }

        public void Init()
        {
            InitGround();
            InitItem();
            InitObstacle();
            InitPenguinDeadZone();
        }

        void InitGround()
        {
            tileHeight = new List<int>();
            int x = -1;
            int y = 0;

            //1. 처음 그려질 Ground
            int bgCount = beginGroundCount.GetRandomOne();
            x = DrawGround(x, y, bgCount + 1);

            tileHeight.AddRange(Enumerable.Repeat(y == 0 ? 1 : y, bgCount));

            //2. 다음부터는 위 또는 아래에 그림
            while(x < MaxDrawBound.x)
            {
                // 2.1. 홀 크기 
                int holeXCnt = holeXPossibleCount.GetRandomOne();
                tileHeight.AddRange(Enumerable.Repeat(y==0?1:y, holeXCnt));
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
                tileHeight.AddRange(Enumerable.Repeat(y == 0 ? 1 : y, drawXCnt));
            }
        }

        void InitItem()
        {
            int squidCount = SquidCount;
            int tunaCount = TunaCount;
            //땅 위.. 또는 점프로 획득 가능한 곳에 배치
            //일단 배치할 X 좌표를 구함
            int[] xPoses = Enumerable.Range(possibleItemBatchStart, tileHeight.Count - possibleItemBatchStart)
                                        .OrderBy(v => UnityEngine.Random.value).Take(squidCount + tunaCount).ToArray();

            for (int x = 0; x < xPoses.Length; x++)
            {


                int xPos = xPoses[x];
                int yPos = tileHeight[xPos] + possibleItemYPos.GetRandomOne();
                yPos = Mathf.Clamp(yPos, 0, 6);


                if (squidCount-- > 0)
                {
                    GameObject squid = Instantiate(ItemSquid, objectTransform);
                    squid.name = $"squid_{xPos}_{yPos}";
                    squid.transform.localPosition = tileMap.GetCellCenterLocal(new Vector3Int(xPos, yPos, 0));
                }
                else if (tunaCount-- > 0)
                {
                    GameObject tuna = Instantiate(ItemTuna, objectTransform);
                    tuna.name = $"tuna_{xPos}_{yPos}";
                    tuna.transform.localPosition = tileMap.GetCellCenterLocal(new Vector3Int(xPos, yPos, 0));
                }
            }
        }

        void InitObstacle()
        {
            int bananaCount = ObstacleBananaCount;
            int sealionCount = ObstacleSealionCount;

            int puddle1 = ObstaclePuddle1Count;
            int puddle2 = ObstaclePuddle1Count;
            int puddle3 = ObstaclePuddle1Count;

            //땅 위.. 또는 점프로 획득 가능한 곳에 배치
            //일단 배치할 X 좌표를 구함
            int[] xPoses = Enumerable.Range(possibleItemBatchStart, tileHeight.Count - possibleItemBatchStart)                                        
                                        .OrderBy(v => UnityEngine.Random.value)
                                        .Take(bananaCount + sealionCount + 
                                        ObstaclePuddle1Count + ObstaclePuddle2Count + ObstaclePuddle3Count)
                                        .ToArray();

            for (int x = 0; x < xPoses.Length; x++)
            {
                int xPos = xPoses[x];
                int yPos = tileHeight[xPos];
                yPos = Mathf.Clamp(yPos, 0, 6);

                if (bananaCount-- > 0)
                {
                    GameObject banana = Instantiate(ObstacleBanana, objectTransform);
                    banana.name = $"banana_{xPos}_{yPos}";
                    banana.transform.localPosition = tileMap.GetCellCenterLocal(new Vector3Int(xPos, yPos+1, 0));
                }
                else if (sealionCount-- > 0)
                {
                    GameObject sealion = Instantiate(ObstacleSealion, objectTransform);
                    sealion.name = $"sealion_{xPos}_{yPos}";
                    sealion.transform.localPosition = tileMap.GetCellCenterLocal(new Vector3Int(xPos, yPos+1, 0));
                }
                else if(puddle1-- > 0)
                {
                    GameObject puddle = Instantiate(ObstaclePuddle1, objectTransform);
                    puddle.name = $"puddle1_{xPos}_{yPos}";
                    puddle.transform.localPosition = tileMap.GetCellCenterLocal(new Vector3Int(xPos, yPos, 0));
                }
                else if (puddle2-- > 0)
                {
                    GameObject puddle = Instantiate(ObstaclePuddle2, objectTransform);
                    puddle.name = $"puddle2_{xPos}_{yPos}";
                    puddle.transform.localPosition = tileMap.GetCellCenterLocal(new Vector3Int(xPos, yPos, 0));
                }
                else if (puddle3-- > 0)
                {
                    GameObject puddle = Instantiate(ObstaclePuddle3, objectTransform);
                    puddle.name = $"puddle3_{xPos}_{yPos}";
                    puddle.transform.localPosition = tileMap.GetCellCenterLocal(new Vector3Int(xPos, yPos, 0));
                }
            }
        }
        void InitPenguinDeadZone()
        {
            Instantiate(Penguin, new Vector3(-7, -2, 0), Quaternion.identity);
            Instantiate(DeadZone, new Vector3(0, 0, 0), Quaternion.identity);
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