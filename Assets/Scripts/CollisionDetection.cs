using UnityEngine;

namespace PhysicsLab
{
    public class CollisionDetection : MonoBehaviour
    {
        public Canvas warningCanvas;

        void OnCollisionEnter(Collision collision)
        {
            // Debugging
            // Print the name of the collided GameObject for debugging
            Debug.Log("Collided with: " + collision.gameObject.name);

            if (collision.gameObject.name == "HollowCylinder" || collision.gameObject.name == "Vascular")
            {
                warningCanvas.gameObject.SetActive(true);

                // Get the collision point
                Vector3 collisionPoint = collision.contacts[0].point;
                Debug.Log("Collision point: " + collisionPoint);
            }
        }

        void OnCollisionExit(Collision collision)
        {
            warningCanvas.gameObject.SetActive(false);
        }

    }
}

