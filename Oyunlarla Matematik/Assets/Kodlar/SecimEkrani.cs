using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SecimEkrani : MonoBehaviour
{
    public Text enyuksekPuanPratikTxt;
    public Text enyuksekPuanOyunTxt;
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "SecimEkrani")
        {
            enyuksekPuanPratikTxt.text = PlayerPrefs.GetInt("puan").ToString();
            enyuksekPuanOyunTxt.text = PlayerPrefs.GetInt("puanOyun").ToString();
            //Bu kısım sadece SecimEkrani sayfasında gerekli. Bu yüzden if içine yazdım;
        }
    }
    public void Oyun()
    {
        SceneManager.LoadScene("Oyun");
    }
    public void Menu()
    {
        SceneManager.LoadScene("AnaMenu");
    }
    public void SecimEkrani_()
    {
        SceneManager.LoadScene("SecimEkrani");
    }
}