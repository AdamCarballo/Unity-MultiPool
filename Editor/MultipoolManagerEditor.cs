/*
 * MultipoolEditor.cs
 * Editor Script. Draws Button and HelpBox on the inspector.
 * 
 * by Adam Carballo under MIT license.
 * https://github.com/AdamCarballo/Unity-MultiPool
 */

using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace F10.Multipool.Editor {
	[CustomEditor(typeof(MultipoolManager))]
	public class MultipoolManagerEditor : UnityEditor.Editor {

		private const string PoolNamePrefName = "Multipool_PoolNames";

		private static List<string> poolNames = null;

		public static IEnumerable<string> PoolNames {
			get {
				if (poolNames == null) {
					GenerateEmitterEnum();
				}

				return poolNames;
			}
		}

		private static readonly string[] _excludeList = {"m_Script"};

		private MultipoolManager _multipool;

		private GUIStyle _runtimeHelpBoxStyle = null;

		private GUIStyle RuntimeHelpBoxStyle =>
			_runtimeHelpBoxStyle ??= new GUIStyle(GUI.skin.GetStyle("HelpBox")) {
				richText = true,
				alignment = TextAnchor.UpperCenter,
				contentOffset = new Vector2(0, 3)
			};

		private GUIStyle _runtimeHelpBoxPlayingStyle = null;

		private GUIStyle RuntimeHelpBoxPlayingStyle =>
			_runtimeHelpBoxPlayingStyle ??= new GUIStyle(_runtimeHelpBoxStyle) {
				richText = true,
				alignment = TextAnchor.UpperLeft,
				contentOffset = new Vector2(10, 8)
			};

		/// <summary>
		/// Set the script reference.
		/// </summary>
		private void OnEnable() {
			_multipool = (MultipoolManager) target;
		}

		/// <summary>
		/// Draw a Button with a call action to GenerateEnum(), draw a HelpBox and hide the MonoBehaviour script.
		/// </summary>
		public override void OnInspectorGUI() {
			EditorGUILayout.TextArea(
				"<size=30><color=magenta>MultiPool</color> v1.3</size>\n" +
				"https://github.com/AdamCarballo/Unity-MultiPool\n"
				, RuntimeHelpBoxStyle);

			if (Application.isPlaying) {
				EditorGUILayout.TextArea(
					$"<size=14>Runtime Stats</size>\n\n{GetRuntimeInformation()}",
					RuntimeHelpBoxPlayingStyle);
			} else {
				EditorGUILayout.HelpBox(
					"Remember to index the object pools to make them appear on the Emitter list.",
					MessageType.Info
				);
				if (GUILayout.Button("Index Object Pools")) {
					SaveEmitterEnum();
					GenerateEmitterEnum();
				}
			}

			EditorGUILayout.Space();

			serializedObject.Update();
			DrawPropertiesExcluding(serializedObject, _excludeList);
			serializedObject.ApplyModifiedProperties();

			EditorUtility.SetDirty(target);
		}

		/// <summary>
		/// Returns current pool information, including active objects and objects created during runtime.
		/// </summary>
		/// <returns>Formatted string.</returns>
		private string GetRuntimeInformation() {
			var info = new StringBuilder();
			var allPools = _multipool.AllPools;

			foreach (var pool in allPools) {
				var activeAmount = pool.ActiveObjectAmount;
				var color = GetStatColor(pool, activeAmount);
				var extraAmount = pool.CurrentSize - pool.StartAmount;

				info.AppendLine($"<size=12><color={color}><b>{pool.Name}</b></color> " +
				                $"(<color=cyan>{activeAmount}</color>/<color=black>{pool.CurrentSize}</color>) " +
				                $"+ [<color=teal>{extraAmount}</color>]</size>");
			}

			return info.ToString();
		}

		/// <summary>
		/// Calculate the pool name color on the editor based on the current usage.
		/// </summary>
		/// <param name="pool">Pool to check.</param>
		/// <param name="activeObj">Objects active from the pool.</param>
		/// <returns>String with color name</returns>
		private static string GetStatColor(MultipoolPool pool, int activeObj) {
			float percentage = (float) activeObj / pool.CurrentSize;

			if (percentage < 0.75f) {
				return "green";
			}

			if (percentage < 0.95f) {
				return "orange";
			}

			if (pool.CanGrow) {
				return "maroon";
			}

			return "red";
		}

		private void SaveEmitterEnum() {
			var poolNamesFormatted = string.Join(",", _multipool.AllPools.Select(pool => pool.Name));
			EditorPrefs.SetString(PoolNamePrefName, poolNamesFormatted);
		}

		/// <summary>
		/// Generates a list of all the object pools names to be shown on an emitter's enum.
		/// </summary>
		private static void GenerateEmitterEnum() {
			poolNames = new List<string>();

			// Load saved names for editor config
			var savedPoolNames = EditorPrefs.GetString(PoolNamePrefName);
			if (string.IsNullOrEmpty(savedPoolNames)) {
				// If no data is saved, try to find the manager in the scene
				var multipool = FindObjectOfType<MultipoolManager>();
				if (multipool == null) {
					Debug.LogWarning("<color=magenta>[MultiPool]</color> Couldn't find any MultiPool data! Please re-index the pools.");
					return;
				}

				foreach (var pool in multipool.AllPools) {
					poolNames.Add(pool.Name);
				}
			} else {
				poolNames = savedPoolNames.Split(',').ToList();
			}
		}

	}
}