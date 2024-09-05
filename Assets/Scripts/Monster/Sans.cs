using System.Collections;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Sans : Monster
{
    [SerializeField]
    private float[] SnasStat; // Hp, Atk, MoveSpeed, _BulletSpeed, ShootDelay
    [SerializeField]
    private GameObject Bullet;
    [SerializeField]
    private GameObject _Blaster;

    private float _DelayOffset = 0;
    private int _random_num;
    private Vector2 _dir;
    public Vector2 dir { get { return _dir; } set { _dir = value; } }
    private float _PositionOffset = 0.5f;
    public float PositionOffset { get { return _PositionOffset; } set { _PositionOffset = value; } }

    private bool _isPlayer;
    public bool IsPlayer { get { return _isPlayer; } set { _isPlayer = value; } }

    public int SoulState;

    // Start is called before the first frame update
    void Awake()
    {
        base.Init(SnasStat); //���� ���� 5ĭ ü��,���ݷ�,�̵��ӵ�,�Ѿ˼ӵ�,������
    }

    private void Start()
    {
        if (_IsSoul == _isSoul.NULL) //�� �̶� ���̾����϶�
        {
            StartCoroutine(AutoShot());
            _random_num = Random.Range(1, 5);
        }
        SoulState = (int)_IsSoul;
        _dir = Vector2.zero;
        _isPlayer = false;
        SetDir(gameObject);
        _Blaster = Instantiate(_Blaster); // ������ ��ȯ
        _Blaster.GetComponent<BlasterMovement>().Init(gameObject);
    }

    private void SetDir(GameObject argObj)
    {
        switch (_random_num)
        {
            case 1:
                _dir = Vector2.left;
                argObj.GetComponent<SpriteRenderer>().flipX = true;
                break;
            case 2:
                _dir = Vector2.right;
                argObj.GetComponent<SpriteRenderer>().flipX = false;
                break;
            case 3:
                _dir = Vector2.up;
                break;
            default:
                _dir = Vector2.down;
                break;
        }
    }

    public void ChangeAttack()
    {
        if(gameObject != null)
            animator.SetBool("Attack", false);
    }

    protected override void MonsterDefaultAttack() //���� ���
    {
        if (Time.time < lastShootTime + stats._ShotDelay) //���� ������
            return;

        // Slime's Attack
        lastShootTime = Time.time;
        if (_StayObj != null) return;

        animator.SetBool("Attack", true); //�ִϸ��̼� ������ ���� ������ ������ ����

        if (_IsSoul == _isSoul.Death) return;
        GameObject _object = Instantiate(Bullet, (Vector2)_Blaster.transform.position + _dir * _PositionOffset, Quaternion.identity); //�Ѿ˼�ȯ


        if (_IsSoul == _isSoul.NULL) //Ai
        {
            _object.GetComponent<Rigidbody2D>().AddForce(_dir * stats._BulletSpeed, ForceMode2D.Force);
        }
        else // �޸��̰���
        {
            _object.GetComponent<Rigidbody2D>().AddForce(_Ddir * stats._BulletSpeed, ForceMode2D.Force);
        }

        _object.GetComponent<Bullet>().Dmg = stats._Atk; //�Ѿ˿� ���ݷ�
        _object.GetComponent<Bullet>().MyObj = gameObject.name; //�Ѿ��� �ڱ��ڽžȋ�����
    }

    public void ReInitPlayerModeStat()
    {
        stats._MoveSpeed = 5.0f;
        _dir = Vector2.zero;
        _Ddir = Vector2.zero;
        _isPlayer = true;
        SoulState = (int)_IsSoul;
        animator.SetBool("Attack", false);
    }

    public IEnumerator AutoShot() //�ڵ�����
    {
        yield return new WaitForSeconds(1.0f);
        while (_IsSoul == _isSoul.NULL)
        {
            MonsterDefaultAttack();
            yield return new WaitForSeconds(stats._ShotDelay * _DelayOffset); //���ʹ� �÷��̾�� �������������� ���ϴµ��� ����
        }
    }
}