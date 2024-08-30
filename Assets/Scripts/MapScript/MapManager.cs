using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public MonsterManager monsterManager;
    private MoveMapCam moveMapCam;
    private grid_tistory grid_Tistory;

    bool isWaveOver;
    int highTileMap = -1;
    int nowTileMap = 0;

    private void Start()
    {
        moveMapCam = GetComponent<MoveMapCam>();
        grid_Tistory = GetComponent<grid_tistory>();
    }

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

    public void MoveMap(int type)
    {
        StartCoroutine(moveMapCam.CameraMove(type));
    }

    void MapReturnCheck()
    {
        bool check;
        check = monsterManager.GetIsMonsterAllDie();

        if(check)
        {
            Debug.Log(1);
            moveMapCam.MoveTriggerinstantiate();
        }
    }
}
