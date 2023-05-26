using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blinker : MonoBehaviour
{
    public GameObject closedEye;
    public float openDuration;
    public bool blinking;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!blinking)
        {
            openDuration = Random.Range(1, 4);
            StartCoroutine(Blink());
            closedEye.SetActive(false);
            Debug.Log("Open");
        }
    }

    private IEnumerator Blink ()
    {
        blinking = true;
        yield return new WaitForSeconds(openDuration);
        closedEye.SetActive(true);
        Debug.Log("Blinking");
        yield return new WaitForSeconds(0.2f);
        blinking = false;
    }
}
