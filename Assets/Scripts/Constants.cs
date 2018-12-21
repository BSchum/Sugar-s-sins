using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public static class Constants
{
    #region Tags
    public const string ENEMY_TAG = "Enemy";
    public const string PLAYER_TAG = "Player";
    public const string TOTEM_TAG = "Totem";
    #endregion
    public static float BURST_PASSIF_MULTIPLICATEUR = 1f;
    #region Tank constants
    public const float MAX_ATTACK_MULTIPLICATOR_GELATIN = 0.5f;
    public const float MIN_ATTACK_MULTIPLICATOR_GELATIN = 0f;

    public const float MAX_DEFENSE_MULTIPLICATOR_GELATIN = 0.5f;
    public const float MIN_DEFENSE_MULTIPLICATOR_GELATIN = 0f;

    public const float ENHANCEMENT_TANK_DAMAGE_REDUCTION = 0.20f;

    public const int MAX_GELATIN_STACK = 10;
    #endregion
}

