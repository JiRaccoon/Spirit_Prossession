using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameStrike : MonoBehaviour
{
    [SerializeField]
    GameObject flameStrikeObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FlamesStrikeOn()
    {
        flameStrikeObject.SetActive(true);
    }

    public void FlamesStrikeDestroy()
    {
        // 애니메이션 이벤트로 실행
        Destroy(gameObject);
    }
}
