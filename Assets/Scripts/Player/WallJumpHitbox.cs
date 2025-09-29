using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJumpHitbox : MonoBehaviour
{
    public Jumping main;
    public PlayerHover hover;
    public PlayerSneak sneak;
    public string Side;
    private void Start()
    {
        sneak = gameObject.GetComponentInParent<PlayerSneak>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.isTrigger && !sneak.IsSneaking)
        {
            if (!other.CompareTag("Untagged") && !other.CompareTag("Slant"))
            {
                main.CurSide = Side;
                main.CanWallJump = true;
            }
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (!other.isTrigger && !sneak.IsSneaking)
    //    {
    //        if (!other.CompareTag("Untagged") && !other.CompareTag("Slant"))
    //        {
    //            main.CurSide = Side;
    //            main.CanWallJump = true;
    //        }
    //    }
    //}
    private void OnTriggerExit(Collider other)
    {
        main.CanWallJump = false;
    }
}
