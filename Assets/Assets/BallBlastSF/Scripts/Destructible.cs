using UnityEngine;
using UnityEngine.Events;

public class Destructible : MonoBehaviour
{
	private int hitPoints;
	public  int maxHitPoints;

	[HideInInspector] public UnityEvent ChangeHitPoints;
	[HideInInspector] public UnityEvent Die;

	private bool isDie;

	private void Start()
	{
		hitPoints = maxHitPoints;
		ChangeHitPoints.Invoke();
	}

	public void ApplyDamage(int damage)
	{
		hitPoints -= damage;

		ChangeHitPoints.Invoke();

		if (hitPoints <= 0) Kill();
	}

	/*public void ApplyHeal(int healPoints)
	{
		hitPoints += healPoints;

		ChangeHitPoints.Invoke();

		if (hitPoints >= maxHitPoints) hitPoints = maxHitPoints;
	}*/

	public void Kill()
	{
		if (isDie) return;

		hitPoints = 0;
		isDie = true;

		ChangeHitPoints.Invoke();
		Die.Invoke();
	}

	public int GetHP() => hitPoints;

	// public int GetMaxHP() => maxHitPoints;
}