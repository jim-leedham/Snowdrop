using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snowdrop
{
    [CreateAssetMenu(fileName = "Item Data", menuName = "Create Item Data", order = 2)]
    public class ItemData : ScriptableObject
    {
        public new string name = "";
        public int state = 0;
        public int defaultState = 0;

        void Reset()
        {
            state = defaultState;
        }
    }
}