using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerExplosion : MonoBehaviour
{
    [SerializeField] GameObject explosion;
    [SerializeField] GameObject Player;
    [SerializeField] GameObject Player_V;
    [SerializeField] GameObject Player_V_text;
    [SerializeField] Image fade;
    float fadeTime = 1;
    bool a;
    int explosion_count;
    public float Player_V_gocount = 3;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Explosion", 0.2f, 0.2f);
        Player_V.GetComponent<AudioSource>().enabled = false;
        Player_V_text.SetActive(false);
    
        a = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (a == false)
        {
            fadeTime -= Time.deltaTime;
        }
        else
        {
            fadeTime += Time.deltaTime;
        }
        if(fadeTime >= 1)
        {
            Invoke("TitleScene", 1);
        }
        if (Player_V_gocount <= 0)
        {
            Player_V.GetComponent<AudioSource>().enabled = true;
            Player_V_text.SetActive(true);
            Player_V.transform.position += transform.right * 10 * Time.deltaTime;
            a = true;
        }
        fade.GetComponent<Image>().color = new Color(0, 0, 0, fadeTime);
    }
    void Explosion()
    {
        explosion_count++;
        if (explosion_count <= 10)
        {
            Instantiate(explosion, new Vector2(UnityEngine.Random.Range(Player.transform.position.x - 1, Player.transform.position.x + 1), UnityEngine.Random.Range(Player.transform.position.y - 1, Player.transform.position.y + 1)), Quaternion.identity);
        }
        else
        {
            Destroy(Player);
            Player_V_gocount -= 50 * Time.deltaTime;
          
          
        }
    }
    void TitleScene()
    {
        SceneManager.LoadScene("Title");
    }
}
