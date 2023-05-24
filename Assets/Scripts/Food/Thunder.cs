using UnityEngine;

public class Thunder : MonoBehaviour
{
	public float minInterval = 2f;
	public float maxInterval = 5f;
	private AudioSource audioSource;
	void Start()
    {
		audioSource = GetComponent<AudioSource>();
		if (audioSource.clip.length > minInterval)
		{
			minInterval = audioSource.clip.length;
		}
		InvokeRepeating("PlayAudioRandomly", Random.Range(minInterval, maxInterval), Random.Range(minInterval, maxInterval));
	}

	void PlayAudioRandomly()
	{
		audioSource.Play();
	}
}