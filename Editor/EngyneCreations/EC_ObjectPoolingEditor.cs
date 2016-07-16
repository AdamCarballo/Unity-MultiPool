/*
 * EC_ObjectPoolingEditor.cs
 * #DESCRIPTION#
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


    private void OnEnable() {

        myScript = (EC_ObjectPooling)target;
    }

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

/*using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomPropertyDrawer(typeof(EC_ObjectPooling.Test))]
public class EC_ObjectPoolingEditor : PropertyDrawer {

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        EditorGUI.PropertyField(position, property, label, true);
        if (property.isExpanded) {
            if (GUI.Button(new Rect(position.xMin + 30f, position.yMax - 20f, position.width - 30f, 20f), "button")) {
                // do things
            }
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
        if (property.isExpanded)
            return EditorGUI.GetPropertyHeight(property) + 20f;
        return EditorGUI.GetPropertyHeight(property);
    }
}*/

/*// Draw the property inside the given rect
public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
    // Using BeginProperty / EndProperty on the parent property means that
    // prefab override logic works on the entire property.
    EditorGUI.BeginProperty(position, label, property);

    // Draw label
    position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

    // Don't make child fields be indented
    int indent = EditorGUI.indentLevel;
    EditorGUI.indentLevel = 0;

    // Calculate rects
    Rect amountRect = new Rect(position.x, position.y, 30, position.height);
    Rect unitRect = new Rect(position.x + 35, position.y, 50, position.height);
    Rect nameRect = new Rect(position.x + 90, position.y, position.width - 90, position.height);

    // Draw fields - passs GUIContent.none to each so they are drawn without labels
    EditorGUI.PropertyField(amountRect, property.FindPropertyRelative("amount"), GUIContent.none);
    EditorGUI.PropertyField(unitRect, property.FindPropertyRelative("unit"), GUIContent.none);
    EditorGUI.PropertyField(nameRect, property.FindPropertyRelative("name"), GUIContent.none);

    // Set indent back to what it was
    EditorGUI.indentLevel = indent;

    EditorGUI.EndProperty();
}
}*/

/*using UnityEditor;

[CustomEditor(typeof(EC_ObjectPooling))]
public class EC_ObjectPoolingEditor : Editor {

    private SerializedProperty _poolObject; // GameObject
    private SerializedProperty _startAmount; // int
    private SerializedProperty _canGrow; // bool

    private void OnEnable() {
        _poolObject = serializedObject.FindProperty("poolObject");
        _startAmount = serializedObject.FindProperty("startAmount");
        _canGrow = serializedObject.FindProperty("canGrow");
    }

    public override void OnInspectorGUI() {

        DrawDefaultInspector();
        EditorGUILayout.HelpBox("Stuff Here", MessageType.Info);
    }
}*/
