/*
 * BulletStartDemo.cs
 * Demostration Script. Uses Generate() to get an object from the MultipoolEmitter selected pool.
 * 
 * Just calling Generate() from the MultipoolEmitter is enough to obtain an object from the pool.
 * 
 * This script generates an object every fireTime and resets his position and rotation.
 * 
 * by Adam Carballo under MIT license.
 * https://github.com/AdamEC/Unity-MultiPool
 */

using UnityEngine;
using System.Collections;
using Multipool;

public class BulletStartDemo : MonoBehaviour {

    #region Class Variables
    [Range(0.1f,2f)]
    [SerializeField] private float _fireTime;
    private MultipoolEmitter emitter;
    #endregion

    #region Unity Methods
    /// <summary>
    /// Reference the Emitter.
    /// </summary>
    void Awake() {

        emitter = GetComponent<MultipoolEmitter>();
    }

    /// <summary>
    /// Start generating each fireTime.
    /// </summary>
    void Start () {

        StartCoroutine(SpawnNew());
	}
    #endregion

    #region Private Methods
    /// <summary>
    /// Retrive from the object pool, check if null, reset position and rotation and set active.
    /// </summary>
    private IEnumerator SpawnNew() {

        while (true) {
            GameObject obj = emitter.Generate();

            if (obj) {
                obj.transform.position = transform.position;
                obj.transform.rotation = transform.rotation;
                obj.SetActive(true);
            }
            yield return new WaitForSeconds(_fireTime);
        }

    }
    #endregion
}