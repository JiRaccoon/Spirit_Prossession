using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPoint : MonoBehaviour
{
    public MoveMapCam _mc;
    private MapManager _mg;
    public int _tmpnum = 0;
    private grid_tistory _gt;
    void Start()
    {
        _mg.GetComponent<MapManager>();
        _gt.GetComponent<grid_tistory>();
        if (this.gameObject.name == "MovePoint_L") _tmpnum = 0;
        else if (this.gameObject.name == "MovePoint_R") _tmpnum = 1;
    }


    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            _mg.MoveMap(_tmpnum);
        }
    }
}
