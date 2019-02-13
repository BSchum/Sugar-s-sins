using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectors : MonoBehaviour {

    private Projector projector;
    public PlayerAttack player;
    public Camera cam;

    public float distance = 5;

    private void Awake()
    {
        projector = GetComponent<Projector>();
    }
    
    void Update () {
		if(Input.GetKeyDown(KeyCode.Mouse1))
        {
            gameObject.SetActive(false);
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
        projector.material = skill.area;
    }
}
