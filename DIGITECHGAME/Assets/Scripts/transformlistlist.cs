using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    [System.Serializable]
    public class TRANSFORMS
    {
        public List<Transform> chunkpieces = new List<Transform>();
        public Vector3 chunkposition = new Vector3();
        public void AddList(Transform transform)
    {
        chunkpieces.Add(transform);
    }
    }
    [System.Serializable]
    public class TRANSFORMSLIST
    {
        public List<TRANSFORMS> chunklist = new List<TRANSFORMS>();
    public void AddList(TRANSFORMS transforms)
    {
        chunklist.Add(transforms);
    }

    }

