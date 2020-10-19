using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snowdrop
{
    public class PlayerAnimController : MonoBehaviour
    {
        public void OnFeetLeaveGround()
        {
            Game.Instance.Player.Jump();
        }
    }
}