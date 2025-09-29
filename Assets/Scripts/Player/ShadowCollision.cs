using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowCollision : MonoBehaviour
{
    public Jumping main;
    public PlayerSneak sneak;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Death") && sneak.IsSneaking == false)
        {
            main.Death();
        }
        
        if (other.CompareTag("ShadowWall") && sneak.IsSneaking == true)
        {
            main.Death();
        }        

    }
}
