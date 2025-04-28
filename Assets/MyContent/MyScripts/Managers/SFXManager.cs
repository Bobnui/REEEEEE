using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance;

    [SerializeField] private AudioSource soundFXobject;

    [Header("Player")]
    [SerializeField] private AudioClip[] jumpSounds;
    [SerializeField] private AudioClip[] dashSounds;
    [SerializeField] private AudioClip[] hoverSounds;
    [SerializeField] private AudioClip[] footstepSounds;

    [Header("Checkpoint")]
    [SerializeField] private AudioClip[] checkpointSounds;

    [Header("Cannon")]
    [SerializeField] private AudioClip[] cannonfireSounds;
    [SerializeField] private AudioClip[] cannonimpactSounds;

    [Header("Dropper")]
    [SerializeField] private AudioClip[] dropperfireSounds;
    [SerializeField] private AudioClip[] dropperimpactSounds;

    [Header("Collectable")]
    [SerializeField] private AudioClip[] collectedSounds;

    [Header("Ambient")]
    [SerializeField] private bool PlayAmbient = false;
    private AudioSource ambientSource;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        ambientSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        if (PlayAmbient)
        {
            ambientSource.mute = false;
        }
        else
        {
            ambientSource.mute = true;
        }
    }
    public void PlayClip(string clip, Transform spawnTransform, float volume, bool is3d)
    {
        AudioSource audioSource = Instantiate(soundFXobject, spawnTransform.position, Quaternion.identity);

        audioSource.volume = volume;

        if (is3d) 
        { 
            audioSource.spatialBlend = 1; 
        }
        else 
        { 
            audioSource.spatialBlend = 0; 
        }

        if(clip == "jump")
        {
            int randInt = Random.Range(0, jumpSounds.Length);
            audioSource.clip = jumpSounds[randInt];
        }
        else if (clip == "dash")
        {
            int randInt = Random.Range(0, dashSounds.Length);
            audioSource.clip = dashSounds[randInt];
        }
        else if (clip == "checkpoint")
        {
            int randInt = Random.Range(0, dashSounds.Length);
            audioSource.clip = checkpointSounds[randInt];
        }
        else if (clip == "hover")
        {
            int randInt = Random.Range(0, hoverSounds.Length);
            audioSource.clip = hoverSounds[randInt];
        }
        else if (clip == "cannonfire")
        {
            int randInt = Random.Range(0, cannonfireSounds.Length);
            audioSource.clip = cannonfireSounds[randInt];
        }
        else if (clip == "cannonimpact")
        {
            int randInt = Random.Range(0, cannonimpactSounds.Length);
            audioSource.clip = cannonimpactSounds[randInt];
        }
        else if (clip == "dropperfire")
        {
            int randInt = Random.Range(0, dropperfireSounds.Length);
            audioSource.clip = dropperfireSounds[randInt];
        }
        else if (clip == "dropperimpact")
        {
            int randInt = Random.Range(0, dropperimpactSounds.Length);
            audioSource.clip = dropperimpactSounds[randInt];
        }
        else if(clip == "footstep")
        {
            int randInt = Random.Range(0, footstepSounds.Length);
            audioSource.clip = footstepSounds[randInt];
        }
        else if(clip == "collect")
        {
            int randInt = Random.Range(0, collectedSounds.Length);
            audioSource.clip = collectedSounds[randInt];
        }

            audioSource.Play();

        float clipLength = audioSource.clip.length;

        Destroy(audioSource.gameObject, clipLength);
    }
}
