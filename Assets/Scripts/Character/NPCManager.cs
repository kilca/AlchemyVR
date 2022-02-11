using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    [SerializeField] 
    public List<GameObject> npcs;
    public GameObject spawn;
    public static bool lastHasDespawn;
    public int index;

    // Start is called before the first frame update
    void Start()
    {
        index = 0;
        spawn = GameObject.FindGameObjectWithTag("Spawn");
        lastHasDespawn = false;
        SpawnPrefab();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(lastHasDespawn);
        if(lastHasDespawn){
            SpawnPrefab();
        }
    }

    public void SpawnPrefab()
    {
        lastHasDespawn = false;
        GameObject prefab = npcs[index];
        Instantiate(prefab, spawn.transform.position, Quaternion.identity);
        if (index == npcs.Count-1){
            index = 0;
        }
        else {index++;}
    }
}
