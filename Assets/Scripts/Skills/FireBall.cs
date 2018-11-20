using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : Skill {

    public override bool CanCast()
    {
        return canCast && !isCasting;
    }

    public override IEnumerator Cast()
    {
        ih = new InputHandlerBuilder().ChooseInputHandler().Build();

        SpawnProjectile();

        isCasting = true;

        castStartTime = Time.time;

        yield return new WaitUntil(() => !ih.FirstSkill());

        Debug.Log("Fin");

        isCasting = false;

        var pushTime = Time.time - castStartTime;

        SetProjectile(pushTime);
    }

    public void SpawnProjectile()
    {
        Vector3 pos = Vector3.zero;
        Quaternion dir = Quaternion.identity;

        var newProjectile =  (GameObject)Instantiate(skillProjectile.gameObject, pos, dir);
    }

    public void SetProjectile (float keyPushTime)
    {
        
    }

}
