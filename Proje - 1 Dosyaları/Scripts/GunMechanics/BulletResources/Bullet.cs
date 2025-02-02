using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;  // Merminin hýzý
    public float lifeTime = 5f;  // Merminin yok olma süresi
    public int damage = 10;  // Merminin hasarý

    private void Start()
    {
        Destroy(gameObject, lifeTime);  // Belirli bir süre sonra mermiyi yok et
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);  // Mermiyi ileri hareket ettir
    }

    private void OnTriggerEnter(Collider other)
    {
        // Düþmana çarparsa hasar ver
        EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(damage);
        }

        // Mermiyi yok et
        Destroy(gameObject);
    }
}
