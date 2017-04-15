/*
 * MultipoolEditor.cs
 * Editor Script. Draws Button and HelpBox on the inspector.
 * 
 * by Adam Carballo under MIT license.
 * https://github.com/AdamEC/Unity-MultiPool
 */

using UnityEditor;
using UnityEngine;

namespace Multipool {
    [CustomEditor(typeof(MultipoolManager))]
    public class MultipoolManagerEditor : Editor {

        #region Class Variables
        public string[] options;
        public int index = 0;

        private MultipoolManager _multipool;
        private static readonly string[] _scriptHolder = new string[] {"m_Script"};
        #endregion

        #region Unity Methods
        /// <summary>
        /// Set the script reference.
        /// </summary>
        private void OnEnable() {

            _multipool = (MultipoolManager)target;
        }

        /// <summary>
        /// Draw a Button with a call action to GenerateEnum(), draw a HelpBox and hide the MonoBehaviour script.
        /// </summary>
        public override void OnInspectorGUI() {

            GUIStyle _runtimeHelpBox = GUI.skin.GetStyle("HelpBox");
            _runtimeHelpBox.richText = true;
            _runtimeHelpBox.alignment = TextAnchor.UpperCenter;
            _runtimeHelpBox.contentOffset = new Vector2(0, 0);
            EditorGUILayout.TextArea(
                "<size=30><color=magenta>MultiPool</color> v1.2.1</size>\n" +
                "https://github.com/AdamEC/Unity-MultiPool\n"
                , _runtimeHelpBox);

            if (Application.isPlaying) {
                _runtimeHelpBox.alignment = TextAnchor.UpperLeft;
                _runtimeHelpBox.contentOffset = new Vector2(10, 8);
                EditorGUILayout.HelpBox(
                    "<size=19>Runtime Stats</size>\n" +
                    "\n" +
                    _multipool.GetRuntimeInformation(), MessageType.None);
            } else {
                EditorGUILayout.HelpBox(
                    "Remember to index the object pools to make them appear on the Emitter list.",
                    MessageType.Info
                );
                if (GUILayout.Button("Index Object Pools")) {
                    _multipool.GenerateEnum();
                    OnEnable();
                }
            }

            EditorGUILayout.Space();
            serializedObject.Update();
            DrawPropertiesExcluding(serializedObject, _scriptHolder);
            serializedObject.ApplyModifiedProperties();

            EditorUtility.SetDirty(target);
        }
        #endregion
    }
}