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
        animator.SetTrigger("Attack"); //애니메이션 공격은 어택 죽음은 데스로 통일
        GameObject _object = Instantiate(Bullet, transform.position, Quaternion.identity); //총알소환
        GameObject _object2 = Instantiate(Bullet, transform.position, Quaternion.identity); //총알소환
        GameObject _object3 = Instantiate(Bullet, transform.position, Quaternion.identity); //총알소환
        GameObject _object4 = Instantiate(Bullet, transform.position, Quaternion.identity); //총알소환

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


        _object.GetComponent<Bullet>().Dmg = stats._Atk; //총알에 공격력
        _object.GetComponent<Bullet>().MyObj = gameObject.name; //총알이 자기자신안떄리게
        _object2.GetComponent<Bullet>().Dmg = stats._Atk; //총알에 공격력
        _object2.GetComponent<Bullet>().MyObj = gameObject.name; //총알이 자기자신안떄리게
        _object3.GetComponent<Bullet>().Dmg = stats._Atk; //총알에 공격력
        _object3.GetComponent<Bullet>().MyObj = gameObject.name; //총알이 자기자신안떄리게
        _object4.GetComponent<Bullet>().Dmg = stats._Atk; //총알에 공격력
        _object4.GetComponent<Bullet>().MyObj = gameObject.name; //총알이 자기자신안떄리게

    }
    public IEnumerator AutoShot() //자동공격
    {
        while (_IsSoul == _isSoul.NULL)
        {
            MonsterDefaultAttack();
            yield return new WaitForSeconds(stats._ShotDelay * 1.5f); //몬스터는 플레이어보다 딜레이좀더느림 원하는데로 조정
        }
    }
}
