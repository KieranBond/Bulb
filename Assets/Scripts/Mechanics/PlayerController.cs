﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(ColourFlipper))]
public class PlayerController : MonoBehaviour
{
    private SpriteRenderer m_spriteRender;
    private Rigidbody2D m_rb;
    private ColourFlipper m_colorFlipper;

    public Color m_activeColor = Color.white;

    private int m_colorIndex = 0;

    [SerializeField]
    private string m_floorLayer;

    [SerializeField]
    private float m_mouseYMoveForJump = 2f;

    [SerializeField]
    private float m_movementSpeed = 10f;
    [SerializeField]
    private float m_jumpForce = 10f;


    private float m_mouseYFirstPress = 0f;
    private float m_mouseYPressEnd = 0f;
    private float m_mouseYMovement = 0f;

    private bool m_isLanded = true;

    // Use this for initialization
    void Start ()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_spriteRender = GetComponent<SpriteRenderer>();
        m_colorFlipper = GetComponent<ColourFlipper>();
    }

    // Update is called once per frame
    void Update ()
    {
        //TODO : Add animations

        //Jump and movement controls

        //Used to check colour flip rather than compare Y move again.
        bool jumped = false;

        
        if(Input.touches.Length > 0)
        {
            //Player is pushing down.
            //Let's add to the movement

            if (Input.touches.Length > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    m_mouseYFirstPress = touch.position.y;
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    m_mouseYPressEnd = touch.position.y;

                    m_mouseYMovement = m_mouseYPressEnd - m_mouseYFirstPress;
                }
            }
        }

        if (m_isLanded && (Input.GetKeyDown(KeyCode.Space) || (m_mouseYMovement >= m_mouseYMoveForJump && Input.GetMouseButtonUp(0))) )
        {
            m_mouseYMovement = 0;

            jumped = true;

            m_isLanded = false;
            
            //Jump!
            m_rb.AddForce(Vector2.up * m_jumpForce);
        }

        //Colour flipping controls
        if (Input.GetKeyDown(KeyCode.LeftControl) || (Input.GetMouseButtonUp(0) && !jumped))
        {
            m_mouseYMovement = 0;

            m_colorFlipper.UpdateColor();
        }

        //Movement for testing.
#if UNITY_EDITOR
        if (Input.GetKey(KeyCode.D))
        {
            m_rb.AddForce(Vector2.right * m_movementSpeed);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            m_rb.AddForce(Vector2.left * m_movementSpeed);
        }
#endif

    }

    private void OnCollisionEnter2D( Collision2D collision )
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(m_floorLayer))
        {
            m_isLanded = true;
        }
    }
}