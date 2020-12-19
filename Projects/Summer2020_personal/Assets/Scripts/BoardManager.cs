using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BoardManager : MonoBehaviour
{
    public GameObject tileMap;


    // Start is called before the first frame update
    void Start()
    {
        tileMap.GetComponent<Tilemap>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
