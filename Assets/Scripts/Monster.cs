using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
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

    private _isSoul _tempSoul;
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

    private GM _GM;


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
        _GM = GameObject.FindWithTag("GameManager").GetComponent<GM>();
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

        Vector3 pos = mainCamera.WorldToViewportPoint(transform.position);
        if (pos.x < 0f) pos.x = 0f;
        if (pos.x > 1f) pos.x = 1f;
        if (pos.y < 0f) pos.y = 0f;
        if (pos.y > 1f) pos.y = 1f;
        transform.position = mainCamera.ViewportToWorldPoint(pos);
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
        if (_tempSoul == _isSoul.PlayerOne || _tempSoul == _isSoul.PlayerTwo)
        {
            GameObject soul = Instantiate(_PlayerSoul, transform.position, Quaternion.identity);
            soul.GetComponent<SpriteRenderer>().sprite = _tempSoul == _isSoul.PlayerOne ? _SoulSprits[0] : _SoulSprits[1];
            _GM.PlayerExist--;
            if (_GM.PlayerExist == 0) _GM.LoadDeathScene();
            Destroy(gameObject);
        }
        else
        { 
            transform.GetComponent<BoxCollider2D>().isTrigger = true;
            GetComponent<PathFinding>().moveSpeed = 0;
            GameObject.FindGameObjectWithTag("MonsterManager").GetComponent<MonsterManager>().RemoveMonster(this.gameObject);
        }      
    }

    public void takeDamage(float argDmg)
    {
        if (stats._Hp <= 0) return;
        stats._Hp -= argDmg;

        if(_IsSoul!=_isSoul.NULL)
        {
            if(transform.GetChild(2).childCount > 0)
            {
                StartCoroutine(HPLight());
            }
        }

        if(stats._Hp <= 0)
        {
            _tempSoul = _IsSoul;
            _IsSoul = _isSoul.Death;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
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
        if (!_HaveSoul) return;

        if (Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.K))
        {
            if (_StayObj == null) return;
            GameObject temp = _StayObj.gameObject;
            _HaveSoul = false;

            temp.tag = "Player";
            temp.GetComponent<Monster>()._IsSoul = _IsSoul == _isSoul.PlayerOne ? _isSoul.PlayerTwo : _isSoul.PlayerOne;
            temp.name = _IsSoul == _isSoul.PlayerOne ? "PlayerTwo" : "PlayerOne";
            temp.GetComponent<Monster>().ResetStat();
            if (temp.GetComponent<Sans>())
            {// 샌즈때문에 넣었어요 - 손준호
                temp.GetComponent<Sans>().ReInitPlayerModeStat();
            }
            _StayObj = null;
            temp = null;
        }
    }

    public void ResetStat()
    {
        stats._Hp = stats._MaxHp;
        _GM.PlayerExist++;
        GetComponent<BoxCollider2D>().isTrigger = false;
        animator.SetBool("Death", false);
        int pNum = _IsSoul == _isSoul.PlayerOne ? 0 : 1;
        GetComponent<PathFinding>().grid._Player_transform[pNum] = gameObject;
        transform.GetChild(pNum).gameObject.SetActive(true);
        gameObject.tag = "Player";

        for (int i = 0; i < stats._MaxHp + 1; i++)
        {
            Instantiate(_HpPoint,transform.GetChild(2));
        }
        rb.constraints = RigidbodyConstraints2D.None;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        StartCoroutine(HPLight());
    }

    public bool CheckDie()
    {
        if (_IsSoul == _isSoul.Death)
            return true;
        return false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Soul" && _IsSoul != _isSoul.NULL && _IsSoul != _isSoul.Death) {
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
