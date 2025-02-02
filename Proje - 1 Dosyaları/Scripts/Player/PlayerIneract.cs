using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This script will have all of the logic to detect interactables and handle our player's input. So we will drag this class on our player.
public class PlayerIneract : MonoBehaviour
{
    //We need a reference camera.
    private Camera cam;

    //A raycast is conceptually like a laser beam that is fired from a point in space along a particular direction.
    //Any object making contact with the beam can be detected and reported.
    //This function returns the number of contacts found and places those contacts in the results array.

    //Serialize that so it will be visible in the inspector.
    [SerializeField]
    private float distance = 3f;//For ray distance.

    [SerializeField]
    private LayerMask mask;

    private PlayerUI playerUI;

    private InputManager inputManager;
    void Start()
    {
        cam = GetComponent<PlayerLook>().cam;//Player's cam will be our reference camera.
        playerUI = GetComponent<PlayerUI>();
        inputManager = GetComponent<InputManager>();
    }

    // Update is called once per frame
    void Update()
    {

        playerUI.UpdateText(string.Empty);

        //Create a ray at the center of the camera, shooting outwards.
        //First parameter is the origin so cam position is the origin. Second parameter is the direction so cam forward.
        Ray ray = new Ray(cam.transform.position,cam.transform.forward);//this have infinite distance we can terminate that.

        Debug.DrawRay(ray.origin,ray.direction*distance);

        RaycastHit hitInfo;//Variable to store our collision information.

        if (Physics.Raycast(ray, out hitInfo, distance, mask))
        {
            if (hitInfo.collider.GetComponent<Interactable>() != null)
            {
                Interactable interactable = hitInfo.collider.GetComponent<Interactable>();
                playerUI.UpdateText(interactable.promptMessage);
                if (inputManager.onFoot.Interact.triggered)
                {
                    interactable.baseInteract();
                }
            }
        }
        //Interactable klasörü içerisine base classtan alýnan bütün classlar koyulacak örneðin keypad için yazýlmýþ olan class.
    }
}
