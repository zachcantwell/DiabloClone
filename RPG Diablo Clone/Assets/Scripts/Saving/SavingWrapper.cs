using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;

namespace RPG.SaveManagement
{
    public class SavingWrapper : MonoBehaviour
    {
        const string _defaultSaveFile = "save";

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                Load();
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                Save();
            }
        }

        public void Load()
        {
            // Call the Saving System and tell it to load
            GetComponent<SavingSystem>().Load(_defaultSaveFile);
        }

        public void Save()
        {
            // Call the Saving System and tell it to load
            GetComponent<SavingSystem>().Save(_defaultSaveFile);
        }
    }
}