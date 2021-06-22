using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sesKontrol : MonoBehaviour
{
    public LayerMask layerMask;
    GameObject Karakter;
    RaycastHit ray;

    float maxUzaklik;
    float katSayi;
    float sesSeviyesi;


    AudioSource ses;
    void Start()
    {
        Karakter = GameObject.FindGameObjectWithTag("Player");
        ses = GetComponent<AudioSource>();
        ses.volume = 0;
        maxUzaklik = 20;
        katSayi = 1 / maxUzaklik;
    }
    void Update()
    {
        Debug.DrawRay(this.transform.position,(Karakter.transform.position-this.transform.position),Color.red);
        if (Physics.Raycast (this.transform.position,Karakter.transform.position-this.transform.position, out ray,25,layerMask))
        {
            Debug.Log(ray.collider.gameObject.name);
            if (ray.collider.gameObject.tag == "Player")
            {
                ses.volume = Mathf.Abs(katSayi * ray.distance - 1);

                if (ray.distance>20)
                {
                    ses.volume = 0;
                }
            }
        }
    }
}