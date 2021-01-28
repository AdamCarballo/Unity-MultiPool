Unity MultiPool
=================
🗄 MultiPool - Expandable Object Pooling for Unity

Overview
----
Using object pooling to replace `Instantiate()` and `Destroy()` can help the CPU on games with multiple instances of the same object, like *bullet hell* games or *shooters*. It also helps enormously on mobile platforms with reduced CPU capabilities.

Object pools will trade a little of memory footprint for a lower CPU usage. This is crucial for maintaining consistent framerates.

This Object Pooling solution can be used for multiple objects and pools without the need of duplicating scripts or GameObjects.

Install
----
The source code is available directly from the repository.<br>
Though, it's recommended to add this code as a **Unity Package**, as it contains all required configurations.

The original version of MultiPool (anything under v2) will work with any version of Unity 5. Newer versions (v2 and higher) haven't been tested in any version lower than Unity 2019.

Usage
----
Object Polling consists of two main scripts, `MultipoolManager.cs`and `MultipoolEmitter.cs`.<br>
Though, there are other optional scripts, like `MultipoolReset.cs` that allow quick setups for simple deployments.

All scripts include the *`F10.Multipool`* namespace to avoid issues with existing scripts. Remember to use it.

Add `MultipoolManager` to any GameObject to start using pools.

#### MultipoolManager.cs
Holds all the current object pools and handles internal calls for new objects.<br>
At `Start()` all the object pools will generate the amount of desired objects ready to be called.

During runtime will display relevant information of how your object pools are performing.

![MultiPool Runtime Inspector](https://i.imgur.com/x07ei9n.png)
___

#### MultipoolEmitter.cs
Receives the amount of object pools available and displays them on a popup in the Inspector.<br>
Use this script to call an specific pool from the list of available pools using `Generate()`.
___

### Creating Object Pools:
Change the size of `MultipoolManager._pools[]`in the inspector to generate more or less object pools.

```csharp
  Name; // Name of the object pool. Will appear on the generated popup.
  ObjectReference; // Object to pool.
  StartAmount; // Number of objects that will be generated on Start().
  CanGrow; // If true, the pool will generate more items than startAmount if needed.
  MaxGrow; // Limit of pool grow. 0 = unlimited.
  CustomParent; // Optional. Sets the object inside a parent for a cleaner Hierarchy.
  DisableIfGameObject; // Disable the available objects after generation if they are GameObjects.
```

After finishing editing the pools, remember to press **Index Object Pools** button to display them correctly.

### Extracting objects from Object Pools:
Define the pool to be used with the `MultipoolEmitter` popup in the inspector.<br>
Keep in mind that you first need to **Index Object Pools** if the popup appears empty.

Using `MultipoolEmitter.Generate<T>()` will return an object from the desired pool.<br>
`Generate<T>()`can also return `null` if the pool is empty and `CanGrow` is set to false, or `MaxGrow` has reached the limit.

Example using `BulletStartDemo.cs`:

```csharp
    [Range(0.1f, 2f)]
	[SerializeField]
	private float _fireTime;
	private MultipoolEmitter _emitter;

	/// <summary>
	/// Reference the Emitter.
	/// </summary>
	private void Awake() {
		_emitter = GetComponent<MultipoolEmitter>();
	}

	/// <summary>
	/// Start generating each fireTime.
	/// </summary>
	private void Start() {
		StartCoroutine(SpawnNew());
	}

	/// <summary>
	/// Retrieve from the object pool, check if null, reset position, rotation and set active.
	/// </summary>
	private IEnumerator SpawnNew() {
		while (true) {
			var obj = _emitter.GenerateGameObject();

			if (obj != null) {
				var thisTransform = transform;
				obj.transform.position = thisTransform.position;
				obj.transform.rotation = thisTransform.rotation;
			}

			yield return new WaitForSeconds(_fireTime);
		}
	}
```

If using the `MultipoolEmitter` is not a good idea in your case (for example, you need multiple objects from different pools in one script), you can access `MultipoolManager` directly using the singleton instance `MultipoolManager.Instance`.<br>
To get an object from a pool, use the function `MultipoolManager.GetPooledObject<T>(int index)` where `index` is the id of the list (position on the inspector).<br>
There is also `MultipoolManager.GetPooledObject<T>(string poolName)` where `poolName` is the name of the pool, although this method is more performance intensive.


Feel free to inspect all functions inside `MultipoolManager` to make it work with your code.

### Returning objects to Object Pools:
The source already includes `MultipoolReset.cs` as an example.<br>
The only requirement is to call `MultipoolEmitter.Return()` (when using the provided emitter), or `MultipoolManager.ReturnPooledObject(T obj, int index / string poolName)` to return an object to the pool.

If you use the object on other lists or arrays, remember to also remove it.<br>
It's also recommended to reset `Rigidbody.velocity`if the object uses physics.

Demo
----
The `Samples~` folder in the source code (included in the Unity Package as a sample) includes a demo scene to understand how everything works.

History
----
Created by Àdam Carballo<br>
Check other works on *[F10.DEV](https://f10.dev)*

License
---
MIT License<br>
Script icons by [Game-Icons.net](https://game-icons.net/)
