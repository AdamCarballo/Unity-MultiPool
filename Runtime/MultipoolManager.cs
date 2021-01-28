/*
 * Multipool.cs
 * Manage the pooling of any object and calls to retrieve them.
 * 
 * by Adam Carballo under MIT license.
 * https://github.com/AdamCarballo/Unity-MultiPool
 */

using System;
using UnityEngine;
using System.Collections.Generic;
using JetBrains.Annotations;
using Object = UnityEngine.Object;

namespace F10.Multipool {
	[HelpURL("https://github.com/AdamCarballo/Unity-MultiPool")]
	[DefaultExecutionOrder(-100)]
	public class MultipoolManager : MonoBehaviour {

		public static MultipoolManager Instance { get; private set; } = null;

		[SerializeField]
		private MultipoolPool[] _pools;

		public IEnumerable<MultipoolPool> AllPools => _pools;

		/// <summary>
		/// Define instance and check for duplicated pools.
		/// </summary>
		private void Awake() {
			if (Instance != null) {
				Debug.LogError("<color=magenta>[MultiPool]</color> MultiPool can only exist once in the scene!");
#if UNITY_EDITOR
				UnityEditor.EditorApplication.isPaused = true;
#endif
			}

			Instance = this;
		}

		/// <summary>
		/// Instantiate the startAmount of poolObject and deactivate them.
		/// </summary>
		private void Start() {
			foreach (var pool in _pools) {
				pool.StartPool();
			}
		}

		/// <summary>
		/// Returns an available object. If there isn't one, and canGrow, will create a new one.
		/// <br/>
		/// It is recommended to use an emitter or use the overload of GetPooledObject() that uses a pools index for better performance.
		/// </summary>
		/// <param name="poolName">Name of the pool.</param>
		/// <returns>Available object marked as active, or null if the pool is empty.</returns>
		[CanBeNull]
		public T GetPooledObject<T>(string poolName) where T : Object {
			for (int i = 0; i < _pools.Length; i++) {
				if (_pools[i].Name == poolName) {
					return GetPooledObject<T>(i);
				}
			}

			throw new ArgumentException($"<color=magenta>[MultiPool]</color> There is no pool with the name {poolName}");
		}

		/// <summary>
		/// Returns an available object. If there isn't one, and canGrow, will create a new one.
		/// </summary>
		/// <param name="index">Index of the pool.</param>
		/// <returns>Available object marked as active, or null if the pool is empty.</returns>
		[CanBeNull]
		public T GetPooledObject<T>(int index) where T : Object {
			var pool = _pools[index];
			return pool.GetPooledObject() as T;
		}

		/// <summary>
		/// Marks the passed object as available to the pool to be re-used.
		/// </summary>
		/// <param name="poolName">Name of the pool.</param>
		/// <param name="obj">Object to return.</param>
		public void ReturnPooledObject<T>(T obj, string poolName) where T : Object {
			for (int i = 0; i < _pools.Length; i++) {
				if (_pools[i].Name == poolName) {
					ReturnPooledObject(obj, i);
				}
			}

			throw new ArgumentException($"<color=magenta>[MultiPool]</color> There is no pool with the name {poolName}");
		}

		/// <summary>
		/// Marks the passed object as available to the pool to be re-used.
		/// </summary>
		/// <param name="index">Index of the pool.</param>
		/// <param name="obj">Object to return.</param>
		public void ReturnPooledObject<T>(T obj, int index) where T : Object {
			var pool = _pools[index];
			pool.ReturnPooledObject(obj.GetInstanceID());
		}

	}
}