using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField]
    private Transform m_spawnParent;

    [SerializeField]
    private Transform m_spawnPosition;

    [SerializeField]
    protected GameObject[] m_prefabsToSpawn;

    [SerializeField]
    private float m_environmentMovementSpeed;

    [SerializeField]
    private float m_timeBetweenObstacles = 2f;

    [SerializeField]
    private Material[] m_changeableMaterials;

    private Color[] m_materialColors;

    private int m_colorIndex = 0;

    private int m_prefabIndex = 0;

    private Material m_setMaterial;

    private Coroutine m_spawnSessionRoutine;

    private void Start()
    {
        m_setMaterial = new Material(m_changeableMaterials[0]);
        m_materialColors = new Color[m_changeableMaterials.Length];

        int n = 0;
        foreach (Material current in m_changeableMaterials)
        {
            m_materialColors[n] = current.color;
        }

        if (m_prefabsToSpawn.Length > 0)
            BeginSpawnSession();
    }

    private void Update()
    {
        //Colour shifter
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            //Get the next colour in the list, and set that as our material colour
            Color nextColor = GetNextColor();
            m_changeableMaterials[m_colorIndex].color = nextColor;

            IncrementColorIndex();
        }
    }

    private void OnGUI()
    {
        if(GUI.Button(new Rect(0, 0, 100, 100), "Spawn"))
        {
            Spawn();
        }
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

    private Color GetNextColor()
    {
        int nextindex = 0;
        if (m_materialColors[m_colorIndex + 1] != null)
        {
            nextindex = m_colorIndex + 1;
        }

        return m_materialColors[m_colorIndex];
    }

    private void IncrementColorIndex()
    {
        m_colorIndex++;

        if (m_colorIndex >= m_materialColors.Length)
            m_colorIndex = 0;
    }
}
