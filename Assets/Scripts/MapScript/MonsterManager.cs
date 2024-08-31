using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MonsterManager : MonoBehaviour
{
    List<GameObject> aliveMonsters;
    public List<GameObject> waveSpawnParent; //���̺� Ÿ�� �з�Ʈ�� �� �־����
    public MapManager mapmanager;
    public GameObject Astar;
    public GameObject astar;

    public int tileMapNumber = 0;

    public bool IsMonsterAllDie = false;
    public int monsterCount =0;

    private void Start()
    {
        aliveMonsters = new List<GameObject>();
        Application.targetFrameRate = 60;
    }

    private void Update()
    {
        monsterCount = aliveMonsters.Count;
        //����̺���� ����
        if (Input.GetKeyDown(KeyCode.Keypad0))
        { TestRemoveAll(); mapmanager.isWaveOver = true; }

    }

    //Ÿ�ϸʿ� �´� ���� ���� ��Ƽ��
    public void ActiveWaveSpawn()
    {
        IsMonsterAllDie = false;

        if (astar != null)
        {
            Destroy(astar);
        }
        astar = Instantiate(Astar, Camera.main.transform.position,Quaternion.identity);
        astar.name = "Astar";
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

    public bool GetIsMonsterAllDie()
    {
        return IsMonsterAllDie;
    }

    public void AddAliveMonster(GameObject monster)
    {
        Debug.Log("����");
        aliveMonsters.Add(monster);
    }

    //���Ϳ��� ������� ����
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
        if (aliveMonsters.Count == 0)
        {
            IsMonsterAllDie = true;
            mapmanager.isWaveOver = true;
        }
        else
        {
            IsMonsterAllDie = false;
        }
        Debug.Log(IsMonsterAllDie);
    }
}
