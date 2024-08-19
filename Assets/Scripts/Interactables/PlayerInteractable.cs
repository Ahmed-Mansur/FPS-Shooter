using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractable : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Camera cam;
    public float distance = 5f;
    RaycastHit hitInfo;
    [SerializeField] LayerMask mask;
    private PlayerUI playerUI;
    Interactable interactable;
    InputActions playerInput;
    void Start()
    {
        cam = GetComponent<PlayerLook>().cam;  
        playerUI = GetComponent<PlayerUI>();
        playerInput = new InputActions();
    }

    // Update is called once per frame
    void Update()
    {
        playerUI.SetMessage(string.Empty);
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * distance, Color.red);
        if (Physics.Raycast(ray, out hitInfo, distance, mask))
        {
            if (hitInfo.collider.gameObject.GetComponent<Interactable>())
            {
                interactable = hitInfo.collider.gameObject.GetComponent<Interactable>();
                playerUI.SetMessage(interactable.message);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    interactable.BaseInteract();
                }
            }
        }
    }
}
