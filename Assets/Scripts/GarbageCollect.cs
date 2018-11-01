using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageCollect : MonoBehaviour
{
    [SerializeField]
    private string garbageTag = "Environment";

    private void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.tag == garbageTag)
        {
            Destroy(collider.gameObject);
        }
    }
}
