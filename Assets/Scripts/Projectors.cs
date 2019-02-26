using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectors : MonoBehaviour {

    private Projector projector;
    public PlayerAttack player;
    public Camera cam;

    //[HideInInspector]
    public Skill skill;

    public float distance = 5;

    private void Awake()
    {
        projector = GetComponent<Projector>();
    }
    
    void Update () {
		if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            StartCoroutine(skill.Cast());
            skill = null;
            this.gameObject.SetActive(false);
        }
        
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 pos = hit.point;
            pos.y = player.transform.position.y + distance;

            transform.position = pos;
        }
	}

    public void SetProjector (Skill skill)
    {
        this.skill = skill;
        projector.material = skill.area;
    }
}
