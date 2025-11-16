using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class gamemanager : MonoBehaviour
{
    [SerializeField] GameObject[] Referee_Text;
    Animator m_Animator;
    Animator m_Animator2;

    [SerializeField] static int Round = 1;
    [SerializeField] static int player1_count = 0;
    [SerializeField] static int player2_count = 0;

    [SerializeField] Image Fade_Image;
    bool fade = false;
    [SerializeField]float fadeCollar = 1;
    int text_count = 0;

    bool Round_b = false;
    bool resultShown = false;

    void Start()
    {
        // Animator取得
        m_Animator = Referee_Text[0].GetComponent<Animator>();
        m_Animator2 = Referee_Text[1].GetComponent<Animator>();

        // TextMeshProUGUI取得
        var text1 = Referee_Text[0].GetComponent<TextMeshProUGUI>();
        var text2 = Referee_Text[1].GetComponent<TextMeshProUGUI>();

        // それぞれ独立したマテリアルインスタンスを作成
        text1.fontMaterial = new Material(text1.fontMaterial);
        text2.fontMaterial = new Material(text2.fontMaterial);

        // 初期テキスト
        text1.text = "Round " + Round;
        text2.text = "Round " + Round;

        // 各アウトライン設定（例）
        text1.fontMaterial.SetColor("_OutlineColor", Color.red);
        text1.fontMaterial.SetFloat("_OutlineWidth", 0.2f);

        text2.fontMaterial.SetColor("_OutlineColor", Color.blue);
        text2.fontMaterial.SetFloat("_OutlineWidth", 0.15f);

        Referee_Text[0].SetActive(false);
        Referee_Text[1].SetActive(false);
    }

    void Update()
    {
        AnimatorStateInfo info = m_Animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo info2 = m_Animator2.GetCurrentAnimatorStateInfo(0);

        if (info.IsName("stop"))
        {
            Referee_Text[0].SetActive(false);
            text_count += 1;
        }

        if (info2.IsName("stop"))
        {
            Referee_Text[1].SetActive(false);
        }

        // プレイヤーが倒れたか判定（まだ結果を表示していない時のみ）
        if (!resultShown)
        {
            bool p1Dead = GameObject.Find("Player1(Clone)") == null;
            bool p2Dead = GameObject.Find("Player2(Clone)") == null;

            if (p1Dead && !p2Dead)
            {
                ShowResult(playerWin: 2);
            }
            else if (!p1Dead && p2Dead)
            {
                ShowResult(playerWin: 1);
            }
            else if (p1Dead && p2Dead)
            {
                ShowDraw(); // 引き分け
            }
        }

        // 2本先取したらタイトルへ
        if (player1_count == 2 || player2_count == 2)
        {
            Round = 1;
            Invoke("TitleScene", 5);
            player1_count = 0;
            player2_count = 0;
            resultShown = true;
        }

        if(fade == false)
        {
            if (fadeCollar > 0)
            {
                fadeCollar -= Time.deltaTime;
            }
        }
        if (fade == true )
        {
            if (fadeCollar < 1 && text_count >= 2)
            {
                fadeCollar += Time.deltaTime;
            }
        }

        Fade_Image.GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, fadeCollar);
        if(fadeCollar < 0f )
        {
            Debug.Log("uuu");
            Referee_Text[0].SetActive(true);
            Referee_Text[1].SetActive(true);
            fadeCollar = 0f;
        }
    }

    void ShowResult(int playerWin)
    {
        resultShown = true; // ← 一回だけ実行

        Referee_Text[0].SetActive(true);
        Referee_Text[1].SetActive(true);

        if (playerWin == 1)
        {
            Referee_Text[0].GetComponent<TextMeshProUGUI>().text = "Loss";
            Referee_Text[1].GetComponent<TextMeshProUGUI>().text = "Victory";
            player1_count++;
        }
        else
        {
            Referee_Text[0].GetComponent<TextMeshProUGUI>().text = "Victory";
            Referee_Text[1].GetComponent<TextMeshProUGUI>().text = "Loss";
            player2_count++;
        }
        fade = true;
       
        Round++;
        Invoke("MainScene", 5);
    }

    void ShowDraw()
    {
        resultShown = true;
        fade = true;
        Referee_Text[0].SetActive(true);
        Referee_Text[1].SetActive(true);

        var text1 = Referee_Text[0].GetComponent<TextMeshProUGUI>();
        var text2 = Referee_Text[1].GetComponent<TextMeshProUGUI>();

        text1.text = "Draw";
        text2.text = "Draw";

        // 引き分けなのでカウントは増やさない
        Round++;

        // 5秒後に次ラウンドへ
        Invoke("MainScene", 5);
    }

    void MainScene()
    {
        Round_b = false;

        SceneManager.LoadScene("Main");
    }

    void TitleScene()
    {
        SceneManager.LoadScene("Title");
    }
}
