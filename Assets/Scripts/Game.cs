using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snowdrop
{
    // TODO:
    // - speech sounds?
    // - ticking clock?
    // - radio blaring?

    //-integrate yarn spinner
    //-inventory system?


    public class Game : Singleton<Game>
    {
        [SerializeField] public bool PerspectiveSimulation = true;
        [SerializeField] public bool ParallaxSimulation = true;

        [SerializeField] private GameData gameData = null;

        [SerializeField] private List<Room> rooms = new List<Room>();
        [SerializeField] private Player player = null;
        [SerializeField] private new SCamera camera = null;

        public Player Player { get; private set; }
        public SCamera Camera { get; private set; }
        public Room Room { get; private set; }

        protected override void Awake()
        {
            Application.targetFrameRate = 60;

            Player = player;
            Camera = camera;
            Room = rooms.Find(room => room.GetName() == gameData.startingRoom);

            base.Awake();
        }

        private void Start()
        {
            Player.EnterRoom();
        }

        public void TransitionToRoom(string name)
        {
            Room = rooms.Find(room => room.GetName() == name);
            gameData.room = Room.GetName();
            Camera.BeginFade();
        }
    }
}