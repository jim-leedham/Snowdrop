using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snowdrop
{
    [CreateAssetMenu(fileName = "Game Data", menuName = "Create Game Data", order = 1)]
    public class GameData : ScriptableObject
    {
        // name of the room the player starts in
        public string startingRoom = "";

        // name of the room the player was last in
        public string room = "";

        // pickups the player has
        public List<string> pickups = new List<string>();
    }
}