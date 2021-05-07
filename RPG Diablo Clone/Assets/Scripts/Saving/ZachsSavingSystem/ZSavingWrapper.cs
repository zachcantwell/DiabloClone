using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Saving
{
    public class ZSavingWrapper : MonoBehaviour
    {
        const string _defaultSaveFile = "save";

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                GetComponent<ZSavingSystem>().Save(_defaultSaveFile);
            }
            else if (Input.GetKeyDown(KeyCode.L))
            {
                GetComponent<ZSavingSystem>().Load(_defaultSaveFile);
            }
        }
    }
}
