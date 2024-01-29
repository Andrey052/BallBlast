using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelProgress : MonoBehaviour
{
	[SerializeField] private LevelState levelState;
	private int currentLevel = 1;
	public int CurrentLevel => currentLevel;

	[SerializeField] private CoinsAmount coinsAmount;
	private int coinsNumber;
	public int CoinsNumber => coinsNumber;

	[Header("Shop Bonus Turret Characteristics Settings")]
	[SerializeField] private Turret turret;

	[SerializeField] private Button bonusButton1;
	[SerializeField] private Button bonusButton2;
	[SerializeField] private Button bonusButton3;

	[SerializeField] private Text bonusButton1CostText;
	private int bonusButton1Cost;
	[SerializeField] private Text bonusButton2CostText;
	private int bonusButton2Cost;
	[SerializeField] private Text bonusButton3CostText;
	private int bonusButton3Cost;

	[SerializeField] private float turretFireRateBonusValue;
	[SerializeField] private int turretDamageBonusValue;
	[SerializeField] private int projectileAmountBonusValue;

	private float turretFireRate;
	private int turretDamage;
	private int projectileAmount;
	
	private void Awake() => Load();

	private void Start()
	{
		coinsAmount.SetLoadingCoins(coinsNumber);
		turret.SetTurretFireRateBonus(turretFireRate);
		turret.SetTurretDamageBonus(turretDamage);
		turret.SetProjectileAmountBonus(projectileAmount);

		levelState.Victory.AddListener(LevelIncreasing);
		levelState.Defeat.AddListener(SetStartLevelCoinAmount);

		int.TryParse(bonusButton1CostText.text, out bonusButton1Cost);
		int.TryParse(bonusButton2CostText.text, out bonusButton2Cost);
		int.TryParse(bonusButton3CostText.text, out bonusButton3Cost);
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.F1)) Reset();

		bonusButton1.interactable = turretFireRate > turretFireRateBonusValue && coinsAmount.CoinsNumber >= bonusButton1Cost;
		bonusButton2.interactable = coinsAmount.CoinsNumber >= bonusButton2Cost;
		bonusButton3.interactable = coinsAmount.CoinsNumber >= bonusButton3Cost;
	}

	public void BuyingTurretFireRateBonus()
	{
		coinsNumber = coinsAmount.CoinsNumber - bonusButton1Cost;
		coinsAmount.SetLoadingCoins(coinsNumber);

		turretFireRate -= turretFireRateBonusValue;
		turretFireRate = (float) Math.Round(turretFireRate, 2);
		turret.SetTurretFireRateBonus(turretFireRate);
		
		if (Mathf.Abs(turretFireRate - turretFireRateBonusValue) < 0.1 || coinsNumber < bonusButton1Cost) bonusButton1.interactable = false;
		Save();
	}

	public void BuyingTurretDamageBonus()
	{
		coinsNumber = coinsAmount.CoinsNumber - bonusButton2Cost;
		coinsAmount.SetLoadingCoins(coinsNumber);

		turretDamage += turretDamageBonusValue;
		turret.SetTurretDamageBonus(turretDamage);

		if (coinsNumber < bonusButton2Cost) bonusButton2.interactable = false;
		Save();
	}
	public void BuyingProjectileAmountBonus()
	{
		coinsNumber = coinsAmount.CoinsNumber - bonusButton3Cost;
		coinsAmount.SetLoadingCoins(coinsNumber);

		projectileAmount += projectileAmountBonusValue;
		turret.SetProjectileAmountBonus(projectileAmount);

		if (coinsNumber < bonusButton3Cost) bonusButton3.interactable = false;
		Save();
	}
	
	private void LevelIncreasing()
	{
		currentLevel++;
		Save();
	}

	private void SetStartLevelCoinAmount() => coinsNumber = PlayerPrefs.GetInt("CoinsNumber:CurrentCoinsNumber", 0);

	private void Save()
	{
		PlayerPrefs.SetInt("LevelProgress:CurrentLevel", currentLevel);
		PlayerPrefs.SetInt("CoinsNumber:CurrentCoinsNumber", coinsAmount.CoinsNumber);
		PlayerPrefs.SetFloat("TurretCharacteristics:FireRate", turretFireRate);
		PlayerPrefs.SetInt("TurretCharacteristics:Damage", turretDamage);
		PlayerPrefs.SetInt("TurretCharacteristics:ProjectileNumber", projectileAmount);
	}

	private void Load()
	{
		currentLevel = PlayerPrefs.GetInt("LevelProgress:CurrentLevel", 1);
		coinsNumber = PlayerPrefs.GetInt("CoinsNumber:CurrentCoinsNumber", 0);
		turretFireRate = PlayerPrefs.GetFloat("TurretCharacteristics:FireRate", turret.FireRate);
		turretDamage = PlayerPrefs.GetInt("TurretCharacteristics:Damage", turret.Damage);
		projectileAmount = PlayerPrefs.GetInt("TurretCharacteristics:ProjectileNumber", turret.ProjectileAmount);
	}

	private void Reset()
	{
		PlayerPrefs.DeleteAll();
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}