using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Saving
{
    public class ZSaveableEntity : MonoBehaviour
    {
        public string GetUniqueIdentifier()
        {
            return "";
        }

        public object CaptureState()
        {
            return null;
        }

        public void RestoreState(object state)
        {
            Dictionary<string, object> stateDict = (Dictionary<string, object>)state;
        }
    }
}