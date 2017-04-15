/*
 * Multipool.cs
 * Manage the pooling of any object and calls to retrieve them.
 * 
 * by Adam Carballo under MIT license.
 * https://github.com/AdamEC/Unity-MultiPool
 */

using UnityEngine;
using System.Collections.Generic;

namespace Multipool {
    [DefaultExecutionOrder(-100)]
    public class Multipool : MonoBehaviour {

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
            [Tooltip("Limit of pool grow. 0 = unlimited. Can be edited at runtime.")]
            public int maxGrow = 0;
            [Tooltip("Will this pool spawn objects inside a custom parent, or use the default one.")]
            public Transform customParent;
            [HideInInspector]
            public List<GameObject> poolList = new List<GameObject>();
        }

        #region Class Variables
        static public Multipool instance;
        static public List<string> generatedEnum = new List<string>();

        [SerializeField] private Pool[] _pool;
        #endregion

        #region Unity Methods
        /// <summary>
        /// Define instance and check for duplicated pools.
        /// </summary>
        private void Awake() {

            instance = this;

            Object[] otherPool = FindObjectsOfType(this.GetType());
            if (otherPool.Length > 1) {
                Debug.LogError(
                    "<color=magenta>[MultiPool]</color> MultiPool can only exist once in the scene!"
                );
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPaused = true;
#endif
            }
        }

        /// <summary>
        /// Instantiate the startAmount of poolObject and deactivate them.
        /// </summary>
        private void Start() {

            for (int i = 0; i < _pool.Length; i++) {
                for (int p = 0; p < _pool[i].startAmount; p++) {
                    GenerateObjectInPool(i);
                }
            }
        }
        #endregion

        #region Editor Methods
#if UNITY_EDITOR
        /// <summary>
        /// Editor function. Generates a list of all the object pools names to be shown on an enum.
        /// </summary>
        public void GenerateEnum() {

            generatedEnum = new List<string>();

            if (_pool.Length == 0) return;

            for (int i = 0; i < _pool.Length; i++) {
                generatedEnum.Add(_pool[i].name);
            }
        }

        /// <summary>
        /// Editor function. Returns current pool information,
        /// including active objects and objects created during runtime.
        /// </summary>
        /// <returns>Formated string.</returns>
        public string GetRuntimeInformation() {

            string result = "";
            int activeObj = 0;

            for (int i = 0; i < _pool.Length; i++) {
                for (int p = 0; p < _pool[i].poolList.Count; p++) {
                    if (_pool[i].poolList[p].activeInHierarchy) {
                        activeObj++;
                    }
                }
                result += string.Format("<size=14><color={0}><b>{1}</b></color>  " + 
                    "(<color=cyan>{2}</color>/<color=black>{3}</color>) + [<color=teal>{4}</color>]</size>\n",
                    GetEditorColor(i, activeObj),
                    _pool[i].name,
                    activeObj,
                    _pool[i].poolList.Count,
                    _pool[i].poolList.Count - _pool[i].startAmount
                );
                activeObj = 0;
            }
            return result;
        }

        /// <summary>
        /// Editor function. Calculate the pool name color on the editor based on the current usage.
        /// </summary>
        /// <param name="index">Index of the pool.</param>
        /// <param name="activeObj">Objects active from the pool.</param>
        /// <returns>String with color name</returns>
        private string GetEditorColor(int index, int activeObj) {

            float percentage = (float)activeObj / _pool[index].poolList.Count;

            if (percentage < 0.75f) {
                return "green";
            } else if (percentage < 0.95f) {
                return "orange";
            } else {
                if (_pool[index].canGrow) {
                    return "maroon";
                } else {
                    return "red";
                }
            }
        }
#endif
        #endregion

        #region Public Methods
        /// <summary>
        /// Returns an availabe object. If there isn't one, and canGrow, will create a new one.
        /// </summary>
        /// <param name="index">Index of the pool.</param>
        /// <returns></returns>
        public GameObject GetPooledObject(int index) {

            for (int i = 0; i < _pool[index].poolList.Count; i++) {
                if (!_pool[index].poolList[i].activeInHierarchy) return _pool[index].poolList[i];
            }

            if (_pool[index].canGrow) {
                if (_pool[index].poolList.Count < (_pool[index].maxGrow + _pool[index].startAmount) ||
                    _pool[index].maxGrow == 0) {
                    return GenerateObjectInPool(index);
                }
            }
            return null;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Instantiate an object inside a pool to be used later.
        /// </summary>
        /// <param name="index">Index of the pool.</param>
        /// <returns>Instantiated object</returns>
        private GameObject GenerateObjectInPool(int index) {

            GameObject obj = Instantiate(_pool[index].poolObject);
            obj.SetActive(false);

            if (_pool[index].customParent) {
                obj.transform.SetParent(_pool[index].customParent, true);
            }

            _pool[index].poolList.Add(obj);
            return obj;
        }
        #endregion
    }
}