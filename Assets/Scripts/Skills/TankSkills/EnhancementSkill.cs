using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnhancementSkill : Skill
{
    public override IEnumerator Cast()
    {
        source.GetComponent<PlayerAttack>().AddBuff(new EnhancementBuff(source));
        source.GetComponent<PlayerAttack>().ApplyBuffs();
        yield return null;
    }
    
}
