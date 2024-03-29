using UnityEngine;
using UnityEngine.UI;

public class UILevelProgress : MonoBehaviour
{
	[SerializeField] private Text currentLevelText;
	[SerializeField] private Text nextLevelText;
	[SerializeField] private Image progressBar;
	[SerializeField] private LevelProgress levelProgress;
	[SerializeField] private StoneSpawner stoneSpawner;

	private float fillAmountStep;

	private void Start()
	{
		currentLevelText.text = levelProgress.CurrentLevel.ToString();
		nextLevelText.text = (levelProgress.CurrentLevel + 1).ToString();
		progressBar.fillAmount = 0;
	}

	// ��������� StoneSpawner �� ������� � ����� ������ ����, �� �� ����� ���� ����� ���������� ������, ������� ������ StoneSpawner ��������� ��� ���������, ����� ����� �����
	public void SetFillAmountStep(float step) => fillAmountStep = step;
	public void FillBar() => progressBar.fillAmount += fillAmountStep;
}