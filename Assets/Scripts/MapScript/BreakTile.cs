using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;
    
public class BreakTile : MonoBehaviour
{
    public Tilemap tilemap;

    private void Start()
    {
        tilemap = GetComponent<Tilemap>();
    }

    public void MakeDot( Vector3 pos)
    {
        Vector3Int cellPosition = tilemap.WorldToCell(pos);

        tilemap.SetTile(cellPosition, null);
    }
}
