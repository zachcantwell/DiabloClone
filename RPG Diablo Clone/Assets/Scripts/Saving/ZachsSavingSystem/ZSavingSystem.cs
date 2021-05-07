using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System;
using System.Runtime.Serialization.Formatters.Binary;

namespace RPG.Saving
{
    public class ZSavingSystem : MonoBehaviour
    {
        public void Save(string saveFile)
        {
            string path = GetPathFromSaveFile(saveFile);
            print("Saving to " + path);
            using (FileStream stream = File.Open(path, FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, CaptureState());
            }
        }

        public void Load(string saveFile)
        {
            string path = GetPathFromSaveFile(saveFile);
            print("loading from " + path);
            using (FileStream stream = File.Open(path, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                RestoreState(formatter.Deserialize(stream));
            }
        }

        private object CaptureState()
        {
            Dictionary<string, object> state = new Dictionary<string, object>();
            foreach (ZSaveableEntity entity in FindObjectsOfType<ZSaveableEntity>())
            {
                state[entity.GetUniqueIdentifier()] = entity.CaptureState();
            }
            return state;
        }

        private void RestoreState(object state)
        {
            Dictionary<string, object> stateDictionary = new Dictionary<string, object>();
            foreach (ZSaveableEntity entity in FindObjectsOfType<ZSaveableEntity>())
            {
                entity.RestoreState(entity.GetUniqueIdentifier());
            }
        }

        private string GetPathFromSaveFile(string saveFile)
        {
            return Path.Combine(Application.persistentDataPath + saveFile);
        }

    }
}