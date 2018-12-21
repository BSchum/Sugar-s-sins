using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TauntSkill : Skill {
    public override IEnumerator Cast()
    {
        yield return false;
    }


    public override bool HasRessource()
    {
        if (gameObject.GetComponent<TankAttacks>().GetGelatinStacks() >= this.cost)
        {
            gameObject.GetComponent<TankAttacks>().AddGelatinStack((int)-this.cost);
            return true;
        }
        return false;
    }
}
