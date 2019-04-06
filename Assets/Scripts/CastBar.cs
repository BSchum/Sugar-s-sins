using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CastBar : MonoBehaviour {

    public static CastBar singleton;

    public Image image;

    void Start()
    {
        CastBar.singleton = this;
        gameObject.SetActive(false);
    }

    public void ChangeState(bool state)
    {
        gameObject.SetActive(state);
    }

    public void UpdateCastBar(float value)
    {
        image.fillAmount = value;
    }
}
