using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    private Animator _animator;


    public void EndFire()
    {
        Destroy(transform.parent.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Monster>())
            collision.gameObject.GetComponent<Monster>().takeDamage(1);
    }
}
