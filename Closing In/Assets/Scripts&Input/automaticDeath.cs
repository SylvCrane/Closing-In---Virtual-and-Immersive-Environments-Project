using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class automaticDeath : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(automaticKill());
    }

    IEnumerator automaticKill()
    {
        yield return new WaitForSeconds(150);
        Application.Quit();
    }
}
