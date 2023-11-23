using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDoorController : MonoBehaviour
{
    public Animator doorAnimator = null;

    public bool openTrigger = false;
    public bool closeTrigger = false;

    public void OpenDoor()
    {
        doorAnimator.SetTrigger("DoorOpen");
    }

    public void CloseDoor()
    {
        doorAnimator.SetTrigger("DoorClose");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (openTrigger)
        {
            OpenDoor();
        }

        if (closeTrigger)
        {
            CloseDoor();
        }
    }
}
