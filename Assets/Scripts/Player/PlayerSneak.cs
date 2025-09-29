using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerSneak : MonoBehaviour
{
    //Used to make arrow darker
    public SpriteRenderer arrow;

    //Gets disabled during sneak
    public ParticleSystem blur;

    //Particle flash when sneak is ready again
    public ParticleSystem ready;

    //not sure but here it is
    private Jumping mainscript;

    //Used to make player trigger instead of collide
    private SphereCollider collide;

    //changes player color
    private SpriteRenderer render;

    //when picking up shadow orb
    public GameObject explosionParticles;

    //saves any object's color that is changed so it can be reverted once sneak ends
    private Color arrowstartcolor;
    private Color playerstartcolor;

    public bool IsSneaking;

    //whether the player can left click to sneak or not
    public bool CanSneak;

    //Whether sneak will active when hitting ground or not
    private bool SneakActivate;

    public JumpHitbox jhb;
    public WallJumpHitbox wjhb1;
    public WallJumpHitbox wjhb2;
    

    void Start()
    {
        mainscript = gameObject.GetComponent<Jumping>();

        collide = gameObject.GetComponent<SphereCollider>();

        render = gameObject.GetComponent<SpriteRenderer>();

        playerstartcolor = render.color;
        arrowstartcolor = arrow.color;

        IsSneaking = false;
        CanSneak = true;

        if (SceneManager.GetActiveScene().buildIndex > 20)
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
        string tag;
        tag = collision.gameObject.tag;

        if (tag == "SneakPower")
        {
            enabled = true;

            GameObject splode;
            splode = Instantiate(explosionParticles, collision.gameObject.transform.position, collision.gameObject.transform.rotation);

            var access = splode.GetComponent<ParticleSystem>().main;
            access.startColor = Color.black;

            Destroy(collision.gameObject);
        }

        if (tag == "Ground" && SneakActivate == true)
        {
            CanSneak = true;
        }

        if (tag == "Ground" && SneakActivate == false && CanSneak == false)
        {
             Invoke("Buffer", 0.5f);
        }      
    }   

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Space) && CanSneak == true)
        {
            StartCoroutine(Sneak());
        }
    }

    private void Buffer()
    {
        CanSneak = true;
    }

    IEnumerator Sneak()
    {
        IsSneaking = true;

        jhb.enabled = false;
        wjhb1.enabled = false;
        wjhb2.enabled = false;

        SneakActivate = false;
        CanSneak = false;

        collide.isTrigger = true;

        render.color = Color.black;
        arrow.color -= new Color(0.3f, 0.3f, 0.3f, 0);

        ready.Play();

        blur.gameObject.SetActive(false);

        yield return new WaitForSeconds(0.8f);

        IsSneaking = false;

        jhb.enabled = true;
        wjhb1.enabled = true;
        wjhb2.enabled = true;

        collide.isTrigger = false;

        arrow.color = arrowstartcolor;
        render.color = playerstartcolor;

        blur.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        SneakActivate = true;    
    }
}
