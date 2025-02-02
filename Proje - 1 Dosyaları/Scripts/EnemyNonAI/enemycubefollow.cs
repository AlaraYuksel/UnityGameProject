using UnityEngine;
using UnityEngine.AI;

public class EnemyCubeFollow : MonoBehaviour
{
    public NavMeshAgent enemycube; // Düþmaný kontrol eden NavMeshAgent
    public Transform player;       // Oyuncunun pozisyonu
    public float stoppingDistance = 3f; // Oyuncuya ne kadar yaklaþýnca duracaðý
    public float attackCooldown = 1f;   // Saldýrý aralýðý (saniye cinsinden)
    public int attackDamage = 10;       // Oyuncuya verilen hasar

    private bool isAttacking = false;   // Saldýrý durumunu kontrol etmek için
    private Animator animator;         // Animator bileþeni
    private float attackTimer = 0f;    // Saldýrý zamanlayýcýsý
    private PlayerHealth playerHealth; // Oyuncunun saðlýk bileþeni

    void Start()
    {
        // Animator bileþenini al
        animator = GetComponent<Animator>();

        // NavMeshAgent'ýn durma mesafesini ayarla
        enemycube.stoppingDistance = stoppingDistance;

        // PlayerHealth bileþenini al
        if (player != null)
        {
            playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth == null)
            {
                Debug.LogError("PlayerHealth bileþeni oyuncu üzerinde bulunamadý!");
            }
        }
    }

    void Update()
    {
        if (player == null) return;

        // Oyuncuyu hedef olarak belirle
        enemycube.SetDestination(player.position);

        // Düþmanýn oyuncuya olan 3D mesafesini kontrol et
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Saldýrý zamanlayýcýsýný güncelle
        attackTimer += Time.deltaTime;

        if (distanceToPlayer <= stoppingDistance && attackTimer >= attackCooldown && !isAttacking)
        {
            // Oyuncunun gerçekten düþmanýn görüþ hattýnda olup olmadýðýný kontrol et
            if (CanSeePlayer())
            {
                AttackPlayer();
            }
        }
        else if (distanceToPlayer > stoppingDistance)
        {
            // Yürüme animasyonu
            animator.SetBool("isWalking", true);
            animator.SetBool("isBreaking", false);
        }
    }

    void AttackPlayer()
    {
        isAttacking = true; // Saldýrý durumu aktif
        attackTimer = 0f;   // Zamanlayýcýyý sýfýrla

        // Saldýrý animasyonunu baþlat
        animator.SetBool("isBreaking", true);
        animator.SetBool("isWalking", false);

        // Oyuncuya hasar ver
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(attackDamage);
            Debug.Log($"Düþman oyuncuya {attackDamage} hasar verdi!");
        }

        // Saldýrýyý sonlandýr
        Invoke(nameof(EndAttack), 1f); // 1 saniye sonra saldýrýyý bitir
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

        // Düþman ile oyuncu arasýndaki hattý kontrol et
        if (Physics.Linecast(transform.position, player.position, out hit))
        {
            // Eðer raycast oyuncuya çarptýysa, düþman oyuncuyu görebiliyor
            if (hit.transform == player)
            {
                return true;
            }
        }

        // Eðer raycast oyuncuya çarpmadýysa, düþman oyuncuyu göremiyor
        return false;
    }
}
