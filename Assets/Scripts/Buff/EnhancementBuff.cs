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
    }

    public override bool isEnded()
    {
        return Time.time > lastApply + duration;
    }
}

