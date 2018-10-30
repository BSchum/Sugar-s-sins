using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerScript : NetworkBehaviour {
    protected InputHandler ih;
    public virtual void Initialize()
    {
        InputHandlerBuilder ihb = new InputHandlerBuilder();
        ih = ihb.ChooseInputHandler().Build();
    }
}
