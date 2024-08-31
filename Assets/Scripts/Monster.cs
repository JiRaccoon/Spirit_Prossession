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
    private Sprite[] _SoulSprits;
    [SerializeField]
    protected GameObject _StayObj;
    [SerializeField]
    private GameObject _HpPoint;

    [SerializeField]
    protected _isSoul _IsSoul;
    public struct _Stats { // Monsters Stats
        public float _MaxHp;
        public float _Hp; 
        public float _Atk;
        public float _MoveSpeed;
        public float _BulletSpeed;
        public float _ShotDelay;
    };

    protected Vector2 bulletDirection = Vector2.up;
    public Vector2 _Ddir; // 샌즈때문에 넣었어요 - 손준호

    public _Stats stats; // Add a field to store the _Stats struct

    Rigidbody2D rb;

    protected Animator animator;

    protected float lastShootTime;

    private Camera mainCamera;


    protected void Init(float[] argStats) 
    {
        stats._MaxHp = argStats[0];
        stats._Hp = stats._MaxHp;
        stats._Atk = argStats[1];
        stats._MoveSpeed = argStats[2];
        stats._BulletSpeed = argStats[3];
        stats._ShotDelay = argStats[4];
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        _Ddir = Vector2.zero; // 샌즈때문에 넣었어요 - 손준호
        mainCamera = Camera.main;
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

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

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
                spriteRenderer.flipX = true;
            }

            // D 또는 오른쪽 방향키를 눌렀을 때
            if (Input.GetKey(KeyCode.D))
            {
                moveX += 1f;
                bulletDirection = new Vector2(1, 0);
                spriteRenderer.flipX = false;
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
                spriteRenderer.flipX = true;
            }

            // D 또는 오른쪽 방향키를 눌렀을 때
            if (Input.GetKey(KeyCode.RightArrow))
            {
                moveX += 1f;
                bulletDirection = new Vector2(1, 0);
                spriteRenderer.flipX = false;
            }
        }

        // 이동 벡터 생성
        moveDirection = new Vector2(moveX, moveY).normalized;
        if (_IsSoul != _isSoul.NULL) _Ddir = bulletDirection; // 샌즈때문에 넣었어요 - 손준호

        // 이동 적용
        Vector2 newPosition = rb.position + moveDirection * stats._MoveSpeed * Time.deltaTime;

        // 화면 경계 계산
        float screenWidth = mainCamera.orthographicSize * mainCamera.aspect;
        float screenHeight = mainCamera.orthographicSize;

        // 화면 경계 내로 위치 제한
        newPosition.x = Mathf.Clamp(newPosition.x, -screenWidth, screenWidth);
        newPosition.y = Mathf.Clamp(newPosition.y, -screenHeight, screenHeight);

        rb.MovePosition(newPosition);

        // 이동 적용
        //rb.MovePosition(rb.position + moveDirection * stats._MoveSpeed * Time.deltaTime);
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
            soul.GetComponent<SpriteRenderer>().sprite = _IsSoul == _isSoul.PlayerOne ? _SoulSprits[0] : _SoulSprits[1];
            Destroy(gameObject);
        }
        else
        { 
            transform.GetComponent<BoxCollider2D>().isTrigger = true;
            GetComponent<PathFinding>().moveSpeed = 0; 
        }

        _IsSoul = _isSoul.Death;
    }

    public void takeDamage(float argDmg)
    {
        if (stats._Hp <= 0) return;
        stats._Hp -= argDmg;
        //Debug.Log(stats._Hp);

        if(_IsSoul!=_isSoul.NULL)
        {
            if(transform.GetChild(2).childCount > 0)
            {
                StartCoroutine(HPLight());
            }
        }

        if(stats._Hp <= 0)
        {
            animator.SetBool("Death",true);
            //MonsterDie();
        }
        else
        {
            StartCoroutine(HitHighlight());
        }
        
    }
    private IEnumerator HPLight()
    {
        transform.GetChild(2).gameObject.SetActive(true);
        Destroy(transform.GetChild(2).GetChild(0).gameObject);
        yield return new WaitForSeconds(0.3f);
        transform.GetChild(2).gameObject.SetActive(false);
    }

    public bool checkAi()
    {
       return  _IsSoul == _isSoul.NULL ? true : false;
    }

    private IEnumerator HitHighlight()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().color = Color.white;
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
            if (_StayObj.GetComponent<Sans>())
            {// 샌즈때문에 넣었어요 - 손준호
                _StayObj.GetComponent<Sans>().ReInitPlayerModeStat();
            }
            _StayObj = null;
        }
    }

    public void ResetStat()
    {
        stats._Hp = stats._MaxHp;
        GetComponent<BoxCollider2D>().isTrigger = false;
        animator.SetBool("Death", false);
        Grid grid =GameObject.Find("Astar").GetComponent<Grid>();
        int pNum = _IsSoul == _isSoul.PlayerOne ? 0 : 1;
        grid._Player_transform[pNum] = gameObject;
        transform.GetChild(pNum).gameObject.SetActive(true);
        gameObject.tag = "Player";

        for (int i = 0; i < stats._MaxHp + 1; i++)
        {
            Instantiate(_HpPoint,transform.GetChild(2));
        }

        StartCoroutine(HPLight());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Soul" && _IsSoul != _isSoul.NULL) {
            Destroy(collision.gameObject);
            _HaveSoul = true;
        }

        if(collision.GetComponent<Monster>()) 
        {
            if(collision.GetComponent<Monster>()._IsSoul == _isSoul.Death && _HaveSoul)
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
