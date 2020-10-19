using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snowdrop
{
    public class Door : Interactable
    {
        [SerializeField]
        private string destination = "";

        public string GetDestination()
        {
            return destination;
        }

        public void Open()
        {
            if (touched)
            {
                Game.Instance.TransitionToRoom(destination);
            }
        }
    }
}