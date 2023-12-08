using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 5; // Adjust this value in the inspector
    public Transform target; // Reference to the target object

    void OnMouseDrag()
    {
        // Calculate the movement amount based on the mouse input
        float movementAmount = Input.GetAxis("Mouse X") * speed * Time.deltaTime;

        // Move the cylinder
        transform.Translate(0, movementAmount, 0);

        // Move the target object by the same amount
        if (target != null)
        {
            target.Translate(0, -movementAmount, 0);
        }
    }
}
