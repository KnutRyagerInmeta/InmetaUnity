using UnityEngine;
using System.Collections;

public static class SpawnManager {


	/// <summary>
	/// Instantiate a GameObject in a location.
	/// </summary>
	//public static GameObject InstantiateGameObject (int id, Vector3 location, Quaternion rotation = default(Quaternion))
	//{
	//	GameObject obj = (GameObject)Object.Instantiate (ResourceManager.GetObject (id), location, rotation);
	//	//obj.transform.parent = Map.Instance.transform;
	//	return obj;
	//}

	/// <summary>
	/// Spawn a structure on the map and set it placed in coordinated x,z
	/// </summary>
	//public static Structure SpawnStructure (int id, int gridX, int gridZ, Quaternion rotation = default(Quaternion))
	//{
	//	Structure structure = SpawnWorldObject (id, gridX, gridZ, null, rotation) as Structure;
	//	return structure;
	//}

	/// <summary>
	/// Spawn a WorldObject and place it on the map in grid points gridX,gridZ.
	/// </summary>
	//public static WorldObject SpawnWorldObject (int id, int gridX, int gridZ, RtsPlayer owner = null, Quaternion rotation = default(Quaternion))
	//{
	//	GameObject obj = InstantiateGameObject (id, default(Vector3), rotation);
	//	WorldObject worldObject = obj.GetComponent<WorldObject> ();
	//	if (worldObject) {
	//		worldObject.Init ();
	//		worldObject.SetLocation (gridX, gridZ);
	//		if(owner != null && (!(worldObject is Fog)))
	//			worldObject.Owner = owner;
	//	}
	//	return worldObject;
	//}

	/// <summary>
	/// Spawn a WorldObject without giving it a position.
	/// </summary>
	//public static WorldObject SpawnWorldObject (int id, RtsPlayer owner = null, Quaternion rotation = default(Quaternion))
	//{
	//	GameObject obj = InstantiateGameObject (id, default(Vector3), rotation);
	//	return obj.GetComponent<WorldObject> ();
	//}

	/// <summary>
	/// Spawn a Unit and place it on the map in a Vector3 location.
	/// </summary>
	//public static Unit SpawnUnit (int id, Vector2d location, RtsPlayer owner, Quaternion rotation = default(Quaternion))	// Rotation TODO
	//{
	//	GameObject obj = InstantiateGameObject (id, location.ToVector3(0f), rotation);
	//	Unit worldObject = obj.GetComponent<Unit> ();
	//	if (worldObject) {
	//		worldObject.Init ();
	//		//Debug.Log ("grids: " + grids.x + ", " + grids.y + "," + location.ToString());
	//		worldObject.SetOwner(owner);
	//		worldObject.SetLocation (location);
	//		worldObject.LateInit ();
	//	}
	//	return worldObject;
	//}



}
