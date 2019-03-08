using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TotemProjectileAnimations : NetworkBehaviour {
    public LineRenderer fireProjectile;
    public LineRenderer lightningProjectiles;
    public delegate IEnumerator Attack();

    public IEnumerator Fire(GameObject currentTarget)
    {
        fireProjectile.gameObject.SetActive(true);
        fireProjectile.SetPosition(0, fireProjectile.transform.position);
        fireProjectile.SetPosition(1, currentTarget.transform.position);
        yield return new WaitForSeconds(0.2f);
        fireProjectile.gameObject.SetActive(false);
    }

    public IEnumerator LightingAttack(Vector3 target, Vector3 source)
    {
        lightningProjectiles.gameObject.SetActive(true);
        lightningProjectiles.SetPosition(0, source);
        lightningProjectiles.SetPosition(1, target);
        yield return new WaitForSeconds(0.3f);
        lightningProjectiles.gameObject.SetActive(false);
    }
}
