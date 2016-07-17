/*
 * EC_BulletStartDemo.cs
 * Demostration Script. Uses Generate() to get an object from the EC_ObjectPoolEmitter selected pool.
 * 
 * Just calling Generate() form EC_ObjectPoolEmitter is required to obtain an object from the pool.
 * 
 * This script generates an object every fireTime and resets his position and rotation.
 * 
 * by Adam Carballo under GPLv3 license.
 * https://github.com/engyne09/UnityObjectPooling
 */

using UnityEngine;

public class EC_BulletStartDemo : MonoBehaviour {

    public float fireTime;
    private EC_ObjectPoolEmitter emitter;


    // Reference the Emitter
    void Awake() {

        emitter = GetComponent<EC_ObjectPoolEmitter>();
    }

    // Start generating each fireTime
    void Start () {

        InvokeRepeating("SpawnNew", 0, fireTime);
	}

    // Retrive from the object pool, check if null, reset position and rotation and set active.
    void SpawnNew() {

        GameObject obj = emitter.Generate();

        if (obj == null) return;

        obj.transform.position = transform.position;
        obj.transform.rotation = transform.rotation;
        obj.SetActive(true);
	}
}