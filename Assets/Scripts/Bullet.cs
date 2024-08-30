using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float _Dmg;
    public float Dmg { get { return _Dmg; } set {  _Dmg = value; } }

    private string _MyObj;
    public string MyObj {  get { return _MyObj; } set {  _MyObj = value; } }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_MyObj == collision.name) return;

        if(collision.gameObject.GetComponent<Monster>())
        collision.gameObject.GetComponent<Monster>().takeDamage(_Dmg);

        if(collision.tag!="Bullet")
            Destroy(gameObject);
    }
}
