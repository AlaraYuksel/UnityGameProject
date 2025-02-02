using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keypad : Interactable
{
    [SerializeField]
    private GameObject Door;
    private bool doorOpen;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //this function is where we will design our interaction using code.
    protected override void Interact()
    {
        doorOpen = !doorOpen;
        Door.GetComponent<Animator>().SetBool("isOpen",doorOpen);
    }

    //Keypad üzerine gelindiði zaman metin gösterilmesi için textmeshpro indirildi.
    // - PlayerInput klasörüne defaultInput klasörü üzerinden UI kýsmý kopyala yapýþtýr yapýlýp defaultInputun yeri PlayerInput ile deðiþtirildi.
    // - Text hizalanmasý ve pozisyonu gerçekleþtirildi.
    // - Crosshair tasarlanmasý için yeni bir image oluþturuldu.

}
