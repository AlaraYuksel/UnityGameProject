using UnityEngine;

public class LongRangeDestroyer : MonoBehaviour
{
    [SerializeField] private Camera cam;
    public float damagePerShot = 10f; // Tek seferde verilen hasar miktarý
    public float fireRate = 0.5f; // Saniyede kaç kere hasar verileceði (örneðin, 0.5 saniyede bir)

    private float nextTimeToFire = 0f; // Bir sonraki ateþleme zamaný
    private EnemyHealth enemyHealth;

    void Start()
    {
        if (cam == null)
        {
            Debug.LogWarning("Kamera atanmadý! Lütfen Inspector'dan bir kamera atayýn.");
        }
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + fireRate; // Bir sonraki ateþleme zamaný ayarlanýr
            ShootRay(); // Ray'i ateþle
        }
    }

    private void ShootRay()
    {
        if (cam == null)
        {
            Debug.LogError("Kamera atanmadý, iþlem yapýlmadý.");
            return;
        }

        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity))
        {
            if (hitInfo.collider != null && hitInfo.collider.CompareTag("lol"))
            {
                enemyHealth = hitInfo.collider.GetComponent<EnemyHealth>();

                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(damagePerShot); // Tek seferde verilen hasar miktarý uygulanýr
                }
            }
            else
            {
                Debug.Log("Tag 'lol' deðil, obje yok edilmedi.");
            }
        }
        else
        {
            Debug.Log("Hiçbir þeye çarpmadý.");
        }
    }
}
