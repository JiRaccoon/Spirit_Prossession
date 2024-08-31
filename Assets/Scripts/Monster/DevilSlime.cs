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
    private GameObject FirePillarPrefab; // �ұ�� ������
    private int attactType = 0;
    private int bulletCount = 0;
    private int nextAttackDelay = 3;

    
    // Start is called before the first frame update
    void Awake()
    {
        base.Init(SlimeStat); //���� ���� 5ĭ ü��,���ݷ�,�̵��ӵ�,�Ѿ˼ӵ�,������
    }
    private void Start()
    {
        if (_IsSoul == _isSoul.NULL) //�� �̶� ���̾����϶�
        {
            StartCoroutine(AutoShot());
        }
    }

    protected override void MonsterDefaultAttack() //���� ���
    {
        if (Time.time < lastShootTime + stats._ShotDelay) //���� ������
            return;
        // Slime's Attack
        lastShootTime = Time.time;
        if (_StayObj != null) return;
        //�ִϸ��̼� ������ ���� ������ ������ ����
        GameObject _object = null;

        if (attactType == 0)
        {
            float randomAngle = Random.Range(0f, 360f);
            Vector2 dir = new Vector2(Mathf.Cos(randomAngle * Mathf.Deg2Rad), Mathf.Sin(randomAngle * Mathf.Deg2Rad));
            _object = Instantiate(Bullet, transform.position, Quaternion.identity); //�Ѿ˼�ȯ
            _object.GetComponent<Rigidbody2D>().AddForce(dir * stats._BulletSpeed, ForceMode2D.Force);
            _object.GetComponent<Bullet>().Dmg = stats._Atk; //�Ѿ˿� ���ݷ�
            _object.GetComponent<Bullet>().MyObj = gameObject.name; //�Ѿ��� �ڱ��ڽžȋ�����
            //ź��
            bulletCount++;
        }
        else if (attactType == 1)
        {
            // ������ ��ȯ
            float x = Random.Range(SkillRange.bounds.min.x, SkillRange.bounds.max.x);
            float y = Random.Range(SkillRange.bounds.min.y, SkillRange.bounds.max.y);
            Vector3 spawnPosition = new Vector3(x, y, 0);
            GameObject _slime = Instantiate(Slimes[Random.Range(0, Slimes.Length)], spawnPosition , Quaternion.identity);
            bulletCount++;

        }
        else if ( attactType == 2)
        {
            animator.SetTrigger("Attack");
            // �ұ��
            float x = Random.Range(SkillRange.bounds.min.x, SkillRange.bounds.max.x);
            float y = Random.Range(SkillRange.bounds.min.y, SkillRange.bounds.max.y);
            Vector3 spawnPosition = new Vector3(x, y, 0);
            Instantiate(FirePillarPrefab, spawnPosition, Quaternion.identity);
            bulletCount++;
        }





    }
    public IEnumerator AutoShot() //�ڵ�����
    {
        while (_IsSoul == _isSoul.NULL)
        {
            MonsterDefaultAttack();
            yield return new WaitForSeconds(stats._ShotDelay); //���ʹ� �÷��̾�� �������������� ���ϴµ��� ����
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
            yield return new WaitForSeconds(0.8f); //���ʹ� �÷��̾�� �������������� ���ϴµ��� ����
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
            yield return new WaitForSeconds(0.6f); //���ʹ� �÷��̾�� �������������� ���ϴµ��� ����
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
