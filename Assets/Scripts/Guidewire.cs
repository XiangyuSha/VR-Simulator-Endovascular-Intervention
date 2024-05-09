using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhysicsLab
{
	public class Guidewire : MonoBehaviour {
		public Transform ParticlePrefab;
		public int Count = 3;
        [Range(0.1f, 10f)]
        public float Space = 1;
		[Range(0, 1)]
		public float Damping = 0.1f;
        public Vector3 ExternForce = Vector3.zero;

        //public RopeMeshCollider meshCollider;
        public static List<Transform> chain = new List<Transform>();
		public static List<Particle> particleList = new List<Particle>();

        [Header("Others")]
        // LineRenderer
        public GameObject lineRendererPrefab; 
        private List<LineRenderer> lineRenderers = new List<LineRenderer>(); // Store LineRenderers
        public Material solidLineMaterial; 
        public Canvas warningCanvas;

        void Start()
		{
			for (int i=0; i<Count; i++)
			{
				var obj = Instantiate(ParticlePrefab, transform, true);
                obj.localScale = new Vector3(0.35f, 0.35f, 0.35f); // 
                obj.Translate(0, -i * Space, 0);

                // Add rigidbody
                var rb = obj.gameObject.AddComponent<Rigidbody>();
                rb.isKinematic = false; 
                rb.useGravity = false;

                // Add sphere colliders
                var sphereColliderComponent = obj.gameObject.AddComponent<SphereCollider>();
                sphereColliderComponent.radius = 0.5f * Space; // Set the collider radius

                // Add script
                var collisionDetection = obj.gameObject.AddComponent<CollisionDetection>();
                collisionDetection.warningCanvas = warningCanvas;

                chain.Add(obj);

				// Construct Particles
				var particle = new Particle();
                particle.invMass = i == 0 ? 0 : 1; // 0 means Mass is infinite
                particle.radius = 0.5f * Space;
				particle.pos = particle.prevPos = new Vector3(i * Space, 0, 0);
				particle.velocity = Vector3.zero;
				particleList.Add(particle);
			}

            // Instantiate LineRenderers dynamically
            for (int i = 0; i < Count - 1; i++)
            {
                var lr = Instantiate(lineRendererPrefab, transform).GetComponent<LineRenderer>();
                lr.widthCurve = AnimationCurve.Constant(0, 1, 0.08f); // Set the thickness
                
                lr.material = solidLineMaterial; // Assign the solid material
                lineRenderers.Add(lr);
            }

        }

		void FixedUpdate ()
		{
            float dt = Time.fixedDeltaTime;

            // Update Velocity
            for (int i = 1; i < Count; i++)
            {
                var particle = particleList[i];

                // Time Integration
                Vector3 vel = particle.velocity + ExternForce * dt;
                particle.prevPos = particle.pos;
                particle.pos = particle.pos + (1 - Damping) * vel * dt;
            }

            // Resolve Constraint. Keep Length Constraint from top to bottom
           
            // Distance Constraint
            for (int i = 1; i < Count; i++)
            {
                Vector3 offsetToParent = particleList[i].pos - particleList[i - 1].pos;
                // Strategy 2: Position Based Dynamics, iteratively
                offsetToParent = Space * offsetToParent.normalized - offsetToParent;
                float invMassSum = particleList[i - 1].invMass + particleList[i].invMass;
                particleList[i - 1].pos -=  particleList[i -1].invMass / invMassSum * offsetToParent;
                particleList[i].pos += particleList[i].invMass / invMassSum * offsetToParent;
            }

            // Bend Constraint
            for (int i = 1; i < Count - 1; i++)
            {
                Particle lastP = particleList[i - 1];
                Particle cP = particleList[i];
                Particle nextP = particleList[i + 1];
            }

            particleList[0].pos = transform.position;

            // Update velocity
            particleList[0].velocity = Vector3.zero;
            for (int i = 1; i < Count; i++)
            {
                particleList[i].velocity = (particleList[i].pos - particleList[i].prevPos) / dt;
            }

            for (int i = 0; i < Count; i++)
            {
                chain[i].position = particleList[i].pos;
            }

            // Update LineRenderers positions
            for (int i = 0; i < lineRenderers.Count; i++)
            {
                lineRenderers[i].SetPosition(0, particleList[i].pos);
                lineRenderers[i].SetPosition(1, particleList[i + 1].pos);
            }
        }
    }

    public class Particle
	{
        public float invMass;
		public float radius;
		public Vector3 pos;
		public Vector3 prevPos;
		public Vector3 velocity;
	
    }
}
