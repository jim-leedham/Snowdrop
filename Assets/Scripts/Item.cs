using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snowdrop
{
    public class Item : Interactable
    {
        [SerializeField] private string pickUpText = "";

        public void PickUp()
        {
            if (touched)
            {
                Speech.Instance.Emit(pickUpText);
                itemData.state = 1;
                Destroy(gameObject);
            }
        }
    }
}