using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyEnemy : MonoBehaviour
{
    public float waitTime = 20f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyChaos());
    }

    IEnumerator DestroyChaos()
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(gameObject);
    }

}
