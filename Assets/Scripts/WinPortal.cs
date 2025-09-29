using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinPortal : MonoBehaviour
{
    public GameObject menu;
    private int Collectible1Left;    
    // Start is called before the first frame update
    void Start()
    {
        var TotalPickups = FindObjectsOfType<Pickup1>();
        Collectible1Left = TotalPickups.Length;

        if (Collectible1Left <= 0)
        {
           Invoke("WinPortalActivate", 0.1f);
        }
        
       Invoke("Disable", 0f);
    }

    private void Disable()
    {
        gameObject.SetActive(false);
    }
    public void Collectible1()
    {
        Collectible1Left -= 1;

        if (Collectible1Left <= 0) 
        {
            WinPortalActivate();
        }
       
    }


    public void WinPortalActivate()
    {
        gameObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        //SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);

        //SceneManager.MoveGameObjectToScene(menu, nextscene);

        //SceneManager.SetActiveScene(nextscene);

    }
}
