using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;

    public float speed = 5f;
    public float gravity = -9.8f;
    public float jumpHeight = 1f;

    private bool isGrounded;

    // Sprint ve crouch için deðiþkenler
    private bool lerpCrouch, crouching, sprinting;
    private float crouchTargetHeight;
    private float crouchSpeed = 5f; // Çömelme hýzý

    // Ayak sesleri için AudioSource'lar
    public AudioSource leftFootstepAudioSource; // Sol ayak ses kaynaðý
    public AudioSource rightFootstepAudioSource; // Sað ayak ses kaynaðý
    public AudioClip leftFootstepSound; // Sol ayak sesi clip'i
    public AudioClip rightFootstepSound; // Sað ayak sesi clip'i
    public float footstepInterval = 0.5f; // Ayak sesi aralýðý (saniye)
    private float footstepTimer = 0f; // Zamanlayýcý

    private bool isLeftFoot = true; // Ýlk baþta sol ayakla baþla

    // Zýplama sesi
    public AudioSource jumpAudioSource; // Zýplama ses kaynaðý
    public AudioClip jumpSound; // Zýplama sesi clip'i

    void Start()
    {
        controller = GetComponent<CharacterController>();

        // Eðer ses kaynaklarý atanmadýysa, yeni bir tane ekle
        if (leftFootstepAudioSource == null)
        {
            leftFootstepAudioSource = gameObject.AddComponent<AudioSource>();
        }
        if (rightFootstepAudioSource == null)
        {
            rightFootstepAudioSource = gameObject.AddComponent<AudioSource>();
        }
        if (jumpAudioSource == null)
        {
            jumpAudioSource = gameObject.AddComponent<AudioSource>();
        }

        leftFootstepAudioSource.loop = false; // Sol ayak sesinin sürekli çalmamasý için loop kapalý
        rightFootstepAudioSource.loop = false; // Sað ayak sesinin sürekli çalmamasý için loop kapalý
        jumpAudioSource.loop = false; // Zýplama sesinin sürekli çalmamasý için loop kapalý
    }

    void Update()
    {
        // Zemin kontrolü
        isGrounded = controller.isGrounded;

        // Çömelme yüksekliði yumuþatma
        if (lerpCrouch)
        {
            controller.height = Mathf.Lerp(controller.height, crouchTargetHeight, crouchSpeed * Time.deltaTime);
            if (Mathf.Abs(controller.height - crouchTargetHeight) < 0.01f)
            {
                controller.height = crouchTargetHeight;
                lerpCrouch = false;
            }
        }

        // Yerçekimi etkisi
        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
        }
        else
        {
            playerVelocity.y += gravity * Time.deltaTime;
        }

        // Ayak sesi çalma
        if (isGrounded && (footstepTimer <= 0f) && (controller.velocity.magnitude > 0.1f)) // Hareket varsa
        {
            PlayFootstepSound();

            // Zamanlayýcýyý sýfýrla
            if (sprinting)
                footstepTimer = footstepInterval / 2;
            else
                footstepTimer = footstepInterval;
        }

        // Zamanlayýcýyý güncelle
        if (footstepTimer > 0f)
        {
            footstepTimer -= Time.deltaTime;
        }
    }

    public void ProcessMove(Vector2 input)
    {
        // Hareket yönü ve hýz
        Vector3 moveDirection = transform.TransformDirection(new Vector3(input.x, 0, input.y)) * speed;

        // Hareket uygula
        controller.Move((moveDirection + playerVelocity) * Time.deltaTime);
    }

    public void Jump()
    {
        if (isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

            // Zýplama sesini çal
            if (jumpAudioSource != null && jumpSound != null)
            {
                jumpAudioSource.PlayOneShot(jumpSound);
            }
        }
    }

    public void Crouch()
    {
        crouching = !crouching;
        crouchTargetHeight = crouching ? 1f : 2f; // Çömelme yüksekliði
        lerpCrouch = true;
    }

    public void Sprint()
    {
        sprinting = !sprinting;
        speed = sprinting ? 8f : 5f; // Sprint hýzý
    }

    // Ayak sesini çalan fonksiyon
    private void PlayFootstepSound()
    {
        if (isLeftFoot)
        {
            if (leftFootstepAudioSource != null && leftFootstepSound != null)
            {
                leftFootstepAudioSource.PlayOneShot(leftFootstepSound);
            }
        }
        else
        {
            if (rightFootstepAudioSource != null && rightFootstepSound != null)
            {
                rightFootstepAudioSource.PlayOneShot(rightFootstepSound);
            }
        }

        // Ayak sesini sýrasýyla deðiþtirmek
        isLeftFoot = !isLeftFoot;
    }
}
