using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public enum Slide {Left,Right,Mid}

public class karakterKontrol : MonoBehaviour
{
    Rigidbody fizik;

    public GameObject kamera;
    public GameObject oyunSonuGameObject;

    public Text metin;
    public Text eklenenPuanText;
    public Text puanText;

    Text oyunSonuText;

    public ParticleSystem sıcrama;
    public ParticleSystem doubleJump;
    public ParticleSystem oyunSonu;

    public Image[] kalpler;

    GameObject skorGO;
    Vector3 kameraIlkPoz, kameraSonPoz;

    [Range(0, 50)]
    public float hiz;

    AudioSource[] ses;
    AudioSource[] puanSesleri;
    AudioSource arkaPlanSesi;

    int bir, iki, uc, random;
    int skorGO_Sayac;
    int randomMax = 10;
    int eklenenPuan = 10;
    int dogruOlan;
    int kalpSize;
    int puan;

    float olumsuzlukSuresi = 0;
    float olumsuzlukSuresiBitis = 0;

    bool ziplamaEngel = false;
    bool ciftZipla = false;
    bool basla = false;
    bool olumsuz = false;
    bool dur = false;
    bool ziplamaOnay = false;
    
    bool sag = false;
    bool sol = false;
    bool geriAt = false;

    public Slide mySlide = Slide.Mid;

    Vector3 yeniKonum;
    float xYeniKonum;
    float x;

