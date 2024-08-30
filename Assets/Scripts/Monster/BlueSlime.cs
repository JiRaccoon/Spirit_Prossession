using System;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using Unity.Mathematics;
using UnityEngine;

public class BlueSlime : Monster
{
    [SerializeField]
    private float[] SlimeStat;
    [SerializeField]
    private GameObject Bullet;

    // Start is called before the first frame update
    void Awake()
    {
        base.Init(SlimeStat); //���� ���� 5ĭ ü��,���ݷ�,�̵��ӵ�,�Ѿ˼ӵ�,������
    }
    private void Start()
    {
        if(_IsSoul==_isSoul.NULL) //�� �̶� ���̾����϶�
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
        animator.SetTrigger("Attack"); //�ִϸ��̼� ������ ���� ������ ������ ����
        GameObject _object = Instantiate(Bullet, transform.position, Quaternion.identity); //�Ѿ˼�ȯ
        if(_IsSoul == _isSoul.NULL) //���̾����϶�
        {
            //4�������� ��ġ��Ƽ� ��°�
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
            _object.GetComponent<Rigidbody2D>().AddForce( dir * stats._BulletSpeed, ForceMode2D.Force);
            
        }
        else //���̾��� �ƴҋ�
            _object.GetComponent<Rigidbody2D>().AddForce(bulletDirection * stats._BulletSpeed, ForceMode2D.Force);

        _object.GetComponent<Bullet>().Dmg = stats._Atk; //�Ѿ˿� ���ݷ�
        _object.GetComponent<Bullet>().MyObj = gameObject.name; //�Ѿ��� �ڱ��ڽžȋ�����
        
    }
    public IEnumerator AutoShot() //�ڵ�����
    {
        while (_IsSoul == _isSoul.NULL)
        {            
            MonsterDefaultAttack();
            yield return new WaitForSeconds(stats._ShotDelay * 1.5f); //���ʹ� �÷��̾�� �������������� ���ϴµ��� ����
        }
    }
}