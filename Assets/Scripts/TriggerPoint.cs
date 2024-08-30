using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPoint : MonoBehaviour
{
    public MoveMapCam _mc;
    public int _tmpnum = 0;
    void Start()
    {
        if (this.gameObject.name == "MovePoint_L") _tmpnum = 0;
        else if (this.gameObject.name == "MovePoint_R") _tmpnum = 1;
    }


    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Debug.Log(1);

            if (!_mc._check)
            {
                Debug.Log(1);
                _mc._check = true;
                StartCoroutine(_mc.CameraMove(_tmpnum));
            }
        }
    }
}
