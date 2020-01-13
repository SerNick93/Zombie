using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField]
    Transform[] spawnPoints;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnLevelWasLoaded()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
           // spawnPoints[i] = GameObject.FindObjectOfType<SpawnPoint>().;
        }
    }
}
