/*
 * MultipoolReset.cs
 * Demo Script. Deactivates himself after the specified time.
 * 
 * This script is optional. Just remember to deactivate objects to make them available again.
 * Also, if your object uses physics, remember to reset the velocity of the rigidbody.
 * 
 * by Adam Carballo under MIT license.
 * https://github.com/AdamCarballo/Unity-MultiPool
 */

using UnityEngine;

namespace F10.Multipool {
	public class MultipoolReset : MonoBehaviour {

		[SerializeField]
		private float _time;
		
		[SerializeField]
		private bool _resetRigidbody;

		public MultipoolEmitter Emitter { get; set; }

		/// <summary>
		/// Execute Return function after time.
		/// </summary>
		private void OnEnable() {
			
			if (_resetRigidbody) {
				var rigidBody = GetComponent<Rigidbody>();
				rigidBody.velocity = Vector3.zero;
				rigidBody.angularVelocity = Vector3.zero;
			}
			
			Invoke(nameof(Return), _time);
		}

		/// <summary>
		/// Avoid multiple activations by canceling the Return() invoke.
		/// </summary>
		private void OnDisable() {
			CancelInvoke();
		}

		/// <summary>
		/// Deactivate current GameObject.
		/// </summary>
		private void Return() {
			if (Emitter == null) {
				Debug.LogError("<color=magenta>[MultiPool]</color> MultipoolReset must be used in conjunction with a MultipoolEmitter.");
				return;
			}

			Emitter.ReturnGameObject(gameObject);
		}

	}
}