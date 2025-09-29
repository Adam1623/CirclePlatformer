using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door1Keys : MonoBehaviour
{
    private GameObject door;
    private Door1 script;
    // Start is called before the first frame update
    void Start()
    {
        door = GameObject.FindGameObjectWithTag("Door1");
        script = door.GetComponent<Door1>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            script.GotKey();
            Destroy(gameObject);
        }
    }
}
