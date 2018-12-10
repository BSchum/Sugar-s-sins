using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


static class BuffListExt
{
    static void Print(this List<Buff> buffs)
    {
        int i = 0;
        foreach(Buff buff in buffs)
        {
            Debug.Log("Buff n" + i + " -- Nom : "+buff.GetType());
            i++;
        }
    }
}

