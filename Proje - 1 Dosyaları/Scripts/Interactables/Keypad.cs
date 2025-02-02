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

    //Keypad �zerine gelindi�i zaman metin g�sterilmesi i�in textmeshpro indirildi.
    // - PlayerInput klas�r�ne defaultInput klas�r� �zerinden UI k�sm� kopyala yap��t�r yap�l�p defaultInputun yeri PlayerInput ile de�i�tirildi.
    // - Text hizalanmas� ve pozisyonu ger�ekle�tirildi.
    // - Crosshair tasarlanmas� i�in yeni bir image olu�turuldu.

}
