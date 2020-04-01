using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    public GameObject fruit;
    bool isSpawn = false;
    List<GameObject> goList = new List<GameObject>();
 
    void SpawnFruits()
    {
        
        isSpawn = true;
        int fruitSpawn = Random.Range(3,6);
        for(int i=0; i <fruitSpawn; i++)
        {
           Vector3 fruitPos = new Vector3(this.transform.position.x + Random.Range(1.9f, 5.0f),
                                           this.transform.position.y + Random.Range(1.5f, 2.0f),
                                           this.transform.position.z + Random.Range(1.9f, 5.0f));
           Instantiate(fruit, fruitPos, Quaternion.identity);
           goList.Add(fruit);
            
        }
        Debug.Log(goList.Count);
        Invoke("Respawn", 120f);
    }

    void Start()
    {
        SpawnFruits();
    }


    void Respawn()
    {
        if (GameObject.Find("BlueBerry(Clone)") == null)
        {

            Debug.Log("There is no more blueberries left in bush.");
            isSpawn = true;
            goList = new List<GameObject>();
            Invoke("SpawnFruits", 60f);
        }
        else
        {
            Debug.Log("There are more blueberries left in bush.");
            Invoke("Respawn", 120f);
        }
    }


}
