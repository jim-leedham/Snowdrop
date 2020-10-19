using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snowdrop
{
    public class PerspectiveSimulator : MonoBehaviour
    {
        [SerializeField] private Transform cam;
        [SerializeField] private Vector3 cameraRelative;
        //[SerializeField] private float perspectiveScale = 5.0f; // hard-coded for now; TODO: make relative to camera
        [SerializeField] private float maxXMovement = 0;
        [SerializeField] private bool front = false;
        [SerializeField] private float deadzone = 0.3f; // this should really be the WIDTH of our sprite, i.e. our extents in x axis
        [SerializeField] private float threshold = 0;

        void Update()
        {
            if (Game.Instance.PerspectiveSimulation)
            {
                cam = UnityEngine.Camera.main.transform;
                cameraRelative = cam.InverseTransformPoint(transform.position);

                if (Mathf.Abs(cameraRelative.x) > threshold)
                {
                    return;
                }

                if (cameraRelative.x > deadzone)
                {
                    cameraRelative.x -= deadzone;
                }
                else if (cameraRelative.x < -deadzone)
                {
                    cameraRelative.x += deadzone;
                }
                else
                {
                    cameraRelative.x = 0.0f;
                }


                float factor = Mathf.Lerp(0.0f, maxXMovement, Mathf.Abs(cameraRelative.x / threshold));
                if (cameraRelative.x < 0.0f)
                {
                    factor *= -1.0f;
                }

                factor *= (front ? 1.0f : -1.0f);

                Vector3 pos = transform.localPosition;
                pos.x = factor;
                transform.localPosition = pos;
            }
        }
    }
}