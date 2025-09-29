using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHover : MonoBehaviour
{
    private Jumping mainscript;

    private Rigidbody rigid;

    public ParticleSystem HoverParticles;

    public GameObject explosionParticles;

    public bool CanHover;

    void Start()
    {        
        mainscript = gameObject.GetComponent<Jumping>();

        rigid = gameObject.GetComponent<Rigidbody>();

        CanHover = false;

        if (SceneManager.GetActiveScene().buildIndex > 10)
        {
            enabled = true;
        }

        else
        {
            enabled = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
       
        if (collision.gameObject.CompareTag("HoverPower")) 
        {
            enabled = true;
            Instantiate(explosionParticles, collision.gameObject.transform.position, collision.gameObject.transform.rotation);
            Destroy(collision.gameObject);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            StopAllCoroutines();
            StopHover();
            CanHover = true;
        }
    }

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.E) && CanHover == true)
        {
            StartCoroutine(Hover());
        }
    }

    IEnumerator Hover()
    {
        CanHover = false;
        rigid.useGravity = false;
        HoverParticles.Play();
        InvokeRepeating("SetYTo0", 0f, 0.01f);

        yield return new WaitForSeconds(1f);

        StopHover();     
    }

    public void StopHover()
    {
        rigid.useGravity = true;
        HoverParticles.Clear();
        HoverParticles.Pause();
        CancelInvoke("SetYTo0");    
    }

    public void Stop()
    {      
        StopCoroutine(Hover());
        
        StopHover();
    }

    private void SetYTo0()
    {
        rigid.linearVelocity = new Vector3(rigid.linearVelocity.x, 0);
    }
}
