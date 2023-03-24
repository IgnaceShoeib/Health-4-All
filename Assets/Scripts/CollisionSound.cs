using UnityEngine;

public class CollisionSound : MonoBehaviour
{
	public AudioClip[] collisionSounds; // An array of audio clips for collision sounds
	private AudioSource audioSource; // Reference to the audio source component

	private void Start()
	{
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

	private void OnCollisionEnter(Collision collision)
	{
		// Play a random collision sound
		if (collisionSounds.Length > 0)
		{
			AudioClip sound = collisionSounds[Random.Range(0, collisionSounds.Length)];
			audioSource.clip = sound;

			// Calculate volume based on velocity
			audioSource.volume = collision.relativeVelocity.magnitude/10;
			print(collision.relativeVelocity.magnitude);

			audioSource.Play();
		}
	}
}