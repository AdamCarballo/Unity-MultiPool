/*
 * EC_ObjectPoolEmitterEditor.cs
 * Editor Script. Generates the popup list from EC_ObjectPooling.generatedEnum.
 * 
 * by Adam Carballo under GPLv3 license.
 * https://github.com/engyne09/UnityObjectPooling
 */

using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EC_ObjectPoolEmitter))]
public class EC_ObjectPoolEmitterEditor : Editor {

    private SerializedProperty _index; // int
    private EC_ObjectPoolEmitter myScript;
    private int selected = 0;


    // Set myScript and configure the popup index
    private void OnEnable() {

        myScript = (EC_ObjectPoolEmitter)target;

        serializedObject.Update();

        _index = serializedObject.FindProperty("index");
        selected = _index.intValue;
    }

    // Draw the popup on the inspector and save a copy locally
    public override void OnInspectorGUI() {

        serializedObject.Update();

        _index = serializedObject.FindProperty("index");

        if (EC_ObjectPooling.generatedEnum.Count == 0) {
            EC_ObjectPooling.generatedEnum = myScript.copyGeneratedEnum;
        }

        myScript.copyGeneratedEnum = EC_ObjectPooling.generatedEnum;

        selected = EditorGUILayout.Popup("Selected Pool", selected, EC_ObjectPooling.generatedEnum.ToArray());
        _index.intValue = selected;

        serializedObject.ApplyModifiedProperties();

        for (int i = 0; i < myScript.copyGeneratedEnum.Count; i++) {
            Debug.Log(myScript.copyGeneratedEnum[i]);
        }
    }
}