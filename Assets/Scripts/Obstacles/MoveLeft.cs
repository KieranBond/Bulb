using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    public float moveSpeed = 1f;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 newPos = transform.position;

        newPos.x = newPos.x - (moveSpeed * Time.deltaTime);
        transform.position = newPos;
	}
}
