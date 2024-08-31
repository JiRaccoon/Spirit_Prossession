using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class GreenSlime : Monster
{
    [SerializeField]
    private float[] SlimeStat;
    [SerializeField]
    private GameObject Bullet;


    private Vector2 lastDirection;
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
        animator.SetTrigger("Attack"); //�ִϸ��̼� ������ ���� ������ ������ ����
        GameObject _object = Instantiate(Bullet, transform.position, Quaternion.identity); //�Ѿ˼�ȯ
        GameObject _object2 = Instantiate(Bullet, transform.position, Quaternion.identity); //�Ѿ˼�ȯ2
        if (_IsSoul == _isSoul.NULL) //���̾����϶�
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
            Vector2 bulletDirection2 = dir;



            // ���������� �߻�
            if (dir == Vector2.right)
            {
                dir = new Vector2(1f, -1f); // ������ �Ʒ��� �߻� (��)
            }
            else if (dir == Vector2.left) // �������� �߻�
            {
                dir = new Vector2(-1f, 1f); // ���� ���� �߻� (��)
            }
            else if (dir == Vector2.up) // ���� �߻�
            {
                dir = new Vector2(1f, 1f); // �� ���������� �߻� (��)
            }
            else if (dir == Vector2.down) // �Ʒ��� �߻�
            {
                dir = new Vector2(-1f, -1f); // �Ʒ� �������� �߻� (��)
            }

            // bulletDirection2�� �����ϰ� ����
            if (bulletDirection2 == Vector2.right)
            {
                bulletDirection2 = new Vector2(1f, 1f); // ������ ���� �߻� (��)
            }
            else if (bulletDirection2 == Vector2.left)
            {
                bulletDirection2 = new Vector2(-1f, -1f); // ���� �Ʒ��� �߻� (��)
            }
            else if (bulletDirection2 == Vector2.up)
            {
                bulletDirection2 = new Vector2(-1f, 1f); // �� �������� �߻� (��)
            }
            else if (bulletDirection2 == Vector2.down)
            {
                bulletDirection2 = new Vector2(1f, -1f); // �Ʒ� ���������� �߻� (��)
            }
            _object.GetComponent<Rigidbody2D>().AddForce(dir * stats._BulletSpeed, ForceMode2D.Force);
            _object2.GetComponent<Rigidbody2D>().AddForce(bulletDirection2 * stats._BulletSpeed, ForceMode2D.Force);

        }
        else //���̾��� �ƴҋ�
        {
            if (bulletDirection == Vector2.zero)
                bulletDirection = lastDirection;
            Vector2 bulletDirection2 = bulletDirection;

            Debug.Log(bulletDirection);
            Debug.Log(bulletDirection2);
            lastDirection = bulletDirection;
            // ���������� �߻�
            if (bulletDirection == Vector2.right)
            {
                bulletDirection = new Vector2(1f, -1f); // ������ �Ʒ��� �߻� (��
                bulletDirection2 = new Vector2(1f, 1f); // ������ ���� �߻� (��)
            }
            else if (bulletDirection == Vector2.left) // �������� �߻�
            {
                bulletDirection = new Vector2(-1f, 1f); // ���� ���� �߻� (��)
                bulletDirection2 = new Vector2(-1f, -1f); // ���� �Ʒ��� �߻� (��)
            }
            else if (bulletDirection == Vector2.up) // ���� �߻�
            {
                bulletDirection = new Vector2(1f, 1f); // �� ���������� �߻� (��)
                bulletDirection2 = new Vector2(-1f, 1f); // �� �������� �߻� (��)
            }
            else if (bulletDirection == Vector2.down) // �Ʒ��� �߻�
            {
                bulletDirection = new Vector2(-1f, -1f); // �Ʒ� �������� �߻� (��)
                bulletDirection2 = new Vector2(1f, -1f); // �Ʒ� ���������� �߻� (��)
            }
            else
            {
                bulletDirection = new Vector2(-1f, -1f); // �Ʒ� �������� �߻� (��)
                bulletDirection2 = new Vector2(1f, -1f); // �Ʒ� ���������� �߻� (��)
            }

            
            // �� ��ü�� �� �߰�
            _object.GetComponent<Rigidbody2D>().AddForce(bulletDirection * stats._BulletSpeed, ForceMode2D.Force);
            _object2.GetComponent<Rigidbody2D>().AddForce(bulletDirection2 * stats._BulletSpeed, ForceMode2D.Force);


            bulletDirection = lastDirection;
        }


        _object.GetComponent<Bullet>().Dmg = stats._Atk; //�Ѿ˿� ���ݷ�
        _object.GetComponent<Bullet>().MyObj = gameObject.name; //�Ѿ��� �ڱ��ڽžȋ�����
        ///_object.GetComponent<Bullet>().Break = true;
        _object2.GetComponent<Bullet>().Dmg = stats._Atk; //�Ѿ˿� ���ݷ�
        _object2.GetComponent<Bullet>().MyObj = gameObject.name; //�Ѿ��� �ڱ��ڽžȋ�����
        //_object2.GetComponent<Bullet>().Break = true;


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
