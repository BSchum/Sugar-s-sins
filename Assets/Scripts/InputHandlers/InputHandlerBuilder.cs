using System.Collections;
using System.Collections.Generic;
using UnityEngine;
class InputHandlerBuilder
{
  
    private InputHandler actualInputHandler;

    public InputHandlerBuilder ChooseInputHandler()
    {
        //TODO verify which input handler we should return ( PC or console );
        actualInputHandler = new KeyBoardInputHandler();
        return this;
    }

    public InputHandler Build()
    {
        return actualInputHandler;
    }


}