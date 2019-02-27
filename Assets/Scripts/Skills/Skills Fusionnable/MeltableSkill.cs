using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public abstract class MeltableSkill : NetworkBehaviour{

    public SkillProjectile skillOne;
    public SkillProjectile skillTwo;

    public abstract void Merge(GameObject firstSkill, GameObject secondSkill);
}
