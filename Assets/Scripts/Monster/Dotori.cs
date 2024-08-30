using System;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using Unity.Mathematics;
using UnityEngine;

public class Dotori : Monster
{
    [SerializeField]
    private float[] Stat;
    [SerializeField]
    private GameObject Bullet;
    [SerializeField]
    private GameObject Mine;

    float _lastMineTime;

    // Start is called before the first frame update
    void Awake()
    {
        base.Init(Stat); //몬스터 스텟 5칸 체력,공격력,이동속도,총알속도,딜레이
    }
    private void Start()
    {
        if (_IsSoul == _isSoul.NULL) //눌 이란 에이아이일때
        {
            StartCoroutine(AutoShot());
            StartCoroutine(AutoSkil());
        }
    }

    protected override void MonsterDefaultAttack() //몬스터 사격
    {
        if (Time.time < lastShootTime + stats._ShotDelay) //몬스터 딜레이
            return;
        // Slime's Attack
        lastShootTime = Time.time;
        if (_StayObj != null) return;
        animator.SetTrigger("Attack"); //애니메이션 공격은 어택 죽음은 데스로 통일
        GameObject _object = Instantiate(Bullet, transform.position, Quaternion.identity); //총알소환
        if (_IsSoul == _isSoul.NULL) //에이아이일때
        {
            //4방향으로 위치잡아서 쏘는거
            Vector2 dir = Vector2.zero;
            if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) > Mathf.Abs(GetComponent<Rigidbody2D>().velocity.y))
            {
                if (GetComponent<Rigidbody2D>().velocity.x < 0)
                    dir = Vector2.left;
                else
                    dir = Vector2.right;
            }
            else
            {
                if (GetComponent<Rigidbody2D>().velocity.y > 0)
                    dir = Vector2.up;
                else
                    dir = Vector2.down;
            }
            _object.GetComponent<Rigidbody2D>().AddForce(dir * stats._BulletSpeed, ForceMode2D.Force);

        }
        else //에이아이 아닐떄
            _object.GetComponent<Rigidbody2D>().AddForce(bulletDirection * stats._BulletSpeed, ForceMode2D.Force);

        _object.GetComponent<DotoriShot>().Dmg = stats._Atk; //총알에 공격력
        _object.GetComponent<DotoriShot>().MyObj = gameObject.name; //총알이 자기자신안떄리게

    }
    protected override void MonsterSpecialAttack() //몬스터 mine
    {
        if (Time.time < _lastMineTime + stats._ShotDelay * 3) //몬스터 딜레이
            return;
        // Slime's Attack
        _lastMineTime = Time.time;
        if (_StayObj != null) return;
        GameObject _object = Instantiate(Mine, transform.position, Quaternion.identity); //mine소환

        _object.GetComponent<DotoriShot>().Dmg = stats._Atk; //총알에 공격력
        _object.GetComponent<DotoriShot>().MyObj = gameObject.name; //총알이 자기자신안떄리게

    }
    public IEnumerator AutoShot() //자동공격
    {
        while (_IsSoul == _isSoul.NULL)
        {
            MonsterDefaultAttack();
            yield return new WaitForSeconds(stats._ShotDelay * 1.5f); //몬스터는 플레이어보다 딜레이좀더느림 원하는데로 조정
        }
    }

    public IEnumerator AutoSkil()
    {
        while(_IsSoul==_isSoul.NULL)
        {
            MonsterSpecialAttack();
            yield return new WaitForSeconds(stats._ShotDelay * 3f);
        }
    }
}