using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Player))]
public class PlayerShooting : MonoBehaviour
{
    private PlayerControls controls;
    private Animator animator;
    private Player playerRef;


    [Header("Shooting")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireRate = 0.2f;
    [SerializeField] private float range = 50f;
    [SerializeField] private int damage = 10;

    [Header("Effects")]
    [SerializeField] private GameObject muzzleFlashPrefab;
    [SerializeField] private GameObject hitEffectPrefab;
    [SerializeField] private TrailRenderer bulletTrailPrefab;

    [Header("Layers")]
    [SerializeField] private LayerMask hitLayers = ~0;

    private float nextFireTime;
    private bool isFiring;

    private void Awake()
    {
        // cache references only
        playerRef = GetComponent<Player>();
      
        animator = GetComponentInChildren<Animator>();

        if (firePoint == null)
        {
            Debug.LogWarning("PlayerShooting: No fire point assigned. Using player transform.");
            firePoint = transform;
        }
    }
    private void Start()
    {
        AssignInputEvents();
    }

    private void AssignInputEvents()
    {
        controls = playerRef.controls;
        controls.Character.Fire.started += OnFireStarted;
        controls.Character.Fire.canceled += OnFireCanceled;

    }

    private void OnFireStarted(InputAction.CallbackContext ctx) => isFiring = true;
    private void OnFireCanceled(InputAction.CallbackContext ctx) => isFiring = false;

    private void Update()
    {
        if (isFiring && Time.time >= nextFireTime)
        {
            Debug.LogWarning("PlayerShooting: we are shooting");
            Fire();
            nextFireTime = Time.time + fireRate;
        }
    }

    private void Fire()
    {
        // Trigger animation
        if (animator != null)
        {
            animator.SetTrigger("Fire");
        }

        // Muzzle flash
        if (muzzleFlashPrefab != null)
        {
            GameObject flash = Instantiate(muzzleFlashPrefab, firePoint.position, firePoint.rotation);
            Destroy(flash, 0.1f);
        }

        // Raycast for hit detection
        Vector3 shootDirection = transform.forward;
        Ray ray = new Ray(firePoint.position, shootDirection);

        if (Physics.Raycast(ray, out RaycastHit hit, range, hitLayers))
        {
            // Spawn hit effect
            if (hitEffectPrefab != null)
            {
                GameObject hitEffect = Instantiate(hitEffectPrefab, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(hitEffect, 1f);
            }

            // Spawn bullet trail
            if (bulletTrailPrefab != null)
            {
                SpawnTrail(firePoint.position, hit.point);
            }

            // Deal damage if target has health component
            IDamageable damageable = hit.collider.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(damage);
            }
        }
        else
        {
            // No hit - trail goes to max range
            if (bulletTrailPrefab != null)
            {
                Vector3 endPoint = firePoint.position + shootDirection * range;
                SpawnTrail(firePoint.position, endPoint);
            }
        }
    }

    private void SpawnTrail(Vector3 start, Vector3 end)
    {
        TrailRenderer trail = Instantiate(bulletTrailPrefab, start, Quaternion.identity);
        StartCoroutine(MoveTrail(trail, start, end));
    }

    private System.Collections.IEnumerator MoveTrail(TrailRenderer trail, Vector3 start, Vector3 end)
    {
        float time = 0f;
        float duration = 0.05f;

        while (time < duration)
        {
            trail.transform.position = Vector3.Lerp(start, end, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        trail.transform.position = end;
        Destroy(trail.gameObject, trail.time);
    }

    // For touch controls
    public void FireOnce()
    {
        if (Time.time >= nextFireTime)
        {
            Fire();
            nextFireTime = Time.time + fireRate;
        }
    }
}

// Interface for damageable objects
public interface IDamageable
{
    void TakeDamage(int damage);
}
