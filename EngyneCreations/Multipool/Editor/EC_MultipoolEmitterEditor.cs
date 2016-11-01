/*
 * EC_MultipoolEmitterEditor.cs
 * Editor Script. Generates the popup list from EC_ObjectPooling.generatedEnum.
 * 
 * by Adam Carballo under GPLv3 license.
 * https://github.com/AdamEC/Unity-MultipoolObjectPooling
 */

using UnityEditor;

[CustomEditor(typeof(EC_MultipoolEmitter))]
public class EC_MultipoolEmitterEditor : Editor {

    private SerializedProperty _index; // int
    private EC_MultipoolEmitter myScript;
    private int selected = 0;


    // Set myScript and configure the popup index
    private void OnEnable() {

        myScript = (EC_MultipoolEmitter)target;

        serializedObject.Update();

        _index = serializedObject.FindProperty("index");
        selected = _index.intValue;
    }

    // Draw the popup on the inspector and save a copy locally
    // Also, don't show the MonoBehaviour script in the inspector
    public override void OnInspectorGUI() {

        serializedObject.Update();

        _index = serializedObject.FindProperty("index");

        if (EC_Multipool.generatedEnum.Count == 0) {
            EC_Multipool.generatedEnum = myScript.copyGeneratedEnum;
        }

        myScript.copyGeneratedEnum = EC_Multipool.generatedEnum;

        selected = EditorGUILayout.Popup("Selected Pool", selected, EC_Multipool.generatedEnum.ToArray());
        _index.intValue = selected;

        serializedObject.ApplyModifiedProperties();

        /*for (int i = 0; i < myScript.copyGeneratedEnum.Count; i++) {
            Debug.Log(myScript.copyGeneratedEnum[i]);
        }*/
    }
}