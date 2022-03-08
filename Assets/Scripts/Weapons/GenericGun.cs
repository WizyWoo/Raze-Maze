using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Com.MyCompany.MyGame;

public class GenericGun : WeaponController, IPunObservable
{
    //bullet 
    public GameObject bullet;

    //bullet force
    public float shootForce/* ,upwardForce*/;

    //Gun stats
    public float timeBetweenShooting, spread, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold, canReload = false;

    int bulletsShot;

    [SerializeField] private int bulletsLeft; 


    //Recoil
    //public Rigidbody playerRb;
    //public float recoilForce;

    //bools
    private bool readyToShoot, reloading;

    //Reference
    //public Camera myCamera;
    public Transform attackPoint;

    //Graphics
    //public GameObject muzzleFlash;
    //public TextMeshProUGUI ammunitionDisplay;

    //bug fixing 
    public bool allowInvoke = true;

    private LayerMask layerMask;

    //GameObject mixerObject;
    //GameObject mixerBlades;

    private void Awake()
    {
        //make sure magazine is full
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }

    private void Start()
    {
        //mixerObject = GameObject.Find("handMixer");
        //mixerBlades = mixerObject.transform.GetChild(1).gameObject;
    }

    private void Update()
    {
        MyInput();

        //Set ammo display, if it exists 
        //if (ammunitionDisplay != null)
        //    ammunitionDisplay.SetText(bulletsLeft / bulletsPerTap + " / " + magazineSize / bulletsPerTap);
    }

    private void MyInput()
    {
        //Check if allowed to hold down button and take corresponding input
        //if (allowButtonHold) Firing = Input.GetKey(KeyCode.Mouse0);
        //else Firing = Input.GetKeyDown(KeyCode.Mouse0);

        //Reloading 
        if (Input.GetKeyDown(KeyCode.T) && canReload == true/*&& bulletsLeft < magazineSize && !reloading*/) 
            Reload();
        //Reload automatically when trying to shoot without ammo
        //if (readyToShoot && Firing && !reloading && bulletsLeft <= 0) 
        //    Reload();

        //Shooting
        if (readyToShoot && Firing && !reloading && bulletsLeft > 0)
        {
            //Set bullets shot to 0
            bulletsShot = 0;

            Shoot();            
        }
    }

   public void Shoot()
    {
        readyToShoot = false;

        //Find the exact hit position using a raycast
        //Ray ray;  //Just a ray through the middle of the current view
        //RaycastHit hit;

        //if (Physics.Raycast(attackPoint.position, transform.forward, out hit, Mathf.Infinity, layerMask, QueryTriggerInteraction.Ignore))
        //{

        //}       

        //check if ray hits something
        //Vector3 targetPoint;
        //if (Physics.Raycast(ray, out hit))
        //    targetPoint = hit.point;
        //else
        //    targetPoint = ray.GetPoint(75); //Just a point far away from the player

        //mixerBlades.gameObject.SetActive(false);

        //Calculate direction from attackPoint to targetPoint
        Vector3 directionWithoutSpread = attackPoint.forward;

        //Calculate spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        //Calculate new direction with spread
        Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, y, 0); //Just add spread to last direction

        RaycastHit hit;
        if(Physics.Raycast(attackPoint.position, attackPoint.forward, out hit, 1000, ~LayerMask.NameToLayer("Player"), QueryTriggerInteraction.Ignore))
        {

            if(hit.transform.tag == "Player")
            {

                hit.transform.GetComponent<PlayerManager>().Damage(this);
                Debug.Log("hit " + hit.transform.name);

            }

            Debug.Log(hit.transform.name);

        }

        Debug.DrawRay(attackPoint.position, attackPoint.forward, Color.green, 10);

        //Instantiate bullet/projectile
        //GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity); //store instantiated bullet in currentBullet
        //currentBullet.GetComponent<BulletScript>().wP = this;

        //Rotate bullet to shoot direction
        //currentBullet.transform.forward = directionWithSpread.normalized;

        //Add forces to bullet
        //currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * shootForce, ForceMode.Impulse);
        //currentBullet.GetComponent<Rigidbody>().AddForce(fpsCam.transform.up * upwardForce, ForceMode.Impulse);

        //Instantiate muzzle flash, if you have one
        //if (muzzleFlash != null)
        //    Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);

        bulletsLeft--;
        bulletsShot++;

        //Invoke resetShot function (if not already invoked), with your timeBetweenShooting
        if (allowInvoke)
        {
            Debug.Log("entered");
            Invoke("ResetShot", timeBetweenShooting);
            allowInvoke = false;

            ////Add recoil to player (should only be called once)
            //playerRb.AddForce(-directionWithSpread.normalized * recoilForce, ForceMode.Impulse);
        }

        //if more than one bulletsPerTap make sure to repeat shoot function
        if (bulletsShot < bulletsPerTap && bulletsLeft > 0)
            Invoke("Shoot", timeBetweenShots);
    }
    private void ResetShot()
    {
        Debug.Log("reseting the shot");
        //Allow shooting and invoking again
        readyToShoot = true;
        allowInvoke = true;
    }

    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime); //Invoke ReloadFinished function with your reloadTime as delay
    }
    private void ReloadFinished()
    {
        //Fill magazine
        bulletsLeft = magazineSize;
        reloading = false;
    }

    public override void FireWeapon(bool _firing)
    {
        Firing = _firing;
    }
}
