using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public abstract class ObjectToSpawn : NetworkBehaviour {

    public ObjectSpawnerController osc;

    public abstract void Destroy();
    public abstract void Spawn();

    protected delegate void OnChangeState();
    protected event OnChangeState ChangingStateEvent;

    [ClientRpc]
    public virtual void RpcChangeState()
    {
        //cast sur les clients grace au cmd du joueur
        if (ChangingStateEvent != null)
            ChangingStateEvent();
            ChangingStateEvent = null;
        //execute l'event puis le reset
    }

    protected virtual void SetState (OnChangeState newState)
    {
        if(ChangingStateEvent == null)
        ChangingStateEvent += newState;
    }

}
