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
        GameObject _object2 = Instantiate(Bullet, transform.position, Quaternion.identity); //총알소환2
        if (_IsSoul == _isSoul.NULL) //에이아이일때
        {
            //4방향으로 위치잡아서 쏘는거

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



            // 오른쪽으로 발사
            if (dir == Vector2.right)
            {
                dir = new Vector2(1f, -1f); // 오른쪽 아래로 발사 (↘)
            }
            else if (dir == Vector2.left) // 왼쪽으로 발사
            {
                dir = new Vector2(-1f, 1f); // 왼쪽 위로 발사 (↖)
            }
            else if (dir == Vector2.up) // 위로 발사
            {
                dir = new Vector2(1f, 1f); // 위 오른쪽으로 발사 (↗)
            }
            else if (dir == Vector2.down) // 아래로 발사
            {
                dir = new Vector2(-1f, -1f); // 아래 왼쪽으로 발사 (↙)
            }

            // bulletDirection2도 동일하게 설정
            if (bulletDirection2 == Vector2.right)
            {
                bulletDirection2 = new Vector2(1f, 1f); // 오른쪽 위로 발사 (↗)
            }
            else if (bulletDirection2 == Vector2.left)
            {
                bulletDirection2 = new Vector2(-1f, -1f); // 왼쪽 아래로 발사 (↙)
            }
            else if (bulletDirection2 == Vector2.up)
            {
                bulletDirection2 = new Vector2(-1f, 1f); // 위 왼쪽으로 발사 (↖)
            }
            else if (bulletDirection2 == Vector2.down)
            {
                bulletDirection2 = new Vector2(1f, -1f); // 아래 오른쪽으로 발사 (↘)
            }
            _object.GetComponent<Rigidbody2D>().AddForce(dir * stats._BulletSpeed, ForceMode2D.Force);
            _object2.GetComponent<Rigidbody2D>().AddForce(bulletDirection2 * stats._BulletSpeed, ForceMode2D.Force);

        }
        else //에이아이 아닐떄
        {
            if (bulletDirection == Vector2.zero)
                bulletDirection = lastDirection;
            Vector2 bulletDirection2 = bulletDirection;

            Debug.Log(bulletDirection);
            Debug.Log(bulletDirection2);
            lastDirection = bulletDirection;
            // 오른쪽으로 발사
            if (bulletDirection == Vector2.right)
            {
                bulletDirection = new Vector2(1f, -1f); // 오른쪽 아래로 발사 (↘
                bulletDirection2 = new Vector2(1f, 1f); // 오른쪽 위로 발사 (↗)
            }
            else if (bulletDirection == Vector2.left) // 왼쪽으로 발사
            {
                bulletDirection = new Vector2(-1f, 1f); // 왼쪽 위로 발사 (↖)
                bulletDirection2 = new Vector2(-1f, -1f); // 왼쪽 아래로 발사 (↙)
            }
            else if (bulletDirection == Vector2.up) // 위로 발사
            {
                bulletDirection = new Vector2(1f, 1f); // 위 오른쪽으로 발사 (↗)
                bulletDirection2 = new Vector2(-1f, 1f); // 위 왼쪽으로 발사 (↖)
            }
            else if (bulletDirection == Vector2.down) // 아래로 발사
            {
                bulletDirection = new Vector2(-1f, -1f); // 아래 왼쪽으로 발사 (↙)
                bulletDirection2 = new Vector2(1f, -1f); // 아래 오른쪽으로 발사 (↘)
            }
            else
            {
                bulletDirection = new Vector2(-1f, -1f); // 아래 왼쪽으로 발사 (↙)
                bulletDirection2 = new Vector2(1f, -1f); // 아래 오른쪽으로 발사 (↘)
            }

            
            // 각 객체에 힘 추가
            _object.GetComponent<Rigidbody2D>().AddForce(bulletDirection * stats._BulletSpeed, ForceMode2D.Force);
            _object2.GetComponent<Rigidbody2D>().AddForce(bulletDirection2 * stats._BulletSpeed, ForceMode2D.Force);


            bulletDirection = lastDirection;
        }


        _object.GetComponent<Bullet>().Dmg = stats._Atk; //총알에 공격력
        _object.GetComponent<Bullet>().MyObj = gameObject.name; //총알이 자기자신안떄리게
        ///_object.GetComponent<Bullet>().Break = true;
        _object2.GetComponent<Bullet>().Dmg = stats._Atk; //총알에 공격력
        _object2.GetComponent<Bullet>().MyObj = gameObject.name; //총알이 자기자신안떄리게
        //_object2.GetComponent<Bullet>().Break = true;


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
