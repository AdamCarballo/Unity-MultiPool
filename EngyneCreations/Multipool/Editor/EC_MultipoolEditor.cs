/*
 * EC_MultipoolEditor.cs
 * Editor Script. Draws Button and HelpBox on the inspector.
 * 
 * by Adam Carballo under GPLv3 license.
 * https://github.com/AdamEC/Unity-MultipoolObjectPooling
 */

using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EC_Multipool))]
public class EC_MultipoolEditor : Editor {

    public string[] options;
    public int index = 0;

    private EC_Multipool myScript;
    private static readonly string[] _dontIncludeMe = new string[] {"m_Script"};

    // Set myScript reference
    private void OnEnable() {

        myScript = (EC_Multipool)target;
    }

    // Draw Button with a call action to GenerateEnum() and draw HelpBox 
    public override void OnInspectorGUI() {

        EditorGUILayout.HelpBox("Remember to save the object pools to make them appear on the list.", MessageType.Info);

        if (GUILayout.Button("Save Object Pools")) {
            myScript.GenerateEnum();
            OnEnable();
        }

        EditorGUILayout.Space();
        //DrawDefaultInspector();
        serializedObject.Update();
        DrawPropertiesExcluding(serializedObject, _dontIncludeMe);
        serializedObject.ApplyModifiedProperties();
    }
}