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
        if (target.GetComponent<TankAttacks>().lastActiveTotem != null)
        {
            TotemProjectile totem = target.GetComponent<TankAttacks>().lastActiveTotem.GetComponent<TotemProjectile>();
            totem.GetComponent<TotemProjectile>().AddBuff(new DamageBuff(totem.gameObject));
            totem.GetComponent<Stats>().BuffDefense(10);
        }
    }

    public override bool isEnded()
    {
        return Time.time > lastApply + duration;
    }
}

