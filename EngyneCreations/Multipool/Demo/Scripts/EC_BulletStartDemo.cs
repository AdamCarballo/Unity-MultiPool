/*
 * EC_BulletStartDemo.cs
 * Demostration Script. Uses Generate() to get an object from the EC_MultipoolEmitter selected pool.
 * 
 * Just calling Generate() from the EC_MultipoolEmitter is enough to obtain an object from the pool.
 * 
 * This script generates an object every fireTime and resets his position and rotation.
 * 
 * by Adam Carballo under GPLv3 license.
 * https://github.com/AdamEC/Unity-MultipoolObjectPooling
 */

using UnityEngine;

public class EC_BulletStartDemo : MonoBehaviour {

    public float fireTime;
    private EC_MultipoolEmitter emitter;


    /// <summary>
    /// Reference the Emitter.
    /// </summary>
    void Awake() {

        emitter = GetComponent<EC_MultipoolEmitter>();
    }

    /// <summary>
    /// Start generating each fireTime.
    /// </summary>
    void Start () {

        InvokeRepeating("SpawnNew", 0, fireTime);
	}

    /// <summary>
    /// Retrive from the object pool, check if null, reset position and rotation and set active.
    /// </summary>
    void SpawnNew() {

        GameObject obj = emitter.Generate();

        if (obj == null) return;

        obj.transform.position = transform.position;
        obj.transform.rotation = transform.rotation;
        obj.SetActive(true);
	}
}