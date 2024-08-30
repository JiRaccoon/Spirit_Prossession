using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Start()
    {
        //Invoke("FrameSet", 1f);
        FrameSet();
        DontDestroyOnLoad(this);
    }

    void FrameSet()
    {
        Application.targetFrameRate = 60;
        Debug.Log(Time.time);
    }
    
    
}
