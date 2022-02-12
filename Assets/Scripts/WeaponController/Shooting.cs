using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    Camera fpsCam;
    [SerializeField] float range = 100f;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] float fireRate = 20;
    [SerializeField] float weaponDamage = 20f;
    [SerializeField] GameObject image;
    float nextTimeToFire = 0f;

    private void Awake()
    {
        fpsCam = Camera.main;
    }

    private void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            if(Time.time >= nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1 / fireRate;
                Shoot();
            }
        }
    }

    void Shoot()
    {
        muzzleFlash.Play();
        RaycastHit hit;
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            HealthManager hm = hit.transform.GetComponent<HealthManager>();
            if (hm)
            {
                hm.TakeDamage(weaponDamage * 100 * Time.deltaTime);
                StartCoroutine(ShowHitmarker());
            }
        }
    }

    IEnumerator ShowHitmarker()
    {
        image.SetActive(true);
        yield return new WaitForSeconds(0.05f);
        image.SetActive(false);
        yield return null;
    }
}
