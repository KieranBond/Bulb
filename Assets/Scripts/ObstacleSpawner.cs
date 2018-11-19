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
    [Range(0.01f, 100f)]
    private float m_minTimeBetweenObstacles = 2f;
    [SerializeField]
    [Range(0.02f, 100f)]
    private float m_maxTimeBetweenObstacles = 2f;

    private List<GameObject> m_obstaclesActive = new List<GameObject>();

    private int m_prefabIndex = 0;

    private Coroutine m_spawnSessionRoutine;

    private void Start()
    {
        //Ensures valid range.
        if (m_maxTimeBetweenObstacles < m_minTimeBetweenObstacles)
            m_maxTimeBetweenObstacles = m_minTimeBetweenObstacles;

        if (m_prefabsToSpawn.Length > 0)
            BeginSpawnSession();
    }

    private void Update()
    {
    }

    public void UpdatePrimaryColor( Color a_newPrimaryColor )
    {
        //Disables colliders on each obstacle with this colour, enables on all not this colour
        List<int> removeObstacles = new List<int>();
        int n = 0;

        foreach (GameObject obstacle in m_obstaclesActive)
        {
            if (obstacle != null)
            {
                //Maybe use a Key Value Pair instead for coloured obstacles?
                Color thisObjColor = obstacle.GetComponent<Obstacle>().GetInformedColor();

                if (thisObjColor == a_newPrimaryColor)
                {
                    obstacle.GetComponent<Collider2D>().enabled = false;
                }
                else
                {
                    obstacle.GetComponent<Collider2D>().enabled = true;
                }
            }
            //else 
                //removeObstacles.Add(n);

            n++;
        }

        foreach (int i in removeObstacles)
        {
            m_obstaclesActive.RemoveAt(i);
        }
    }

    public GameObject Spawn()
    {
        Transform parent = m_spawnParent != null ? m_spawnParent : this.transform;

        GameObject chosenPrefab = m_prefabsToSpawn[m_prefabIndex];
        Vector3 chosenSpawnPosition = m_spawnPositions[m_prefabIndex].position;

        GameObject ourNewObj = Instantiate(chosenPrefab, chosenSpawnPosition, chosenPrefab.transform.rotation, parent);
        ourNewObj.GetComponent<MoveLeft>().moveSpeed = m_environmentMovementSpeed;
        ourNewObj.GetComponent<Obstacle>().InformColour(GetRandomColor());

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
        return m_availableColors[m_colorIndex];
    }
}
