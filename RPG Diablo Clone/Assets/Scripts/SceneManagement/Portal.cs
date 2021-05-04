using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using RPG.Core;
using RPG.Saving;
using RPG.SaveManagement;

namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        [System.Serializable]
        public enum DestinationPortal
        {
            A, B, C, D, E
        }
        private Transform _spawnPoint;

        [SerializeField] public DestinationPortal _desiredPortal;
        public int _desiredScene = 0;
        public float _waitTime = 0.5f;

        void Awake()
        {
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
            DontDestroyOnLoad(this.gameObject);

            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<ActionScheduler>().CancelCurrentAction();

            FadePanel fader = FindObjectOfType<FadePanel>();
            yield return fader.IEFadeIn();

            // Save Currrent LEvel Here
            SavingWrapper wrapper = FindObjectOfType<SavingWrapper>();
            wrapper.Save();

            yield return SceneManager.LoadSceneAsync(_desiredScene);
            wrapper.Load();

            // Load next LEvel Here
            Portal otherPortal = GetOtherPortal();
            UpdatePlayer(otherPortal);

            wrapper.Save();

            yield return new WaitForSeconds(_waitTime);
            yield return fader.IEFadeOut();

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
                    if (portal._desiredPortal == this._desiredPortal)
                    {
                        return portal;
                    }
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
                    player.GetComponent<NavMeshAgent>().enabled = false;
                    player.transform.position = otherPortal._spawnPoint.position;
                    player.transform.rotation = otherPortal._spawnPoint.rotation;
                    player.GetComponent<NavMeshAgent>().enabled = true;
                }
            }
        }
    }
}