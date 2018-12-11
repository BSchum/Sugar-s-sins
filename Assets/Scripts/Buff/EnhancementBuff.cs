using System.Linq;
using System.Text;
using UnityEngine;
public class EnhancementBuff : Buff
{
    float lastApply;

    float duration = 10f;
    public EnhancementBuff(GameObject target) : base(target)
    {
        lastApply = Time.time;
    }

    public override void ApplyBuff()
    {
        target.GetComponent<Stats>().SetDamageReductionInPercent(Constants.ENHANCEMENT_TANK_DAMAGE_REDUCTION);
    }

    public override bool isEnded()
    {
        return Time.time > lastApply + duration;
    }
}

