using UnityEngine;

public class SoundManager : MonoBehaviour 
{
    public static SoundManager instance;

    [SerializeField] private AudioSource m_AudioSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void PlaySound(AudioClip audioClip, Transform spawnTransform)
    {
        AudioSource audioSource = Instantiate(m_AudioSource, spawnTransform.position, Quaternion.identity);

        audioSource.clip = audioClip;

        audioSource.volume = 1.0f;

        audioSource.Play();

        float clipLength = audioSource.clip.length;

        Destroy(audioSource.gameObject, clipLength);
    }
}
