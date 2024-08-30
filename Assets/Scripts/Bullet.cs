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
        if (collision.transform.GetComponent<BreakTile>() != null)
        {
            Vector2 closestPoint = collision.ClosestPoint(transform.position);
            collision.transform.GetComponent<BreakTile>().MakeDot(closestPoint);
        }


        if (_MyObj == collision.name) return;
        if (collision.tag == "Soul" || collision.tag == "Bullet" || collision.gameObject.GetComponent<BoxCollider2D>().isTrigger)
            return;
               

        if (collision.gameObject.GetComponent<Monster>())
        collision.gameObject.GetComponent<Monster>().takeDamage(_Dmg);

        Destroy(gameObject);
    }
}
