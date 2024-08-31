using System;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using Unity.Mathematics;
using UnityEngine;

public class AcherGoblin : Monster
{
    [SerializeField]
    private float[] Stat;
    [SerializeField]
    private GameObject Bullet;
    [SerializeField]
    private GameObject rain;

    float _lastSkilTime;

    Vector2 _lastPostion;

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
            StartCoroutine(WalkCheck());
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
                {
                    dir = Vector2.left;
                    transform.GetComponent<SpriteRenderer>().flipX = true;
                    _object.transform.rotation = Quaternion.Euler(0, 0, 180);
                }
                else
                {
                    dir = Vector2.right;
                    transform.GetComponent<SpriteRenderer>().flipX = false;
                    _object.transform.rotation = Quaternion.Euler(0, 0, 0);
                }
            }
            else
            {
                if (GetComponent<Rigidbody2D>().velocity.y > 0)
                {
                    dir = Vector2.up;

                    _object.transform.rotation = Quaternion.Euler(0, 0, 90);
                }
                else
                {
                    dir = Vector2.down;

                    _object.transform.rotation = Quaternion.Euler(0, 0, -90);
                }
            }
            transform.GetChild(3).GetComponent<SpriteRenderer>().flipX = transform.GetComponent<SpriteRenderer>().flipX;

            _object.GetComponent<Rigidbody2D>().AddForce(dir * stats._BulletSpeed, ForceMode2D.Force);

        }
        else //에이아이 아닐떄
        {
            transform.GetChild(3).GetComponent<SpriteRenderer>().flipX = transform.GetComponent<SpriteRenderer>().flipX;
            _object.GetComponent<Rigidbody2D>().AddForce(bulletDirection * stats._BulletSpeed, ForceMode2D.Force);
        }

        _object.GetComponent<ArrowShot>().Dmg = stats._Atk; //총알에 공격력
        _object.GetComponent<ArrowShot>().MyObj = gameObject.name; //총알이 자기자신안떄리게

    }
    protected override void MonsterSpecialAttack() //몬스터 rain player dash
    {
        if (Time.time < _lastSkilTime + stats._ShotDelay * 5) //몬스터 딜레이
            return;
        // Slime's Attack
        _lastSkilTime = Time.time;
        if (_StayObj != null) return;

        if (_IsSoul == _isSoul.NULL)//boss rain
        {
            animator.SetTrigger("Attack");
            Vector3 playerPostion = GetComponent<PathFinding>().target.transform.position;
            GameObject _object = Instantiate(rain, playerPostion, Quaternion.identity); //mine소환
            _object.GetComponent<ArrowShot>().Dmg = stats._Atk; //총알에 공격력
            _object.GetComponent<ArrowShot>().MyObj = gameObject.name; //총알이 자기자신안떄리게
        }
        else
        {
            StartCoroutine(SpeedUP());
        }

    }
    private IEnumerator SpeedUP()
    {
        stats._MoveSpeed = stats._MoveSpeed * 3;
        yield return new WaitForSeconds(1f);
        stats._MoveSpeed = stats._MoveSpeed / 3;
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
        while (_IsSoul == _isSoul.NULL)
        {
            MonsterSpecialAttack();
            yield return new WaitForSeconds(stats._ShotDelay * 5f);
        }
    }

    private IEnumerator WalkCheck()
    {
        while(_IsSoul == _isSoul.NULL)
        {
            _lastPostion = transform.position;
            yield return new WaitForSeconds(0.2f);
            if (_lastPostion != (Vector2)transform.position)
            {
                animator.SetBool("Walk", true);
            }
            else
                animator.SetBool("Walk", false);
        }
    }
}