using System;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    protected GameObject[] m_prefabsToSpawn;
    [SerializeField]
    protected Vector2[] m_spawnPositions;

    //[SerializeField]
    //public UnityEngine.Object[] m_attachables;

    public float xSpacing = 0.64f;
    public float ySpacing = 0.64f;

    private int m_numberSpawned = 0;

    private void OnGUI()
    {
        if(GUI.Button(new Rect(0, 0, 100, 100), "Spawn"))
        {
            SpaceSpawn(this.gameObject.transform);
        }
    }

    //Returns the created GO.
    public GameObject[] SpaceSpawn(Transform parent)
    {
        float xSpawnPos = 0f;
        float ySpawnPos = 0f;

        xSpawnPos = 0 + (m_numberSpawned * xSpacing);
        ySpawnPos = 0 + (m_numberSpawned * ySpacing);
        Debug.Log("Spawned: " + m_numberSpawned + "\nxSpawnPos: " + xSpawnPos + "\nySpawnPos: " + ySpawnPos);

        m_numberSpawned++;//Only add when using our own figuring out. This way we don't throw off the next spawn.

        return Spawn(parent, xSpawnPos, ySpawnPos);
    }

    public GameObject[] Spawn(Transform parent)
    {
        return Spawn(parent, m_spawnPositions);
    }

    //Overloaded version. Allows you to set the xPos and yPos per obj.
    public GameObject[] Spawn(Transform parent, float[] xPos, float[] yPos)
    {
        if(xPos.Length != yPos.Length)
        {
            return null;
        }

        Vector2[] pos = new Vector2[xPos.Length];
        for(int i = 0; i < xPos.Length; i++)
        {
            pos[i].x = xPos[i];
            pos[i].y = yPos[i];
        }

        return Spawn(parent, pos);
    }

    public GameObject[] Spawn(Transform parent, Vector2[] spawnPos)
    {
        List<GameObject> objsSpawned = new List<GameObject>();

        for(int i = 0; i < m_prefabsToSpawn.Length; i++)
        {
            GameObject current = m_prefabsToSpawn[i];

            float xSpawnPos = spawnPos[i].x;
            float ySpawnPos = spawnPos[i].y;

            Vector3 position = new Vector3(xSpawnPos, ySpawnPos, current.transform.position.z);
            GameObject newSpawnedObj = Instantiate(current, position, current.transform.rotation, parent);
            newSpawnedObj.AddComponent<MoveLeft>();

            objsSpawned.Add(newSpawnedObj);
        }

        return objsSpawned.ToArray();

    }

    //Overloaded version. Allows you to set the xPos and yPos.
    public GameObject[] Spawn(Transform parent, float xPos, float yPos)
    {
        float xSpawnPos = xPos;
        float ySpawnPos = yPos;

        List<GameObject> objsSpawned = new List<GameObject>();

        foreach(GameObject current in m_prefabsToSpawn)
        {
            Vector3 position = new Vector3(xSpawnPos, ySpawnPos, current.transform.position.z);
            GameObject newSpawnedObj = Instantiate(current, position, current.transform.rotation, parent);
            newSpawnedObj.AddComponent<MoveLeft>();

            objsSpawned.Add(newSpawnedObj);
        }


        //foreach(var current in m_attachables)//If any attachables, add them.
        //{
            //Type obj =  current.GetType();
            //newSpawnedObj.AddComponent(obj);
            ////if(current == typeof(Component))
            ////    newSpawnedObj.AddComponent(current);
        //}

        return objsSpawned.ToArray();
    }
}
