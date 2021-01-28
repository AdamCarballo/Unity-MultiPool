/*
 * BulletStartDemo.cs
 * Demo Script. Uses Generate() to get an object from the MultipoolEmitter selected pool.
 * 
 * Just calling Generate() from the MultipoolEmitter is enough to obtain an object from the pool.
 * 
 * This script generates an object every fireTime and resets his position and rotation.
 * 
 * by Adam Carballo under MIT license.
 * https://github.com/AdamCarballo/Unity-MultiPool
 */

using UnityEngine;
using System.Collections;
using F10.Multipool;

public class BulletStartDemo : MonoBehaviour {

	[Range(0.1f, 2f)]
	[SerializeField]
	private float _fireTime;
	private MultipoolEmitter _emitter;

	/// <summary>
	/// Reference the Emitter.
	/// </summary>
	private void Awake() {
		_emitter = GetComponent<MultipoolEmitter>();
	}

	/// <summary>
	/// Start generating each fireTime.
	/// </summary>
	private void Start() {
		StartCoroutine(SpawnNew());
	}

	/// <summary>
	/// Retrieve from the object pool, check if null, reset position, rotation and set active.
	/// </summary>
	private IEnumerator SpawnNew() {
		while (true) {
			var obj = _emitter.GenerateGameObject();

			if (obj != null) {
				var thisTransform = transform;
				obj.transform.position = thisTransform.position;
				obj.transform.rotation = thisTransform.rotation;
			}

			yield return new WaitForSeconds(_fireTime);
		}

		// ReSharper disable once IteratorNeverReturns
	}

}