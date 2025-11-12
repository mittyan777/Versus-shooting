using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class gamemanager : MonoBehaviour
{
    [SerializeField] GameObject Text;
     Animator m_Animator;

    
    // Start is called before the first frame update
    void Start()
    {
        m_Animator = Text.GetComponent<Animator>();
     
    }

    // Update is called once per frame
    void Update()
    {
        AnimatorStateInfo info = m_Animator.GetCurrentAnimatorStateInfo(0);

        // 該当アニメーションが再生中か？
       
      

    }
}
