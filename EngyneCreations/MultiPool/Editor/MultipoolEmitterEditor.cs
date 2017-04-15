/*
 * MultipoolEmitterEditor.cs
 * Editor Script. Generates the popup list from ObjectPooling.generatedEnum.
 * 
 * by Adam Carballo under MIT license.
 * https://github.com/AdamEC/Unity-MultiPool
 */

using UnityEditor;

namespace Multipool {
    [CustomEditor(typeof(MultipoolEmitter))]
    public class MultipoolEmitterEditor : Editor {

        #region Class Variables
        private SerializedProperty _index; // int
        private MultipoolEmitter _multipoolEmitter;
        private int _selected = 0;
        #endregion

        #region Unity Methods
        /// <summary>
        /// Set MultipoolEmitter and configure the popup index.
        /// </summary>
        private void OnEnable() {

            _multipoolEmitter = (MultipoolEmitter)target;

            serializedObject.Update();

            _index = serializedObject.FindProperty("index");
            _selected = _index.intValue;
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

            _index = serializedObject.FindProperty("index");

            if (MultipoolManager.generatedEnum.Count == 0) {
                MultipoolManager.generatedEnum = _multipoolEmitter.copyGeneratedEnum;
            }

            _multipoolEmitter.copyGeneratedEnum = MultipoolManager.generatedEnum;

            _selected = EditorGUILayout.Popup("Selected Pool", _selected, MultipoolManager.generatedEnum.ToArray());
            _index.intValue = _selected;

            serializedObject.ApplyModifiedProperties();
        }
        #endregion
    }
}