using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageCollect : MonoBehaviour
{
    [SerializeField]
    private string[] m_garbageTags;

    [SerializeField]
    private bool m_logging = false;

    public void OnTriggerExit2D( Collider2D other )
    {
        if (m_logging)
            Debug.Log("Object left: " + other.gameObject.name);

        foreach (string current in m_garbageTags)
        {
            if (other.gameObject.tag == current)
            {
                Destroy(other.gameObject);
            }
        }
    }
}
