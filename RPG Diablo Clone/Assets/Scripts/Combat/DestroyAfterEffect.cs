using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class DestroyAfterEffect : MonoBehaviour
    {
        void Update()
        {
            if (GetComponent<ParticleSystem>().IsAlive() == false)
            {
                Destroy(this.gameObject);
            }
        }
    }
}