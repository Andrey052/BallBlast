using System;
using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

public class StoneSpawner : MonoBehaviour
{
	[Header("Spawn")]
	[SerializeField] private Stone stonePrefab;
	[SerializeField] private Transform[] spawnPoints;
	[SerializeField] private float spawnRate;

	[Header("Balance")]
	[SerializeField] private Turret turret;
	[SerializeField] [Range(0.0f, 1.0f)] private float minHitPointsPercentage;
	[SerializeField] private float maxHitPointsRate;

	[Header("Progress Bar")]
	[SerializeField] private LevelProgress levelProgress;
	[SerializeField] private UILevelProgress uiLevelProgress;

	[SerializeField] private LevelState levelState;

	private int[] stonesSizeArr;					//	������ �������� ��������������� �������� ��� ������
	private int stonesSizeArrIndex;					//	������ ������� ��������, ����� ������������� ��� ������ ������ �����
	private int allStonesAmountAtLevel;				//	�������� ���������� ������ ���� ������ �� ������ (� ������ ������� �� ������������ �������)
	private int currentLevelStonesRespawnAmount;	//	���������� ����������� StoneSpawner ������ (���������� �������), ����� �������� ������

	private float timer;
	private float amountSpawned;

	private int stoneMaxHitPoints;
	private int stoneMinHitPoints;
	
	[SerializeField] private Color[] stoneColors;
	private List<int> colorNumbers;

	[HideInInspector] public UnityEvent Completed;

	[SerializeField] private GameObject StoneSpawningSoundEffect;

	private void Awake()
	{
		currentLevelStonesRespawnAmount = levelProgress.CurrentLevel;
		SetSizeForRespawnStones();
		foreach (int stonesSizeArrElement in stonesSizeArr) AddStonesAmount(stonesSizeArrElement);
		uiLevelProgress.SetFillAmountStep(1f / allStonesAmountAtLevel);
	}

	//	����� ��������� ������� �������� ���������� ���������
	private void SetSizeForRespawnStones()
	{
		stonesSizeArr = new int[currentLevelStonesRespawnAmount];	//	������������� ������� ������, ������ ���������� ���������� StoneSpawner ������
		for (int i = 0; i < stonesSizeArr.Length; i++) stonesSizeArr[i] = Random.Range(0, 4);
	}

	//	�����, �������������� �������� ����� ���� ������ �� ������, ������ �� �������, �������� ��� ������� �� ������������ �������
	private void AddStonesAmount(int stoneSize)
	{
		switch (stoneSize)
		{
			case 0:
				allStonesAmountAtLevel++;
				break;
			case 1:
				allStonesAmountAtLevel += 3;
				break;
			case 2:
				allStonesAmountAtLevel += 7;
				break;
			case 3:
				allStonesAmountAtLevel += 15;
				break;
		}
	}

	private void Start()
	{
		int damagePerSecond = (int) (turret.Damage * turret.ProjectileAmount * (1 / turret.FireRate));

		stoneMaxHitPoints = (int) (damagePerSecond * maxHitPointsRate);
		stoneMinHitPoints = (int) (stoneMaxHitPoints * minHitPointsPercentage);

		timer = spawnRate;

		colorNumbers = Enumerable.Range(0, stoneColors.Length).ToList();
		Instantiate(StoneSpawningSoundEffect);
	}

	private void Update()
	{
		timer += Time.deltaTime;
		
		if (timer >= spawnRate)
		{
			Spawn();
			timer = 0;
		}

		if (Math.Abs(amountSpawned - currentLevelStonesRespawnAmount) > 0) return;

		enabled = false;
		Completed.Invoke();
	}

	private void Spawn()
	{
		Stone stone = Instantiate(stonePrefab, spawnPoints[Random.Range(0, spawnPoints.Length)].position, Quaternion.identity);
		
		Stone.Size size = (Stone.Size) stonesSizeArr[stonesSizeArrIndex];	//	������ ������ ������� �� �������
		stone.SetSize(size);
		stonesSizeArrIndex++;

		//stone.maxHitPoints = Random.Range(stoneMinHitPoints, stoneMaxHitPoints + 1);
		stone.maxHitPoints = 1;

		stone.transform.GetChild(0).GetComponent<SpriteRenderer>().color = stoneColors[colorNumbers[Random.Range(0, colorNumbers.Count)]];

		stone.Die.AddListener(uiLevelProgress.FillBar);
		stone.SetUILevelProgress(uiLevelProgress);	//	������ �� UILevelProgress ��������� ���������� �����

		stone.Die.AddListener(levelState.TryToSetImmortality);
		stone.Die.AddListener(levelState.TryToSetStopStoneMovement);
		stone.SetLevelState(levelState);    //	������ �� LevelState ��������� ���������� �����

		amountSpawned++;
	}
}