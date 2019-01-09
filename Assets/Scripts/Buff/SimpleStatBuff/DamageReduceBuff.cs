using UnityEngine;

class DamageReduceBuff : Buff
{

    public DamageReduceBuff(GameObject target) : base(target)
    {
    }

    public override void ApplyBuff()
    {
        this.target.GetComponent<Stats>().SetDamageReductionInPercent(Constants.ENHANCEMENT_TANK_DAMAGE_REDUCTION);
    }

    public override bool isEnded()
    {
        return Time.time > lastApply + duration;
    }
}
