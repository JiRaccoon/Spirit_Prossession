using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Break : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // BreakTile ������Ʈ�� ���� ������Ʈ�� �浹�� ���
        BreakTile breakTile = collision.transform.GetComponent<BreakTile>();
        if (breakTile != null)
        {
            // �浹 ������ ������� Ÿ�� ����
            ContactPoint2D[] contacts = collision.contacts;
            if (contacts.Length > 0)
            {
                Vector3 hitPosition = contacts[0].point;  // ù ��° �浹 ����
                Debug.LogWarning(hitPosition);
                breakTile.MakeDot(hitPosition);           // �ش� �������� Ÿ�� ����
                Debug.LogWarning(hitPosition);            // �浹 ���� ���
            }
        }
    }
}
