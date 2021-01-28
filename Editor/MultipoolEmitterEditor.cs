/*
 * MultipoolEmitterEditor.cs
 * Editor Script. Generates the popup list from ObjectPooling.generatedEnum.
 * 
 * by Adam Carballo under MIT license.
 * https://github.com/AdamCarballo/Unity-MultiPool
 */

using System.Linq;
using UnityEditor;

namespace F10.Multipool.Editor {
	[CustomEditor(typeof(MultipoolEmitter))]
	public class MultipoolEmitterEditor : UnityEditor.Editor {

		private SerializedProperty _index; // int

		/// <summary>
		/// Set MultipoolEmitter and configure the popup index.
		/// </summary>
		private void OnEnable() {
			serializedObject.Update();

			_index = serializedObject.FindProperty("_index");
		}

		/// <summary>
		/// Draw the popup on the inspector and save a copy locally.
		/// </summary>
		public override void OnInspectorGUI() {
			serializedObject.Update();

			EditorGUILayout.HelpBox(
				"Remember to index the object pools to make them appear on the Emitter list.",
				MessageType.Info
			);

			var poolNames = MultipoolManagerEditor.PoolNames.ToArray();
			_index.intValue = EditorGUILayout.Popup("Selected Pool", _index.intValue, poolNames);

			serializedObject.ApplyModifiedProperties();
		}

	}
}