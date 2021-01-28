using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using Object = UnityEngine.Object;

namespace F10.Multipool {
	[Serializable]
	public class MultipoolPool {

		[Tooltip("Name of this pool as it will appear on the dropdown of the emitters.")]
		[SerializeField]
		private string _name;

		public string Name => _name;

		[Tooltip("Object to be pooled.")]
		[SerializeField]
		private Object _objectReference;

		[Tooltip("How many objects will get pre-spawned before the scene starts (pool size).")]
		[SerializeField]
		private int _startAmount;

		public int StartAmount => _startAmount;

		[Tooltip("Can this pool grow if the requested items are higher than the start amount.")]
		[SerializeField]
		private bool _canGrow = true;

		public bool CanGrow {
			get => _canGrow;
			set => _canGrow = value;
		}

		[Tooltip("Limit of pool grow. 0 = unlimited.")]
		[SerializeField]
		private int _maxGrow = 0;

		public int MaxGrow {
			get => _maxGrow;
			set => _maxGrow = value;
		}

		[Tooltip("Will this pool spawn objects inside a custom parent, or use the default one.")]
		[SerializeField]
		private Transform _customParent;

		public Transform CustomParent {
			get => _customParent;
			set => _customParent = value;
		}

		[Tooltip("Disable the available objects after generation if they are GameObjects.")]
		[SerializeField]
		private bool _disableIfGameObject = true;

		private Stack<Object> _availableObjects = null;
		private Dictionary<int, Object> _activeObjects = null;

		/// <summary>
		/// Amount of active (in use) objects.
		/// <br/>
		/// This method will allocate garbage each time is called. Use carefully.
		/// </summary>
		public int ActiveObjectAmount => _activeObjects.Count;

		/// <summary>
		/// Current size of the pool, regardless of startAmount.
		/// </summary>
		public int CurrentSize => _availableObjects.Count + _activeObjects.Count;

		/// <summary>
		/// Start and generate all the objects defined by startAmount.
		/// </summary>
		/// <returns>Instantiated object.</returns>
		public void StartPool() {
			_availableObjects = new Stack<Object>(_startAmount);
			_activeObjects = new Dictionary<int, Object>(_startAmount);

			for (int i = 0; i < _startAmount; i++) {
				GenerateObjectInPool();
			}
		}

		/// <summary>
		/// Instantiate an object inside the pool to be used later.
		/// </summary>
		/// <returns>Instantiated object.</returns>
		public void GenerateObjectInPool() {
			var obj = Object.Instantiate(_objectReference);

			if (_disableIfGameObject && obj is GameObject gameObj) {
				gameObj.SetActive(false);

				if (_customParent != null) {
					gameObj.transform.SetParent(_customParent, true);
				}
			}

			_availableObjects.Push(obj);
		}

		/// <summary>
		/// Returns an available object. If there isn't one, and canGrow, will create a new one.
		/// </summary>
		/// <returns>Available object marked as active.</returns>
		[CanBeNull]
		public Object GetPooledObject() {
			if (_availableObjects.Count == 0) {
				if (!_canGrow) return null;

				if (_maxGrow == 0 || CurrentSize < (_maxGrow + _startAmount)) {
					GenerateObjectInPool();
				} else {
					return null;
				}
			}

			var obj = _availableObjects.Pop();
			_activeObjects.Add(obj.GetInstanceID(), obj);

			return obj;
		}

		/// <summary>
		/// Marks the passed object as available to the pool to be re-used.
		/// </summary>
		/// <param name="objInstanceId">Object's instance ID to return to the pool.</param>
		/// <returns>Available object marked as active.</returns>
		[CanBeNull]
		public void ReturnPooledObject(int objInstanceId) {
			if (!_activeObjects.ContainsKey(objInstanceId)) {
#if UNITY_EDITOR
				throw new ArgumentException($"<color=magenta>[MultiPool]</color> The pool {_name} is not the owner of {UnityEditor.EditorUtility.InstanceIDToObject(objInstanceId).name}");
#else
					throw new ArgumentException($"<color=magenta>[MultiPool]</color> The pool {_name} is not the owner of an object with ID {objInstanceId}");
#endif
			}

			var obj = _activeObjects[objInstanceId];
			_activeObjects.Remove(objInstanceId);

			_availableObjects.Push(obj);
		}

	}
}