using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WaveManager : MonoBehaviour
{
    public int wave = 0;
    public int enyNum = 0;
    private int q = 0;

    private float waveTime = 0.0f;
    private float timeDelay = 2.0f;

    public List<GameObject> Enemies;
    public GameObject spearEnemy;




    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        if(Enemies.Count == 0)
        {
            waveTime += Time.deltaTime;

            if (waveTime >= timeDelay)
            {
                ChangeWave(++wave);
                waveTime = 0.0f;
            }
        }
        
        if (Enemies.Count > 0)
        {
            RemoveEnemies();
        }

    }


    private void ChangeWave(int waveNum)
    {
        int i = 0;
        Enemies.Clear();

        enyNum = waveNum * 3 + 3;
        //enyNum = 3;


        for (i = 0; i < enyNum; i++)
        {
            SpawnEnemy(spearEnemy);


        }



    }

    /// <summary>
    /// Allows for the Enemy to spawn
    /// </summary>
    /// <param name="enemy"></param>
    private void SpawnEnemy(GameObject enemy)
    {
        //Enemies.Add(enemy);
        //Instantiate(enemy, RandPos(), Quaternion.identity);
        Enemies.Add(Instantiate(enemy, RandPos(), Quaternion.identity));

    }

    private Vector2 RandPos()
    {
        float randX = Random.Range(-3.6f, 3.4f);
        float randY = Random.Range(3.6f, -3.3f);

        return new Vector2(randX, randY);

    }

    /// <summary>
    /// Allows for the removal of Enemies from the enemies list
    /// </summary>
    private void RemoveEnemies()
    {
        List<GameObject> numToRemove = new List<GameObject>();
        for(int i = 0; i < Enemies.Count; i++)
        {
            if(!Enemies[i].GetComponent<EnemyScript>().IsAlive)
            {
                numToRemove.Add(Enemies[i]);

            }
        }

        foreach(GameObject enemy in numToRemove)
        {
            if(Enemies.Contains(enemy))
            {
                Enemies.Remove(enemy);
                Destroy(enemy);

            }

        }

    }


}
