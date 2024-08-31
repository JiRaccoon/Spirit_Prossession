using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Break : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // BreakTile 컴포넌트를 가진 오브젝트와 충돌한 경우
        BreakTile breakTile = collision.transform.GetComponent<BreakTile>();
        if (breakTile != null)
        {
            // 충돌 지점을 기반으로 타일 삭제
            ContactPoint2D[] contacts = collision.contacts;
            if (contacts.Length > 0)
            {
                Vector3 hitPosition = contacts[0].point;  // 첫 번째 충돌 지점
                Debug.LogWarning(hitPosition);
                breakTile.MakeDot(hitPosition);           // 해당 지점에서 타일 삭제
                Debug.LogWarning(hitPosition);            // 충돌 지점 출력
            }
        }
    }
}
