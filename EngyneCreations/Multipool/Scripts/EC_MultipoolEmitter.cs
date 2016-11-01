/*
 * EC_MultipoolEmitter.cs
 * Emitter Configurator. Extract objects with EC_MultipoolEmmitter.Generate();
 * 
 * This script is required to extract the generated enum from the EC_Multipool array.
 * 
 * by Adam Carballo under GPLv3 license.
 * https://github.com/AdamEC/Unity-MultipoolObjectPooling
 */

using System.Collections.Generic;
using UnityEngine;

public class EC_MultipoolEmitter : MonoBehaviour {

    [HideInInspector]
    public int index;
    [HideInInspector]
    public List<string> copyGeneratedEnum = new List<string>();


    // Retrive from the index object pool, check if null, and return the object already activated to the caller.
    public GameObject Generate() {

        GameObject obj = EC_Multipool.instance.GetPooledObject(index);

        if (obj == null) return null;

        obj.SetActive(true);

        return obj;
    }
}