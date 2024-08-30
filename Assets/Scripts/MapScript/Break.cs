using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Break : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.GetComponent<BreakTile>() != null)
        {
            ContactPoint2D[] contacts = collision.contacts;
            if (contacts.Length > 0)
            {
                Vector3 hitPosition = contacts[0].point;  // 첫 번째 충돌 지점
                collision.transform.GetComponent<BreakTile>().MakeDot(hitPosition);
            }
        }
    }
}
