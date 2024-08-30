using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.EventSystems;

public class Monster : MonoBehaviour
{
    protected enum _isSoul { PlayerOne = 1, PlayerTwo, NULL, Death } // player's soul
    [SerializeField]
    protected bool _HaveSoul = false;
    [SerializeField]
    private GameObject _PlayerSoul;
    [SerializeField]
    private GameObject _StayObj;

    [SerializeField]
    private _isSoul _IsSoul;
    protected struct _Stats { // Monsters Stats
        public float _MaxHp;
        public float _Hp; 
        public float _Atk;
        public float _MoveSpeed;
        public float _BulletSpeed;
        public float _ShotDelay;
    };

    protected Vector2 bulletDirection = Vector2.up;

    protected _Stats stats; // Add a field to store the _Stats struct

    Rigidbody2D rb;

    
    protected void Init(float[] argStats) 
    {
        stats._MaxHp = argStats[0];
        stats._Hp = stats._MaxHp;
        stats._Atk = argStats[1];
        stats._MoveSpeed = argStats[2];
        stats._BulletSpeed = argStats[3];
        stats._ShotDelay = argStats[4];
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        MonsterMovement();
        PreMonsterAttack();
        pushSoul();
    }

    // Move
    protected void MonsterMovement()
    {
        float moveX = 0f;
        float moveY = 0f;

        Vector2 moveDirection = new Vector2();

        if (_IsSoul == _isSoul.NULL) return;

        if (_IsSoul == _isSoul.Death) return;

        if (_IsSoul == _isSoul.PlayerOne)
        {
            // W 또는 위 방향키를 눌렀을 때
            if (Input.GetKey(KeyCode.W))
            {
                moveY += 1f;
                bulletDirection = new Vector2(0, 1);
            }

            // S 또는 아래 방향키를 눌렀을 때
            if (Input.GetKey(KeyCode.S))
            {
                moveY -= 1f;
                bulletDirection = new Vector2(0, -1);
            }

            // A 또는 왼쪽 방향키를 눌렀을 때
            if (Input.GetKey(KeyCode.A))
            {
                moveX -= 1f;
                bulletDirection = new Vector2(-1, 0);
            }

            // D 또는 오른쪽 방향키를 눌렀을 때
            if (Input.GetKey(KeyCode.D))
            {
                moveX += 1f;
                bulletDirection = new Vector2(1, 0);
            }
        }
        else if (_IsSoul == _isSoul.PlayerTwo)
        {
            // W 또는 위 방향키를 눌렀을 때
            if (Input.GetKey(KeyCode.UpArrow))
            {
                moveY += 1f;
                bulletDirection = new Vector2(0, 1);
            }

            // S 또는 아래 방향키를 눌렀을 때
            if (Input.GetKey(KeyCode.DownArrow))
            {
                moveY -= 1f;
                bulletDirection = new Vector2(0, -1);
            }

            // A 또는 왼쪽 방향키를 눌렀을 때
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                moveX -= 1f;
                bulletDirection = new Vector2(-1, 0);
            }

            // D 또는 오른쪽 방향키를 눌렀을 때
            if (Input.GetKey(KeyCode.RightArrow))
            {
                moveX += 1f;
                bulletDirection = new Vector2(1, 0);
            }
        }

        // 이동 벡터 생성
        moveDirection = new Vector2(moveX, moveY).normalized;

        // 이동 적용
        rb.MovePosition(rb.position + moveDirection * stats._MoveSpeed * Time.deltaTime);
    }



    // Attack
    protected void PreMonsterAttack()
    {
        if(_IsSoul == _isSoul.PlayerOne) 
        {
            if (Input.GetKey(KeyCode.F))
            {
                MonsterDefaultAttack();
            }
            else if (Input.GetKey(KeyCode.G))
            {
                MonsterSpecialAttack();
            }
        }
        else if(_IsSoul == _isSoul.PlayerTwo)
        {
            if (Input.GetKey(KeyCode.K))
            {
                MonsterDefaultAttack();
            }
            else if (Input.GetKey(KeyCode.L))
            {
                MonsterSpecialAttack();
            }
        }
        
    }

    protected virtual void MonsterDefaultAttack()
    {

    }

    protected virtual void MonsterSpecialAttack()
    {

    }

    protected void MonsterDie()
    {
        if (_IsSoul == _isSoul.PlayerOne || _IsSoul == _isSoul.PlayerTwo)
        {
            GameObject soul = Instantiate(_PlayerSoul, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else
        {
            transform.GetComponent<BoxCollider2D>().isTrigger = true;

        }


        _IsSoul = _isSoul.Death;
    }

    public void takeDamage(float argDmg)
    {
        stats._Hp -= argDmg;
        //Debug.Log(stats._Hp);

        if(stats._Hp <= 0)
        {
            MonsterDie();
        }
    }

    private void pushSoul()
    {
        if (_StayObj == null) return;

        if (Input.GetKey(KeyCode.F) || Input.GetKey(KeyCode.K))
        {
            _StayObj.tag = "Player";
            _StayObj.GetComponent<Monster>()._IsSoul = _IsSoul == _isSoul.PlayerOne ? _isSoul.PlayerTwo : _isSoul.PlayerOne;
            _StayObj.name = _IsSoul == _isSoul.PlayerOne ? "PlayerTwo" : "PlayerOne";
            _StayObj.GetComponent<Monster>().ResetStat();
            _StayObj = null;
        }
    }

    public void ResetStat()
    {
        stats._Hp = stats._MaxHp;
        GetComponent<BoxCollider2D>().isTrigger = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Soul" && _IsSoul != _isSoul.NULL) {
            Destroy(collision.gameObject);
            _HaveSoul = true;
        }

        if(collision.GetComponent<Monster>()) 
        {
            if(collision.GetComponent<Monster>()._IsSoul == _isSoul.Death)
                _StayObj = collision.gameObject;
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Monster>())
        {
            if (collision.GetComponent<Monster>()._IsSoul == _isSoul.Death)
                _StayObj = null;
        }
    }
}
