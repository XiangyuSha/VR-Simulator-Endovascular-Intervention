using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    public float speed = 700;
    public Transform target;

    void OnMouseDrag()
    {
        /*Vector3 Rotation = new Vector3(Input.GetAxis("Mouse Y"), 0, 0);
        transform.Rotate(Rotation * Time.deltaTime * speed);*/

        float rotationAmount = Input.GetAxis("Mouse Y") * speed * Time.deltaTime;
        transform.Rotate(0, rotationAmount, 0);

        // Apply the same rotation to the target object
        if (target != null)
        {
            target.Rotate(0, rotationAmount, 0);
        }
    }
}
