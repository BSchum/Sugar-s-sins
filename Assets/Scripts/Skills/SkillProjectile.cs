using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[CreateAssetMenu(fileName = "Projectile", menuName = "New ¨Projectile", order = 2)]
public class SkillProjectile : ScriptableObject {

    public float speed, damage, lifeTime;
    
    public GameObject projectilePrefab;

    public void Initiate(Rigidbody rb, Transform t)
    {
        rb.AddForce(t.forward * speed * 250);
        t.localScale *= speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        var hitEntity = collision.transform.gameObject;
        if(hitEntity.tag == Constants.ENEMY_TAG)
        {
            //remove life
            
        }
    }

    public IEnumerator DieAfterSecond (GameObject gameobject)
    {
        yield return new WaitForSeconds(lifeTime);

        Destroy(gameobject);
    }
}
