using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject win_Panel;
    [SerializeField] GameObject lose_Panel;

    [SerializeField] Image[] lifes_Img;
    [SerializeField] TextMeshProUGUI score_Txt;

    
    public void Win_Panel_Active()
    {
        Time.timeScale = 0;
        win_Panel.SetActive(true);
    }
    public void Lose_Panel_Active()
    {
        Time.timeScale = 0;
        lose_Panel.SetActive(true);
    }
    public void Heart_Animation(int lifes)
    {
        for(int i = 0; i < lifes_Img.Length; i++)
        {
            if(i > lifes) { lifes_Img[i].color = Color.grey; }
        }
    }
    public void Score_Display(int score)
    {
        score_Txt.text = score.ToString();
    }
    public void LoadScene(int load)
    {
        SceneManager.LoadScene(load);
    }
}
