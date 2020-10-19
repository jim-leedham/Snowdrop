using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snowdrop
{
    public enum InteractableType { None, Door, PickUp }

    public class Interactable : MonoBehaviour
    {
        [SerializeField] protected ItemData itemData;
        [SerializeField] protected string lookAtText;
        [SerializeField] protected InteractableType type;

        protected bool touched;

        void Start()
        {
            if (itemData != null && itemData.state > 0)
            {
                this.gameObject.SetActive(false);
            }
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Hand"))
            {
                touched = true;
            }
        }

        void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Hand"))
            {
                touched = false;
            }
        }

        internal bool Touching()
        {
            return touched;
        }

        public InteractableType GetInteractableType()
        {
            return type;
        }

        public string Description()
        {
            return lookAtText;
        }

        public void LookAt()
        {
            Speech.Instance.Emit(lookAtText);
        }
    }
}