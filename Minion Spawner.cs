using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Threading;
using UnityEngine;

public class MinionSpawner : MonoBehaviour
{
    public GameObject minionPrefab;
    public GameObject maxionPrefab;
    private float spawnRate = 15.0f;
    private UnityEngine.Vector3 vector;
    public bool isRedTeam;

    // Start is called before the first frame update
    void Start()
    {
        vector = gameObject.transform.position;
        InvokeRepeating("SpawnMinion",1,spawnRate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnMinion()
    {
        if(GameManager.instance.isGameActive)
        {
            for (int i = 0; i < 5; i++)
            {

                if (isRedTeam)
                {
                    vector -= new UnityEngine.Vector3(0, 0, i * 0.75f);
                    if (i == 0)
                        Instantiate(maxionPrefab, vector, minionPrefab.transform.rotation);
                    else
                        Instantiate(minionPrefab, vector, minionPrefab.transform.rotation);

                }

                else
                {
                    vector += new UnityEngine.Vector3(0, 0, i * 0.75f);
                    if (i == 0)
                        Instantiate(maxionPrefab, vector, minionPrefab.transform.rotation);
                    else
                        Instantiate(minionPrefab, vector, minionPrefab.transform.rotation);
                }
            }
            vector = gameObject.transform.position;
        }
    }

}

