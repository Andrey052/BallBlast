using UnityEngine;
using UnityEngine.Events;

public class CoinsAmount : MonoBehaviour
{
	private int coinsNumber;
	public int CoinsNumber => coinsNumber;

	[HideInInspector] public UnityEvent ChangeCoinsNumber;

	public void AddCoins(int amount)
	{
		coinsNumber += amount;
		ChangeCoinsNumber.Invoke();
	}

	public void SetLoadingCoins(int amount)
	{
		coinsNumber = amount;
		ChangeCoinsNumber.Invoke();
	}
}