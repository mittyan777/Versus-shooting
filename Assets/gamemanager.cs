using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gamemanager : MonoBehaviour
{
    [SerializeField] GameObject []Referee_Text;
     Animator m_Animator;
    Animator m_Animator2;
    [SerializeField] static int Round = 1;
    [SerializeField] static int player1_count = 0;
    [SerializeField] static int player2_count = 0;
    bool Round_b = false;

    // Start is called before the first frame update
    void Start()
    {
        m_Animator = Referee_Text[0].GetComponent<Animator>();
        m_Animator2 = Referee_Text[0].GetComponent<Animator>();
        Referee_Text[0].GetComponent<Text>().text = "Round" + gamemanager.Round;
        Referee_Text[1].GetComponent<Text>().text = "Round" + gamemanager.Round;
    }

    // Update is called once per frame
    void Update()
    {
        AnimatorStateInfo info = m_Animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo info2 = m_Animator2.GetCurrentAnimatorStateInfo(0);

        // 該当アニメーションが再生中か？
        if (info.IsName("stop"))
        {
            Referee_Text[0].SetActive(false);
           
        }
        if (info2.IsName("stop"))
        {
            Referee_Text[1].SetActive(false);
          
        }
        if(GameObject.Find("Player1(Clone)") == null)
        {
         
            Referee_Text[0].SetActive(true);
            Referee_Text[1].SetActive(true);
            if (player1_count < 2 )
            {
          
                Referee_Text[1].GetComponent<Text>().text = "Victory";
                Referee_Text[0].GetComponent<Text>().text = "Loss";
                Invoke("MainScene", 5);
            }
        }
        if (GameObject.Find("Player2(Clone)") == null)
        {
           
            Referee_Text[0].SetActive(true);
            Referee_Text[1].SetActive(true);

            if (player1_count < 2 )
            {
               
                Referee_Text[0].GetComponent<Text>().text = "Victory";
                Referee_Text[1].GetComponent<Text>().text = "Loss";
                Invoke("MainScene", 5);
            }
        }
        if(gamemanager.player1_count == 2)
        {
            Invoke("TitleScene", 5);
        }
        if (gamemanager.player2_count == 2)
        {
            Invoke("TitleScene", 5);
        }
    }
    void MainScene()
    {
      
            if (player1_count < 2 && Round_b == false)
            {
                Round += 1;
                player1_count += 1;
                Round_b = true;
            }
            if (player2_count < 2 && Round_b == false)
            {
                Round += 1;
                player2_count += 1;
                Round_b = true;
            }
            
       
        SceneManager.LoadScene("Main");
    }
    void TitleScene()
    {
        SceneManager.LoadScene("Title");
    }
}
