using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;

    public float speed = 5f;
    public float gravity = -9.8f;
    public float jumpHeight = 1f;

    private bool isGrounded;

    // Sprint ve crouch i�in de�i�kenler
    private bool lerpCrouch, crouching, sprinting;
    private float crouchTargetHeight;
    private float crouchSpeed = 5f; // ��melme h�z�

    // Ayak sesleri i�in AudioSource'lar
    public AudioSource leftFootstepAudioSource; // Sol ayak ses kayna��
    public AudioSource rightFootstepAudioSource; // Sa� ayak ses kayna��
    public AudioClip leftFootstepSound; // Sol ayak sesi clip'i
    public AudioClip rightFootstepSound; // Sa� ayak sesi clip'i
    public float footstepInterval = 0.5f; // Ayak sesi aral��� (saniye)
    private float footstepTimer = 0f; // Zamanlay�c�

    private bool isLeftFoot = true; // �lk ba�ta sol ayakla ba�la

    // Z�plama sesi
    public AudioSource jumpAudioSource; // Z�plama ses kayna��
    public AudioClip jumpSound; // Z�plama sesi clip'i

    void Start()
    {
        controller = GetComponent<CharacterController>();

        // E�er ses kaynaklar� atanmad�ysa, yeni bir tane ekle
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

        leftFootstepAudioSource.loop = false; // Sol ayak sesinin s�rekli �almamas� i�in loop kapal�
        rightFootstepAudioSource.loop = false; // Sa� ayak sesinin s�rekli �almamas� i�in loop kapal�
        jumpAudioSource.loop = false; // Z�plama sesinin s�rekli �almamas� i�in loop kapal�
    }

    void Update()
    {
        // Zemin kontrol�
        isGrounded = controller.isGrounded;

        // ��melme y�ksekli�i yumu�atma
        if (lerpCrouch)
        {
            controller.height = Mathf.Lerp(controller.height, crouchTargetHeight, crouchSpeed * Time.deltaTime);
            if (Mathf.Abs(controller.height - crouchTargetHeight) < 0.01f)
            {
                controller.height = crouchTargetHeight;
                lerpCrouch = false;
            }
        }

        // Yer�ekimi etkisi
        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
        }
        else
        {
            playerVelocity.y += gravity * Time.deltaTime;
        }

        // Ayak sesi �alma
        if (isGrounded && (footstepTimer <= 0f) && (controller.velocity.magnitude > 0.1f)) // Hareket varsa
        {
            PlayFootstepSound();

            // Zamanlay�c�y� s�f�rla
            if (sprinting)
                footstepTimer = footstepInterval / 2;
            else
                footstepTimer = footstepInterval;
        }

        // Zamanlay�c�y� g�ncelle
        if (footstepTimer > 0f)
        {
            footstepTimer -= Time.deltaTime;
        }
    }

    public void ProcessMove(Vector2 input)
    {
        // Hareket y�n� ve h�z
        Vector3 moveDirection = transform.TransformDirection(new Vector3(input.x, 0, input.y)) * speed;

        // Hareket uygula
        controller.Move((moveDirection + playerVelocity) * Time.deltaTime);
    }

    public void Jump()
    {
        if (isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

            // Z�plama sesini �al
            if (jumpAudioSource != null && jumpSound != null)
            {
                jumpAudioSource.PlayOneShot(jumpSound);
            }
        }
    }

    public void Crouch()
    {
        crouching = !crouching;
        crouchTargetHeight = crouching ? 1f : 2f; // ��melme y�ksekli�i
        lerpCrouch = true;
    }

    public void Sprint()
    {
        sprinting = !sprinting;
        speed = sprinting ? 8f : 5f; // Sprint h�z�
    }

    // Ayak sesini �alan fonksiyon
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

        // Ayak sesini s�ras�yla de�i�tirmek
        isLeftFoot = !isLeftFoot;
    }
}
