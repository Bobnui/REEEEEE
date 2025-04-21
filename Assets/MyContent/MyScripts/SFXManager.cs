using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance;

    [SerializeField] private AudioSource soundFXobject;

    [SerializeField] private AudioClip[] jumpSounds;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void PlayClip(string clip, Transform spawnTransform, float volume)
    {
        AudioSource audioSource = Instantiate(soundFXobject, spawnTransform.position, Quaternion.identity);

        audioSource.volume = volume;

        if(clip == "jump")
        {
            int jumpInt = Random.Range(0, jumpSounds.Length);
            audioSource.clip = jumpSounds[jumpInt];
        }

        audioSource.Play();

        float clipLength = audioSource.clip.length;

        Destroy(audioSource.gameObject, clipLength);
    }
}
