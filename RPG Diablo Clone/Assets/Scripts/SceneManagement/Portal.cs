using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        private Transform _spawnPoint;
        public int _desiredSceneIndex = 0;

        void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
            _spawnPoint = transform.GetChild(0);

            if (_spawnPoint)
            {
                print("Spawn Pt found in Portal");
            }
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                StartCoroutine(IELevelTransition());
            }
        }

        private IEnumerator IELevelTransition()
        {
            print("Portal hit");
            yield return SceneManager.LoadSceneAsync(_desiredSceneIndex);

            Portal otherPortal = GetOtherPortal();
            UpdatePlayer(otherPortal);

            print("Portal loaded scene");
            Destroy(this.gameObject);
        }

        private Portal GetOtherPortal()
        {
            Portal[] portals = GameObject.FindObjectsOfType<Portal>();
            foreach (Portal portal in portals)
            {
                if (portal != this)
                {
                    return portal;
                }
            }
            return null;
        }

        private void UpdatePlayer(Portal otherPortal)
        {
            if (otherPortal != null)
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");

                if (player != null)
                {
                    player.GetComponent<NavMeshAgent>().Warp(otherPortal._spawnPoint.position);
                    player.transform.rotation = otherPortal._spawnPoint.rotation;
                }
            }
        }
    }
}