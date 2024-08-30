using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class Slime : Monster
{
    [SerializeField]
    private float[] SlimeStat;
    [SerializeField]
    private GameObject Bullet;

    private float lastShootTime;     // 마지막 발사 시간


    // Start is called before the first frame update
    void Start()
    {
        base.Init(SlimeStat);
    }

    protected override void MonsterDefaultAttack()
    {
        if (Time.time < lastShootTime + stats._ShotDelay)
            return;
        // Slime's Attack
        lastShootTime = Time.time;
        if (_StayObj != null) return;
        animator.SetTrigger("Attack");
        GameObject _object = Instantiate(Bullet, transform.position, Quaternion.identity);
        _object.GetComponent<Rigidbody2D>().AddForce(bulletDirection * stats._BulletSpeed, ForceMode2D.Force);
        _object.GetComponent<Bullet>().Dmg = stats._Atk;
        _object.GetComponent<Bullet>().MyObj = gameObject.name;
        
    }
}