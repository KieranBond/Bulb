using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField]
    private string m_playerTag;

    private void OnTriggerEnter2D( Collider2D collision )
    {
        //When the player enters

        if(collision.tag == m_playerTag)
        {
            //Trigger game over

            //Destroy the GO.
            Destroy(collision.gameObject);
        }
    }
}
