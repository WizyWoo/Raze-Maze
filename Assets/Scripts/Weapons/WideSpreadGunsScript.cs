using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.MyCompany.MyGame;

public class WideSpreadGunsScript : GenericGun
{
    public float rayWideness;

    public override void Shoot()
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
        if (Physics.SphereCast(attackPoint.position, rayWideness, attackPoint.forward, out hit, rayLength, WeaponMask, QueryTriggerInteraction.Collide))
        {

            Debug.Log("hit smth" + hit.transform.name);
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Player"))
            {

                hit.transform.root.GetComponent<PlayerManager>().Damage(Damage);
                Debug.Log("hit " + hit.transform.name);

                //change enemy color upon hit
                //hit.transform.root.GetComponent<Renderer>().material.color = Color.red;
            }

        }
        else
        {Debug.Log("No hit");}

        Debug.DrawRay(attackPoint.position, attackPoint.forward, Color.green, 10);

        if (particleSystemu != null)
            particleSystemu.Play();

        if (bullet != null)
        {
            //Instantiate bullet/projectile
            GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity); //store instantiated bullet in currentBullet
                                                                                                       //currentBullet.GetComponent<BulletScript>().wP = this;

            //Rotate bullet to shoot direction
            currentBullet.transform.forward = directionWithSpread.normalized;

            //Add forces to bullet
            currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * shootForce, ForceMode.Impulse);
        }

        //bulletsLeft--;
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


}
