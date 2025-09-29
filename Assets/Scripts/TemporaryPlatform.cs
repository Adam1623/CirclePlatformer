using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryPlatform : MonoBehaviour
{
    private Animator anim;
    private void Start()
    {
        anim = gameObject.GetComponent<Animator>();        
    }
    private void OnCollisionExit(Collision collision)
    {
        StartCoroutine(rip(collision.gameObject.GetComponent<PlayerHover>()));
    }  

    IEnumerator rip(PlayerHover hover)
    {
        anim.SetTrigger("Destroy");
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
  
}
