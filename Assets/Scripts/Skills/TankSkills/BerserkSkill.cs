using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BerserkSkill : Skill {
    public override IEnumerator Cast()
    {
        yield return false;
    }

    public override bool HasRessource()
    {
        return false;
    }
}
