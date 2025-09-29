using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door2Keys : MonoBehaviour
{
    private GameObject door;
    private Door2 script;
    // Start is called before the first frame update
    void Start()
    {
        door = GameObject.FindGameObjectWithTag("Door2");
        script = door.GetComponent<Door2>();
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
