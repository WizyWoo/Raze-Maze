using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.MyCompany.MyGame;

public class MixerScript : WideSpreadGunsScript
{
    public GameObject mixerObject, mixerBlades;

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
        if (Physics.Raycast(attackPoint.position, attackPoint.forward, out hit, rayLength, WeaponMask, QueryTriggerInteraction.Collide))
        {

            if (HitMaskCheck(hit.transform))
            {

                hit.transform.GetComponent<IHit>().Damage(Damage);
            }
        }

        Debug.DrawRay(attackPoint.position, attackPoint.forward, Color.green, 10);

        if (particleSystemu != null)
            particleSystemu.Play();

        if (bullet != null)
        {
            if (mixerBlades != null)
                mixerBlades.gameObject.SetActive(false);

            //Instantiate bullet/projectile
            GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity); //store instantiated bullet in currentBullet
                                                                                                       //currentBullet.GetComponent<BulletScript>().wP = this
            //Rotate bullet to shoot direction
            currentBullet.transform.forward = directionWithSpread.normalized;

            //Add forces to bullet
            currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * shootForce, ForceMode.Impulse);
        }

        bulletsLeft--;
        bulletsShot++;

        Destroy(this);

        //Invoke resetShot function (if not already invoked), with your timeBetweenShooting
        if (allowInvoke)
        {
            Invoke("ResetShot", timeBetweenShooting);
            allowInvoke = false;
        }

        //if more than one bulletsPerTap make sure to repeat shoot function
        if (bulletsShot < bulletsPerTap && bulletsLeft > 0)
            Invoke("Shoot", timeBetweenShots);
    }
}
