using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public GameObject[] Tiles;
    public GameObject Camera;
    public GameObject Player;
    public List<GameObject> ActiveTiles = new List<GameObject>();

    private float YSpawn = 0f;
    private float CameraPosition = 0f;
    private int TileIndex;
    public float TileLength = 9.97f;
    private int MaxActiveTiles = 2;
    private bool FirstTile = true;
    private int TileCounter;

    void Start()
    {
        Tiles = Resources.LoadAll<GameObject>("Tiles");

        for(int i=0; i < MaxActiveTiles; i++)
        {
            SpawnNewTile();
        }
    }

    void Update()
    {
        if(Camera.transform.position != new Vector3(0, CameraPosition, -10))
        {
            Camera.transform.position = Vector3.Lerp(Camera.transform.position, new Vector3(0, CameraPosition, -10), Time.deltaTime);
        }
    }

    public void MoveCamera()
    {
        CameraPosition += TileLength;
    }

    public void SpawnNewTile()
    {
        TileCounter ++;
        TileIndex = Random.Range(0, Tiles.Length);
        GameObject Tile = Instantiate(Tiles[TileIndex], new Vector2(0, YSpawn), new Quaternion(0,0,0,0));
        ActiveTiles.Add(Tile);
        
        if(TileCounter > MaxActiveTiles)
        {
            DeleteOldTile();
        }

        YSpawn += TileLength;
    }

    public void ActivateTile()
    {
        ActiveTiles[1].SetActive(true);
        ActiveTiles[2].SetActive(true);
    }

    void DeleteOldTile()
    {
        if(!FirstTile)
        {
            Destroy(ActiveTiles[0]);
            ActiveTiles.RemoveAt(0);
        }

        FirstTile = false;
    }

}