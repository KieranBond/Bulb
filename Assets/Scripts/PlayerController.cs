using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D m_rb;

    [SerializeField]
    private float m_movementSpeed = 10f;
    [SerializeField]
    private float m_jumpForce = 10f;

    // Use this for initialization
    void Start ()
    {
        m_rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            //Jump!
            m_rb.AddForce(Vector2.up * m_jumpForce);
        }

        if(Input.GetKey(KeyCode.D))
        {
            m_rb.AddForce(Vector2.right * m_movementSpeed);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            m_rb.AddForce(Vector2.left * m_movementSpeed);
        }
    }
}
