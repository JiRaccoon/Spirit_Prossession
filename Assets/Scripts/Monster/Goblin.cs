using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : Monster
{
    [SerializeField]
    private float[] GoblinStat;

    [SerializeField]
    private GameObject GoblinAttackCall;

    [SerializeField]
    private GameObject GoblinAttackCallL;

    bool isAttack = false;

    private void Awake()
    {
        base.Init(GoblinStat);
    }

    SpriteRenderer spriteRenderer;


    void Start()
    {
        //SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        animator.SetBool("isWalkBool", true);
        if (_IsSoul == _isSoul.NULL) //눌 이란 에이아이일때
        {

            StartCoroutine(AutoShot());
        }
        StartCoroutine(MyUpdate());
    }

    protected override void MonsterDefaultAttack()
    {
        if (Time.time < lastShootTime + stats._ShotDelay) //몬스터 딜레이
            return;


        //animator.SetBool("isWalkBool", false);
        lastShootTime = Time.time;


        if (_StayObj != null || _IsSoul == _isSoul.Death) return;

        animator.SetTrigger("Attack"); //애니메이션 공격은 어택 죽음은 데스로 통일

        if (_IsSoul == _isSoul.NULL) //에이아이일때
        {
            //4방향으로 위치잡아서 쏘는거

            if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) > Mathf.Abs(GetComponent<Rigidbody2D>().velocity.y))
            {
                if (GetComponent<Rigidbody2D>().velocity.x < 0)
                {
                    Debug.Log("들어옴1");
                    CallGoblinAttackOnEnable(true);
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                }
                else
                {
                    CallGoblinAttackOnEnable(false);
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                }
            }

        }
        else
        {

            //if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            //{
            //    Debug.Log("A");
            //    isAttack = true;
            //    CallGoblinAttackOnEnable(isAttack);
            //}
            //if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            //{
            //    Debug.Log("B");
            //    isAttack = false;
            //    CallGoblinAttackOnEnable(isAttack);
            //}
        }

        //GoblinAttackCall.GetComponent<BoxCollider2D>().enabled = true;
        //Debug.Log("나감"+GoblinAttackCall.GetComponent<BoxCollider2D>().enabled);

        CallGoblinAttackOnEnable(isAttack);

        GoblinAttackCall.GetComponent<MeleeAttack>().Dmg = stats._Atk;
        GoblinAttackCall.GetComponent<MeleeAttack>().MyObj = gameObject.name;

        GoblinAttackCallL.GetComponent<MeleeAttack>().Dmg = stats._Atk;
        GoblinAttackCallL.GetComponent<MeleeAttack>().MyObj = gameObject.name;

        if (!DeathCheck())
            animator.SetBool("isWalkBool", true);
    }

    IEnumerator MyUpdate()
    {
        while(true)
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                Debug.Log("A");
                isAttack = true;
                //CallGoblinAttackOnEnable(isAttack);
            }
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                Debug.Log("B");
                isAttack = false;
                //CallGoblinAttackOnEnable(isAttack);
            }
            yield return null;
        }
    }
    bool DeathCheck()
    {
        return _IsSoul == _isSoul.Death;
    }
    public IEnumerator AutoShot() //자동공격
    {
        while (_IsSoul == _isSoul.NULL)
        {
            //if (GoblinAttackCall.GetComponent<BoxCollider2D>().enabled == true)
            //    GoblinAttackCall.GetComponent<BoxCollider2D>().enabled = false;

            MonsterDefaultAttack();

            yield return new WaitForSecondsRealtime(animator.GetCurrentAnimatorStateInfo(0).length);
            
            

            yield return new WaitForSeconds(stats._ShotDelay * 1.5f); //몬스터는 플레이어보다 딜레이좀더느림 원하는데로 조정
        }
    }

    public void CallGoblinAttackOnEnable(bool a)
    {
        if (a)
            GoblinAttackCallL.GetComponent<BoxCollider2D>().enabled = true;
        else if(!a)
            GoblinAttackCall.GetComponent<BoxCollider2D>().enabled = true;
    }

    public void CallGoblinAttackUnEnable()
    {
        
            GoblinAttackCallL.GetComponent<BoxCollider2D>().enabled = false;
        
            GoblinAttackCall.GetComponent<BoxCollider2D>().enabled = false;
    }

    
}
