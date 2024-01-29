using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Destructible))]
public class StoneHitPointsText : MonoBehaviour
{
	[SerializeField] private Text hitPointsText;

	private Destructible destructible;

	private void Awake()
	{
		destructible = GetComponent<Destructible>();
		destructible.ChangeHitPoints.AddListener(OnChangeHitPoints);
	}

	private void OnDestroy() => destructible.ChangeHitPoints.RemoveListener(OnChangeHitPoints);

	private void OnChangeHitPoints()
	{
		int hitPoints = destructible.GetHP();
		if (hitPoints >= 1000) hitPointsText.text = hitPoints / 1000 + "K";
		else hitPointsText.text = hitPoints.ToString();
	}
}