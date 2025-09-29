using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door2 : MonoBehaviour
{
    public GameObject ExtraDoor1;
    public GameObject ExtraDoor2;
    public GameObject EnabledDoor1;
    public GameObject EnabledDoor2;
    private int KeysLeft;
    // Start is called before the first frame update
    void Start()
    {
        var TotalPickups = FindObjectsOfType<Door2Keys>();
        KeysLeft = TotalPickups.Length;

        if (EnabledDoor1 != null)
        {
            EnabledDoor1.SetActive(false);
        }

        if (EnabledDoor2 != null)
        {
            EnabledDoor2.SetActive(false);
        }

    }

    public void GotKey()
    {
        KeysLeft -= 1;

        if (KeysLeft <= 0)
        {
            DoorOpen();
        }

    }
    public void DoorOpen()
    {
        gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, -1);
        gameObject.GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, 6);

        if (ExtraDoor1 != null)
        {
            Destroy(ExtraDoor1);
        }

        if (ExtraDoor2 != null)
        {
            Destroy(ExtraDoor2);
        }

        if (EnabledDoor1 != null)
        {
            EnabledDoor1.SetActive(true);
        }

        if (EnabledDoor2 != null)
        {
            EnabledDoor2.SetActive(true);
        }
    }
}
