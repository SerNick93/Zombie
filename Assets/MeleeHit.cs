using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeHit : MonoBehaviour
{
    //This weapon is animated in the activate weapon script, as that function was implemented in the weapon controller.

    EnemyController enemyController;
    private void OnTriggerExit(Collider collider)
    {
        if (collider.transform.tag == "Enemy")
        {
            enemyController = collider.transform.GetComponent<EnemyController>();
            enemyController.Knockback(WeaponController.MyInstance.ThisIsTheActiveWeapon.Damage);
            //transform.GetComponent<SphereCollider>().enabled = false;
            return;
        }
        //transform.GetComponent<SphereCollider>().enabled = true;
    }
    


}
