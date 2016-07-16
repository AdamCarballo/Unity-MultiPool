/*
 * EC_ObjectPoolEmitter.cs
 * Emitter Configurator. Extract objects with EC_ObjectPoolEmmitter.Generate();
 * 
 * This script is required to extract the generated enum from the EC_ObjectPooling array.
 * 
 * by Adam Carballo under GPLv3 license.
 * https://github.com/engyne09/UnityObjectPooling
 */

using System.Collections.Generic;
using UnityEngine;

public class EC_ObjectPoolEmitter : MonoBehaviour {

    [HideInInspector]
    public int index;
    [HideInInspector]
    public List<string> copyGeneratedEnum = new List<string>();


    // Retrive from the index object pool, check if null, and return the object already activated to the caller.
    public GameObject Generate() {

        GameObject obj = EC_ObjectPooling.instance.GetPooledObject(index);

        if (obj == null) return null;

        obj.SetActive(true);

        if (EC_ObjectPooling.instance.pool[index].customParent != null) {
            obj.transform.SetParent(EC_ObjectPooling.instance.pool[index].customParent, true);
        }
        return obj;
    }
}