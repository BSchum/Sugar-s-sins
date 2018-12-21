using UnityEngine;

class DamageReduceBuff : Buff
{
    float lastApply;

    float duration = 10f;
    public DamageReduceBuff(GameObject target) : base(target)
    {
        lastApply = Time.time;
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
