using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOnPosition : MonoBehaviour
{
    [SerializeField]
    private Transform m_spawnParent;

    [SerializeField]
    private Transform m_spawnPosition;

    [SerializeField]
    protected GameObject[] m_prefabsToSpawn;

    [SerializeField]
    protected float[] m_prefabsXSizes;

    [SerializeField]
    private float m_environmentMovementSpeed;

    private int m_prefabIndex = 0;

    private Coroutine m_spawnSessionRoutine;

    private void Start()
    {
        if(m_prefabsToSpawn.Length > 0)
            BeginSpawnSession();
    }

    public GameObject Spawn()
    {
        Transform parent = m_spawnParent != null ? m_spawnParent : this.transform;

        GameObject chosenPrefab = m_prefabsToSpawn[m_prefabIndex];

        GameObject ourNewObj = Instantiate(chosenPrefab, m_spawnPosition.position, chosenPrefab.transform.rotation, parent);
        ourNewObj.GetComponent<MoveLeft>().moveSpeed = m_environmentMovementSpeed;

        IncrementIndex();

        return ourNewObj;
    }

    protected IEnumerator SpawnInTime()
    {
        //Calculating before as index gets incremented in Spawn()
        float timeToWait = (int)(m_prefabsXSizes[m_prefabIndex] / m_environmentMovementSpeed);
        Debug.Log("Time to wait: " + timeToWait);

        Spawn();

        yield return new WaitForSeconds(timeToWait);

        m_spawnSessionRoutine = StartCoroutine(SpawnInTime());
    }

    private void BeginSpawnSession()
    {
        m_spawnSessionRoutine = StartCoroutine(SpawnInTime());
    }

    private void IncrementIndex()
    {
        m_prefabIndex++;

        if (m_prefabIndex >= m_prefabsToSpawn.Length)
            m_prefabIndex = 0;
    }

}
