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
    public float shootForce;//* ,upwardForce

    //Gun stats
    public float timeBetweenShooting, spread, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold, canReload = false;

    protected int bulletsShot;

    [SerializeField] protected int bulletsLeft;

    public float rayLength;

    public ParticleSystem particleSystemu;

    //bools
    protected bool readyToShoot, reloading;

    //Reference
    //public Camera myCamera;
    public Transform attackPoint;

    //bug fixing 
    public bool allowInvoke = true;

    protected LayerMask layerMask;

    private void Start()
    {
        
        bulletsLeft = magazineSize;
        readyToShoot = true;

    }

    private void Update()
    {
        MyInput();
    }

    private void MyInput()
    {
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

        if(!photonView.IsMine && Firing)
        {

            Shoot();

        }
    }

   public virtual void Shoot()
    {
        readyToShoot = false; 

        //Calculate direction from attackPoint to targetPoint
        Vector3 directionWithoutSpread = attackPoint.forward;

        //Calculate spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        //Calculate new direction with spread
        Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, y, 0); //Just add spread to last direction

        RaycastHit hit;
        if(Physics.Raycast(attackPoint.position, attackPoint.forward, out hit, rayLength, WeaponMask, QueryTriggerInteraction.Collide))
        {

            Debug.Log(hit.transform.name);
            if(hit.transform.gameObject.layer == LayerMask.NameToLayer("Player"))
            {

                hit.transform.root.GetComponent<IHit>().Damage(Damage);
                Debug.Log("hit " + hit.transform.name);

            }


        }

        Debug.DrawRay(attackPoint.position, attackPoint.forward, Color.green, 10);

        if(particleSystemu != null)
            particleSystemu.Play();

        if(bullet != null)
        {
            //Instantiate bullet/projectile
            GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity); //store instantiated bullet in currentBullet
           //currentBullet.GetComponent<BulletScript>().wP = this;

           //Rotate bullet to shoot direction
           currentBullet.transform.forward = directionWithSpread.normalized;

           //Add forces to bullet
           currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * shootForce, ForceMode.Impulse);
           //currentBullet.GetComponent<Rigidbody>().AddForce(fpsCam.transform.up * upwardForce, ForceMode.Impulse);
        }

        bulletsShot++;

        //Invoke resetShot function (if not already invoked), with your timeBetweenShooting
        if (allowInvoke)
        {
            Debug.Log("entered");
            Invoke("ResetShot", timeBetweenShooting);
            allowInvoke = false;
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

    protected void Reload()
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
