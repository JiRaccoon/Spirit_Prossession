using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMapCam : MonoBehaviour
{
    [SerializeField] private Camera _cam;
    [SerializeField] private GameObject _moveobj;

    private void Start()
    {
        _cam = GetComponent<Camera>();
        _moveobj = GetComponent<GameObject>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            if(!_moveobj.activeSelf) _moveobj.SetActive(true);
        }
    }

}
