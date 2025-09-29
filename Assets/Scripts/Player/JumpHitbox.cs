using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpHitbox : MonoBehaviour
{
    public Jumping main;
    public PlayerHover hover;
    public PlayerSneak sneak;

    private void Start()
    {
        sneak = gameObject.GetComponentInParent<PlayerSneak>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.isTrigger && !sneak.IsSneaking)
        {
            hover.enabled = false;
            main.LastSide = "Reset";
            if (!other.CompareTag("Untagged"))
            {
                main.Jumps = main.MaxJumps;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.isTrigger)
        {
            hover.enabled = true;
        }
    }
}
