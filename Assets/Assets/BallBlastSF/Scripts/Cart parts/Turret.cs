using UnityEngine;

public class Turret : MonoBehaviour
{
	[SerializeField] private Projectile projectilePrefab;
	[SerializeField] private Transform shootPoint;
	[SerializeField] private float fireRate;
	[SerializeField] private int damage;
	[SerializeField] private int projectileAmount;
	[SerializeField] private float projectileInterval;
	[SerializeField] private LevelProgress levelProgress;

	public int Damage => damage;
	public int ProjectileAmount => projectileAmount;
	public float FireRate => fireRate;

	private float timer;

	private void Update() => timer += Time.deltaTime;

	private void SpawnProjectile()
	{
		float startPosX = shootPoint.position.x - projectileInterval * (projectileAmount - 1) * 0.5f;

		for (int i = 0; i < projectileAmount; i++)
		{
			Projectile projectile = Instantiate(
				projectilePrefab,
				new Vector3(startPosX + i * projectileInterval, shootPoint.position.y, shootPoint.position.z),
				transform.rotation
				);
			projectile.SetDamage(damage);
		}
	}

	public void Fire()
	{
		if (timer < fireRate) return;
		
		SpawnProjectile();
		timer = 0;
	}

	public void SetTurretFireRateBonus(float increasedChar) => fireRate = increasedChar;
	public void SetTurretDamageBonus(int increasedChar) => damage = increasedChar;
	public void SetProjectileAmountBonus(int increasedChar) => projectileAmount = increasedChar;
}