using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snowdrop
{
    public class Parallax : MonoBehaviour
    {
        public float posScale;

        public float moveVelocity;

        private Rigidbody2D rigidbody2d;
        private Vector3 previousCameraPosition;

        void Start()
        {
            rigidbody2d = GetComponent<Rigidbody2D>();
            previousCameraPosition = UnityEngine.Camera.main.transform.position;
        }

        void Update()
        {
            if (Game.Instance.ParallaxSimulation && System.Math.Abs(previousCameraPosition.x - UnityEngine.Camera.main.transform.position.x) > float.Epsilon)
            {
                Vector2 movement = new Vector2(UnityEngine.Input.GetAxis("Horizontal") * -moveVelocity, 0.0f);
                rigidbody2d.velocity = movement;
                previousCameraPosition = UnityEngine.Camera.main.transform.position;
            }
            else
            {
                rigidbody2d.velocity = Vector3.zero;
            }
        }
    }
}