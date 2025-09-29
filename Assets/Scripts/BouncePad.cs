using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePad : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
       gameObject.GetComponent<Animator>().SetTrigger("On");
    }
}
