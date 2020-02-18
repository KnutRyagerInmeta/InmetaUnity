using UnityEngine;
public class AudioManager : MonoBehaviour
{

    private const int MAX_SOUND_SFX_CHANNELS = 10;

    public static AudioManager Instance { get; private set; }

    public const float DEFAULT_MUSIC_VOLUME = 0.5f;

    private AudioSource musicAudioSource;
    private AudioSource[] soundEffectAudioSources = new AudioSource[MAX_SOUND_SFX_CHANNELS];
    private AudioSource soundEffectAudioSource;
    private int soundEffectAudioSourceCount;

    public float musicVolume = 0.5f;
    public float soundEffectVolume = 0.5f;

    private static bool created;

    void Awake()
    {
        if (!created)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            created = true;
        }
        else
        {
            Destroy(gameObject);
            Instance.PlayMainMenuTheme();
        }
    }

    private void CheckAudioSources()
    {
        if (musicAudioSource == null || soundEffectAudioSource == null)
        {
            AudioSource[] sources = Camera.main.GetComponents<AudioSource>();
            musicAudioSource = sources[0];
            musicAudioSource.loop = true;
            soundEffectAudioSourceCount = sources.Length - 1;
            for (int i = 0; i < soundEffectAudioSourceCount; ++i)
            {
                AudioSource source = sources[i + 1];
                soundEffectAudioSources[i] = source;
                source.loop = false;
                source.minDistance = 40f;
                source.maxDistance = 85f;
                source.rolloffMode = AudioRolloffMode.Custom;
                source.spatialBlend = 1.0f;
                source.volume = soundEffectVolume;
            }
            soundEffectAudioSource = soundEffectAudioSources[0];
        }
    }

    void Start()
    {
    }

    public void PlaySong(string name)
    {
        if (name == null)
            return;
        CheckAudioSources();
        if (musicAudioSource != null)
        {
            AudioClip clip = ResourceManager.GetAudioClip(name);
            if (clip == null)
                return;
            musicAudioSource.volume = musicVolume;
            musicAudioSource.clip = clip;
            musicAudioSource.Play();
        }
    }
    private static int previousSoundEffectIndex = 0;
    private static int previousAudioSourceIndex = 0;
    private string previousPlayedSound = null;
    public void PlaySoundEffect(string name, AudioSource audioSource = null)
    {
        if (name == null) return;
        //Debug.Log ("playing " + name);
        bool wasIncludedSource = (audioSource != null);
        if (!wasIncludedSource)
        {
            audioSource = soundEffectAudioSource;
        }
        if (audioSource != null)
        {
            AudioClip[] clips = ResourceManager.GetSoundEffects(name);
            int clipCount = clips.Length;
            if (clipCount == 0) return;
            int index = Util.GetRandomNumber(0, clipCount);
            // Don't use same sound two times in a row
            if (index == previousSoundEffectIndex && name.Equals(previousPlayedSound))
            {
                index += Util.GetRandomNumber(1, clipCount);
                index %= clipCount;
            }
            previousSoundEffectIndex = index;
            AudioClip clip = clips[index];
            if (clip == null)
                return;
            if ((!wasIncludedSource) && audioSource.isPlaying)
            {
                previousAudioSourceIndex++;
                previousAudioSourceIndex %= soundEffectAudioSourceCount;
                soundEffectAudioSource = soundEffectAudioSources[previousAudioSourceIndex];
                audioSource = soundEffectAudioSource;
            }

            audioSource.clip = clip;
            audioSource.minDistance = 40f;
            audioSource.maxDistance = 85f;
            audioSource.rolloffMode = AudioRolloffMode.Custom;
            audioSource.spatialBlend = 1.0f;
            audioSource.volume = soundEffectVolume;
            audioSource.Play();
            previousPlayedSound = name;
        }
    }

    public enum CommandSound { SELECT, MOVE, ATTACK, ACCEPT, COLLECT }

    private int previousUnitType, previousUnitInstance;
    private string previousSoundToPlay;
    private CommandSound previousCommandSound;
    //private System.DateTime

    private double lastCommandSoundPlayTime;
    private const double CLOSEST_COMMAND_SOUND_TIME = 50; // milliseconds
    public void SetMusicVolume(float value)
    {
        if (value == musicVolume)
            return;
        musicVolume = value;
        if (musicAudioSource != null)
        {
            musicAudioSource.volume = value;
            //SaveManager.SaveMusicVolume (value);
        }
    }

    public void SetSoundEffectVolume(float value)
    {
        if (value == soundEffectVolume)
            return;
        soundEffectVolume = value;
        for (int i = 0; i < soundEffectAudioSourceCount; ++i)
        {
            AudioSource source = soundEffectAudioSources[i];
            if (source != null)
            {
                source.volume = value;
            }
        }
        //SaveManager.SaveSoundEffectVolume (value);
    }

    //
    public void PlayMainMenuTheme()
    {
        PlaySong("MISSING");
    }
}
