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
    public int maxAmmo = 8;
    public float reloadTime = 4f;
    public Animator animator;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;


    private Camera fpsCam;
    // how long laser remains visible in gameView 
    private WaitForSeconds shotDuration = new WaitForSeconds(.07f);
    public AudioSource gunShot;
    public AudioSource gunOutOfAmmo;
    public AudioSource hitMarker;
    private LineRenderer laserLine;
    private float nextFire;
    private int currentAmmo;
    private bool isReloading = false;


    void Start()
    {
        laserLine = GetComponent<LineRenderer>();
        fpsCam = GetComponentInParent<Camera>();
        currentAmmo = maxAmmo;
    }


    void Update()
    {
        if (isReloading) return;

       // if(currentAmmo <= 0f)
        //{
        //    gunOutOfAmmo.Play();
        //    StartCoroutine(Reload());
          //  return;
        //}

        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            currentAmmo--;
            if (currentAmmo < 0)
            {
                gunOutOfAmmo.Play();
                StartCoroutine(Reload());
                return;
            }
            else
            {
                shoot();
            }

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




        nextFire = Time.time + fireRate;


        muzzleFlash.Play();
        gunShot.Play();
    

        Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));

        RaycastHit hit;

        laserLine.SetPosition(0, gunEnd.position);

        if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, weaponRange))
        {
            laserLine.SetPosition(1, hit.point);

            GameObject impact = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impact, 2f);

            shootableTarget health = hit.collider.GetComponent<shootableTarget>();

            if (health != null)
            {
                hitMarker.Play();
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

}
