using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MonsterManager : MonoBehaviour
{
    List<GameObject> aliveMonsters;
    public List<GameObject> waveSpawnParent; //���̺� Ÿ�� �з�Ʈ�� �� �־����

    public int tileMapNumber = 0;

    public bool IsEndWave = false;

    private void Start()
    {
        aliveMonsters = new List<GameObject>();
    }

    //Ÿ�ϸʿ� �´� ���� ���� ��Ƽ��
    public void ActiveWaveSpawn()
    {
        if (null == waveSpawnParent[tileMapNumber])
        {
            Debug.LogError("MonsterManager::ActiveWaveSpawn()["+ tileMapNumber +"��° waveSpanwnParent is Null");
        }
        waveSpawnParent[tileMapNumber].SetActive(true);
        for(int i=0; i < waveSpawnParent[tileMapNumber].transform.childCount; i++)
        {
            SpawnPoint _spawnPoint = waveSpawnParent[tileMapNumber].transform.GetChild(i).GetComponent<SpawnPoint>();
            if (null == _spawnPoint.monster)
            {
                Debug.LogError("MonsterManager::ActiveWaveSpawn()[" + (i + 1) + "��° ���� is Null");
            }
            else
                _spawnPoint.CreateEnemy();
        }
    }

    public void AddAliveMonster(GameObject monster)
    {
        aliveMonsters.Add(monster);
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
