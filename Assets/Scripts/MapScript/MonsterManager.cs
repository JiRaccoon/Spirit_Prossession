using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MonsterManager : MonoBehaviour
{
    List<GameObject> aliveMonsters;
    public List<GameObject> waveSpawnParent; //웨이브 타일 패런트를 다 넣어야함

    public int tileMapNumber = 0;

    public bool IsMonsterAllDie = false;

    private void Start()
    {
        aliveMonsters = new List<GameObject>();
    }

    private void Update()
    {
        //얼라이브몬스터 비우기
        if(Input.GetKeyDown(KeyCode.Keypad0))
            TestRemoveAll();

        if (Input.GetMouseButtonDown(0))
        {
            ActiveWaveSpawn();
        }
    }

    //타일맵에 맞는 몬스터 스폰 액티브
    public void ActiveWaveSpawn()
    {
        IsMonsterAllDie = false;

        if (null == waveSpawnParent[tileMapNumber])
        {
            Debug.LogError("MonsterManager::ActiveWaveSpawn()["+ tileMapNumber +"번째 waveSpanwnParent is Null");
        }
        waveSpawnParent[tileMapNumber].SetActive(true);
        for(int i=0; i < waveSpawnParent[tileMapNumber].transform.childCount; i++)
        {
            SpawnPoint _spawnPoint = waveSpawnParent[tileMapNumber].transform.GetChild(i).GetComponent<SpawnPoint>();
            if (null == _spawnPoint.monster)
            {
                Debug.LogError("MonsterManager::ActiveWaveSpawn()[" + (i + 1) + "번째 몬스터 is Null");
            }
            else
                _spawnPoint.CreateEnemy();
        }
    }

    public bool GetIsMonsterAllDie()
    {
        return IsMonsterAllDie;
    }

    public void AddAliveMonster(GameObject monster)
    {
        aliveMonsters.Add(monster);
    }

    //몬스터에서 사라질때 실행
    public void RemoveMonster(GameObject monster)
    {
        aliveMonsters.Remove(monster);
        CheckAliveMonsters();
    }

    public void TestRemoveAll()
    {
        aliveMonsters.Clear();
        CheckAliveMonsters();
    }

    public void CheckAliveMonsters()
    {
       if(aliveMonsters.Count == 0)
       {
           IsMonsterAllDie = true;
       }
        IsMonsterAllDie = false;
        Debug.Log(IsMonsterAllDie);
    }
}
