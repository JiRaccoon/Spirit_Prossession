using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GM : MonoBehaviour
{
    private int _PlayerExist = 2;
    public int PlayerExist { get { return _PlayerExist; } set { _PlayerExist = value; } }

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
    }

    public void LoadDeathScene()
    {
        Invoke("LoadDeathScene2", 2.0f);
    }

    public void LoadDeathScene2()
    {
        SceneManager.LoadScene(2);
    }
}
