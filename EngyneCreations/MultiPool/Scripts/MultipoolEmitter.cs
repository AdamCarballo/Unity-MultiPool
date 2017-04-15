/*
 * MultipoolEmitter.cs
 * Emitter Configurator. Extract objects with MultipoolEmmitter.Generate();
 * 
 * This script is required to extract the generated enum from the Multipool array.
 * 
 * by Adam Carballo under MIT license.
 * https://github.com/AdamEC/Unity-MultiPool
 */

using UnityEngine;
using System.Collections.Generic;

namespace Multipool {
    public class MultipoolEmitter : MonoBehaviour {

        #region Class Variables
        [HideInInspector]
        public int index;
        [HideInInspector]
        public List<string> copyGeneratedEnum = new List<string>();
        #endregion

        #region Public Methods
        /// <summary>
        /// Retrive from the index object pool, check if null, and return the object already activated to the caller.
        /// </summary>
        /// <returns>Pool object or null.</returns>
        public GameObject Generate() {

            GameObject obj = MultipoolManager.instance.GetPooledObject(index);

            if (!obj) return null;

            obj.SetActive(true);
            return obj;
        }
        #endregion
    }
}