using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupLights : MonoBehaviour
{

    [SerializeField]
    private GameObject item;


    public GameObject player;

    public Light light;

    Vector3 playerPos;
    Vector3 itemPos;

    float dist;

    // Start is called before the first frame update
    void Start()
    {
        playerPos = player.transform.position;

        itemPos = item.transform.position;

        dist = 1000;
        
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = player.transform.position;

        itemPos = item.transform.position;

        dist = Distance(itemPos, playerPos);

        if (dist <= 0.5)
        {
            light.intensity = 1000;
        }

        else if(dist >= 10)
        {
            light.intensity = 0;

        }

        else
        {
            light.intensity = (1 / dist) * 7 + 3;

        }

    }


    float Distance(Vector3 SPos, Vector3 TPos)
    {
        float formula = Mathf.Sqrt((Mathf.Pow((TPos.x - SPos.x), 2) + Mathf.Pow((TPos.z - SPos.z), 2)));

        return formula;

    }
}
