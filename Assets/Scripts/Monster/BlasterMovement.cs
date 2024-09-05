using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlasterMovement : MonoBehaviour
{
    private Sans _Sans;
    private GameObject _TargetMonster;
    private SpriteRenderer _spriteRenderer;

    public void Init(GameObject argObj)
    {
        _Sans = argObj.GetComponent<Sans>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _TargetMonster = argObj;
    }

    void Update()
    {
        Movement();
        ChangAttackAnim();
    }

    private void ChangAttackAnim()
    {
        if (_Sans.SoulState == 3) return; // _IsSoul == NULL  = AI상태

        if (_Sans.SoulState == 1)
        { // 플레이어1
            if (Input.GetKeyUp(KeyCode.F))
            {
                if(_Sans != null)
                    _Sans.ChangeAttack();
            }
        }
        else if (_Sans.SoulState == 2) {
            if (Input.GetKeyUp(KeyCode.K))
            {
                if (_Sans != null)
                    _Sans.ChangeAttack();
            }
        }
    }

    private void Movement()
    {
        if (_TargetMonster == null) return;
        
        if(_Sans._Ddir == Vector2.zero)
            gameObject.transform.position = (Vector2)_TargetMonster.transform.position + _Sans.dir * _Sans.PositionOffset;
        else
            gameObject.transform.position = (Vector2)_TargetMonster.transform.position + _Sans._Ddir * _Sans.PositionOffset;



        if (!_Sans.IsPlayer)
        {
            if (_Sans.dir == Vector2.up)
                gameObject.transform.eulerAngles = new Vector3(0, 0, 90);
            else if (_Sans.dir == Vector2.down)
                gameObject.transform.eulerAngles = new Vector3(0, 0, -90);
            else if (_Sans.dir == Vector2.right)
            {
                _spriteRenderer.flipX = false;
                gameObject.transform.eulerAngles = Vector3.zero;
            }
            else if (_Sans.dir == Vector2.left)
            {
                _spriteRenderer.flipX = true;
                gameObject.transform.eulerAngles = Vector3.zero;
            }
        }
        else
        {
            if (_Sans._Ddir == Vector2.up)
            {
                _spriteRenderer.flipX = false;
                gameObject.transform.eulerAngles = new Vector3(0, 0, 90);
            }
            else if (_Sans._Ddir == Vector2.down)
            {
                _spriteRenderer.flipX = false;
                gameObject.transform.eulerAngles = new Vector3(0, 0, -90);
            }
            else if (_Sans._Ddir == Vector2.right)
            {
                _spriteRenderer.flipX = false;
                gameObject.transform.eulerAngles = Vector3.zero;
            }
            else if (_Sans._Ddir == Vector2.left)
            {
                _spriteRenderer.flipX = true;
                gameObject.transform.eulerAngles = Vector3.zero;
            }
        }
    }
}
