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
        yield return new WaitForSeconds(spawnTime);
        Instantiate(monster, transform);
    }
}
