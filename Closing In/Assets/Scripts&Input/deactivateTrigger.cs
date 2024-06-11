using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class deactivateTrigger : MonoBehaviour
{
    void Start()
    {

    }

    private void Update()
    {
       
    }

    public void destroyTrigger()
    {
        Destroy(GetComponent<EventTrigger>());
    }
}
