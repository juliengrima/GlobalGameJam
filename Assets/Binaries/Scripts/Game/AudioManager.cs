using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource _source;

    public static AudioManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void PlayOneShot(AudioClip clip, float volume = .5f)
        => _source.PlayOneShot(clip, volume);
}