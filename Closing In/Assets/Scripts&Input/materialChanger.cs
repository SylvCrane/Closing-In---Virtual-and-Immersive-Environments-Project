using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class materialChanger : MonoBehaviour
{
    public Material mat1;
    public Material mat2;
    public Material mat3;
    public Material mat4;
    public Material mat5;
    public Material mat6;
    public Material mat7;
    public Material mat8;
    public bool materialChanging = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if ((Aggression.aggression >= 2) && (Aggression.aggression <= 5) && (!materialChanging))
        {
            StartCoroutine(TakeaBreak());
        }

        if ((Aggression.aggression > 5) && (!materialChanging))
        {
            StartCoroutine(TakeaBreak2());
        }

        
    }

    IEnumerator TakeaBreak()
    {
        materialChanging = true;

        yield return new WaitForSeconds(0.5f);

        int rando = Random.Range(1, 5);

        switch (rando)
        {
            case 1:
                GetComponent<Renderer>().material = mat1;
                break;
            case 2:
                GetComponent<Renderer>().material = mat2;
                break;
            case 3:
                GetComponent<Renderer>().material = mat3;
                break;
            case 4:
                GetComponent<Renderer>().material = mat4;
                break;
        }

        materialChanging = false;
    }

    IEnumerator TakeaBreak2()
    {
        materialChanging = true;

        yield return new WaitForSeconds(0.5f);

        int rando = Random.Range(1, 5);

        switch (rando)
        {
            case 1:
                GetComponent<Renderer>().material = mat5;
                break;
            case 2:
                GetComponent<Renderer>().material = mat6;
                break;
            case 3:
                GetComponent<Renderer>().material = mat7;
                break;
            case 4:
                GetComponent<Renderer>().material = mat8;
                break;
        }

        materialChanging = false;
    }
}
