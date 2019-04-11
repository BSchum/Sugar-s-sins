using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class CristalRain : NetworkBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(DealDamage());
	}

    IEnumerator DealDamage()
    {
        for (int i = 0; i < 10; i++)
        {
            Collider[] colls = Physics.OverlapSphere(this.transform.position, 8);
            IEnumerable<Collider> playerColls = colls.Where(c => c.tag == Constants.PLAYER_TAG);

            foreach (Collider col in playerColls)
            {
                col.GetComponent<Health>().TakeDamage(4);
            }
            yield return new WaitForSeconds(1f);
        }

        Destroy(this.gameObject);
    }
    private void OnDrawGizmos()
    {
        //Gizmos.DrawWireSphere(this.transform.position, 8);
    }
}
