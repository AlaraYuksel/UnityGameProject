using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//We are defining this class abstract, because we want to use it for every interactable item. It could be a door or a bot.
public abstract class Interactable : MonoBehaviour
{
    //Add or remove an InteractionEvent component to this gameobject.
    public bool useEvents;


    //Message displayed to player when looking at an interactable.
    [SerializeField] public string promptMessage;

    public void baseInteract()
    {
        if (useEvents)
            GetComponent<InteractionEvent>().OnInteract.Invoke();
        
        Interact();
    }
    protected virtual void Interact()
    {
        //we wont have any code written in this function
        //this will be a template function to be overridden by our subclasses
    }

    
}
