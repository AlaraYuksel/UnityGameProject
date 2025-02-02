using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    [Header("Gun Settings")]
    [SerializeField] private Camera cam;
    private WeaponData currentWeaponData;
    private float nextTimeToFire = 0f;

    [Header("Ammo Settings")]
    private int currentAmmo;
    private int totalAmmo;
    public bool isReloading = false;

    [Header("UI Elements")]
    public TextMeshProUGUI ammoText;
    [SerializeField] private Image hitEffectImage1;
    [SerializeField] private Image hitEffectImage2;
    [SerializeField] private Image hitEffectImage3;
    [SerializeField] private Image hitEffectImage4;
    public float effectDuration = 0.2f;

    [Header("Bullet Settings")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletSpawnPoint;

    [Header("Sound Effects")]
    private AudioClip shootSound;
    private AudioClip reloadSound;
    private AudioSource audioSource;


    [Header("AmmoBox")]
    private MoneyManager moneyManager;

    private void Start()
    {
        moneyManager = GetComponent<MoneyManager>();
        if (isReloading) return;

        // Reload tuþuna basýldýðýnda ve gerekli koþullar saðlandýðýnda reload baþlat
        if (Input.GetKeyDown(KeyCode.R) && currentAmmo < currentWeaponData.maxAmmo && totalAmmo > 0)
        {
            // Eðer daha önce bir reload iþlemi baþlamýþsa durdur
            if (reloadCoroutine != null)
            {
                StopCoroutine(reloadCoroutine);
            }
            reloadCoroutine = StartCoroutine(Reload());
            return;
        }



        if (currentWeaponData != null)
        {
            currentAmmo = currentWeaponData.currentAmmo >= 0 ? currentWeaponData.currentAmmo : currentWeaponData.maxAmmo;
            totalAmmo = currentWeaponData.totalAmmo;

            UpdateAmmoUI();
            shootSound = currentWeaponData.shootSound;
            reloadSound = currentWeaponData.reloadSound;
        }

        hitEffectImage1.enabled = false;
        hitEffectImage2.enabled = false;
        hitEffectImage3.enabled = false;
        hitEffectImage4.enabled = false;

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void Update()
    {
        if (isReloading) return;

        if (Input.GetKeyDown(KeyCode.R) && currentAmmo < currentWeaponData.maxAmmo && totalAmmo > 0)
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetMouseButton(0) && Time.time >= nextTimeToFire)
        {
            if (currentAmmo <= 0)
            {
                Debug.Log("Mermi bitti! Yeniden doldur.");
                return;
            }

            nextTimeToFire = Time.time + currentWeaponData.fireRate;
            Shoot();
        }
    }

    private void Shoot()
    {
        currentAmmo--;
        currentWeaponData.currentAmmo = currentAmmo;
        UpdateAmmoUI();

        if (shootSound != null)
        {
            audioSource.PlayOneShot(shootSound);
        }

        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit[] hits = Physics.RaycastAll(ray, currentWeaponData.range);

        foreach (RaycastHit hit in hits)
        {
            EnemyHealth enemyHealth = hit.transform.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                float damage = currentWeaponData.damage;
                if (hit.collider.CompareTag("Head"))
                {
                    damage *= 2;
                    Debug.Log("Headshot! 2x hasar uygulandý.");
                }

                enemyHealth.TakeDamage(damage);
                ShowHitEffect();
                break;
            }
        }
    }

    private void ShowHitEffect()
    {
        hitEffectImage1.enabled = true;
        hitEffectImage2.enabled = true;
        hitEffectImage3.enabled = true;
        hitEffectImage4.enabled = true;

        StartCoroutine(FadeOutEffect(hitEffectImage1));
        StartCoroutine(FadeOutEffect(hitEffectImage2));
        StartCoroutine(FadeOutEffect(hitEffectImage3));
        StartCoroutine(FadeOutEffect(hitEffectImage4));
    }

    private IEnumerator FadeOutEffect(Image effectImage)
    {
        Color startColor = effectImage.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0f);

        float elapsedTime = 0f;
        while (elapsedTime < effectDuration)
        {
            elapsedTime += Time.deltaTime;
            effectImage.color = Color.Lerp(startColor, endColor, elapsedTime / effectDuration);
            yield return null;
        }

        effectImage.enabled = false;
        effectImage.color = Color.white;
    }
    private Coroutine reloadCoroutine;

    private IEnumerator Reload()
    {
        isReloading = true;
        ammoText.text = "Yeniden Dolduruluyor...";

        if (reloadSound != null)
        {
            audioSource.PlayOneShot(reloadSound);
        }

        yield return new WaitForSeconds(currentWeaponData.reloadTime);

        int ammoNeeded = currentWeaponData.maxAmmo - currentAmmo;
        int ammoToReload = Mathf.Min(ammoNeeded, totalAmmo);

        currentAmmo += ammoToReload;
        totalAmmo -= ammoToReload;

        currentWeaponData.currentAmmo = currentAmmo;
        currentWeaponData.totalAmmo = totalAmmo;

        UpdateAmmoUI();
        isReloading = false;
    }

    public void StopReloading()
    {
        if (reloadCoroutine != null)
        {
            StopCoroutine(reloadCoroutine); // Coroutine'i durdur
            reloadCoroutine = null; // Coroutine referansýný sýfýrla
            isReloading = false; // Reload durumu sýfýrlanýr

            // UI güncellemesi
            ammoText.text = "Ammo: " + currentAmmo + " / " + totalAmmo;
        }
    }



    public void UpdateAmmoUI()
    {
        if (ammoText != null)
        {
            ammoText.text = "Ammo: " + currentAmmo + " / " + totalAmmo;
        }
    }

    public void SetWeaponData(WeaponData newWeaponData)
    {
        if (currentWeaponData != null)
        {
            currentWeaponData.currentAmmo = currentAmmo;
            currentWeaponData.totalAmmo = totalAmmo;
        }

        currentWeaponData = newWeaponData;

        currentAmmo = currentWeaponData.currentAmmo >= 0 ? currentWeaponData.currentAmmo : currentWeaponData.maxAmmo;
        totalAmmo = currentWeaponData.totalAmmo;

        UpdateAmmoUI();

        shootSound = currentWeaponData.shootSound;
        reloadSound = currentWeaponData.reloadSound;
    }
    
    public void AddAmmo()
    {
        if (moneyManager.money >= 500)
        {
            moneyManager.DeductMoney(500);
            totalAmmo = currentWeaponData.mustBeTotal;
            currentWeaponData.totalAmmo = currentWeaponData.mustBeTotal;
            UpdateAmmoUI();
        }
        else
        {
            Debug.Log("Yeterli para yok!");
        }
    }
}
