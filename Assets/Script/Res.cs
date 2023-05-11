using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Res : MonoBehaviour
{
    public TMP_Text txt;
    public static Res instance;
    public Image im;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Win()
    {
        im.gameObject.SetActive(true);
        txt.text = "GAME WIN";
    }
    public void Lose()
    {
        im.gameObject.SetActive(true);

        txt.text = "GAME LOSE";

    }
    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
