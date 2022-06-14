using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BattleMap : MonoBehaviour
{
    private BattlePlayerController player;

    public Tilemap battleBGTilemap;
    private int sizeX;
    private int sizeY;
    private int widthOfBound = 10;
    private bool[,] battleArray;

    public Tilemap battleBoundsTilemap;
    public List<Tile> mapBG;
    public List<Tile> boundsBG;

    public List<GameObject> obstaclesPrefab;
    private List<ObstaclesStats> obstaclesStats;

    public GameObject obstaclesContainer;
    private List<GameObject> obstaclesOnMap = new List<GameObject>();

    private void Start()
    {
        player = GlobalStorage.instance.player.GetComponentInChildren<BattlePlayerController>();

        foreach (var obstacle in obstaclesPrefab)
            obstaclesStats.Add(obstacle.GetComponent<ObstaclesStats>());
    }

    public void InitializeMap(bool mode)
    {
        if (mode == false)
        {
            GetBattleMapSize();
            DrawTheBackgroundMap();
        }
        else
        {
            ClearMap();
        }        
    }

    private void DrawTheBackgroundMap() 
    {
        int sizeXWithBound = sizeX + widthOfBound * 2;
        int sizeYWithBound = sizeY + widthOfBound * 2;

        for (int x = 0; x < sizeYWithBound; x++)
        {
            for (int y = 0; y < sizeXWithBound; y++)
            {
                //draw bounds
                if ((x < widthOfBound || x >= sizeXWithBound - widthOfBound) || (y < widthOfBound || y >= sizeYWithBound - widthOfBound))
                {                   
                    Tile currentBoundTile = boundsBG[0];

                    if (x == y && x < widthOfBound) 
                        currentBoundTile = boundsBG[1];
                    

                    if (x - (widthOfBound + sizeX) == y - (widthOfBound + sizeY) && y >= widthOfBound + sizeY) 
                        currentBoundTile = boundsBG[1];


                    if (-x - 1 + (sizeX + 2 * widthOfBound) == y && y < widthOfBound) 
                        currentBoundTile = boundsBG[1];


                    if (sizeYWithBound - x - 1 == y && y >= widthOfBound + sizeY) 
                        currentBoundTile = boundsBG[1];

                    battleBoundsTilemap.SetTile(new Vector3Int(x, y, -20), currentBoundTile);
                    battleArray[y, x] = false;
                }
                else
                {
                    battleBGTilemap.SetTile(new Vector3Int(x, y, -20), mapBG[Random.Range(0, mapBG.Count)]);
                    battleArray[y, x] = true;
                }               
            }
        }

        int falseBox = 0;
        int trueBox = 0;

        for (int i = 0; i < battleArray.GetLength(0); i++)
        {
            for (int j = 0; j < battleArray.GetLength(1); j++)
            {
                if (battleArray[i, j] == true)
                    trueBox++;
                else
                    falseBox++;
            }
        }

        Debug.Log("False: " + falseBox + "; True = " + trueBox);
    }

    private void DrawObstacles()
    {
        for (int x = 0; x < battleArray.GetLength(0); x++)
        {
            for (int y = 0; y < battleArray.GetLength(1); y++)
            {
                if (true)
                {

                }
            }
        }
    }

    private void ClearMap()
    {
        battleBoundsTilemap.ClearAllTiles();
        battleBGTilemap.ClearAllTiles();
    }

    public void GetBattleMapSize()
    {
        Vector3Int size = GlobalStorage.instance.battleManager.GetBattleMapSize();
        sizeX = size.x;
        sizeY = size.y;
        battleArray = new bool[sizeX + 2 * widthOfBound, sizeY + 2 * widthOfBound];
    }

    private void OnEnable()
    {
        EventManager.ChangePlayer += InitializeMap;
    }

    private void OnDisable()
    {
        EventManager.ChangePlayer -= InitializeMap;
    }
}
