using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Saving
{
    [ExecuteAlways]
    public class ZSaveableEntity : MonoBehaviour
    {
        [SerializeField] string _uniqueIdentifier = "";


        public string GetUniqueIdentifier()
        {
            return _uniqueIdentifier;
        }

        public object CaptureState()
        {
            return new SerializableVector3(transform.position);
        }

        public void RestoreState(object state)
        {
            SerializableVector3 pos = (SerializableVector3)state;
            GetComponent<NavMeshAgent>().enabled = false;
            transform.position = pos.ToVector();
            GetComponent<NavMeshAgent>().enabled = true;
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

#if UNITY_EDITOR
        void Update()
        {
            if (string.IsNullOrEmpty(gameObject.scene.path))
            {
                return;
            }

            if (Application.IsPlaying(gameObject) == true)
            {
                return;
            }

            SerializedObject obj = new SerializedObject(this);
            SerializedProperty property = obj.FindProperty("_uniqueIdentifier");

            if (property.stringValue == "")
            {
                property.stringValue = System.Guid.NewGuid().ToString();
                obj.ApplyModifiedProperties();
            }
            print("Editing");
        }
    }
#endif
}