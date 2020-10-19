using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snowdrop
{
    public class Room : MonoBehaviour
    {
        [SerializeField] private new string name = "";
        [SerializeField] private GameObject door = null;

        private BoxCollider2D extents;

        private void Awake()
        {
            extents = GetComponent<BoxCollider2D>();
        }

        public Vector3 GetDoorPos()
        {
            return door.transform.position;
        }

        public string GetName()
        {
            return name;
        }

        public BoxCollider2D GetExtents()
        {
            return extents;
        }

    }
}