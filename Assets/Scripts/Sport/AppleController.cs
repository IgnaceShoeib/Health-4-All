using UnityEngine;

public class AppleController : SportGame
{
	public float SpawnHeight;
	public float SpawnMaxZ;
	public float SpawnMinZ;
    public float SpawnMaxX;
    public float SpawnMinX;
    public GameObject ApplePrefab;

	public void RespawnApple()
	{
		var apple = Instantiate(ApplePrefab, new Vector3(), new Quaternion(), transform);
		apple.transform.localPosition = new Vector3(Random.Range(SpawnMinX, SpawnMaxX), SpawnHeight, Random.Range(SpawnMinZ, SpawnMaxZ));
	}

	public override void OnActiveGameTrue()
	{
		RespawnApple();
	}
	public override void OnActiveGameFalse()
	{
		var apples = FindObjectsOfType<Apple>();
		foreach (var apple in apples)
		{
			Destroy(apple.gameObject);
		}
	}
}
