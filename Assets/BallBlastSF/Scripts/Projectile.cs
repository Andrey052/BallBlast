using UnityEngine;
public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float lifeTime;

    private int damage;
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }        
    void Update()
    {
        transform.position += transform.up * speed * Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destructible destructible = collision.transform.root.GetComponent<Destructible>();

        if (destructible != null)
        {
            destructible.ApplyDamage(damage);
        }
        Destroy(gameObject);
    }
    public void SetDamage(int damage)
    {
        this.damage = damage;
    }
}