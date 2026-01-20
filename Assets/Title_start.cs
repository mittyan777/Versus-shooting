using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Title_start : MonoBehaviour
{
    [SerializeField]GameObject []p_sprite;
    bool fade = false;
    [SerializeField]GameObject fade_obj;
    [SerializeField]float fade_Collar = 1;
    [SerializeField] GameObject Credit;
    int currentConnectionCount = 0;
    [SerializeField] GameObject message;
    [SerializeField]TextMeshProUGUI controller_count;
    // Start is called before the first frame update
    void Start()
    {
        p_sprite[0].SetActive(false);
        p_sprite[1].SetActive(false);
    }
    void Rapid_fire()
    {
        p_sprite[0].SetActive(true);
        p_sprite[1].SetActive(true);

    }
        // Update is called once per frame
    void Update()
    {
        if (p_sprite[0].activeSelf == true && p_sprite[0].activeSelf == true)
        {
            p_sprite[0].transform.position += transform.up * 8 * Time.deltaTime;
            p_sprite[1].transform.position -= transform.up * 8 * Time.deltaTime;
        }

        if(p_sprite[0].transform.position.y >= 15)
        {
            SceneManager.LoadScene("Main");
        }
        if (p_sprite[0].transform.position.y >= 5)
        {
           fade = true;
        }

        //if (Gamepad.current == null) return;

        var pad = Gamepad.current;

        if (currentConnectionCount == 2)
        {
            // どれかのボタンが押されたら
            if (Gamepad.current != null && AnyGamepadButtonPressed(Gamepad.current))
            {
                Debug.Log("何かのボタンが押された！");
                Game_start();
            }
            message.SetActive(false);
        }
        else
        {
            message.SetActive(true);
            controller_count.text = ($"{currentConnectionCount}/2");
        }

            string[] cName = Input.GetJoystickNames();
        currentConnectionCount = 0;
        for (int i = 0; i < cName.Length; i++)
        {
            if (cName[i] != "")
            {
                currentConnectionCount++;
            }
        }
        Debug.Log(currentConnectionCount);

        if (fade == true)
        {
            if (fade_Collar <= 1)
            {
                fade_Collar += 1f * Time.deltaTime;
            }
            
        }
        else
        {
            if (fade_Collar > 0)
            {
                fade_Collar -= 1f * Time.deltaTime;
            }
        }
        fade_obj.GetComponent<Image>().color = new Color(0, 0, 0, fade_Collar);

        if(Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    public void Game_start()
    {
        p_sprite[0].SetActive(true);
        p_sprite[1].SetActive(true);
        
    }
    bool AnyGamepadButtonPressed(Gamepad pad)
    {
        return
            pad.buttonSouth.wasPressedThisFrame ||   // A
            pad.buttonEast.wasPressedThisFrame ||    // B
            pad.buttonWest.wasPressedThisFrame ||    // X
            pad.buttonNorth.wasPressedThisFrame ||   // Y
            pad.startButton.wasPressedThisFrame ||
            pad.selectButton.wasPressedThisFrame ||
            pad.leftShoulder.wasPressedThisFrame ||
            pad.rightShoulder.wasPressedThisFrame ||
            pad.leftStickButton.wasPressedThisFrame ||
            pad.rightStickButton.wasPressedThisFrame ||
            pad.dpad.up.wasPressedThisFrame ||
            pad.dpad.down.wasPressedThisFrame ||
            pad.dpad.left.wasPressedThisFrame ||
            pad.dpad.right.wasPressedThisFrame;
    }
    public void crediton()
    {
        Credit.SetActive(true);
    }
    public void creditof()
    {
        Credit.SetActive(false);
    }
}
