using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class LevelState : MonoBehaviour
{
	[SerializeField] private StoneSpawner spawner;
	[SerializeField] private Cart cart;
	[SerializeField] private GameObject loseMenu;
	[SerializeField] private GameObject winMenu;
	[SerializeField] private Menu menu;

	[Space(5)]
	public UnityEvent Victory;
	public UnityEvent Defeat;

	[SerializeField] private GameObject VictorySoundEffect;
	[SerializeField] private GameObject DefeatSoundEffect;

	public bool isWin;
	public bool isLose;

	private float timer;
	private bool checkPassed;

	private bool immortality;
	[SerializeField] private int establishedBonus1Probability;
	[SerializeField] private float establishedBonus1MinTime;
	[SerializeField] private float establishedBonus1MaxTime;

	[SerializeField] private int establishedBonus2Probability;
	[SerializeField] private float establishedBonus2MinTime;
	[SerializeField] private float establishedBonus2MaxTime;

	public static bool isActiveStoneMovement = true;
	
	private void Awake()
	{
		spawner.Completed.AddListener(OnSpawnCompleted);
		cart.CollisionStone.AddListener(OnCartCollisionStone);
	}

	private void Update()
	{
		timer += Time.deltaTime;
		if (timer <= 0.5) return;

		if (checkPassed && FindObjectsOfType<Stone>().Length == 0 && FindObjectsOfType<Coin>().Length == 0)
		{
			Victory.Invoke();
			winMenu.SetActive(true);
			menu.enabled = false;
			Time.timeScale = 0f;
			isWin = true;
			Instantiate(VictorySoundEffect);
		}

		timer = 0;
	}

	private void OnDestroy()
	{
		spawner.Completed.RemoveListener(OnSpawnCompleted);
		cart.CollisionStone.RemoveListener(OnCartCollisionStone);
	}

	private void OnCartCollisionStone()
	{
		if (immortality) return;
		Defeat.Invoke();
		loseMenu.SetActive(true);
		menu.enabled = false;
		Time.timeScale = 0f;
		isLose = true;
		Instantiate(DefeatSoundEffect);
	}

	private void OnSpawnCompleted() => checkPassed = true;
	
	public void TryToSetStopStoneMovement()
	{
		if (Random.Range(1, 101) < 100 - establishedBonus1Probability || !isActiveStoneMovement) return;

		float time = Random.Range(establishedBonus1MinTime, establishedBonus1MaxTime);
		isActiveStoneMovement = false;
		StartCoroutine(ReturnStoneMovement(time));
	}

	public void TryToSetImmortality()
	{
		if (Random.Range(1, 101) < 100 - establishedBonus2Probability || immortality) return;

		float time = Random.Range(establishedBonus2MinTime, establishedBonus2MaxTime);
		immortality = true;
		StartCoroutine(ReturnVulnerability(time));
	}
	IEnumerator ReturnStoneMovement(float time)
	{
		yield return new WaitForSeconds(time);
		isActiveStoneMovement = true;
	}

	IEnumerator ReturnVulnerability(float time)
	{
		yield return new WaitForSeconds(time);
		immortality = false;
	}
}