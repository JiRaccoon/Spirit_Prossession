using System.Collections;
using UnityEngine;

public class PurpleSlime : Monster
{
    [SerializeField]
    private float[] SlimeStat;
    [SerializeField]
    private GameObject Bullet;

    private int attactType = 0;
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
        GameObject _object2 = Instantiate(Bullet, transform.position, Quaternion.identity); //�Ѿ˼�ȯ
        GameObject _object3 = Instantiate(Bullet, transform.position, Quaternion.identity); //�Ѿ˼�ȯ
        GameObject _object4 = Instantiate(Bullet, transform.position, Quaternion.identity); //�Ѿ˼�ȯ

        if (attactType == 0)
        {
            _object.GetComponent<Rigidbody2D>().AddForce(Vector2.up * stats._BulletSpeed, ForceMode2D.Force);
            _object2.GetComponent<Rigidbody2D>().AddForce(Vector2.down * stats._BulletSpeed, ForceMode2D.Force);
            _object3.GetComponent<Rigidbody2D>().AddForce(Vector2.left * stats._BulletSpeed, ForceMode2D.Force);
            _object4.GetComponent<Rigidbody2D>().AddForce(Vector2.right * stats._BulletSpeed, ForceMode2D.Force);
            attactType = 1;
        }
        else
        {
            _object.GetComponent<Rigidbody2D>().AddForce(new Vector2(1,1) * stats._BulletSpeed, ForceMode2D.Force);
            _object2.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1, 1) * stats._BulletSpeed, ForceMode2D.Force);
            _object3.GetComponent<Rigidbody2D>().AddForce(new Vector2(1, -1) * stats._BulletSpeed, ForceMode2D.Force);
            _object4.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1, -1) * stats._BulletSpeed, ForceMode2D.Force);
            attactType = 0;
        }


        _object.GetComponent<Bullet>().Dmg = stats._Atk; //�Ѿ˿� ���ݷ�
        _object.GetComponent<Bullet>().MyObj = gameObject.name; //�Ѿ��� �ڱ��ڽžȋ�����
        _object2.GetComponent<Bullet>().Dmg = stats._Atk; //�Ѿ˿� ���ݷ�
        _object2.GetComponent<Bullet>().MyObj = gameObject.name; //�Ѿ��� �ڱ��ڽžȋ�����
        _object3.GetComponent<Bullet>().Dmg = stats._Atk; //�Ѿ˿� ���ݷ�
        _object3.GetComponent<Bullet>().MyObj = gameObject.name; //�Ѿ��� �ڱ��ڽžȋ�����
        _object4.GetComponent<Bullet>().Dmg = stats._Atk; //�Ѿ˿� ���ݷ�
        _object4.GetComponent<Bullet>().MyObj = gameObject.name; //�Ѿ��� �ڱ��ڽžȋ�����

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
