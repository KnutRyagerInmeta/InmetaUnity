using UnityEngine;

/**
 * Singleton that keeps track of all the game objects that can be created
 * in the world. This persists across all maps to allow us to readily create
 * prefabs while loading from a saved file or when constructing new Units and
 * Buildings.
 */

public class GameObjectList : MonoBehaviour
{

    public GameObject[] unitEditors;
    public GameObject[] buildings;
    public GameObject[] units;
    public GameObject[] environmentResources;
    public GameObject[] worldObjects;
    public GameObject[] structures;
    public GameObject[] projectiles;
    //private const int numberOfBuildings = 0;
    //private const int numberOfUnits = 0;
    //private const int numberOfWorldObjects = 1;
    public GameObject player;
    public GameObject hud;
    public Texture2D[] avatars;
    public Texture2D[] buildingIconsSmall;
    public Texture2D[] unitIconsSmall;
    public Texture2D[] groundStateIcons;
    public Texture2D[] abilityIcons;
    public Texture2D[] upgradeIcons;
    public Texture2D[] itemIcons;
    public Texture2D[] miscTextures;
    public Texture2D[] oreTextures;
    public Texture2D[] happinessIcons;

    public AudioClip[] musicTracks;
    public AudioClip[] soundEffects;
    public AudioClip[] selectSoundEffects;



    private static bool created = false;

    void Awake()
    {
        if (!created)
        {
            DontDestroyOnLoad(transform.gameObject);    //TODO confirm if use
            ResourceManager.SetGameObjectList(this);
            //PlayerManager.Load();
            //RTS.PlayerManager.SetAvatarTextures (avatars);
            created = true;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }


    private GameObject FindGameObjectInList<T>(GameObject[] list, string name) where T : MonoBehaviour
    {
        for (int i = 0; i < list.Length; i++)
        {
            GameObject current = list[i];
            T component = current.GetComponent<T>();
            if (component && component.name == name)
                return current;
        }
        return null;
    }

    public AudioClip GetAudioClip(string name)
    {
        int index = FoundInList(name, musicTracks);
        if (index >= 0)
            return musicTracks[index];
        return null;
    }

    public AudioClip GetSoundEffect(string name)
    {
        int index = FoundInList(name, soundEffects);
        if (index >= 0)
            return soundEffects[index];
        index = FoundInList(name, selectSoundEffects);
        if (index >= 0)
            return selectSoundEffects[index];
        return null;
    }

    public AudioClip[] GetSoundEffects(string name)
    {
        AudioClip[] found = FindAllInList(name, soundEffects);
        if (found.Length == 0)
            found = FindAllInList(name, selectSoundEffects);
        return found;
    }

    public int GetSoundEffectCount(string name)
    {
        int count = CountInList(name, soundEffects);
        if (count == 0)
            count = CountInList(name, selectSoundEffects);
        return count;
    }

    public GameObject GetWorldObject(string name)
    {
        return FindGameObjectInList<WorldObject>(worldObjects, name);
    }

    //public GameObject GetObject (string name)
    //{
    //	GameObject obj = GetUnit (name);
    //	if (obj == null)
    //		obj = GetBuilding (name);
    //	if (obj == null)
    //		obj = GetResource (name);
    //	if (obj == null)
    //		obj = GetStructure (name);
    //	if (obj == null)
    //		obj = GetWorldObject (name);

    //	int index = FoundInList (name, worldObjects);
    //	if (index >= 0)
    //		return worldObjects [index];
    //	return obj;
    //}

    public GameObject GetPlayerObject()
    {
        return player;
    }

    public GameObject GetHudObject()
    {
        return hud;
    }

    /// <summary>
    /// Compare two strings. Returns true if all characters of the first string matches second string.
    /// </summary>
    private bool CompareStrings(string str1, string str2, bool toLowerCase = true)
    {
        if (toLowerCase)
        {
            str1 = str1.ToLower();
            str2 = str2.ToLower();
        }
        if (str1.Length > str2.Length)
            return false;
        for (int i = 0; i < str1.Length; ++i)
        {
            if (str1[i] != str2[i])
                return false;
        }
        return true;
    }

    /// <summary>
    /// Return position in list object was found. If not found, returns -1.
    /// </summary>
    private int FoundInList<T>(string str, T[] list) where T : UnityEngine.Object
    {
        for (int i = 0; i < list.Length; ++i)
        {
            //Debug.Log ("compare: " + str + " to " + list[i].name + ", was: " + (CompareStrings(str,list[i].name)));
            if (CompareStrings(str, list[i].name))
                return i;
        }
        return -1;
    }

    private T[] FindAllInList<T>(string str, T[] list) where T : UnityEngine.Object
    {
        int foundCount = CountInList(str, list);
        T[] found = new T[foundCount];
        int count = 0;
        for (int i = 0; i < list.Length; ++i)
        {
            T current = list[i];
            //Debug.Log ("compare: " + str + " to " + list[i].name + ", was: " + (CompareStrings(str,list[i].name)));
            if (CompareStrings(str, current.name))
            {
                found[count] = current;
                count++;
            }
        }
        return found;
    }

    private int CountInList<T>(string str, T[] list) where T : UnityEngine.Object
    {
        int count = 0;
        for (int i = 0; i < list.Length; ++i)
        {
            //Debug.Log ("compare: " + str + " to " + list[i].name + ", was: " + (CompareStrings(str,list[i].name)));
            if (CompareStrings(str, list[i].name))
                count++;
        }
        return count;
    }

    /// <summary>
    /// Get the build image of unit with name. Slow version.
    /// </summary>
    public Texture2D GetBuildImage(string name)
    {
        Texture2D image = LookForTexture(name, unitIconsSmall);
        if (image == null)
            image = LookForTexture(name, buildingIconsSmall);
        return image != null ? image : LookForTexture(name, groundStateIcons);
        //Debug.Log ("couldn't find damn '" + name + "'");
        /*for(int i=0; i<buildings.Length; i++) {
			Building building = buildings[i].GetComponent<Building>();
			if(building && building.name == name) return building.buildImage;
		}
		for(int i=0; i<units.Length; i++) {
			Unit unit = units[i].GetComponent<Unit>();
			if(unit && unit.name == name) return unit.buildImage;
		}*/
    }

    public Texture2D GetMiscTexture(string name)
    {
        return LookForTexture(name, miscTextures);

    }

    private Texture2D LookForTexture(string name, Texture2D[] list)
    {
        int index = FoundInList(name, list);
        if (index >= 0)
            return list[index];
        return null;
    }

    public Texture2D GetAbilityImage(string name)
    {
        int index = FoundInList(name, abilityIcons);
        if (index >= 0)
            return abilityIcons[index];
        return null;
    }

    public Texture2D GetUpgradeImage(string name)
    {
        int index = FoundInList(name, upgradeIcons);
        if (index >= 0)
            return upgradeIcons[index];
        return null;
    }

    public Texture2D GetItemImage(string name)
    {
        int index = FoundInList(name, itemIcons);
        if (index >= 0)
            return itemIcons[index];
        return null;
    }

    public Texture2D[] GetAvatars()
    {
        return avatars;
    }

    public Texture2D GetOreTexture(string name)
    {
        int index = FoundInList(name, oreTextures);
        if (index >= 0)
            return oreTextures[index];
        return null;
    }

    public Texture2D GetHappinessIcon(string name)
    {
        int index = FoundInList(name, happinessIcons);
        if (index >= 0)
            return happinessIcons[index];
        return null;
    }

}