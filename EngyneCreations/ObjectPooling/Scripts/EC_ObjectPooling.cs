/*
 * EC_ObjectPooling.cs
 * Manage the pooling of any object and calls to retrieve them.
 * 
 * by Adam Carballo under GPLv3 license.
 * https://github.com/engyne09/UnityObjectPooling
 */

using UnityEngine;
using System.Collections.Generic;

public class EC_ObjectPooling : MonoBehaviour {

    [System.Serializable]
    public class Pool {
        public string name;
        public GameObject poolObject;
        public int startAmount;
        public bool canGrow = true;
        public Transform customParent;
        [HideInInspector]
        public List<GameObject> poolList = new List<GameObject>();
    }

    public Pool[] pool;

    static public EC_ObjectPooling instance;
    static public List<string> generatedEnum = new List<string>();


    // Define Reference
    void Awake() {
        instance = this;
    }

    // Instantiate the startAmount of poolObject and deactivate them.
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

    // EDITOR function. Generates a list of all the object pools names to be shown on an enum.
#if UNITY_EDITOR
    public void GenerateEnum() {

        generatedEnum = new List<string>();

        if (pool.Length == 0) {
            return;
        }

        for (int i = 0; i < pool.Length;i++) {
            generatedEnum.Add(pool[i].name);
        }
    }
#endif

    // Returns an availabe object. If there isn't one, and canGrow, will create a new one.
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