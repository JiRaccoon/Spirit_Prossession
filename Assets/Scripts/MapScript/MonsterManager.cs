using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MonsterManager : MonoBehaviour
{
    List<GameObject> aliveMonsters;
    Dictionary<string, GameObject> monsterName;

    public List<GameObject> waveSpawnParent; //웨이브 타일 패런트를 다 넣어야함
    public List<Transform> waveChildSpawnPoint;


    public Dictionary<float, GameObject> spawnPoint;

    int tileMapNumber = 0;

    private void Start()
    {
        spawnPoint = new Dictionary<float, GameObject>();
        aliveMonsters = new List<GameObject>();
        waveSpawnParent = new List<GameObject>();
    }

    private void Update()
    {
        
    }

    //타일맵에 맞는 스폰포인트 업데이트
    public void UpdateWaveSpawn()
    {
        spawnPoint.Clear();

        for (int i = 0; i < waveSpawnParent[tileMapNumber].transform.childCount; i++)
        {
            SpawnPoint _spawnPoint = waveSpawnParent[tileMapNumber].transform.GetChild(i).GetComponent<SpawnPoint>();
            spawnPoint.Add(_spawnPoint.spawnTime, _spawnPoint.gameObject);
        }
    }

    public void CreateMonsters()
    {

    }

    private void SpawnMonster()
    {

    }

    public bool CheckAliveMonsters()
    {
       if(aliveMonsters.Count == 0)
       {
            return true;
       }

        return false;
    }
}
