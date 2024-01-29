using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(StoneMovement))]
public class Stone : Destructible
{
	public enum Size { Small, Normal, Big, Huge }

	[SerializeField] private Size size;
	[SerializeField] private float spawnUpForce;

	private StoneMovement movement;
	private UILevelProgress uiLevelProgress;
	private LevelState levelState;

	[SerializeField] private Color[] stoneColors;
	private List<int> colorNumbers;

	[SerializeField] private GameObject StoneDestroyingSoundEffect;

	private void Awake()
	{
		movement = GetComponent<StoneMovement>();
		colorNumbers = Enumerable.Range(0, stoneColors.Length).ToList();
		Die.AddListener(OnStoneDestroyed);
		SetSize(size);
	}

	private void OnDestroy() => Die.RemoveListener(OnStoneDestroyed);

	private void OnStoneDestroyed()
	{
		if (size is not Size.Small) SpawnStones();
		Instantiate(StoneDestroyingSoundEffect);
		Destroy(gameObject);
	}

	private void SpawnStones()
	{
		for (int i = 0; i < 2; i++)
		{
			Stone stone = Instantiate(this, transform.position, Quaternion.identity);
			stone.SetSize(size - 1);
			stone.maxHitPoints = Math.Clamp(maxHitPoints / 2, 1, maxHitPoints);
			stone.transform.GetChild(0).GetComponent<SpriteRenderer>().color = stoneColors[colorNumbers[Random.Range(0, colorNumbers.Count)]];
			stone.movement.AddVerticalVelocity(spawnUpForce);
			stone.movement.SetHorizontalDirection(i % 2 * 2 - 1);

			stone.Die.AddListener(uiLevelProgress.FillBar);
			stone.SetUILevelProgress(uiLevelProgress);

			stone.Die.AddListener(levelState.TryToSetImmortality);
			stone.Die.AddListener(levelState.TryToSetStopStoneMovement);
			stone.SetLevelState(levelState);
		}
	}

	public void SetSize(Size size)
	{
		if (size < 0) return;

		transform.localScale = GetVectorFromSize(size);
		this.size = size;
	}

	private Vector3 GetVectorFromSize(Size size)
	{
		/*switch (size)
		{
			case Size.Huge: return new Vector3(1, 1, 1);
			case Size.Big: return new Vector3(.75f, .75f, .75f);
			case Size.Normal: return new Vector3(.6f, .6f, .6f);
			case Size.Small: return new Vector3(.4f, .4f, .4f);
		}
		return Vector3.one;*/

		return size switch
		{
			Size.Huge => Vector3.one,
			Size.Big => new Vector3(.75f, .75f, .75f),
			Size.Normal => new Vector3(.6f, .6f, .6f),
			Size.Small => new Vector3(.4f, .4f, .4f),
			_ => Vector3.one,
		};
	}

	public void SetUILevelProgress(UILevelProgress levelProgressReference) => uiLevelProgress = levelProgressReference;
	public void SetLevelState(LevelState levelStateReference) => levelState = levelStateReference;
}