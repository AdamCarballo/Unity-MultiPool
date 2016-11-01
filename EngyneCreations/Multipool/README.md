Unity Multipool Object Pooling
=================

Multipool expandable Object Pooling for Unity

<br>
Overview
----
Using object pooling to replace `Instantiate()` and `Destroy()` can help the CPU on games with multiple instances of the same object, like *bullet hell* games or *shooters*. It also helps enormously on mobile platforms with reduced CPU capabilities.

Object pools will trade a little of memory footprint for a lower usage of CPU. This is crucial for maintaining consistent framerates.

This Object Pooling solution can be used for separated objects and pools without the need of duplicating scripts or GameObjects.

<br>
Install
----
The source code is available directly from the folders, or if you want you can download only the Unitypackage and import from there.

<br>
Usage
----
Object Polling consists of two scripts, `Multipool.cs`and `MultipoolEmitter.cs` to work.<br>
All scripts include the *`EC_`* prefix to avoid issues with existing scripts. Remember to use them.

<br>

####Multipool.cs
Holds all the current object pools and handles internal calls for new objects.<br>
At `Start()`all the object pools will generate the amount of desired objects ready to be called.

___

####MultipoolEmitter.cs
Receives the amount of object pools available and displays them on a popup in the Inspector.<br>
Use this script to call an specific pool from the list of available pools using `Generate()`.

___
<br>

###Creating Object Pools:
Change the size of `Multipool.pool[]`in the inspector to generate more or less pools.

```csharp
  name; // Name of the object pool. Will appear on the generated popup.
  poolObject; // GameObject to pool.
  startAmount; // Number of objects that will be generated on Start().
  canGrow; // If true, the pool will generate more items than startAmount if needed.
  customParent; // Optional. Sets the object inside a parent for a cleaner Hierarchy.
```

After finishing editing the pools, remember to press **Save Object Pools** button to display them correctly.

<br>

###Extracting objects from Object Pools:
Define the pool to be used with `MultipoolEmitter.cs` popup in the inspector.<br>
Keep in mind that you first need to **Save Object Pools** if the popup appears empty.

Using `MultipoolEmitter.Generate()` will return an object from the desired pool already activated and ready to be used.<br>
`Generate()`can also return `null`if the pool is empty and `canGrow`is set to false.

Example using `BulletStartDemo.cs`:

```csharp
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
```

<br>

###Returning objects to Object Pools:
The source already includes `MultipoolReset.cs` as an example.<br>
The only requirement is to call `gameObject.SetActive(false)` to return an object to the pool.

If you use the object on other lists or arrays, remember to also remove it.<br>
I also recommend to reset `Rigidbody.velocity`if the object uses physics.

<br>
Demo
----
The Demo folder from the source code (included in the Unitypackage) includes a demo scene to understand how everything works.

<br>
History
----
Created by Àdam Carballo (AdamEC)

Check other works on *[Engyne Creations](http://engynecreations.com)*.

<br>
Licence
---
GNU General Public License v3.0
