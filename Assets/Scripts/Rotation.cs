using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PhysicsLab;

public class Rotation : MonoBehaviour
{
    public float rotationSpeed = 700; // Speed of rotation
    public Transform target; // Target object to rotate along with the rope
    public float positionChangeSpeed = 0.1f; // Speed of position change for the first particle

    void OnMouseDrag()
    {
        // Get the rotation amount based on mouse input and speed
        float rotationAmount = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

        // Move the cylinder
        transform.Rotate(0, rotationAmount, 0); 

        // Move the guidewire remotely and change the position of particles
        target.Rotate(Vector3.right, rotationAmount); 
        Vector3 newPosition = Guidewire.particleList[0].pos + new Vector3(0, positionChangeSpeed * rotationAmount, 0);
        Guidewire.particleList[0].pos = newPosition;

        // Apply Particle Data to Transform
        for (int i = 0; i < Guidewire.particleList.Count; i++)
        {
            Guidewire.chain[i].position = Guidewire.particleList[i].pos;
        }
    }
}