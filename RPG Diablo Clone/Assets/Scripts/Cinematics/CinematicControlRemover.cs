using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using RPG.Core;
using RPG.Control;

namespace RPG.Cinematics
{
    public class CinematicControlRemover : MonoBehaviour
    {
        void Start()
        {
            GetComponent<PlayableDirector>().stopped += EnableControl;
            GetComponent<PlayableDirector>().played += DisableControl;
        }

        public void EnableControl(PlayableDirector pd)
        {
            print("Enabled");
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            if (player)
            {
                player.GetComponent<ActionScheduler>().CancelCurrentAction();
                player.GetComponent<PlayerController>().enabled = true;
            }
        }

        public void DisableControl(PlayableDirector pd)
        {
            print("Disabled");
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            if (player)
            {
                player.GetComponent<PlayerController>().enabled = false;
            }
        }
    }
}