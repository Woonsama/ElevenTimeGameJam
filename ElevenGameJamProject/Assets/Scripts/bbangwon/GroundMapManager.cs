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

        [SerializeField]
        GameObject ItemSquid, ItemTuna;

        public int SquidCount, TunaCount;

        [Header("������ ������ +Y ��ġ(�������)")]
        public MinAndMaxInt possibleItemYPos;

        public int possibleItemBatchStart;

        [SerializeField]
        Transform objectTransform;

        [SerializeField]
        GameObject ObstacleBanana, ObstacleSealion, ObstaclePuddle1, ObstaclePuddle2, ObstaclePuddle3;

        public int ObstacleBananaCount, ObstacleSealionCount, ObstaclePuddle1Count, ObstaclePuddle2Count, ObstaclePuddle3Count;

        public GameObject Penguin, DeadZone;

        [SerializeField]
        GameObject home;

        public int homeDrawIndex;
        int homeIndex = 0;

        bool init = false;

        GameObject Player;

        List<Item> itemList = new List<Item>();

        private void Awake()
        {
            tileMap = GetComponentInChildren<Tilemap>();
        }

        public void Init()
        {
            if(!init)
            {
                InitGround();
                InitHome();
                InitItem();
                InitObstacle();
                InitPenguinDeadZone();
                init = true;
            }
            else
            {
                foreach (var item in itemList)
                {
                    item.gameObject.SetActive(true);
                }
            }
        }

        void InitGround()
        {
            tileHeight = new List<int>();
            int x = -1;
            int y = 0;

            //1. ó�� �׷��� Ground
            int bgCount = beginGroundCount.GetRandomOne();
            x = DrawGround(x, y, bgCount + 1);

            tileHeight.AddRange(Enumerable.Repeat(y == 0 ? 1 : y, bgCount));

            //2. �������ʹ� �� �Ǵ� �Ʒ��� �׸�
            while(x < MaxDrawBound.x)
            {
                // 2.1. Ȧ ũ�� 
                int holeXCnt = holeXPossibleCount.GetRandomOne();
                tileHeight.AddRange(Enumerable.Repeat(y==0?1:y, holeXCnt));
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

                //3.31  Home Index ���
                if ((x + drawXCnt) >= homeDrawIndex && homeIndex == 0)
                {
                    //�̰��� ��ġ�� ����
                    homeIndex = x + (drawXCnt / 2);
                }

                x = DrawGround(x, y, drawXCnt);
                tileHeight.AddRange(Enumerable.Repeat(y == 0 ? 1 : y, drawXCnt));


            }
        }

        void InitHome()
        {
            int xPos = homeIndex;
            int yPos = tileHeight[homeIndex] + 1;

            GameObject Home = Instantiate(home, objectTransform);
            Home.name = $"Home_{xPos}_{yPos}";
            Home.transform.localPosition = tileMap.GetCellCenterLocal(new Vector3Int(xPos, yPos, 0));            
        }

        void InitItem()
        {
            int squidCount = SquidCount;
            int tunaCount = TunaCount;
            //�� ��.. �Ǵ� ������ ȹ�� ������ ���� ��ġ
            //�ϴ� ��ġ�� X ��ǥ�� ����
            int[] xPoses = Enumerable.Range(possibleItemBatchStart, homeIndex - possibleItemBatchStart)
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
                    itemList.Add(squid.GetComponent<Item>());
                }
                else if (tunaCount-- > 0)
                {
                    GameObject tuna = Instantiate(ItemTuna, objectTransform);
                    tuna.name = $"tuna_{xPos}_{yPos}";
                    tuna.transform.localPosition = tileMap.GetCellCenterLocal(new Vector3Int(xPos, yPos, 0));
                    itemList.Add(tuna.GetComponent<Item>());
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

            //�� ��.. �Ǵ� ������ ȹ�� ������ ���� ��ġ
            //�ϴ� ��ġ�� X ��ǥ�� ����
            int[] xPoses = Enumerable.Range(possibleItemBatchStart, homeIndex - possibleItemBatchStart)                                        
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
            Instantiate(DeadZone, new Vector3(0, 0, 0), Quaternion.identity);
        }

        public void NewPlayer()
        {
            if(Player != null)
                Destroy(Player);

            Player = Instantiate(Penguin, new Vector3(-7, -2, 0), Quaternion.identity);
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