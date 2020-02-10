using UnityEngine;
using System.Collections.Generic;

public static class ResourceManager
{
    public static int ScrollWidth { get { return 15; } }

    public static float ScrollSpeed { get { return 25; } }

    public static float RotateAmount { get { return 10; } }

    public static float RotateSpeed { get { return 100; } }

    public static float MinCameraHeight { get { return 30; } }

    public static float MaxCameraHeight { get { return 120; } }

    public static int MaxUnitsSelected { get { return 1000; } }

    public static int MaxBuildQueueCount { get { return 5; } }

    public static int DecimalsGUI { get { return 2; } }

    //public delegate eeee;
    //public static Dictionary<string, delegate> commands;

    private const int INVALID_POS = -99999;
    private static readonly Vector3 invalidPosition = new Vector3(INVALID_POS, INVALID_POS, INVALID_POS);
    //private static readonly Vector2d invalidPosition2d = new Vector2d(INVALID_POS, INVALID_POS);
    //private static readonly Vector2Int invalidVector2Int = new Vector2Int(INVALID_POS, INVALID_POS);
    //private static readonly Coordinate invalidCoordinate = new Coordinate(INVALID_POS, INVALID_POS);
    //private static readonly Bounds invalidBounds = new Bounds(new Vector3(INVALID_POS, INVALID_POS, INVALID_POS), new Vector3(0, 0, 0));

    public static Vector3 InvalidPosition { get { return invalidPosition; } }

    //public static Vector2d InvalidPosition2d { get { return invalidPosition2d; } }

    //public static Vector2Int InvalidVector2Int { get { return invalidVector2Int; } }

    //public static Coordinate InvalidCoordinate { get { return invalidCoordinate; } }

    //public static Bounds InvalidBounds { get { return invalidBounds; } }


    //public static Color mineIntentionColor = new Color(1f, 1f, 0.2f, 0.25f);

    //public static Rect PlayingArea { get { return playingArea; } }

    //public static bool MenuOpen { get; set; }

    //public static string LevelName { get; set; }


    private static GUISkin selectBoxSkin;

    public static GUISkin SelectBoxSkin { get { return selectBoxSkin; } }

    private static Texture2D healthyTexture, damagedTexture, criticalTexture;

    public static Texture2D HealthyTexture { get { return healthyTexture; } }

    public static Texture2D DamagedTexture { get { return damagedTexture; } }

    public static Texture2D CriticalTexture { get { return criticalTexture; } }



    private static GameObjectList gameObjectList;

    public static void SetGameObjectList(GameObjectList objectList)
    {
        gameObjectList = objectList;
    }

    public static GameObject GetWorldObject(string name)
    {
        return gameObjectList.GetWorldObject(name);
    }


    public static Texture2D GetUpgradeImage(string name, int state = 0)
    {
        return (gameObjectList != null) ? gameObjectList.GetUpgradeImage(name + state) : null;
    }



    public static Texture2D[] GetAvatars()
    {
        return gameObjectList.GetAvatars();
    }

    public static WorldObject GetWorldObject(GameObject obj)
    {
        WorldObject worldObject = obj.GetComponent<WorldObject>();
        if (!worldObject)
            worldObject = obj.transform.parent.GetComponent<WorldObject>();
        return worldObject;
    }

    private static WorldObject dummyWorldObject;

    //private static WorldObject DummyWorldObject{	//TODO
    //get{ if(dummyWorldObject == null)
    //dummyWorldObject = new
    //}

    public static WorldObject GetWorldObjectInstance(int instanceID)
    {
        //int tileCount = Map.TileCount;
        //// Look in players stuff
        //RtsPlayer[] players = RTS.Team.Players;
        //for (int i = 0; i < RTS.Team.PlayerCount; ++i)
        //{
        //    RtsPlayer current = players[i];
        //    if (current == null)
        //        continue;
        //    SortedArraySet<WorldObject> stuff = current.unitsAndBuildings;
        //    for (int j = 0; j < stuff.Count; ++j)
        //    {
        //        if (instanceID == stuff[j].InstanceID)
        //            return stuff[j];
        //    }
        //}
        return null;
    }



    public static AudioClip GetAudioClip(string name)
    {
        return gameObjectList.GetAudioClip(name);
    }

    public static AudioClip GetSoundEffect(string name)
    {
        return gameObjectList.GetSoundEffect(name);
    }

    public static AudioClip[] GetSoundEffects(string name)
    {
        return gameObjectList.GetSoundEffects(name);
    }

    public static int GetSoundEffectCount(string name)
    {
        return gameObjectList.GetSoundEffectCount(name);
    }

}
