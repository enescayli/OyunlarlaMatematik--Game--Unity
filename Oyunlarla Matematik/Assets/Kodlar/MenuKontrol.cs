using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuKontrol : MonoBehaviour
{
    public Text highScoreTxt;
  //  public ParticleSystem patlama;

    void Start()
    {
        PlayerPrefs.SetInt("Islem", 1);
        highScoreTxt.text = PlayerPrefs.GetInt("puan").ToString();
    }
    void Update()
    {
        
    }
    public void Zorluk(int zorluk) 
    {
        PlayerPrefs.SetInt("Zorluk",zorluk);
        SceneManager.LoadScene("Islem");
    }

    public void IslemCek(int islem)
    {
        if (islem == 1) //Toplama
        {
            PlayerPrefs.SetInt("Islem", islem);
            PlayerPrefs.SetInt("katSayi",1);
            // patlama.transform.position=
        }
        else if (islem == 2) // Çıkarma
        {
            PlayerPrefs.SetInt("Islem", islem);
            PlayerPrefs.SetInt("katSayi", 2);
        }
        else if (islem == 3) //Çarpma
        {
            PlayerPrefs.SetInt("Islem", islem);
            PlayerPrefs.SetInt("katSayi", 4);
        }
        else if (islem == 4) // Bölme
        {
            PlayerPrefs.SetInt("Islem", islem);
            PlayerPrefs.SetInt("katSayi", 1);
        }
    }
    public void Oyun()
    {
        SceneManager.LoadScene("Oyun");
    }
}