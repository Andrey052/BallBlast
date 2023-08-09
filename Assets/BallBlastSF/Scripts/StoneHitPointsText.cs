using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Destructible))]
public class StoneHitPointsText : MonoBehaviour
{
    [SerializeField] private Text hitpointText;

    private Destructible destructible;
    private void Awake()
    {
        destructible = GetComponent<Destructible>();

        destructible.ChangeHitPoints.AddListener(OnChangeHitpoint);
    }
    private void OnDestroy()
    {
        destructible.ChangeHitPoints.RemoveListener(OnChangeHitpoint);
    }
    private void OnChangeHitpoint()
    {
        int hitPoints = destructible.GetHitPoints();

        if (hitPoints >= 1000)
            hitpointText.text = hitPoints / 1000 + "k";
        else
            hitpointText.text = hitPoints.ToString();
    }
}