using UnityEngine;

public class Projectile : MonoBehaviour
{
	[SerializeField] private float speed;
	[SerializeField] private float lifeTime;
	[SerializeField] private int damage;
	[SerializeField] private GameObject ShootingEffect;

	private void Start()
	{
		Instantiate(ShootingEffect);
		Destroy(gameObject, lifeTime);
	}

	private void Update()
	{
		//transform.Translate(0, speed * Time.deltaTime, 0);
		transform.position += transform.up * speed * Time.deltaTime;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (!collision.transform.root.TryGetComponent(out Destructible destructible)) return;
		destructible.ApplyDamage(damage);
		Destroy(gameObject);
	}

	public void SetDamage(int damage) => this.damage = damage;
}