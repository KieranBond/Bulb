using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] m_prefabsToSpawn;

    [SerializeField]
    private GameObject m_parent;

    public void Spawn()
    {
        foreach(GameObject obj in m_prefabsToSpawn)
        {
            GameObject go = Instantiate(obj, m_parent.transform);
        }
    }
}
