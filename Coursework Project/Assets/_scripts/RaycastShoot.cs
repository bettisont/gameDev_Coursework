using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastShoot : MonoBehaviour
{

    public int gunDamage = 1;
    // how often we cn shoot 
    public float fireRate = 0f;
    public float weaponRange = 50f;
    // how much force will be applid to objects hit 
    public float hitForce = 100f;
    public Transform gunEnd;
    public int maxAmmo = 10;
    public float reloadTime = 1.5f;
    public Animator animator;


    private Camera fpsCam;
    // how long laser remains visible in gameView 
    private WaitForSeconds shotDuration = new WaitForSeconds(.07f);
    private AudioSource gunAudio;
    private LineRenderer laserLine;
    private float nextFire;
    private int currentAmmo;
    private bool isReloading = false;


    void Start()
    {
        laserLine = GetComponent<LineRenderer>();
        gunAudio = GetComponent<AudioSource>();
        fpsCam = GetComponentInParent<Camera>();
        currentAmmo = maxAmmo;
    }


    void Update()
    {
        if (isReloading) return;

        if(currentAmmo <= 0f)
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            shoot();
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;

        Debug.Log("Reloading...");

        animator.SetBool("Reloading", true);

        yield return new WaitForSeconds(reloadTime);

        animator.SetBool("Reloading", false);

        currentAmmo = maxAmmo;

        isReloading = false;
    }

    private void shoot()
    {
        currentAmmo--;

        nextFire = Time.time + fireRate;

        StartCoroutine(ShotEffect());

        Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));

        RaycastHit hit;

        laserLine.SetPosition(0, gunEnd.position);

        if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, weaponRange))
        {
            laserLine.SetPosition(1, hit.point);

            shootableTarget health = hit.collider.GetComponent<shootableTarget>();

            if (health != null)
            {
                health.Damage(gunDamage);
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * hitForce);
            }
        }
        else
        {
            laserLine.SetPosition(1, rayOrigin + (fpsCam.transform.forward * weaponRange));
        }
    }

    private IEnumerator ShotEffect() {

        gunAudio.Play();
        laserLine.enabled = true;

        yield return shotDuration;
        laserLine.enabled = false;
    }
}
