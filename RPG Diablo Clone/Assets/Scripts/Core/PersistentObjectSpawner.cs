using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class PersistentObjectSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _persistentObjPrefab;

        static bool _hasSpawned = false;

        void Awake()
        {
            if (_hasSpawned)
            {
                return;
            }
            else
            {
                SpawnPersistentObjects();
                _hasSpawned = true;
            }
        }

        void SpawnPersistentObjects()
        {
            GameObject persistentObj = Instantiate(_persistentObjPrefab);
            DontDestroyOnLoad(persistentObj);
        }
    }
}