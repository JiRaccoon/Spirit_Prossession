using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class ArrowShot : MonoBehaviour
{
    private float _Dmg;
    public float Dmg { get { return _Dmg; } set { _Dmg = value; } }

    private string _MyObj;
    public string MyObj { get { return _MyObj; } set { _MyObj = value; } }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_MyObj == collision.name) return;

        if (collision.gameObject.GetComponent<Monster>())
            collision.gameObject.GetComponent<Monster>().takeDamage(_Dmg);

        StartCoroutine(timedestroy());
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(_MyObj == collision.gameObject.name) return;

        if (collision.gameObject.GetComponent<Monster>())
            collision.gameObject.GetComponent<Monster>().takeDamage(_Dmg);

        if(collision.gameObject.tag!="Bullet" || !collision.gameObject.GetComponent<BoxCollider2D>().isTrigger)
            Destroyself();
    }
    private IEnumerator timedestroy()
    {
        yield return new WaitForSeconds(5);
        Destroyself();
    }

    public void Destroyself()
    {
        Destroy(gameObject);
    }
}
