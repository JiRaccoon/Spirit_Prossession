using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float _Dmg;
    public float Dmg { get { return _Dmg; } set {  _Dmg = value; } }

    private string _MyObj;
    public string MyObj {  get { return _MyObj; } set {  _MyObj = value; } }

    Rigidbody2D _Rigidbody2D;

    private void Start()
    {
        _Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Box") return;
        if (collision.transform.GetComponent<BreakTile>() != null)
        {
            int offset = 0;
            if (_Rigidbody2D.velocity.x < 0) offset = -1;
            else if(_Rigidbody2D.velocity.x > 0) offset = 1;
            Vector2 closestPoint = collision.ClosestPoint(transform.position);
            collision.transform.GetComponent<BreakTile>().MakeDot(closestPoint + new Vector2(offset,0));
        }


        if (_MyObj == collision.name) return;
        if (collision.tag == "Soul" || collision.tag == "Bullet")
            return;


        Debug.Log(collision.name);

        if (collision.gameObject.GetComponent<Monster>())
        {
            collision.gameObject.GetComponent<Monster>().takeDamage(_Dmg);
        }

        if (collision.gameObject.GetComponent<Monster>() && collision.gameObject.GetComponent<Monster>().CheckDie())
        {
            return;
        }
        Destroy(gameObject);
    }
}
