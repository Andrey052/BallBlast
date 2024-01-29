using UnityEngine;

public class ImpactEffect : MonoBehaviour
{
	[SerializeField] private float lifeTime;

	private void Start() => Destroy(gameObject, lifeTime);
}