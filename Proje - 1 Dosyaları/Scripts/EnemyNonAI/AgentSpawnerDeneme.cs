using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AgentSpawner : MonoBehaviour
{
    public GameObject agentPrefab; // Spawnlanacak zombi prefab'�
    public Transform target; // Zombilerin hedefi

    private void Start()
    {
        StartCoroutine(SpawnAgent());
    }
    private void Update()
    {
        
    }
    private IEnumerator SpawnAgent()
    {
        while (true)
        {
            // E�er LevelManager daha fazla spawn yap�lmas�na izin veriyorsa
            LevelManager levelManager = FindObjectOfType<LevelManager>();
            if (levelManager != null && levelManager.CanSpawn())
            {
                // Yeni zombiyi spawnla
                GameObject newAgent = Instantiate(agentPrefab, transform.position, Quaternion.identity);

                // Zombiye hedef atamas� yap
                NavMeshAgent agentComponent = newAgent.GetComponent<NavMeshAgent>();
                if (agentComponent != null && target != null)
                {
                    agentComponent.destination = target.position;
                }

                // LevelManager'a bildir
                levelManager.ZombieSpawned();
            }

            // Spawn aral���n� bekle
            LevelManager currentLevelManager = FindObjectOfType<LevelManager>();
            if (currentLevelManager != null)
            {
                yield return new WaitForSeconds(currentLevelManager.spawnInterval);
            }
        }
    }
}
