using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;  // Merminin h�z�
    public float lifeTime = 5f;  // Merminin yok olma s�resi
    public int damage = 10;  // Merminin hasar�

    private void Start()
    {
        Destroy(gameObject, lifeTime);  // Belirli bir s�re sonra mermiyi yok et
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);  // Mermiyi ileri hareket ettir
    }

    private void OnTriggerEnter(Collider other)
    {
        // D��mana �arparsa hasar ver
        EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(damage);
        }

        // Mermiyi yok et
        Destroy(gameObject);
    }
}
