using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawner : Spawner
{
    [SerializeField]
    private Transform levelRoot;

    [SerializeField]
    private float startDelay = 0f;

    [SerializeField]
    private float timeBetweenSpawns;

    private Coroutine spawnCoroutine;

    private void Start()
    {
        spawnCoroutine = StartCoroutine(DelayRoutine());
    }

    private IEnumerator DelayRoutine()
    {
        yield return new WaitForSeconds(startDelay);

        spawnCoroutine = StartCoroutine(SpawningRoutine());
    }

    private IEnumerator SpawningRoutine()
    {
        //float[] xSpawnPosAr = new float[4] { 0f, 0, 0f, 1.92f};
        //float[] ySpawnPosAr = new float[4] { 0f, 0.64f, 2.56f, 2.56f };

        Spawn(levelRoot, m_spawnPositions);
        yield return new WaitForSeconds(timeBetweenSpawns);

        spawnCoroutine = StartCoroutine(SpawningRoutine());
    }
}
