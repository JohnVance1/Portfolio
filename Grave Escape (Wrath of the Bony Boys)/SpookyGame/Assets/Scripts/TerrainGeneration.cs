using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TerrainGeneration class
// Placed on a terrain game object
// Generates a Perlin noise-based heightmap

public class TerrainGeneration : MonoBehaviour 
{

	private TerrainData myTerrainData;
	public Vector3 worldSize;
	public int resolution = 513;			// number of vertices along X and Z axes
	float[,] heightArray;
    float scale = 2f;
    

    void Start () 
	{
		myTerrainData = gameObject.GetComponent<TerrainCollider> ().terrainData;
		worldSize = new Vector3 (100, 10, 140);
		myTerrainData.size = worldSize;
		myTerrainData.heightmapResolution = resolution;
		heightArray = new float[resolution, resolution];

		// Fill the height array with values!
		// Uncomment the Ramp and Perlin methods to test them out!
		//Flat(1.0f);
		//Ramp();
		Perlin();

		// Assign values from heightArray into the terrain object's heightmap
		myTerrainData.SetHeights (0, 0, heightArray);
	}
	

	void Update () 
	{
		
	}

	/// <summary>
	/// Flat()
	/// Assigns heightArray identical values
	/// </summary>
	void Flat(float value)
	{
		// Fill heightArray with 1's
		for(int i = 0; i < resolution; i++)
		{
			for(int j = 0; j < resolution; j++)
			{
				heightArray [i, j] = value;
			}
		}
	}
		

	/// <summary>
	/// Ramp()
	/// Assigns heightsArray values that form a linear ramp
	/// </summary>
	void Ramp()
	{
        // Fill heightArray with linear values
        for (int i = 0; i < resolution; i++)
        {
            for(int j = 0; j < resolution; j++)
            {
                heightArray[i, j] = ((float)i / (float)resolution);

            }

        }


	}

	/// <summary>
	/// Perlin()
	/// Assigns heightsArray values using Perlin noise
	/// </summary>
	void Perlin()
	{
        // Fill heightArray with Perlin-based values
        float xCoord = 0;

        for (int x = 0; x < resolution; x++)
        {
            float yCoord = 0;

            for (int y = 0; y < resolution; y++)
            {
                xCoord = ((float)x / resolution) * scale;
                yCoord = ((float)y / resolution) * scale;
                heightArray[x, y] = Mathf.PerlinNoise(xCoord, yCoord);
                


            }

            

        }


    }
}
