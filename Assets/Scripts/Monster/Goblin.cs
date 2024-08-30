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
        if (_IsSoul == _isSoul.NULL) //�� �̶� ���̾����϶�
        {

            StartCoroutine(AutoShot());
        }
    }

    protected override void MonsterDefaultAttack()
    {
        if (Time.time < lastShootTime + stats._ShotDelay) //���� ������
            return;


        //animator.SetBool("isWalkBool", false);
        lastShootTime = Time.time;

        Debug.Log("����"+GoblinAttackCall.GetComponent<BoxCollider2D>().enabled);
        if (_StayObj != null)
            return;

        animator.SetTrigger("Attack"); //�ִϸ��̼� ������ ���� ������ ������ ����


        if (_IsSoul == _isSoul.NULL) //���̾����϶�
        {
            //4�������� ��ġ��Ƽ� ��°�
            
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
        Debug.Log("����"+GoblinAttackCall.GetComponent<BoxCollider2D>().enabled);

        GoblinAttackCall.GetComponent<MeleeAttack>().Dmg = stats._Atk;
        GoblinAttackCall.GetComponent<MeleeAttack>().MyObj = gameObject.name;
        
        if(!DeathCheck())
            animator.SetBool("isWalkBool", true);
    }

    bool DeathCheck()
    {
        return _IsSoul == _isSoul.Death;
    }
    public IEnumerator AutoShot() //�ڵ�����
    {
        while (_IsSoul == _isSoul.NULL)
        {
            //if (GoblinAttackCall.GetComponent<BoxCollider2D>().enabled == true)
            //    GoblinAttackCall.GetComponent<BoxCollider2D>().enabled = false;

            MonsterDefaultAttack();

            yield return new WaitForSecondsRealtime(animator.GetCurrentAnimatorStateInfo(0).length);
            
            

            yield return new WaitForSeconds(stats._ShotDelay * 1.5f); //���ʹ� �÷��̾�� �������������� ���ϴµ��� ����
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
