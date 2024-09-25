using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupObject : MonoBehaviour
{
    [SerializeField] private Transform grabPoint;   //Add to where you want item to go aka hands or above head.
    [SerializeField] private Transform rayPoint;    //Raycast that check if something is able to be picked up.
    [SerializeField] private float rayDistance;     //How far the ray will draw.

    private GameObject grabbedObject = null;        //This is the most imporant part. This how we grab the compents of said gameobject.
    private int layerIndex;                         //This makes it so we don't have to use a string make a layer of pickupable?

    private void Start()
    {
        layerIndex = LayerMask.NameToLayer("Objects"); //Grab the layer in the form of an int; 
    }

    private void Update()
    {
        PickupItem();
    }

    //Next we will need to add force to toss said item.
    private void PickupItem()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(rayPoint.position, transform.right, rayDistance); // when flipping sprites the raycast should change too.


        //if the item in front of me have a layerIndex of the proper layer we can pick up this item if not then do nothing. 
        if (hitInfo.collider != null && hitInfo.collider.gameObject.layer == layerIndex)
        {
            if (Input.GetKeyDown(KeyCode.E) && grabbedObject == null)
            {
                grabbedObject = hitInfo.collider.gameObject;
                grabbedObject.GetComponent<Rigidbody2D>().isKinematic = true; //Setting to kinematic lets the object be grabable. 
                grabbedObject.GetComponent<BoxCollider2D>().isTrigger = true; //Setting the boxcollider to trigger lets you move around I couldn't get it to work.
                grabbedObject.transform.position = grabPoint.position;        //Set to this postion.
                grabbedObject.transform.SetParent(transform);                 //Add to charater trasnform
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                grabbedObject.GetComponent<Rigidbody2D>().isKinematic = false;//Turn off
                grabbedObject.GetComponent<BoxCollider2D>().isTrigger = false;//Let game object not fallpast the floor.
                grabbedObject.transform.SetParent(null);                      //Leave game Object
                grabbedObject = null;                                         //Set game Object to null so we can pick up something else or the same thing. 
            }
        }
        Debug.DrawRay(rayPoint.position, transform.right * rayDistance); //Lets you see raycast in scene mode. 
    }
}
