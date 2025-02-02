using UnityEngine;
using UnityEngine.AI;

public class EnemyCubeFollow : MonoBehaviour
{
    public NavMeshAgent enemycube; // D��man� kontrol eden NavMeshAgent
    public Transform player;       // Oyuncunun pozisyonu
    public float stoppingDistance = 3f; // Oyuncuya ne kadar yakla��nca duraca��
    public float attackCooldown = 1f;   // Sald�r� aral��� (saniye cinsinden)
    public int attackDamage = 10;       // Oyuncuya verilen hasar

    private bool isAttacking = false;   // Sald�r� durumunu kontrol etmek i�in
    private Animator animator;         // Animator bile�eni
    private float attackTimer = 0f;    // Sald�r� zamanlay�c�s�
    private PlayerHealth playerHealth; // Oyuncunun sa�l�k bile�eni

    void Start()
    {
        // Animator bile�enini al
        animator = GetComponent<Animator>();

        // NavMeshAgent'�n durma mesafesini ayarla
        enemycube.stoppingDistance = stoppingDistance;

        // PlayerHealth bile�enini al
        if (player != null)
        {
            playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth == null)
            {
                Debug.LogError("PlayerHealth bile�eni oyuncu �zerinde bulunamad�!");
            }
        }
    }

    void Update()
    {
        if (player == null) return;

        // Oyuncuyu hedef olarak belirle
        enemycube.SetDestination(player.position);

        // D��man�n oyuncuya olan 3D mesafesini kontrol et
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Sald�r� zamanlay�c�s�n� g�ncelle
        attackTimer += Time.deltaTime;

        if (distanceToPlayer <= stoppingDistance && attackTimer >= attackCooldown && !isAttacking)
        {
            // Oyuncunun ger�ekten d��man�n g�r�� hatt�nda olup olmad���n� kontrol et
            if (CanSeePlayer())
            {
                AttackPlayer();
            }
        }
        else if (distanceToPlayer > stoppingDistance)
        {
            // Y�r�me animasyonu
            animator.SetBool("isWalking", true);
            animator.SetBool("isBreaking", false);
        }
    }

    void AttackPlayer()
    {
        isAttacking = true; // Sald�r� durumu aktif
        attackTimer = 0f;   // Zamanlay�c�y� s�f�rla

        // Sald�r� animasyonunu ba�lat
        animator.SetBool("isBreaking", true);
        animator.SetBool("isWalking", false);

        // Oyuncuya hasar ver
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(attackDamage);
            Debug.Log($"D��man oyuncuya {attackDamage} hasar verdi!");
        }

        // Sald�r�y� sonland�r
        Invoke(nameof(EndAttack), 1f); // 1 saniye sonra sald�r�y� bitir
    }

    void EndAttack()
    {
        isAttacking = false;
        animator.SetBool("isBreaking", false);
        animator.SetBool("isWalking", true);
    }

    bool CanSeePlayer()
    {
        RaycastHit hit;

        // D��man ile oyuncu aras�ndaki hatt� kontrol et
        if (Physics.Linecast(transform.position, player.position, out hit))
        {
            // E�er raycast oyuncuya �arpt�ysa, d��man oyuncuyu g�rebiliyor
            if (hit.transform == player)
            {
                return true;
            }
        }

        // E�er raycast oyuncuya �arpmad�ysa, d��man oyuncuyu g�remiyor
        return false;
    }
}
