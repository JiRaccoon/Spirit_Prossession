using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class DevilSlime : Monster
{
    [SerializeField]
    private float[] SlimeStat;
    [SerializeField]
    private GameObject Bullet;

    [SerializeField]
    private GameObject[] Slimes;

    [SerializeField]
    private BoxCollider2D SkillRange;

    [SerializeField]
    private GameObject FirePillarPrefab; // 불기둥 프리팹
    private int attactType = 0;
    private int bulletCount = 0;
    private int nextAttackDelay = 3;

    
    // Start is called before the first frame update
    void Awake()
    {
        base.Init(SlimeStat); //몬스터 스텟 5칸 체력,공격력,이동속도,총알속도,딜레이
    }
    private void Start()
    {
        if (_IsSoul == _isSoul.NULL) //눌 이란 에이아이일때
        {
            StartCoroutine(AutoShot());
        }
    }

    protected override void MonsterDefaultAttack() //몬스터 사격
    {
        if (Time.time < lastShootTime + stats._ShotDelay) //몬스터 딜레이
            return;
        // Slime's Attack
        lastShootTime = Time.time;
        if (_StayObj != null) return;
        //애니메이션 공격은 어택 죽음은 데스로 통일
        GameObject _object = null;

        if (attactType == 0)
        {
            float randomAngle = Random.Range(0f, 360f);
            Vector2 dir = new Vector2(Mathf.Cos(randomAngle * Mathf.Deg2Rad), Mathf.Sin(randomAngle * Mathf.Deg2Rad));
            _object = Instantiate(Bullet, transform.position, Quaternion.identity); //총알소환
            _object.GetComponent<Rigidbody2D>().AddForce(dir * stats._BulletSpeed, ForceMode2D.Force);
            _object.GetComponent<Bullet>().Dmg = stats._Atk; //총알에 공격력
            _object.GetComponent<Bullet>().MyObj = gameObject.name; //총알이 자기자신안떄리게
            //탄막
            bulletCount++;
        }
        else if (attactType == 1)
        {
            // 슬라임 소환
            float x = Random.Range(SkillRange.bounds.min.x, SkillRange.bounds.max.x);
            float y = Random.Range(SkillRange.bounds.min.y, SkillRange.bounds.max.y);
            Vector3 spawnPosition = new Vector3(x, y, 0);
            GameObject _slime = Instantiate(Slimes[Random.Range(0, Slimes.Length)], spawnPosition , Quaternion.identity);
            bulletCount++;

        }
        else if ( attactType == 2)
        {
            animator.SetTrigger("Attack");
            // 불기둥
            float x = Random.Range(SkillRange.bounds.min.x, SkillRange.bounds.max.x);
            float y = Random.Range(SkillRange.bounds.min.y, SkillRange.bounds.max.y);
            Vector3 spawnPosition = new Vector3(x, y, 0);
            Instantiate(FirePillarPrefab, spawnPosition, Quaternion.identity);
            bulletCount++;
        }





    }
    public IEnumerator AutoShot() //자동공격
    {
        while (_IsSoul == _isSoul.NULL)
        {
            MonsterDefaultAttack();
            yield return new WaitForSeconds(stats._ShotDelay); //몬스터는 플레이어보다 딜레이좀더느림 원하는데로 조정
            if (bulletCount >= 50)
            {
                attactType = 1;
                bulletCount = 0;
                yield return new WaitForSeconds(nextAttackDelay);
                StartCoroutine(SpawnSlime());
                break;

            }
        }
        
    }

    public IEnumerator SpawnSlime() 
    {
        while (_IsSoul == _isSoul.NULL)
        {
            MonsterDefaultAttack();
            yield return new WaitForSeconds(0.8f); //몬스터는 플레이어보다 딜레이좀더느림 원하는데로 조정
            if (bulletCount >= 5)
            {
                attactType = 2;
                bulletCount = 0;
                yield return new WaitForSeconds(nextAttackDelay);
                StartCoroutine(SpawnFire());
                break;
            }

        }
    }

    public IEnumerator SpawnFire()
    {
        while (_IsSoul == _isSoul.NULL)
        {
            MonsterDefaultAttack();
            yield return new WaitForSeconds(0.6f); //몬스터는 플레이어보다 딜레이좀더느림 원하는데로 조정
            if (bulletCount >= 15)
            {
                attactType = 0;
                bulletCount = 0;
                yield return new WaitForSeconds(nextAttackDelay);
                StartCoroutine(AutoShot());
                break;

            }

        }
    }
}
