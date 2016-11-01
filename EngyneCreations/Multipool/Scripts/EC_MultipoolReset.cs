/*
 * EC_MultipoolReset.cs
 * Demostration Script. Deactivates himself after time.
 * 
 * This script is optional. Just remember to deactivate objects to make them available again.
 * Also, if your object uses physics, remember to reset velocity.
 * 
 * by Adam Carballo under GPLv3 license.
 * https://github.com/AdamEC/Unity-MultipoolObjectPooling
 */

using UnityEngine;

public class EC_MultipoolReset : MonoBehaviour {

    public float time;
    public bool resetRigidbody;

    /// <summary>
    /// Execute Destroy function after time.
    /// </summary>
	void OnEnable () {

        Invoke("Destroy", time);
	}

    /// <summary>
    /// Deactivate current gameObject.
    /// </summary>
    void Destroy() {

        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Avoid multiple activations by canceling the Destroy invoke.
    /// </summary>
	void OnDisable () {

        CancelInvoke();	
	}
}