using UnityEngine;

public class Coin : MonoBehaviour
{
	[SerializeField] private GameObject CoinPickedUpSoundEffect;
	[SerializeField] private GameObject CoinFellSoundEffect;

	[SerializeField] private float gravity;
	private Vector3 velocity;
	private bool isFall;

	private LevelEdge levelEdge;

	private void Awake() => levelEdge = FindObjectOfType<LevelEdge>();

	private void Start() => Instantiate(CoinFellSoundEffect);

	private void Update()
	{
		if (!isFall)
		{
			velocity.y -= gravity * Time.deltaTime;
			transform.position += velocity * Time.deltaTime;
		}
		else velocity.y = transform.position.y;

		GetCoinFalling();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (!collision.transform.root.TryGetComponent(out Cart cart) || !cart.TryGetComponent(out CoinsAmount coinsAmount)) return;
		
		coinsAmount.AddCoins(1);
		Instantiate(CoinPickedUpSoundEffect);
		Destroy(gameObject);
	}

	private void GetCoinFalling()
	{
		if (levelEdge.Type is not EdgeType.Bottom) return;
		if (transform.position.y - GetComponentInChildren<PolygonCollider2D>().bounds.extents.y > levelEdge.transform.position.y + levelEdge.GetComponent<BoxCollider2D>().size.y / 2) return;
		isFall = true;
	}
}