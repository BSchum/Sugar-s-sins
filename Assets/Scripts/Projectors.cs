using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Projectors : NetworkBehaviour {

    private Projector projector;
    public Transform unit;
    public Camera cam;

    [HideInInspector]
    public Skill skill;

    [SerializeField]
    private Vector3 offset;


    private void Awake()
    {
        projector = GetComponent<Projector>();
    }
    
    void Update () {
        if(unit.tag == Constants.PLAYER_TAG)
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                gameObject.SetActive(false);
            }
            if (Input.GetKeyDown(KeyCode.Mouse0))
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
                pos += offset;

                transform.position = pos;
            }
        }
        
        if(unit.tag == Constants.BOSS_TAG)
        {
            Vector3 newPos = unit.position + unit.forward + offset;
            this.transform.position = newPos;
        }
	}

    public void SetProjector (Skill skill)
    {
        this.skill = skill;
        this.projector.material = skill.area;
        this.offset = skill.projectorOffset;
    }
}
