using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingSpikes : MonoBehaviour
{
    private Vector3 startpos;

    private Animator anim;
    //Set this int in the scene if you want the object to go left or right
    public float Distance;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();

        startpos = transform.position;
        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        print("AAAAA");
        anim.SetTrigger("Left");

        yield return new WaitForSeconds(1f);

        anim.SetTrigger("Right");

        yield return new WaitForSeconds(1f);

        StartCoroutine(Move());

        //if (transform.position.x < startpos.x + Distance)
        //{
        //  //  transform.position += new Vector3(Distance / 20, 0, 0) ;
        //   // Invoke("Move", 0.25f);
        //    //print("moved");
        //}
    }
}
