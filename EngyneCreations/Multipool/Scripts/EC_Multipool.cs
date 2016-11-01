/*
 * EC_Multipool.cs
 * Manage the pooling of any object and calls to retrieve them.
 * 
 * by Adam Carballo under GPLv3 license.
 * https://github.com/AdamEC/Unity-MultipoolObjectPooling
 */

using UnityEngine;
using System.Collections.Generic;

public class EC_Multipool : MonoBehaviour {

    [System.Serializable]
    public class Pool {
        [Tooltip("Name of this pool as will appear on the dropdown of the emitters.")]
        public string name;
        [Tooltip("Object used by this pool.")]
        public GameObject poolObject;
        [Tooltip("How many objects will get pre-spawned before the scene starts (Pool size).")]
        public int startAmount;
        [Tooltip("Can this pool grow if the requested items are higher than the start amount.")]
        public bool canGrow = true;
        [Tooltip("Will this pool spawn objects inside a custom parent, or use the default one.")]
        public Transform customParent;
        [HideInInspector]
        public List<GameObject> poolList = new List<GameObject>();
    }

    public Pool[] pool;

    static public EC_Multipool instance;
    static public List<string> generatedEnum = new List<string>();

    /// <summary>
    /// Define Reference
    /// </summary>
    void Awake() {
        instance = this;

        Object[] otherPool= FindObjectsOfType(this.GetType());
        if (otherPool.Length > 1) {
            Debug.LogError("Multipool can only exist once in the scene.");
        }
    }

    /// <summary>
    /// Instantiate the startAmount of poolObject and deactivate them.
    /// </summary>
    void Start () {

        for (int i = 0; i < pool.Length; i++) {
            for (int p = 0; p < pool[i].startAmount; p++) {
                GameObject obj = Instantiate(pool[i].poolObject);
                obj.SetActive(false);

                if (pool[i].customParent) {
                    obj.transform.SetParent(pool[i].customParent, true);
                }

                pool[i].poolList.Add(obj);
            }
        }
	}

    /// <summary>
    /// EDITOR function. Generates a list of all the object pools names to be shown on an enum.
    /// </summary>
#if UNITY_EDITOR
    public void GenerateEnum() {

        generatedEnum = new List<string>();

        if (pool.Length == 0) return;

        for (int i = 0; i < pool.Length;i++) {
            generatedEnum.Add(pool[i].name);
        }
    }
#endif

    /// <summary>
    /// Returns an availabe object. If there isn't one, and canGrow, will create a new one.
    /// </summary>
    /// <param name="index">Index of the pool.</param>
    /// <returns></returns>
    public GameObject GetPooledObject(int index) {

        for (int i = 0; i < pool[index].poolList.Count;i++) {
            if (!pool[index].poolList[i].activeInHierarchy) return pool[index].poolList[i];
        }

        if (pool[index].canGrow) {
            GameObject obj = Instantiate(pool[index].poolObject);

            if (pool[index].customParent) {
                obj.transform.SetParent(pool[index].customParent, true);
            }

            pool[index].poolList.Add(obj);
            return obj;
        }
        return null;
    }
}