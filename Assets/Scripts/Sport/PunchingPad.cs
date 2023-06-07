using UnityEngine;

public class PunchingPad : MonoBehaviour
{
	public string HandObjectName;
	public AudioClip[] hitSounds;
	private AudioSource audioSource;
	private PunchingPadController punchingPadController;
	void Start()
	{
		punchingPadController = FindAnyObjectByType<PunchingPadController>();

		// Try to get an existing AudioSource component on the same object
		audioSource = GetComponent<AudioSource>();

		// If no AudioSource component exists, add one to the same object
		if (audioSource == null)
		{
			audioSource = gameObject.AddComponent<AudioSource>();
		}
		// Make sound 3D
		audioSource.spatialBlend = 1;
		audioSource.maxDistance = 10;
	}
	void OnTriggerEnter(Collider other)
	{
		if (other.transform.parent.name == HandObjectName)
		{
			punchingPadController.Hit(name);
			AudioClip sound = hitSounds[Random.Range(0, hitSounds.Length)];
			audioSource.clip = sound;

			audioSource.Play();
		}
	}
}
