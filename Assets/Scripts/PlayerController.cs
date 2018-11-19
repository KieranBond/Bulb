using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerController : MonoBehaviour
{
    private SpriteRenderer m_spriteRender;

    public Color m_activeColor = Color.white;

    private int m_colorIndex = 0;

    [SerializeField]
    private string m_floorLayer;

    [SerializeField]
    private float m_movementSpeed = 10f;
    [SerializeField]
    private float m_jumpForce = 10f;

    private Rigidbody2D m_rb; 

    private bool m_isLanded = true;

    // Use this for initialization
    void Start ()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_spriteRender = GetComponent<SpriteRenderer>();

    }
	
	// Update is called once per frame
	void Update ()
    {
        //TODO : Add animations
        if(Input.GetKeyDown(KeyCode.Space) && m_isLanded)
        {
            m_isLanded = false;
            
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

    private void OnCollisionEnter2D( Collision2D collision )
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(m_floorLayer))
        {
            m_isLanded = true;
        }
    }
}
