using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public GameObject monster;
    public float spawnTime;

    public void CreateEnemy()
    {
        StartCoroutine(instant(spawnTime, monster));
    }

    IEnumerator instant(float spawnTime, GameObject monster)
    {
        GameObject _monster = Instantiate(monster, transform);
        GameObject.FindWithTag("MonsterManager").GetComponent<MonsterManager>().AddAliveMonster(_monster);
        _monster.SetActive(false);
        yield return new WaitForSeconds(spawnTime);
        _monster.SetActive(true);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 0.5f);
    }
#endif
}
