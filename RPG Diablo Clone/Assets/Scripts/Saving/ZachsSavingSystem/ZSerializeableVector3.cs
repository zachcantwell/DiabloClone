using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Saving
{
    [System.Serializable]
    public class ZSerializeableVector3
    {
        float x, y, z;

        public ZSerializeableVector3(Vector3 vector)
        {
            x = vector.x;
            y = vector.y;
            z = vector.z;
        }

        public Vector3 ToVector()
        {
            return new Vector3(x, y, z);
        }
    }
}