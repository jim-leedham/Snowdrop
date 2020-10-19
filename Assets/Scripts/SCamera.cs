using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snowdrop
{ 
    public class SCamera : MonoBehaviour
    {
        [SerializeField] private GameObject fader = null;

        public void BeginFade()
        {
            fader.transform.position = Game.Instance.Player.transform.position + new Vector3(0.0f, 5.0f, 0.0f);
            Animator fadeAnimator = fader.GetComponent<Animator>();
            fadeAnimator.SetTrigger("fadeout");
        }

    }
}
