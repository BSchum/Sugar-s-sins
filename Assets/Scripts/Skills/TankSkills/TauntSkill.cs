using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TauntSkill : Skill {
    public override IEnumerator Cast()
    {
        yield return null;
    }


    public override bool HasRessource()
    {
        return false;
    }
}
