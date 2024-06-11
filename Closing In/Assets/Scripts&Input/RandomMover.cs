using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMover : MonoBehaviour
{
    public RectTransform eyeTransform;
    public float randX;
    public float randY;
    public Vector3 newPos;

    // Start is called before the first frame update
    void Start()
    {
        eyeTransform = GetComponent<RectTransform>();
        newPos = GetComponent<RectTransform>().localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        randX = Random.Range(-400, 400);
        randY = Random.Range(-300, 200);
        newPos.x = randX;
        newPos.y = randY;
        eyeTransform.localPosition = newPos;
    }
}
