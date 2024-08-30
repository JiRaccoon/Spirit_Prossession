using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    MonsterManager monsterManager;
    MoveMapCam moveMapCam;
    grid_tistory grid_Tistory;

    bool isWaveOver;
    int highTileMap = 0;
    int nowTileMap = 0;


    private void Update()
    {
        UpdateTileMap();
    }

    void UpdateTileMap()
    {
        if (highTileMap < grid_Tistory.tileMapNumber)
        {
            highTileMap = grid_Tistory.tileMapNumber;
            monsterManager.ActiveWaveSpawn();
        }

        nowTileMap = grid_Tistory.tileMapNumber;
    }
}
