using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BossController : EnemyController {
    #region Unity's method
    // Use this for initialization
    void Start () {
        base.Start();
        UIManager.instance.BossHPSubscribe(this.stats);

    }

    // Update is called once per frame
    void Update () {
        base.Update();
        AutoAttack();
    }
    #endregion

    #region Skills
    public void AutoAttack()
    {
        LookHightestThreat();
    }
    #endregion


}
