using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private Vector2 direction = Vector2.right;

    private List<Transform> segments;

    private void Start()
    {
        segments = new List<Transform>();
        segments.Add(this.transform);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            direction = Vector2.up;
            Debug.Log("Up");
        }
        else if(Input.GetKeyDown(KeyCode.S))
        {
            direction = Vector2.down;
            Debug.Log("Down");
        }
        else if(Input.GetKeyDown(KeyCode.A))
        {
            direction = Vector2.left;
            Debug.Log("Left");
        }
        else if(Input.GetKeyDown(KeyCode.D))
        {
            direction = Vector2.right;
            Debug.Log("Right");
        }
    }

    private void FixedUpdate()
    {
        this.transform.position = new Vector3(
                Mathf.Round(this.transform.position.x) + direction.x,
                Mathf.Round(this.transform.position.y) + direction.y,
                0.0f
            );
    }
}
