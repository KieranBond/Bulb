using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField]
    private Transform m_spawnParent;

    [SerializeField]
    private Transform[] m_spawnPositions;

    [SerializeField]
    protected GameObject[] m_prefabsToSpawn;

    [SerializeField]
    private Color[] m_availableColors;

    [SerializeField]
    private float m_environmentMovementSpeed;

    [SerializeField]
    private float m_timeBetweenObstacles = 2f;



    private int m_prefabIndex = 0;

    private Coroutine m_spawnSessionRoutine;

    private void Start()
    {
        if (m_prefabsToSpawn.Length > 0)
            BeginSpawnSession();
    }

    private void Update()
    {
    }

    public GameObject Spawn()
    {
        Transform parent = m_spawnParent != null ? m_spawnParent : this.transform;

        GameObject chosenPrefab = m_prefabsToSpawn[m_prefabIndex];
        Vector3 chosenSpawnPosition = m_spawnPositions[m_prefabIndex].position;

        GameObject ourNewObj = Instantiate(chosenPrefab, chosenSpawnPosition, chosenPrefab.transform.rotation, parent);
        ourNewObj.GetComponent<MoveLeft>().moveSpeed = m_environmentMovementSpeed;
        ourNewObj.GetComponent<Obstacle>().InformColour(GetRandomColor());

        IncrementIndex();

        return ourNewObj;
    }

    protected IEnumerator SpawnInTime()
    {
        //Calculating before as index gets incremented in Spawn()
        Spawn();

        yield return new WaitForSeconds(m_timeBetweenObstacles);

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

    private Color GetRandomColor()
    {
        int m_colorIndex = 0;

        m_colorIndex = UnityEngine.Random.Range(0, m_availableColors.Length);
        Debug.Log(m_colorIndex);
        Debug.Log(m_availableColors[m_colorIndex]);
        return m_availableColors[m_colorIndex];
    }
}
