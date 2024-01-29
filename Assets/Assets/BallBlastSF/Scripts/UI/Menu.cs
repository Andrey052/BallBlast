using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
	public bool isPause;
	public bool isShop;

	[SerializeField] private GameObject MenuObj;
	[SerializeField] private GameObject WinMenuObj;
	[SerializeField] private GameObject LoseMenuObj;
	[SerializeField] private GameObject ShopMenu;
	[SerializeField] private GameObject stoneSpawner;
	[SerializeField] private GameObject cart;
	[SerializeField] private Button restartBtn;
	[SerializeField] private LevelState levelState;

	private void Awake() => PauseMode();

	private void Start() => restartBtn.interactable = false;

	private void Update()
	{
		if (!Input.GetKeyDown(KeyCode.Escape)) return;
		if (!isPause) PauseMode(); else Play();
	}

	public void Play()
	{
		if (MenuObj.activeSelf) MenuObj.SetActive(false);
		if (!stoneSpawner.activeSelf) stoneSpawner.SetActive(true);
		if (!cart.activeSelf) cart.SetActive(true);

		if (Resources.FindObjectsOfTypeAll(typeof(Stone)) is Stone[] { Length: > 0 } stones)
			foreach (Stone stone in stones)
				stone.gameObject.SetActive(true);

		if (Resources.FindObjectsOfTypeAll(typeof(Projectile)) is Projectile[] { Length: > 0 } projectiles)
			foreach (Projectile projectile in projectiles)
				projectile.gameObject.SetActive(true);

		if (Resources.FindObjectsOfTypeAll(typeof(Coin)) is Coin[] { Length: > 0 } coins)
			foreach (Coin coin in coins)
				coin.gameObject.SetActive(true);

		if (isPause) isPause = false;
		Time.timeScale = 1f;
	}

	public void PauseMode()
	{
		if (levelState.isWin) if (!WinMenuObj.activeSelf) WinMenuObj.SetActive(true);
		if (levelState.isLose) if (!LoseMenuObj.activeSelf) LoseMenuObj.SetActive(true);
		if (!levelState.isWin && !levelState.isLose) if (!MenuObj.activeSelf) MenuObj.SetActive(true);

		if (ShopMenu.activeSelf) ShopMenu.SetActive(false);
		if (stoneSpawner.activeSelf) stoneSpawner.SetActive(false);
		if (cart.activeSelf) cart.SetActive(false);

		if (FindObjectsOfType(typeof(Stone)) is Stone[] { Length: > 0 } stones)
			foreach (Stone stone in stones)
				stone.gameObject.SetActive(false);

		if (FindObjectsOfType(typeof(Projectile)) is Projectile[] { Length: > 0 } projectiles)
			foreach (Projectile projectile in projectiles)
				projectile.gameObject.SetActive(false);

		if (FindObjectsOfType(typeof(Coin)) is Coin[] { Length: > 0 } coins)
			foreach (Coin coin in coins)
				coin.gameObject.SetActive(false);

		if (Resources.FindObjectsOfTypeAll(typeof(Stone)).Length > 0 || Resources.FindObjectsOfTypeAll(typeof(Coin)).Length > 0)
			restartBtn.interactable = true;

		if (!isPause) isPause = true;
		if (isShop) isShop = false;
		Time.timeScale = 0f;
	}

	public void ShopMenuMode()
	{
		if (MenuObj.activeSelf) MenuObj.SetActive(false);
		if (WinMenuObj.activeSelf) WinMenuObj.SetActive(false);
		if (LoseMenuObj.activeSelf) LoseMenuObj.SetActive(false);
		if (!ShopMenu.activeSelf) ShopMenu.SetActive(true);

		if (isPause) isPause = false;
		if (!isShop) isShop = true;
	}

	public void RestartLevel()
	{
		if (isPause) isPause = false;
		if (isShop) isShop = false;
		Time.timeScale = 1f;
		if (!LevelState.isActiveStoneMovement) LevelState.isActiveStoneMovement = true;
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void Quit()
	{
		if (isPause) isPause = false;
		if (isShop) isShop = false;
		Time.timeScale = 1f;
		Application.Quit();
	}
}