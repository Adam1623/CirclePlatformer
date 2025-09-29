using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Jumping : MonoBehaviour
{
    public GameObject JumpIndicator;

    public Rigidbody rigid;

    public GameObject camera1;

    private PlayerHover hover;

    private PlayerSneak sneak;

    private JumpHitbox hb1;

    public int MaxJumps;
    public int Jumps;

    public string CurSide;
    public string LastSide;
    public bool CanWallJump;
    private int BoostCheck;
    //private bool CanDashLeft;
    //private bool CanDashRight;

    // Start is called before the first frame update
    void Start()
    {
        camera1 = GameObject.FindGameObjectWithTag("MainCamera");
        hover = gameObject.GetComponent<PlayerHover>();
        sneak = gameObject.GetComponent<PlayerSneak>();
        hb1 = gameObject.GetComponent<JumpHitbox>();
        Time.fixedDeltaTime = 0.005f;
        //0.005f
        MaxJumps = 1;
        Jumps = MaxJumps;
        LastSide = "None";
        CanWallJump = false;
        //CanDashLeft = true;
        //CanDashRight = true;
    }

    private void LateUpdate()
    {

        if (gameObject.transform.position.y > Camera.main.transform.position.y && gameObject.transform.position.y - Camera.main.transform.position.y > 0)
        {
            Camera.main.transform.position = new Vector3(0, gameObject.transform.position.y, -10);
        }

        if (gameObject.transform.position.y < Camera.main.transform.position.y && Camera.main.transform.position.y > 0)
        {
            Camera.main.transform.position = new Vector3(0, gameObject.transform.position.y, -10);
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        BoostCheck = 0;

        if (Input.anyKey)
        {
            ButtonPress();
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            if (Jumps > 0)
            {
                Jumps -= 1;
                Jump(10);
            }
        }

        if (Jumps > 0)
        {
            JumpIndicator.SetActive(true);
        }

        if (Jumps == 0)
        {
            JumpIndicator.SetActive(false);
        }

        //if (rigid.velocity.y > 10f)
        //{
        //    rigid.velocity = new Vector3(rigid.velocity.x, 10);
        //}

        if (transform.position.y < -10f)
        {
            Death();
        }
    }

    private void ButtonPress()
    {
        if (Input.GetKey(KeyCode.W) && CanWallJump == true && LastSide != CurSide)
        {
            LastSide = CurSide;
            Jump(10);                      
        }
        Vector3 left = new Vector3(-0.1f, 0, 0);
        if (Input.GetKey(KeyCode.A) && rigid.linearVelocity.x > -10f)
        {
            rigid.AddForce(left, ForceMode.VelocityChange);
        }

        Vector3 right = new Vector3(0.1f, 0, 0);
        if (Input.GetKey(KeyCode.D) && rigid.linearVelocity.x < 10f)
        {
            rigid.AddForce(right, ForceMode.VelocityChange);
        }
        
        
        //if (Input.GetKey(KeyCode.D) && Input.GetKeyDown(KeyCode.Space))
        //{
        //    StartCoroutine(DashRight());
        //}

        //if (Input.GetKey(KeyCode.A) && Input.GetKeyDown(KeyCode.Space))
        //{
        //    StartCoroutine(DashLeft());
        //}
    }

   
    //IEnumerator DashLeft()
    //{
    //    if (CanDashLeft == true)
    //    {
    //        rigid.velocity = new Vector3(0, rigid.velocity.y);
    //        rigid.AddForce(new Vector3(-20f, 0), ForceMode.VelocityChange);
    //        CanDashLeft = false;
    //    }
    //    yield return new WaitForSeconds(1f);
    //    CanDashLeft = true;
    //}

    //IEnumerator DashRight()
    //{
    //    if (CanDashRight == true)
    //    {
    //        rigid.velocity = new Vector3(0, rigid.velocity.y);
    //        rigid.AddForce(new Vector3(20f, 0), ForceMode.VelocityChange);
    //        CanDashRight = false;
    //    }
    //    yield return new WaitForSeconds(1f);
    //    CanDashRight = true;
    //}
  
    private void Jump(float power)
    {
        BoostCheck += 1;
        var up = new Vector3(0, power, 0);
        if (BoostCheck == 2)
        {
            up /= 1.8f;
        }
        rigid.linearVelocity -= new Vector3(0, rigid.linearVelocity.y / 2);
        rigid.AddForce(up, ForceMode.VelocityChange);

        if (hover.IsInvoking("SetYTo0"))
        {
            hover.Stop();
        }
    }

    public void Death()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnCollisionEnter(Collision collision)
    {
        string ColliderTag;
        ColliderTag = collision.gameObject.tag;

        if (ColliderTag == "Slant")
        {
            Jumps = MaxJumps;
        }

        var Boostdirection = Vector3.zero;
        if (ColliderTag == "Bouncer")
        {
            Boostdirection = new Vector3(0, 18f, 0);
            Bouncer(Boostdirection);
        }
        
        if (ColliderTag == "BouncerRight")
        {
            Boostdirection = new Vector3(12f, 0, 0);
            Bouncer(Boostdirection);
        }

        if (ColliderTag == "BouncerLeft")
        {
            Boostdirection = new Vector3(-12f, 0, 0);
            Bouncer(Boostdirection);
        }
    }

    private void Bouncer(Vector3 Direction)
    {
        Jumps = MaxJumps;
        rigid.linearVelocity += new Vector3(0, 1);
        rigid.AddForce(Direction, ForceMode.VelocityChange);

        hover.StopAllCoroutines();
        hover.StopHover();
        hover.CanHover = true;

        sneak.CanSneak = true;
    }
}