using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keypad : Interactable
{
    public bool isopen;
    public GameObject door;
    // Start is called before the first frame update
    void Start()
    {
        isopen = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void Interact()
    {
        Debug.Log("Interacting with keypad");
        isopen = !isopen;
        door.GetComponent<Animator>().SetBool("isOpen" , isopen);

    }
}
