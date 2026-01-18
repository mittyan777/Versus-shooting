using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Referee Texts")]
    [SerializeField] private GameObject[] Referee_Text; // [0]:Player1, [1]:Player2
    private Animator m_Animator;
    private Animator m_Animator2;

    [Header("Round & Count")]
    [SerializeField] private static int Round = 1;
    [SerializeField] private static int player1_count = 0;
    [SerializeField] private static int player2_count = 0;
    [SerializeField] GameObject[] Player_star;
    [SerializeField] GameObject[] Player2_star;
    [Header("Fade")]
    [SerializeField] private Image Fade_Image;
    private bool fade = false;
    [SerializeField] private float fadeCollar = 1;
    private int text_count = 0;

    private bool resultShown = false;

    public bool resultStarted = false;

    [Header("Delay Settings")]
    [SerializeField] private float delay = 0.5f;

    [Header("Audio")]
    [SerializeField] private AudioSource Round1;
    [SerializeField] private AudioSource Round2;
    [SerializeField] private AudioSource Round3;
    [SerializeField] private AudioSource fight;
    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioSource You_Win;
    [SerializeField] private AudioSource You_Lose;

    private void Start()
    {
        // Animator取得
        m_Animator = Referee_Text[0].GetComponent<Animator>();
        m_Animator2 = Referee_Text[1].GetComponent<Animator>();

        // TextMeshProUGUI取得 & マテリアルコピー
        var text1 = Referee_Text[0].GetComponent<TextMeshProUGUI>();
        var text2 = Referee_Text[1].GetComponent<TextMeshProUGUI>();
        text1.fontMaterial = new Material(text1.fontMaterial);
        text2.fontMaterial = new Material(text2.fontMaterial);

        // 初期テキスト
        text1.text = "Round " + Round;
        text2.text = "Round " + Round;

        // アウトライン設定
        text1.fontMaterial.SetColor("_OutlineColor", Color.red);
        text1.fontMaterial.SetFloat("_OutlineWidth", 0.2f);
        text2.fontMaterial.SetColor("_OutlineColor", Color.blue);
        text2.fontMaterial.SetFloat("_OutlineWidth", 0.15f);

        Referee_Text[0].SetActive(false);
        Referee_Text[1].SetActive(false);
        StartCoroutine(starOF());
    }

    private void Update()
    {
        // Animatorの状態確認
        AnimatorStateInfo info = m_Animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo info2 = m_Animator2.GetCurrentAnimatorStateInfo(0);
        float t = info.normalizedTime;

        if (t >= 0.6f && text_count == 0)
        {
            fight.Play();
            Referee_Text[0].GetComponent<TextMeshProUGUI>().text = "Fight";
            Referee_Text[1].GetComponent<TextMeshProUGUI>().text = "Fight";
            bgmSource.Play();
        }
        else if (t >= 0.6f)
        {
            Referee_Text[0].GetComponent<TextMeshProUGUI>().text = "";
            Referee_Text[1].GetComponent<TextMeshProUGUI>().text = "";
        }

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

            Debug.Log($"Check result: p1Dead={p1Dead}, p2Dead={p2Dead}");

            if (p1Dead && !p2Dead)
            {
                StartCoroutine(ShowResult(2));
                resultStarted = true;
            }
            else if (!p1Dead && p2Dead)
            {
                StartCoroutine(ShowResult(1));
                resultStarted = true;
            }
            else if (p1Dead && p2Dead)
            {
                StartCoroutine(ShowDraw());
                resultStarted = true;
            }
        }
        if (player1_count == 1)
        {
            Player_star[0].SetActive(true);
            Player_star[1].SetActive(false);
        }
        if (player2_count == 1)
        {
            Player2_star[0].SetActive(true);
            Player2_star[1].SetActive(false);
        }
        // 2本先取でタイトル
        if (player1_count == 2)
        {
            Player_star[0].SetActive(true);
            Player_star[1].SetActive(true);
            Round = 1;
           
            Invoke("Player1Scene", 5);
            player1_count = 0;
            player2_count = 0;
            resultShown = true;
        }
        else if (player2_count == 2)
        {
            Player2_star[0].SetActive(true);
            Player2_star[1].SetActive(true);
            Round = 1;
          
            Invoke("Player2Scene", 5);
            player1_count = 0;
            player2_count = 0;
            resultShown = true;
        }

        // Fade処理
        if (!fade && fadeCollar > 0) fadeCollar -= Time.deltaTime;
        if (fade && fadeCollar < 1 ) fadeCollar += Time.deltaTime;

        Fade_Image.color = new Color(0, 0, 0, fadeCollar);

        if (fadeCollar < 0f)
        {
            if (Round == 1) Round1.Play();
            if (Round == 2) Round2.Play();
            if (Round == 3) Round3.Play();

            Referee_Text[0].SetActive(true);
            Referee_Text[1].SetActive(true);
            fadeCollar = 0f;
        }
    }
    private IEnumerator starOF()
    {
        resultStarted = true;
        yield return new WaitForSeconds(4f);
        resultStarted = false;
    }
    private IEnumerator ShowResult(int playerWin)
    {
        resultShown = true;
         
        // 最初は両方非表示
        Referee_Text[0].SetActive(false);
        Referee_Text[1].SetActive(false);

        if (playerWin == 1)
        {
            Referee_Text[1].SetActive(true);
            Referee_Text[1].GetComponent<TextMeshProUGUI>().text = "You_Win";
            You_Win.Play();

            yield return new WaitForSeconds(2f);
            Referee_Text[1].SetActive(false);
            yield return new WaitForSeconds(1f); // ここでタイミングをずらす

            // Player1負け → Player2勝ち
           
            Referee_Text[0].SetActive(true);
            Referee_Text[0].GetComponent<TextMeshProUGUI>().text = "You_Lose";
            You_Lose.Play();



            player1_count++;
            
            Round++;
            if (player2_count != 2 && player1_count != 2)
            {
                Invoke("MainScene", 5);
            }
            yield return new WaitForSeconds(2f);
            fade = true;
            Referee_Text[0].SetActive(false);
        }
        else
        {
            // Player2負け → Player1勝ち
            Referee_Text[0].SetActive(true);
            Referee_Text[0].GetComponent<TextMeshProUGUI>().text = "You_Win";
            You_Win.Play();

            yield return new WaitForSeconds(2f);
            Referee_Text[0].SetActive(false);
            yield return new WaitForSeconds(1f); // タイミングずらし

           
            Referee_Text[1].SetActive(true);
            Referee_Text[1].GetComponent<TextMeshProUGUI>().text = "You_Lose";
            You_Lose.Play();

            player2_count++;
           
            Round++;
            if (player2_count != 2 && player1_count != 2)
            {
                Invoke("MainScene", 5);
            }
            yield return new WaitForSeconds(2f);
            fade = true;
            Referee_Text[1].SetActive(false);

        }

       
    }


    private IEnumerator ShowDraw()
    {
        resultShown = true;
        fade = true;

        Referee_Text[0].SetActive(true);
        Referee_Text[1].SetActive(true);

        Referee_Text[0].GetComponent<TextMeshProUGUI>().text = "Draw";
        Referee_Text[1].GetComponent<TextMeshProUGUI>().text = "Draw";

        Round++;
        yield return new WaitForSeconds(5f);
        MainScene();
    }

    private void MainScene()
    {
        SceneManager.LoadScene("Main");
    }

    private void TitleScene()
    {
        SceneManager.LoadScene("Title");
    }

    private void Player1Scene()
    {
        SceneManager.LoadScene("Player1_Victory");
    }

    private void Player2Scene()
    {
        SceneManager.LoadScene("Player2_Victory");
    }
}
