/*
 * MultipoolReset.cs
 * Demostration Script. Deactivates himself after time.
 * 
 * This script is optional. Just remember to deactivate objects to make them available again.
 * Also, if your object uses physics, remember to reset the velocity of the rigidbody.
 * 
 * by Adam Carballo under MIT license.
 * https://github.com/AdamEC/Unity-MultiPool
 */

using UnityEngine;

namespace Multipool {
    public class MultipoolReset : MonoBehaviour {

        #region Class Variables
        public float _time;
        public bool _resetRigidbody;
        #endregion

        #region Unity Methods
        /// <summary>
        /// Execute Destroy function after time.
        /// </summary>
        private void OnEnable() {

            Invoke("Destroy", _time);
        }

        /// <summary>
        /// Avoid multiple activations by canceling the Destroy() invoke.
        /// </summary>
        private void OnDisable() {

            CancelInvoke();
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Deactivate current GameObject.
        /// </summary>
        private void Destroy() {

            if (_resetRigidbody) {
                gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
            gameObject.SetActive(false);
        }
        #endregion
    }
}