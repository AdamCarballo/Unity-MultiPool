/*
 * MultipoolEmitter.cs
 * Emitter Configurator. Extract objects with MultipoolEmitter.Generate();
 *
 * This emitter has a helper class that supports GameObjects by default, and works best with a MultipoolReset attached to the pooled objects.
 * This script is not required to interact with the MultipoolManager, but the custom inspector may be useful when configuring multiple pools.
 * 
 * by Adam Carballo under MIT license.
 * https://github.com/AdamCarballo/Unity-MultiPool
 */

using UnityEngine;
using JetBrains.Annotations;

namespace F10.Multipool {
    public class MultipoolEmitter : MonoBehaviour {

        [HideInInspector]
        public int _index;
        
        /// <summary>
        /// Retrieve from the index GameObject pool, check if null, and return the object already activated to the caller.
        /// </summary>
        /// <returns>Pooled GameObject or null.</returns>
        [CanBeNull]
        public GameObject GenerateGameObject() {
            var obj = MultipoolManager.Instance.GetPooledObject<GameObject>(_index);
            if (obj == null) return null;

            var reset = obj.GetComponent<MultipoolReset>();
            if (reset != null) {
                reset.Emitter = this;
            }
            
            obj.SetActive(true);
            return obj;
        }
        
        /// <summary>
        /// Retrieve from the index object pool, check if null, and return the object already activated to the caller.
        /// <br/>
        /// This method does not support MultipoolReset by default.
        /// </summary>
        /// <returns>Pooled object or null.</returns>
        [CanBeNull]
        public T Generate<T>() where T : Object {
            return MultipoolManager.Instance.GetPooledObject<T>(_index);
        }

        /// <summary>
        /// Disable the passed GameObject and send it back into the pool.
        /// </summary>
        /// <param name="object">GameObject to return.</param>
        public void ReturnGameObject(GameObject @object) {
            @object.SetActive(false);
            MultipoolManager.Instance.ReturnPooledObject(@object, _index);
        }
        
        /// <summary>
        /// Send the passed object back into the pool.
        /// </summary>
        /// <param name="object">Object to return.</param>
        public void Return<T>(T @object) where T : Object {
            MultipoolManager.Instance.ReturnPooledObject(@object, _index);
        }
    }
}