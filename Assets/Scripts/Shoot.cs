using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform gunTransform;
    [SerializeField] private float rayLength=100f;


    private int layer;
    private bool isShooting = false;
    private Animator animator;
    private static readonly int IsShootingID = Animator.StringToHash("isShooting");
    private AudioSource audioSource;

    private void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponentInChildren<AudioSource>();

        layer = LayerMask.GetMask("Enemy");
    }

    private void Update()
    {
        if (CanSeeEnemy() && !isShooting)
        {
            isShooting = !isShooting;
            animator.SetBool(IsShootingID,isShooting);
        }
        else if (!CanSeeEnemy() && isShooting)
        {
            isShooting = !isShooting;
            animator.SetBool(IsShootingID,isShooting);
        }

    }

    public void Fire()
    {
        // Fire projectile
        GameObject projectile =
            PoolManager.Instance.ReuseObject(projectilePrefab, gunTransform.position, Quaternion.identity);
        projectile.SetActive(true);

        audioSource.Play();
    }

    public bool CanSeeEnemy()
    {
        RaycastHit2D hit2D =
            Physics2D.Raycast(transform.position, Vector2.right, rayLength, layer);

        return hit2D.collider!=null;
    }
}
