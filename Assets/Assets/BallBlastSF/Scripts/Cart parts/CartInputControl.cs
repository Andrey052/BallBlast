using UnityEngine;

public class CartInputControl : MonoBehaviour
{
	[SerializeField] private Camera mainCamera;
	[SerializeField] private Cart cart;
	[SerializeField] private Turret turret;

	private void Update()
	{
		Camera mainCamera = FindObjectOfType<Camera>();
		cart.SetMovementTarget(mainCamera.ScreenToWorldPoint( Input.mousePosition ) );
		if (Input.GetMouseButton(0)) turret.Fire();
	}
}