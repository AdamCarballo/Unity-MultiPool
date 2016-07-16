/*
 * EC_ObjectPoolReset.cs
 * Demostration Script. Deactivates himself after time.
 * 
 * This script is optional. Just remember to deactivate objects to make them available again.
 * Also, if your object uses physics, remember to reset velocity.
 * 
 * by Adam Carballo under GPLv3 license.
 * https://github.com/engyne09/UnityObjectPooling
 */

using UnityEngine;

public class EC_ObjectPoolReset : MonoBehaviour {

    public float time;

    // Execute Destroy function after time
	void OnEnable () {

        Invoke("Destroy", time);
	}
    
    // Deactivate current gameObject
    void Destroy() {

        gameObject.SetActive(false);
    }

    // Avoid multiple activations by canceling the Destroy invoke
	void OnDisable () {

        CancelInvoke();	
	}
}