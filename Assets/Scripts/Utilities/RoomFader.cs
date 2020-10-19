using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snowdrop
{
    public class RoomFader : Singleton<RoomFader>
    {
        public void OnFadeOutComplete()
        {
            Game.Instance.Player.EnterRoom();
            transform.position = Game.Instance.Player.transform.position + new Vector3(0.0f, 5.0f, 0.0f);
        }
    }
}