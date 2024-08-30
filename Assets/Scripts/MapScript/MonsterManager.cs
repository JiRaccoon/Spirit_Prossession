using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    Dictionary<string, GameObject> monsterName;
    List<GameObject> aliveMonsters;

    Dictionary<Vector2, string> spawnPoint;


    private void Start()
    {
        aliveMonsters = new List<GameObject>();
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
