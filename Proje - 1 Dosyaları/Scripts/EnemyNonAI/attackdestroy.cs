using UnityEngine;

public class LongRangeDestroyer : MonoBehaviour
{
    [SerializeField] private Camera cam;
    public float damagePerShot = 10f; // Tek seferde verilen hasar miktar�
    public float fireRate = 0.5f; // Saniyede ka� kere hasar verilece�i (�rne�in, 0.5 saniyede bir)

    private float nextTimeToFire = 0f; // Bir sonraki ate�leme zaman�
    private EnemyHealth enemyHealth;

    void Start()
    {
        if (cam == null)
        {
            Debug.LogWarning("Kamera atanmad�! L�tfen Inspector'dan bir kamera atay�n.");
        }
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + fireRate; // Bir sonraki ate�leme zaman� ayarlan�r
            ShootRay(); // Ray'i ate�le
        }
    }

    private void ShootRay()
    {
        if (cam == null)
        {
            Debug.LogError("Kamera atanmad�, i�lem yap�lmad�.");
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
                    enemyHealth.TakeDamage(damagePerShot); // Tek seferde verilen hasar miktar� uygulan�r
                }
            }
            else
            {
                Debug.Log("Tag 'lol' de�il, obje yok edilmedi.");
            }
        }
        else
        {
            Debug.Log("Hi�bir �eye �arpmad�.");
        }
    }
}
