/*
 * EC_ObjectPoolingEditor.cs
 * Editor Script. Draws Button and HelpBox on the inspector.
 * 
 * by Adam Carballo under GPLv3 license.
 * https://github.com/engyne09/UnityObjectPooling
 */

using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EC_ObjectPooling))]
public class EC_ObjectPoolingEditor : Editor {

    public string[] options;
    public int index = 0;

    private EC_ObjectPooling myScript;


    // Set myScript reference
    private void OnEnable() {

        myScript = (EC_ObjectPooling)target;
    }

    // Draw Button with a call action to GenerateEnum() and draw HelpBox 
    public override void OnInspectorGUI() {

        EditorGUILayout.HelpBox("Remember to save the object pools to make them appear on the list.", MessageType.Info);

        if (GUILayout.Button("Save Object Pools")) {
            myScript.GenerateEnum();
            OnEnable();
        }

        EditorGUILayout.Space();
        DrawDefaultInspector();
    }
}