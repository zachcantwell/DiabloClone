using System.Collections;
using System.Collections.Generic;
using RPG.Control;
using UnityEngine;

namespace RPG.Combat
{
    public class WeaponPickup : MonoBehaviour
    {
        public Weapon _weapon;
        void OnTriggerEnter(Collider other)
        {
            Fighter fighter = other.gameObject.GetComponent<Fighter>();
            Debug.Log("Fighter = " + fighter);

            if (fighter && other.GetComponent<PlayerController>())
            {
                fighter.EquipWeapon(_weapon);
                Destroy(this.gameObject);
            }
        }
    }
}