using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : Monster
{
    [SerializeField]
    private float[] GoblinStat;

    [SerializeField]
    private GameObject GoblinAttackCall;

    
    private void Awake()
    {
        base.Init(GoblinStat);
    }




    void Start()
    {
        animator.SetBool("isWalkBool", true);
        if (_IsSoul == _isSoul.NULL) //눌 이란 에이아이일때
        {

            StartCoroutine(AutoShot());
        }
    }

    protected override void MonsterDefaultAttack()
    {
        if (Time.time < lastShootTime + stats._ShotDelay) //몬스터 딜레이
            return;


        //animator.SetBool("isWalkBool", false);
        lastShootTime = Time.time;

        Debug.Log("들어옴"+GoblinAttackCall.GetComponent<BoxCollider2D>().enabled);
        if (_StayObj != null)
            return;

        animator.SetTrigger("Attack"); //애니메이션 공격은 어택 죽음은 데스로 통일


        if (_IsSoul == _isSoul.NULL) //에이아이일때
        {
            //4방향으로 위치잡아서 쏘는거
            
            if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) > Mathf.Abs(GetComponent<Rigidbody2D>().velocity.y))
            {
                if (GetComponent<Rigidbody2D>().velocity.x < 0)
                {
                    
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                }
                else
                {
                    
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                }
            }

        }


        GoblinAttackCall.GetComponent<BoxCollider2D>().enabled = true;
        Debug.Log("나감"+GoblinAttackCall.GetComponent<BoxCollider2D>().enabled);

        GoblinAttackCall.GetComponent<MeleeAttack>().Dmg = stats._Atk;
        GoblinAttackCall.GetComponent<MeleeAttack>().MyObj = gameObject.name;
        
        if(!DeathCheck())
            animator.SetBool("isWalkBool", true);
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

    public void CallGoblinAttackOnEnable()
    {
        GoblinAttackCall.GetComponent<BoxCollider2D>().enabled = true;
    }

    public void CallGoblinAttackUnEnable()
    {
        GoblinAttackCall.GetComponent<BoxCollider2D>().enabled = false;
    }

    
}
