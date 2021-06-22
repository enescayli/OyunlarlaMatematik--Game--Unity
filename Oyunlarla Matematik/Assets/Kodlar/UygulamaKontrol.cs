using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UygulamaKontrol : MonoBehaviour
{
    public Text ilkSayi;
    public Text ikinciSayi;
    public Text sonuc;

    public Text sayacTxt;
    public Text sureText;

    public Text puanText;
    public Text islemTxt;

    public ParticleSystem patlama;
    Text puanAnim;

    AudioSource ses;
    AudioSource sesTemel;

    public AudioClip []sesTemelKlip;
    public AudioClip[] klip;

    //  public InputField girdi;

    int zorluk;
    int ilk,iki,islemSonucu,puan=0;

    int eklenenPuan;
    int eksilenPuan;
    
    int islem;
    int cevap;
    int puanKatsayisi;

    int[] bolenler;

    float sayac = 20;
    float sure = 20;

    Animator anim;

    void Start()
    {
        //   ilkSayi = GameObject.FindWithTag("Player").GetComponent<Text>();
        ses = GetComponent<AudioSource>();
        sesTemel = GameObject.FindGameObjectWithTag("ses").GetComponent<AudioSource>();
        anim = GameObject.FindGameObjectWithTag("Animasyon").GetComponent<Animator>();
        puanAnim = GameObject.FindGameObjectWithTag("puanAnim").GetComponent<Text>();

        zorluk = PlayerPrefs.GetInt("Zorluk");
        islem = PlayerPrefs.GetInt("Islem");

        puanKatsayisi = PlayerPrefs.GetInt("katSayi");
        sure = 130;
        YeniSayi();
        Islem();
    }
    void Update()
    {
        if (puan > PlayerPrefs.GetInt("puan"))
        {
            PlayerPrefs.SetInt("puan", puan);
        }
        puanText.text = puan.ToString();
        sureText.text = puan.ToString();
        if (sayac>=1f)
        {
            sayacTxt.text = ((int)sayac).ToString();
            sayac -= Time.deltaTime;
        }
        else
        {
            sayacTxt.text = "0";
        }

        if (sure>=1.2f)
        {
            sureText.text= ((int)sure).ToString();
            sure -= Time.deltaTime;
           // sureText.color = new Color((255 - (int)sure * 4)/255, 0, 0);
            
            Color newColor = new Color((255-sure*2)/255, 0, 0);
            sureText.color = newColor; 
        }
        else
        {
            sureText.text = "0";
            sure = 0;
            SceneManager.LoadScene("AnaMenu");
        }
        
    }
    public void SayiCek(int sayi)
    {
        if (sonuc.text.Length<9)
        {
            sonuc.text = sonuc.text + sayi.ToString();
            ses.clip = klip[sayi];
            ses.Play();
        }
    }
    public void Temizle() 
    {
        if (sonuc.text!="")
        {
        patlama.Play();
        sesTemel.clip = sesTemelKlip[3];
        sesTemel.Play();
        sonuc.text = "";
        }
    }
    public void SonRakamiSil()
    {
        if (sonuc.text!="")
        {
            sesTemel.clip = sesTemelKlip[2];
            sesTemel.Play();
         //  sonuc.color = Color.red;
            sonuc.text = sonuc.text.Substring(0, sonuc.text.Length - 1);
        }
   //    sonuc.text.Remove(1,2);     
    }
    public void Kontrol()
    {
        if (sure >0 && sonuc.text!="")
        {
            if (islemSonucu == int.Parse(sonuc.text))
            {
                sesTemel.clip = sesTemelKlip[0];
                sesTemel.Play();
                if (sayac>=1)
                {
                    eklenenPuan = eklenenPuan * (int)sayac;
                }
                else
                {
                    eklenenPuan = eklenenPuan*1; // Bir şey değiştirmese de kontrol & sonradan güncelleme durumları için.
                }
                puan += eklenenPuan;
                puanAnim.text = "+" + eklenenPuan;
                puanAnim.color = Color.green;
                anim.SetTrigger("kazanma");
            }
            else
            {
                sesTemel.clip = sesTemelKlip[1];
                sesTemel.Play();
                if (sayac >= 1)
                {
                    eksilenPuan = eksilenPuan * (int)sayac;
                }
                else
                {
                    eksilenPuan = eksilenPuan * 1;
                }

                puan -= eksilenPuan;
                puanAnim.text = "-" + eksilenPuan;
                puanAnim.color = Color.red;
                anim.SetTrigger("kazanma");
            }
            YeniSayi();
        }

    }
    void YeniSayi()
    {
        if (zorluk==4)
        {
            eklenenPuan = 10 * puanKatsayisi;
            eksilenPuan = 5;

            ilk = Random.Range(20, 1200);

            if (islem == 2)
            {
                iki = Random.Range(0, ilk);
                Debug.Log("Bueaya Girdiğe");
            }
            else if (islem==3)
            {
                iki= Random.Range(4, 40);
            }
            else if (islem == 4)
            {
                Bolenler();
            }
            else
            {
                iki = Random.Range(20, 1200);
            }

        }

        else if (zorluk == 3)
        {
            eklenenPuan = 7 * puanKatsayisi ;
            eksilenPuan = 4;
            ilk = Random.Range(7, 350);

            if (islem == 2)
            {
                iki = Random.Range(0, ilk);
            }
            else if (islem==3)
            {
                iki = Random.Range(4, 24);
            }
            else if (islem == 4)
            {
                Bolenler();
            }
            else
            {
                iki = Random.Range(7, 350);
            }

        }

        else if (zorluk == 2)
        {
            eklenenPuan = 4 * puanKatsayisi * puanKatsayisi;
            eksilenPuan = 3 ;
            ilk = Random.Range(4, 150);

            if (islem == 2)
            {
                iki = Random.Range(0, ilk);
            }
            else if (islem == 3)
            {
                iki = Random.Range(4, 12);
            }
            else if (islem == 4)
            {
                Bolenler();
            }
            else
            {
                iki = Random.Range(4, 150);
            }

        }

        else if (zorluk == 1)
        {
            eklenenPuan = 3 * puanKatsayisi;
            eksilenPuan = 2 ;
            ilk = Random.Range(2, 70);

            if (islem == 2)
            {
                iki = Random.Range(0, ilk);
            }
            else if (islem == 3)
            {
                iki = Random.Range(2, 7);
            }
            else if (islem == 4)
            {
                Bolenler();
            }
            else
            {
                iki = Random.Range(2, 70);
            }

        }
        
        else if (zorluk==0)
        {
            eklenenPuan = 2 * puanKatsayisi;
            eksilenPuan = 1;
            ilk = Random.Range(0, 10);

            if (islem==2)
            {
                iki = Random.Range(0,ilk);
            }
            else if (islem == 3)
            {
                iki = Random.Range(0, 5);
            }
            else if (islem==4)
            {
                Bolenler();
            }
            else
            {
               iki = Random.Range(0, 10);
            }         

        }

        ilkSayi.text = ilk.ToString();
        ikinciSayi.text = iki.ToString();

        Islem();
        islemSonucu = cevap;
        sonuc.text = "";
        sayac = 20;
        Debug.Log(cevap);
    }
    private void Bolenler()
    {
        int j = 0;
        int temp;
        bolenler = new int[ilk];
        iki = 1;
        for (int i = 1; i <= ilk; i++)
        {
            if (ilk % i == 0)
            {
                bolenler[j] = i;
                j++;
            }
        }
        if (j > 1)
        {
            temp = Random.Range(1, j);
            iki = bolenler[temp];
        }
        else
        {
            iki = 1;
        }
    }
    void Islem()
    {
        if (islem == 1)
        {
            cevap = ilk + iki;
            islemTxt.text = "+";
        }
        else if (islem == 2)
        {
            cevap = ilk - iki;
            islemTxt.text = "-";
        }
        else if (islem == 3)
        {
            cevap = ilk * iki;
            islemTxt.text = "*";
        }
        else if (islem == 4)
        {
            cevap = ilk / iki;
            islemTxt.text = "/";
        }
    }
    public void LevelSecimEkrani()
    {
        SceneManager.LoadScene("AnaMenu");

        if ((GameObject.FindGameObjectWithTag("arkaPlanSesi"))!=null)
        {
            Destroy((GameObject.FindGameObjectWithTag("arkaPlanSesi")));
        }
    }
    public void Tekrar()
    {
        SceneManager.LoadScene("Islem");
    }
}