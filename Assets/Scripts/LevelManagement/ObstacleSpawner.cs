using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField]
    private ObstacleSpawner m_otherSpawner;

    public GameObject m_previousSpawnPrefab;

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
    [Range(0.01f, 100f)]
    private float m_minTimeBetweenObstacles = 2f;
    [SerializeField]
    [Range(0.02f, 100f)]
    private float m_maxTimeBetweenObstacles = 2f;

    [SerializeField]
    private List<GameObject> m_obstaclesActive = new List<GameObject>();

    private int m_prefabIndex = 0;

    private Color m_activeColor;

    private Coroutine m_spawnSessionRoutine;

    private void Start()
    {
        //Ensures valid range.
        if (m_maxTimeBetweenObstacles < m_minTimeBetweenObstacles)
            m_maxTimeBetweenObstacles = m_minTimeBetweenObstacles;

        if (m_prefabsToSpawn.Length > 0)
            StartCoroutine(BeginSpawnSession());
    }

    public void UpdatePrimaryColor( Color a_newPrimaryColor )
    {
        //Updates our active colour
        m_activeColor = a_newPrimaryColor;

        //Updates colliders
        UpdateObjectColliders();
    }

    private void UpdateObjectCollider(GameObject a_obstacleGO)
    {
        //Early out
        if (m_activeColor == null || a_obstacleGO.GetComponent<Obstacle>() == null)
            return;


        //Maybe use a Key Value Pair instead for coloured obstacles?
        Color thisObjColor = a_obstacleGO.GetComponent<Obstacle>().GetInformedColor();
        Collider2D[] colliders = a_obstacleGO.GetComponentsInChildren<Collider2D>();

        if (thisObjColor == m_activeColor)
        {
            foreach(Collider2D current in colliders)
            {
                current.enabled = false;
            }
        }
        else
        {
            foreach (Collider2D current in colliders)
            {
                current.enabled = true;
            }
        }
    }

    private void UpdateObjectColliders()
    {
        //Disables colliders on each obstacle with our active colour, enables on all not this colour

        foreach (GameObject obstacle in m_obstaclesActive)
        {
            if (obstacle != null)
            {
                UpdateObjectCollider(obstacle);               
            }
        }
    }

    public GameObject Spawn()
    {
        Transform parent = m_spawnParent != null ? m_spawnParent : this.transform;

        int prefabRandomIndex = GetRandomInt(0, m_prefabsToSpawn.Length);
        int spawnPositionRandomIndex = GetRandomInt(0, m_spawnPositions.Length);

        //This should hopefully stop us spawning two yellows?
        if (m_prefabsToSpawn[prefabRandomIndex].GetComponent<Obstacle>() == null)//Yellows don't have the obstacle component
        {
            //Now see if the other spawner previously spawned a yellow
            if ((m_otherSpawner.m_previousSpawnPrefab) && m_otherSpawner.m_previousSpawnPrefab == m_prefabsToSpawn[prefabRandomIndex])
            {
                return Spawn();
            }
        }

        GameObject chosenPrefab = m_prefabsToSpawn[prefabRandomIndex];
        m_previousSpawnPrefab = chosenPrefab;

        Vector3 chosenSpawnPosition = m_spawnPositions[spawnPositionRandomIndex].position;

        GameObject ourNewObj = Instantiate(chosenPrefab, chosenSpawnPosition, chosenPrefab.transform.rotation, parent);
        ourNewObj.GetComponent<MoveLeft>().moveSpeed = m_environmentMovementSpeed;

        //Some obstacles don't use colour flip, so got to check for it.
        if (ourNewObj.GetComponent<Obstacle>())
            ourNewObj.GetComponent<Obstacle>().InformColour(GetRandomColor());

        UpdateObjectCollider(ourNewObj);

        m_obstaclesActive.Add(ourNewObj);


        IncrementIndex();

        return ourNewObj;
    }

    protected IEnumerator SpawnInTime()
    {
        //Calculating before as index gets incremented in Spawn()
        Spawn();

        float timeBetweenObstacles = UnityEngine.Random.Range(m_minTimeBetweenObstacles, m_maxTimeBetweenObstacles);

        yield return new WaitForSeconds(timeBetweenObstacles);

        m_spawnSessionRoutine = StartCoroutine(SpawnInTime());
    }

    private IEnumerator BeginSpawnSession()
    {
        float randomStartTimer = UnityEngine.Random.Range(m_minTimeBetweenObstacles, m_maxTimeBetweenObstacles);
        yield return new WaitForSeconds(randomStartTimer);

        m_spawnSessionRoutine = StartCoroutine(SpawnInTime());
    }

    private void IncrementIndex()
    {
        m_prefabIndex++;

        if (m_prefabIndex >= m_prefabsToSpawn.Length)
            m_prefabIndex = 0;
    }

    /// <summary>
    /// lower = inclusive, upper = exclusive
    /// </summary>
    private int GetRandomInt( int lower, int upper)
    {
        return UnityEngine.Random.Range(lower, upper);
    }

    private Color GetRandomColor()
    {
        int m_colorIndex = 0;

        m_colorIndex = UnityEngine.Random.Range(0, m_availableColors.Length);
        return m_availableColors[m_colorIndex];
    }
}
