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
        if (_IsSoul == _isSoul.NULL) //�� �̶� ���̾����϶�
        {

            StartCoroutine(AutoShot());
        }
        StartCoroutine(MyUpdate());
    }

    protected override void MonsterDefaultAttack()
    {
        if (Time.time < lastShootTime + stats._ShotDelay) //���� ������
            return;


        //animator.SetBool("isWalkBool", false);
        lastShootTime = Time.time;


        if (_StayObj != null || _IsSoul == _isSoul.Death) return;

        animator.SetTrigger("Attack"); //�ִϸ��̼� ������ ���� ������ ������ ����

        if (_IsSoul == _isSoul.NULL) //���̾����϶�
        {
            //4�������� ��ġ��Ƽ� ��°�

            if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) > Mathf.Abs(GetComponent<Rigidbody2D>().velocity.y))
            {
                if (GetComponent<Rigidbody2D>().velocity.x < 0)
                {
                    Debug.Log("����1");
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
        //Debug.Log("����"+GoblinAttackCall.GetComponent<BoxCollider2D>().enabled);

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
