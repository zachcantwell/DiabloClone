using System.Collections;
using System.Collections.Generic;
using RPG.Control;
using UnityEngine;

namespace RPG.Combat
{
    public class WeaponPickup : MonoBehaviour
    {
        [SerializeField] private float _respawnTime = 2f;
        public Weapon _weapon;
        private bool _wasAlreadyHidden = false;

        void OnTriggerEnter(Collider other)
        {
            Fighter fighter = other.gameObject.GetComponent<Fighter>();
            Debug.Log("Fighter = " + fighter);

            if (fighter && other.GetComponent<PlayerController>())
            {
                fighter.EquipWeapon(_weapon);
                StartCoroutine(IEHideForSeconds(_respawnTime));
            }
        }

        private void EnablePickup(bool isActivated)
        {
            GetComponent<Collider>().enabled = isActivated;

            Renderer[] children = transform.GetComponentsInChildren<Renderer>();

            if (children != null && !_wasAlreadyHidden)
            {
                foreach (Renderer gObj in children)
                {
                    gObj.enabled = isActivated;
                }
            }

            if (!_wasAlreadyHidden)
            {
                _wasAlreadyHidden = true;
            }
        }


        private IEnumerator IEHideForSeconds(float seconds)
        {
            EnablePickup(false);
            yield return new WaitForSeconds(seconds);
            EnablePickup(true);
        }
    }
}