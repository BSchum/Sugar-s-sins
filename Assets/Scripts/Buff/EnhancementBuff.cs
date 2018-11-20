using System.Linq;
using System.Text;
using UnityEngine;
public class EnhancementBuff : Buff
{
    float lastApply;

    float duration = 10f;
    public EnhancementBuff(GameObject target) : base(target)
    {
    }

    public override void ApplyBuff()
    {
        lastApply = Time.time;
        target.GetComponent<Stats>().SetDamageReductionInPercent(Constants.ENHANCEMENT_TANK_DAMAGE_REDUCTION);
        target.GetComponent<TankAttacks>().lastActiveTotem.GetComponent<TotemProjectile>().damage += 10;
        TotemProjectile totem = target.GetComponent<TankAttacks>().lastActiveTotem.GetComponent<TotemProjectile>();
        totem.damage += 10;
        totem.GetComponent<Stats>().AddDefense(10);
    }

    public override bool isEnded()
    {
        return Time.time > lastApply + duration;
    }
}

