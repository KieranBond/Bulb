using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activator : MonoBehaviour
{
    [SerializeField]
    private GameObject[] m_objsToActivate;

    private void Start()
    {
    }

    public void Activate()
    {
        foreach (GameObject obj in m_objsToActivate)
        {
            Debug.Log("Activating");
            obj.SetActive(true);
        }
    }
}
