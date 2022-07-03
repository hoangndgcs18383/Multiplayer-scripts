using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireHandGunMultiplayer : MonoBehaviour
{
    public static bool isBusy = false;

    [Header("Setting Fire Gun Is Mine")]
    [SerializeField] GameObject theGun;
    [SerializeField] Camera fpsCam;
    [SerializeField] GameObject muzzleFlash;
    [SerializeField] AudioSource gunFireFX;
    [SerializeField] AudioSource emptyFX;

    [Header("Setting Fire Gun Isn't Mine")]
    [SerializeField] PhotonView PV;
    [SerializeField] GameObject animFire;
    [SerializeField] GameObject muzzleFlashIsntMine;
    [SerializeField] AudioSource gunFireFXIsntMinde;

    [Header("Setting")]
    [SerializeField] float weaponRanged;
    [SerializeField] float fireRate = 0.5f;

    [Header("Statics")]
    public static int maxDame = 7;
    public static int minDame = 4;

    bool isFire = false;
    public float targetDistance;

    private void Start()
    {
        PV = GetComponentInParent<PhotonView>();

        fpsCam = GetComponentInParent<Camera>();
    }

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.timeScale == 1f)
        {
            if (Stats.currentAmmo < 1)
            {
                emptyFX.Play();
            }
            else
            {
                if (isFire == false && isBusy == false)
                {
                    StartCoroutine(FiringHandgun());
                }

            }
        }
    }

    IEnumerator FiringHandgun()
    {

        Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));

        RaycastHit theShot;

        isFire = true;
        Stats.currentAmmo--;

        if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out theShot, weaponRanged))
        {
            targetDistance = theShot.distance;


            var damage = Random.Range(minDame + Stats.currentTrueDame, maxDame + Stats.currentTrueDame);

            theShot.transform.SendMessage("TakeDame", damage, SendMessageOptions.DontRequireReceiver);
        }

        if (PV.IsMine)
        {
            theGun.GetComponent<Animator>().Play("HandgunFire");
            muzzleFlash.SetActive(true);
            gunFireFX.Play();
            yield return new WaitForSeconds(0.05f);
            muzzleFlash.SetActive(false);
            yield return new WaitForSeconds(fireRate);
            theGun.GetComponent<Animator>().Play("Reset");
        }
        else
        {
            animFire.GetComponent<Animator>().Play("Fire");
            muzzleFlashIsntMine.SetActive(true);
            gunFireFXIsntMinde.Play();
            new WaitForSeconds(0.05f);
            muzzleFlashIsntMine.SetActive(false);
            new WaitForSeconds(fireRate);
            animFire.GetComponent<Animator>().Play("Reset");
        }
        isFire = false;
    }
}
