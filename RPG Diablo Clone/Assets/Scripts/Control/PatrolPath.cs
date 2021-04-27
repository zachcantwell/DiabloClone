using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class PatrolPath : MonoBehaviour
    {
        [SerializeField]
        private Transform[] _pathPoints;

        private void Awake() {
            _pathPoints = transform.GetComponentsInChildren<Transform>();    
        }

        private void OnDrawGizmosSelected() {
            
            _pathPoints = transform.GetComponentsInChildren<Transform>();    
            Gizmos.color = Color.magenta;

            for(int i = 1; i < _pathPoints.Length - 1; i++)
            {
                Gizmos.DrawLine(_pathPoints[i].position, _pathPoints[i+1].position);
            }
           
        }
    }
}

