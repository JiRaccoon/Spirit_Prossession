using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public MonsterManager monsterManager;
    private MoveMapCam moveMapCam;
    private grid_tistory grid_Tistory;

    public bool isWaveOver;
    int highTileMap = -1;
    int nowTileMap = 0;
    bool mapcheck = false;
    private void Start()
    {
        isWaveOver = false;
        moveMapCam = GetComponent<MoveMapCam>();
        grid_Tistory = GetComponent<grid_tistory>();
    }

    private void Update()
    {
        UpdateTileMap();
        MapCheck();
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

    void MapCheck()
    {
        if (highTileMap < grid_Tistory.tileMapNumber)
        {
            return;
        }
        else
        {
            if(isWaveOver == true) MapReturnCheck();
        }
    }

    public void MoveMap(int type)
    {
        StartCoroutine(moveMapCam.CameraMove(type));
    }

    void MapReturnCheck()
    {
        moveMapCam.MoveTriggerinstantiate();
        
    }
}
