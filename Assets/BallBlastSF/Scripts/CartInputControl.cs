using UnityEngine;
public class CartInputControl : MonoBehaviour
{
    [SerializeField] private Cart cart;
    [SerializeField] private Turret turret;

    void Update()
    {
        cart.SetMovementTarget( Camera.main.ScreenToWorldPoint( Input.mousePosition ));

        if (Input.GetMouseButton(0) == true)
        {
            turret.Fire();
        }
    }
}
