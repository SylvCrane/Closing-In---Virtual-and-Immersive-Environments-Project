using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public float speed = 3;
    public Transform guyTransform;
    public Vector3 newPos;

    // Start is called before the first frame update
    void Start()
    {
        guyTransform = GetComponent<Transform>();
        newPos = GetComponent<Transform>().localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        newPos.z += 0.02f;
        guyTransform.localPosition = newPos;
    }
}
