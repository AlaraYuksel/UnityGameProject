using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent agent;
    public float transitionSpeed = 2.0f; // Geçiş hızı
    public LayerMask obstacleLayer;      // Engel tespit katmanı
    public float detectionRadius = 1.0f; // Engel tespit mesafesi

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.autoTraverseOffMeshLink = false; // Manuel geçiş yönetimi
        agent.baseOffset = 0.5f; // Ajanın zeminle uyumlu olması için baseOffset ayarı
        agent.speed = 2.0f; // Yavaş geçiş
        agent.acceleration = 5.0f; // Düşük ivme
    }

    void Update()
    {
        if (agent.isOnOffMeshLink) // NavMeshLink üzerinde olup olmadığını kontrol et
        {
            if (IsObstacleInFront())
            {
                Collider[] obstacles = Physics.OverlapSphere(transform.position, detectionRadius, obstacleLayer);
                foreach (Collider obstacle in obstacles)
                {
                    BreakObstacle(obstacle); // Engeli kırma işlemi
                }
            }
            else
            {
                StartCoroutine(UseNavMeshLink()); // Engel yoksa NavMeshLink'ten geçiş yap
            }
        }
    }

    bool IsObstacleInFront()
    {
        Collider[] obstacles = Physics.OverlapSphere(transform.position, detectionRadius, obstacleLayer);
        return obstacles.Length > 0;
    }

    void BreakObstacle(Collider obstacle)
    {
        
            Destroy(obstacle.gameObject); // Direkt yok et
        
    }

    IEnumerator UseNavMeshLink()
    {
        agent.isStopped = true;

        // Geçiş konumlarını al
        Vector3 startPos = agent.currentOffMeshLinkData.startPos;
        Vector3 endPos = agent.currentOffMeshLinkData.endPos;

        // Zemin seviyesini kontrol et ve doğru yükseklikte olmasını sağla
        Vector3 correctedStartPos = new Vector3(startPos.x, GetGroundHeight(startPos), startPos.z);
        Vector3 correctedEndPos = new Vector3(endPos.x, GetGroundHeight(endPos), endPos.z);

        // Geçiş süreci
        float t = 0; // Geçiş süreci için bir sayaç
        while (t < 1.0f)
        {
            t += Time.deltaTime * (transitionSpeed / Vector3.Distance(correctedStartPos, correctedEndPos));
            transform.position = Vector3.Lerp(correctedStartPos, correctedEndPos, t); // Yumuşak geçiş
            yield return null;
        }

        // Geçiş tamamlandığında agent'i yeniden başlat
        agent.CompleteOffMeshLink();
        agent.isStopped = false;
    }

    // Zemin seviyesini kontrol et ve uygun yükseklik değerini döndür
    float GetGroundHeight(Vector3 position)
    {
        RaycastHit hit;
        if (Physics.Raycast(position + Vector3.up * 10f, Vector3.down, out hit, Mathf.Infinity))
        {
            return hit.point.y;
        }
        return position.y; // Varsayılan olarak mevcut pozisyonu kullan
    }
}
