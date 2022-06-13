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

    public List<GameObject> obstacles;

    private void Start()
    {
        player = GlobalStorage.instance.player.GetComponentInChildren<BattlePlayerController>();
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

        for (int x = 0; x < sizeXWithBound; x++)
        {
            for (int y = 0; y < sizeYWithBound; y++)
            {
                if ((x < widthOfBound || x > sizeXWithBound - widthOfBound) || (y < widthOfBound || y > sizeYWithBound - widthOfBound))
                {
                    battleBoundsTilemap.SetTile(new Vector3Int(x, y, -20), boundsBG[0]);

                    if (x == y && x < widthOfBound)
                    {
                        battleBoundsTilemap.SetTile(new Vector3Int(x, y, -20), boundsBG[1]);
                    }

                    if (x +(sizeX + widthOfBound) == y  && x > sizeX + widthOfBound)
                    {
                        battleBoundsTilemap.SetTile(new Vector3Int(x, y, -20), boundsBG[1]);
                    }


                }
                else
                {
                    battleBGTilemap.SetTile(new Vector3Int(x, y, -20), mapBG[Random.Range(0, mapBG.Count)]);
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
