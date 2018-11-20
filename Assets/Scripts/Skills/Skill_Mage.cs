using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Mage Skill", menuName = "New Mage Skills", order = 1)]
public class Skill_Mage : Skill {

    InputHandlerBuilder builder;
    InputHandler ih;

    public override bool CanCast(Stats stats)
    {
        // Check if current health >= max health as exemeple
        bool validate = stats.GetHealth() >= stats.GetMaxHealth() && !isCooldown && !isCasting;

        return validate;
    }

    public override bool CanCast(PlayerAttack pInfo)
    {
        return cost == pInfo.GetPassifVal() && !isCooldown && !isCasting;
    }

    public override IEnumerator Cast()
    {
        isCasting = true;

        castStartTime = Time.time;
        
        // deux conditions : soit push la touche, soit push jusqua push le max time
        yield return new WaitUntil(() => !ih.FirstSkill() && Time.time - castStartTime < castTime || Time.time - castStartTime >= minCastTime);

        isCasting = false;

        castTime = Time.time - castStartTime;

        //Dans le cas du sort 1 du mage pas besoin de min time pour cast le sort
        //De plus le sort utilise un projectile donc on va le faire spawn

        SpawnProjectile(skillProjectile, Vector3.zero, Quaternion.identity);
    }

    public IEnumerator OnCooldown ()
    {
        isCooldown = true;

        yield return new WaitForSeconds(cooldown);

        isCooldown = false;
    }

    public void ApplyBuff()
    {

    }

    public void ApplyDebuff()
    {

    }

    public void SpawnProjectile(SkillProjectile projectileInfo, Vector3 pos, Quaternion dir)
    {
        var newProjectile =  (GameObject)Instantiate(skillProjectile.projectilePrefab, pos, dir);

        //Par défault le projectile à un script 
    }

}
