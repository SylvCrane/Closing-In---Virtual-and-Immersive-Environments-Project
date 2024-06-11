using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public float speed = 3;
    public Transform guyTransform;
    public Vector3 newPos;
    public int direction;

    // Start is called before the first frame update
    void Start()
    {
        guyTransform = GetComponent<Transform>();
        newPos = GetComponent<Transform>().localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (direction == 1)
        {
            newPos.x += 0.02f;
        }
        else if (direction == 2)
        {
            newPos.x -= 0.02f;
        }
        else if (direction == 3)
        {
            newPos.z += 0.02f;
        }
        else if (direction == 4)
        {
            newPos.z -= 0.02f;
        }


        guyTransform.localPosition = newPos;
    }
}
