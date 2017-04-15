Unity MultiPool
=================
🗄 MultiPool - Expandable Object Pooling for Unity

Overview
----
Using object pooling to replace `Instantiate()` and `Destroy()` can help the CPU on games with multiple instances of the same object, like *bullet hell* games or *shooters*. It also helps enormously on mobile platforms with reduced CPU capabilities.

Object pools will trade a little of memory footprint for a lower usage of CPU. This is crucial for maintaining consistent framerates.

This Object Pooling solution can be used for separated objects and pools without the need of duplicating scripts or GameObjects.

Install
----
The source code is available directly from the folders, or if you want you can download only the [Unitypackage](https://github.com/AdamEC/Unity-MultiPool/releases) and import from there.

Should work with any version of Unity 5. Tested from Unity5.4 and up.

Usage
----
Object Polling consists of two scripts, `MultipoolManager.cs`and `MultipoolEmitter.cs` to work.<br>
All scripts include the *`Multipool`* namespace to avoid issues with existing scripts. Remember to use it.


#### MultipoolManager.cs
Holds all the current object pools and handles internal calls for new objects.<br>
At `Start()`all the object pools will generate the amount of desired objects ready to be called.

During runtime will display relevant information of how your object pools are performing.

![MultiPool Runtime Inspector](https://i.imgur.com/x07ei9n.png)
___

#### MultipoolEmitter.cs
Receives the amount of object pools available and displays them on a popup in the Inspector.<br>
Use this script to call an specific pool from the list of available pools using `Generate()`.
___

### Creating Object Pools:
Change the size of `MultipoolManager.pool[]`in the inspector to generate more or less object pools.

```csharp
  name; // Name of the object pool. Will appear on the generated popup.
  poolObject; // GameObject to pool.
  startAmount; // Number of objects that will be generated on Start().
  canGrow; // If true, the pool will generate more items than startAmount if needed.
  maxGrow; // Limit of pool grow. 0 = unlimited. Can be edited at runtime.
  customParent; // Optional. Sets the object inside a parent for a cleaner Hierarchy.
```

After finishing editing the pools, remember to press **Index Object Pools** button to display them correctly.

### Extracting objects from Object Pools:
Define the pool to be used with the `MultipoolEmitter` popup in the inspector.<br>
Keep in mind that you first need to **Index Object Pools** if the popup appears empty.

Using `MultipoolEmitter.Generate()` will return an object from the desired pool. The object **will not be activated** until you do it manually with `gameObject.SetActive(true)`. Remember that if the object is not activated will still count as "free" for other scripts.<br>
`Generate()`can also return `null`if the pool is empty and `canGrow`is set to false, or maxGrow has reached the limit.

Example using `BulletStartDemo.cs`:

```csharp
  [Range(0.1f,2f)]
  [SerializeField] private float _fireTime;
  private MultipoolEmitter emitter;

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
```

If using the `MultipoolEmitter` is not a good idea in your case (for example, you need multiple objects from different pools in one script), you can access `MultipoolManager` directly using the singleton instance `MultipoolManager.instance`.<br>
To get an object from a pool, use the function `MultipoolManager.GetPooledObject(int index)` where `index` is the id of the list (position on the inspector).

Feel free to inspect all functions inside `MultipoolManager` to make it work with your code.

### Returning objects to Object Pools:
The source already includes `MultipoolReset.cs` as an example.<br>
The only requirement is to call `gameObject.SetActive(false)` to return an object to the pool.

If you use the object on other lists or arrays, remember to also remove it.<br>
I also recommend to reset `Rigidbody.velocity`if the object uses physics.

Demo
----
The Demo folder in the source code (included in the Unitypackage) includes a demo scene to understand how everything works.

History
----
Created by Àdam Carballo (AdamEC)<br>
Check other works on *[Engyne Creations](http://engynecreations.com)*.

License
---
MIT License
