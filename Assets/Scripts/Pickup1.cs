using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup1 : MonoBehaviour
{
    private GameObject Win;
    private WinPortal script;
    // Start is called before the first frame update
    void Start()
    {
        Win = GameObject.FindGameObjectWithTag("Win");
        script = Win.GetComponent<WinPortal>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
           script.Collectible1();
           Destroy(gameObject);
        }
    }
}
