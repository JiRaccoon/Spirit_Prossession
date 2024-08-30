using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class grid_tistory : MonoBehaviour
{
    Vector2 MousePosition;
    public Camera Camera;
    public List<Tilemap> Tilemap;
    public int tileMapNumber = 0;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            tileMapNumber++;
        }
        if (Input.GetMouseButtonDown(0))
        {
            MousePosition = Input.mousePosition;
            MousePosition = Camera.ScreenToWorldPoint(MousePosition);

            Debug.Log(Tilemap[tileMapNumber].WorldToCell(MousePosition));
        }
    }
}