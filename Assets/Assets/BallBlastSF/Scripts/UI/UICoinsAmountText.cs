using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CoinsAmount))]
public class UICoinsAmountText : MonoBehaviour
{
	[SerializeField] private CoinsAmount coinsAmount;
	[SerializeField] private LevelProgress levelProgress;
	[SerializeField] private LevelState levelState;
	[SerializeField] private Text text;

	private void Start()
	{
		text.text = levelProgress.CoinsNumber.ToString();
		coinsAmount.ChangeCoinsNumber.AddListener(OnChangeCoins);
		levelState.Defeat.AddListener(SetStartLevelCoinAmount);
	}
	
	private void OnDestroy() => coinsAmount.ChangeCoinsNumber.RemoveListener(OnChangeCoins);
	
	private void SetStartLevelCoinAmount() => text.text = levelProgress.CoinsNumber.ToString();
	private void OnChangeCoins() => text.text = coinsAmount.CoinsNumber.ToString();
}