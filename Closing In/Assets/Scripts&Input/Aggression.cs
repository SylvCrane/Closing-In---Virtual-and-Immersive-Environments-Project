using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aggression : MonoBehaviour
{
    public static int aggression;
    public GameObject streets;
    public GameObject buildings;
    public Material eyes;
    public GameObject eyesMovingEnd;
    public Transform eyesMovingTransform;
    public Vector3 newPos;
    public GameObject normalEyes;
    public GameObject chaosEyes;

    // Start is called before the first frame update
    void Start()
    {
        aggression = 0;
        eyesMovingTransform = eyesMovingEnd.GetComponent<RectTransform>();
        newPos = eyesMovingEnd.GetComponent<RectTransform>().localPosition;
    }

    public void increaseAggression()
    {
        aggression++;
    }

    private void Update()
    {
        if (aggression == 7)
        {
            Destroy(streets);
            Destroy(buildings);
            Destroy(normalEyes);
            Destroy(chaosEyes);
            RenderSettings.skybox = eyes;
            
            newPos.y += 0.5f;

            eyesMovingTransform.localPosition = newPos;
            StartCoroutine(DestroyWorld());
        }

        
    }

    IEnumerator DestroyWorld()
    {
        yield return new WaitForSeconds(5);
        Application.Quit();
    }

}
