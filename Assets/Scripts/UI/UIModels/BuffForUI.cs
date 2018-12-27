using UnityEngine;
public class BuffForUI
{
    public float lastApply;
    public Sprite sprite;
    public float cooldown;

    public BuffForUI(float lastApply, Sprite sprite, float cooldown)
    {
        this.lastApply = lastApply;
        this.sprite = sprite;
        this.cooldown = cooldown;
    }
}

