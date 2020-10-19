using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snowdrop
{
    public class Debugger : MonoBehaviour
    {
        public bool resetScriptableObjects;

        public bool instantiatePrefabs;

        [SerializeField] private List<ItemData> items = new List<ItemData>();

        void Start()
        {
            if (resetScriptableObjects)
            {
                foreach (ItemData item in items)
                {
                    item.state = 0;
                }
            }
        }
    }
}