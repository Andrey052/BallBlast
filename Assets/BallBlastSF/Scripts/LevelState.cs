using UnityEngine;
using UnityEngine.Events;

public class LevelState : MonoBehaviour
{
    [SerializeField] private StoneSpawner spawner;
    [SerializeField] private Cart cart;

    [Space(5)]
    public UnityEvent Passed;
    public UnityEvent Defeat;

    public float timer;
    private bool chekPassed;

    private void Awake()
    {
        spawner.Completed.AddListener(OnSpawnComplited);
        cart.CollisionStone.AddListener(OnCartCollisionStone);
    }
    private void OnDestroy()
    {
        spawner.Completed.RemoveListener(OnSpawnComplited);
        cart.CollisionStone.RemoveListener(OnCartCollisionStone);
    }
    private void OnCartCollisionStone()
    {
        Defeat.Invoke();
    }
    private void OnSpawnComplited()
    {
        chekPassed = true;
    }
    private void Update()
    {
        timer += Time.deltaTime;

        if (timer > 0.5f)
        {
            if (chekPassed == true)
            {
                if (FindObjectOfType<Stone>().Length == 0)
                {
                    Passed.Invoke();
                }
            }
           timer = 0;
        }
    }
}