    Animator anim;
    Animator karakter;
    void Start()
    {
        anim = GameObject.FindGameObjectWithTag("Animasyon").GetComponent<Animator>();
        ses = GetComponents<AudioSource>();
        puanSesleri = GameObject.FindGameObjectWithTag("puanSesleri").GetComponents<AudioSource>();

        fizik = GetComponent<Rigidbody>();
        kameraIlkPoz = kamera.transform.position - transform.position;
        skorGO = GameObject.FindGameObjectWithTag("skor");

        puan = 0;
        kalpSize = kalpler.Length;
        karakter = GetComponent<Animator>();
        YeniSayi();

        yeniKonum = transform.position;
        xYeniKonum = transform.position.x;            
    }
    void Awake()
    {
        DontDestroyOnLoad(GameObject.FindGameObjectWithTag("arkaPlanSesi"));
        arkaPlanSesi = GameObject.FindGameObjectWithTag("arkaPlanSesi").GetComponent<AudioSource>();
        if (!arkaPlanSesi.isPlaying)
        {
            arkaPlanSesi.Play();
        }
        else
        {

        }
    }
    void Update()
    {
  //     BilgisayarKontrolleri();
       TelefonKontrolleri();
        Olumsuzluk();
        SagSolHareket();
        puanText.text = puan.ToString();  
    }
    void FixedUpdate()
    {
        if (basla)
        {
            if (!dur)
            {
               //   fizik.velocity = new Vector3(fizik.velocity.x, fizik.velocity.y, hiz);
                  transform.Translate(0, 0, Time.deltaTime * hiz);
            }
            else
            {
                
            }
        }
    }
    private void SagSolHareket()
    {
        if (sag)
        {
            if (mySlide == Slide.Mid)
            {
                yeniKonum = new Vector3(4.4f, transform.position.y, transform.position.z);
                xYeniKonum = 4.4f;
                mySlide = Slide.Right;
                ses[0].Play();
                //     karakter.SetTrigger("sag");
            }
            else if (mySlide == Slide.Left)
            {
                yeniKonum = new Vector3(0, transform.position.y, transform.position.z);
                xYeniKonum = 0;
                mySlide = Slide.Mid;
                ses[0].Play();
                //      karakter.SetTrigger("sag");
            }
        }
        else if (sol)
        {
            if (mySlide == Slide.Mid)
            {
                yeniKonum = new Vector3(-4.4f, transform.position.y, transform.position.z);
                xYeniKonum = -4.4f;
                mySlide = Slide.Left;
                ses[0].Play();
                //       karakter.SetTrigger("sol");
            }
            else if (mySlide == Slide.Right)
            {
                yeniKonum = new Vector3(0, transform.position.y, transform.position.z);
                xYeniKonum = 0;
                mySlide = Slide.Mid;
                ses[0].Play();
                //        karakter.SetTrigger("left");
            }
        }
        x = Mathf.Lerp(x, xYeniKonum, Time.deltaTime * 10);
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
    }
    private void YeniSayi()
    {
        skorGO_Sayac--;
        int temp;
        int islem;

        randomMax += 5;

        random = Random.Range(0, 300);
        dogruOlan = random % 3;
        islem = (random % 3) + 1;

        if (islem == 1)
        {
            bir = Random.Range((int)(randomMax / 10), randomMax);
            iki = Random.Range((int)(randomMax / 10), randomMax);
            uc = bir + iki;
            metin.text = bir + " + " + iki;
        }
        else if (islem == 2)
        {
            bir = Random.Range((int)(randomMax / 10), randomMax);
            iki = Random.Range((int)(bir / 10), bir);
            uc = bir - iki;
            metin.text = bir + " - " + iki;
        }
        else if (islem == 3)
        {
            bir = Random.Range((int)(randomMax / 10), (int)(randomMax / 10) + 3);
            iki = Random.Range((int)(randomMax / 10), (int)(randomMax / 10) + 3);
            uc = bir * iki;
            metin.text = bir + " * " + iki;
        }

        temp = uc + 10;

        for (int j = 0; j < 3; j++)
        {
            if (j == dogruOlan)
            {
                skorGO.transform.GetChild(j).GetChild(0).GetChild(0).GetComponent<Text>().text = (uc).ToString();
            }
            else
            {
                skorGO.transform.GetChild(j).GetChild(0).GetChild(0).GetComponent<Text>().text = (temp).ToString();
                if (temp - 20>=0)
                {
                    temp -= 20;
                }
                else
                {
                    temp -= 4;
                }
            }
        }
    }
    private void Olumsuzluk()
    {
        if (olumsuz)
        {
            olumsuzlukSuresiBitis += Time.deltaTime;
            if (olumsuzlukSuresiBitis < 5f)
            {
                olumsuzlukSuresi += Time.deltaTime;
                if (olumsuzlukSuresi < 0.2f)
                {
                    transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().enabled = false;
                }
                else if (olumsuzlukSuresi < 0.4f)
                {
                    transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().enabled = true;
                }
                else
                {
                    olumsuzlukSuresi = 0;
                }
            }
            else
            {
                olumsuzlukSuresiBitis = 0;
                transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().enabled = true;
                olumsuz = false;
            }
        }
    }
    void LateUpdate()
    {
        KameraKontrol();
    }
    void KameraKontrol()
    {
        kameraSonPoz = kameraIlkPoz + transform.position;  
        kamera.transform.position = Vector3.Lerp(kamera.transform.position, kameraSonPoz, 0.82f);
   
    } 
    void SecimEkranı()
    {
        arkaPlanSesi.Stop();
        SceneManager.LoadScene("SecimEkrani");
    }
    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "ustSinir")
        {
            ziplamaEngel = false;
        }
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "skorEkran")
        {
            if (int.Parse(col.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text) == uc)
            {
                eklenenPuan = randomMax;
                puan += eklenenPuan;
                eklenenPuanText.text = "+" + eklenenPuan.ToString();
                eklenenPuanText.color = Color.green;
                anim.SetTrigger("puanAnim");
                puanSesleri[0].Play();
            }
            else
            {
                HataliYol();
                Can();
                puan -= eklenenPuan;
            }
            YeniSayi();
            KonumDegistir(col);
        }
        if (col.gameObject.tag == "engel" && !olumsuz)
        {
            Carpma();
            Can();
        }
        if (col.gameObject.tag == "engel2")
        {
            HataliYol();
            Can();
            YeniSayi();
            KonumDegistir(col);
        }
        if (col.gameObject.tag == "araba" )
        {
            if (!olumsuz)
            {
                Carpma();
                Can();
                col.transform.parent.transform.GetChild(0).gameObject.SetActive(false);
            }
            else
            {
                col.transform.parent.transform.GetChild(0).gameObject.SetActive(false);
            }

        }
        if (col.gameObject.tag == "ustSinir")
        {
            ziplamaEngel = true;
        }
        if (col.gameObject.tag == "lav")
        {
            sıcrama.transform.position = transform.position + new Vector3(0, 0.4f, 0);
            sıcrama.Play();
            puanSesleri[2].Play();
            transform.position = new Vector3(0, 0, transform.position.z - 20f);
            Can();
        }
        if (col.gameObject.tag == "elleZipla")
        {
            karakter.SetTrigger("elleZipla");
        }
        if (col.gameObject.tag == "altin")
        {
            col.gameObject.GetComponent<MeshRenderer>().enabled = false;
            col.gameObject.GetComponent<Collider>().enabled = false;
            col.gameObject.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
            col.gameObject.GetComponent<AudioSource>().Play();
            Destroy(col.gameObject,4);
            puan += 5;
        }
        if (col.gameObject.tag == "duvarCol" && !olumsuz)
        {
            Can();
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.4f);
            karakter.SetTrigger("duvar");
            dur = true;
            Invoke("Duvar", 2.5f);
            olumsuz = true;
            col.transform.parent.gameObject.GetComponent<Collider>().enabled = false;
            dur = true;
        }
        if (col.gameObject.tag == "oyunSonu")
        {
            PuanıKaydet();
            metin.gameObject.SetActive(false);
            skorGO.SetActive(false);
            GameObject.FindGameObjectWithTag("Player").GetComponent<Collider>().enabled = false;
            transform.GetComponent<Rigidbody>().isKinematic = true;
            dur = true;
            karakter.SetTrigger("ziplamaOnay");
        //    Invoke("oyunSonuFonksiyon",1);
        }
        if (col.gameObject.tag == "ziplamaOnay")
        {
             ziplamaOnay = true;
        }
        if (col.gameObject.tag =="ziplamaEngel")
        {
            ziplamaOnay = false;
        }
    }
    private void oyunSonuFonksiyon()
    {
        oyunSonuGameObject.SetActive(true);
        oyunSonuGameObject.GetComponent<Animator>().SetTrigger("oyunSonuEfekt");
        oyunSonuText = GameObject.FindGameObjectWithTag("oyunSonuText").GetComponent<Text>();
        oyunSonuText.text = puanText.text;
    }
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "yer")
        {
            ciftZipla = false;
            ziplamaEngel = false;
        }
    }
    private void HataliYol()
    {
        eklenenPuan = randomMax - 8;
        eklenenPuanText.text = "-" + eklenenPuan.ToString();
        eklenenPuanText.color = Color.red;
        anim.SetTrigger("puanAnim");
        puanSesleri[1].Play();
    }
    private void Duvar()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 8f);
        dur = false;
    }
    private void Carpma()
    {
        olumsuz = true;
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 12f);
        eklenenPuan = 30;
        eklenenPuanText.text = "-" + eklenenPuan.ToString();
        eklenenPuanText.color = Color.red;
        anim.SetTrigger("puanAnim");
    }
    private void Can()
    {
        if (kalpSize > 0)
        {
            Destroy(kalpler[kalpSize - 1]);
            kalpSize--;
        }
        else
        {
            dur = true;
            ziplamaOnay = false;
            karakter.SetTrigger("death");
            PuanıKaydet();
            StartCoroutine(OyunBasla());
        }
    }

    private IEnumerator OyunBasla()
    {
        yield return new WaitForSeconds(2f);
       SceneManager.LoadScene("Oyun");
    }

    private void PuanıKaydet()
    {
        if (puan > PlayerPrefs.GetInt("puanOyun"))
        {
            PlayerPrefs.SetInt("puanOyun", puan);
        }
    }
    private static void KonumDegistir(Collider col)
    {
        col.transform.parent.transform.position += new Vector3(0, 0, 35f);
        col.transform.parent.transform.position = new Vector3(
            col.transform.parent.transform.position.x,
            Random.Range(3, 6),
            col.transform.parent.transform.position.z);
    }
    public void Kay()
    {
        if (basla)
        {
            karakter.SetTrigger("kay");
        }    
    }
    void TelefonKontrolleri()
    {
        sag = Input.GetKeyDown(KeyCode.E);
        sol = Input.GetKeyDown(KeyCode.Q);
        if (Input.touchCount > 0)
            {
                Touch parmak = Input.GetTouch(0);
                if ( (Mathf.Abs(parmak.deltaPosition.y) > Mathf.Abs(parmak.deltaPosition.x))
                && parmak.phase == TouchPhase.Ended)
            {
                Kay();
            }
                else
                {
                sag = parmak.deltaPosition.x > 0 && parmak.phase == TouchPhase.Ended;
                sol = parmak.deltaPosition.x < 0 && parmak.phase == TouchPhase.Ended;
                 }

               if (parmak.deltaPosition.y == 0 && parmak.phase == TouchPhase.Ended && !ziplamaEngel && ziplamaOnay)
            {
                if (basla)
                {
                    ses[1].Play();

                    fizik.velocity = new Vector3(0, 0, fizik.velocity.z);
                    fizik.AddForce(new Vector2(0, 350));
                    if (!ciftZipla)
                    {
                        karakter.SetTrigger("zipla");
                        ciftZipla = true;
                    }
                    else
                    {
                        karakter.SetTrigger("ziplaX2");
                        ziplamaEngel = true;
                    }
                }
            }
               else
                 {
                karakter.SetTrigger("basla");
                basla = true;
                 }
        }    
    }
    private void BilgisayarKontrolleri()
    {
        sag = Input.GetKeyDown(KeyCode.E);
        sol = Input.GetKeyDown(KeyCode.Q);

        if (Input.GetButtonDown("Fire1") && !basla)
        {
            if (!basla)
            {
                karakter.SetTrigger("basla");
                basla = true;
            }
        }
        if (Input.GetButtonDown("Fire1")  && !ziplamaEngel && ziplamaOnay && !EventSystem.current.IsPointerOverGameObject())
        {
            if (!basla)
            {
                karakter.SetTrigger("basla");
            }
            if (basla)
            {
                ses[1].Play();

                fizik.velocity = new Vector3(0, 0, fizik.velocity.z);
                fizik.AddForce(new Vector2(0, 350));

                if (!ciftZipla)
                {
                    karakter.SetTrigger("zipla");
                    ciftZipla = true;
                }
                else
                {
                    karakter.SetTrigger("ziplaX2");
                    ziplamaEngel = true;
                }
            }
            basla = true;
        }
        if ((Input.GetKeyDown(KeyCode.S)))
        {
            Kay();
        }
    }
}