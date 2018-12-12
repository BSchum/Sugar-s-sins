using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public abstract class ObjectToSpawn : NetworkBehaviour
{
    protected abstract void Destroy();
    protected abstract void Spawn();

    protected delegate void ChangeState();
    protected ChangeState changeState;
    protected event ChangeState OnChangeState;

    [ClientRpc]
    public virtual void RpcChangeState()
    {
        if (OnChangeState != null)
            OnChangeState();
        OnChangeState = null;
    }

    protected virtual void SetState(ChangeState newState)
    {
        if (OnChangeState == null)
            OnChangeState += newState;
    }

}
