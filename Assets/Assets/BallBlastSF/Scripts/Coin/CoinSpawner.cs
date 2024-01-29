using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
	[SerializeField] private Coin coinPrefab;
	[SerializeField] private int establishedDropProbability;
	
	private void Awake() => GetComponent<Stone>().Die.AddListener(SpawnCoin);

	private void OnDestroy() => GetComponent<Stone>().Die.RemoveListener(SpawnCoin);

	private void SpawnCoin()
	{
		Vector3 transformPositionVar = transform.position;

		if ( transform.position.x < LevelBoundary.Instance.LeftBorder)
			transformPositionVar.x = LevelBoundary.Instance.LeftBorder;

		if (transform.position.x > LevelBoundary.Instance.RightBorder)
			transformPositionVar.x = LevelBoundary.Instance.RightBorder;

		transform.position = transformPositionVar;

		int randomValue = Random.Range(1, 101);
		if (randomValue > 100 - establishedDropProbability) Instantiate(coinPrefab, transform.position, Quaternion.identity);
	}
}