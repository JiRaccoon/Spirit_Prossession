using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Start()
    {
        FrameSet();
    }

    void FrameSet()
    {
        Application.targetFrameRate = 60;
        Debug.Log(Time.time);
    }
    
    
}
