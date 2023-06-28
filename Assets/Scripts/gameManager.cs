using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    public int itemCount;
    public int totalCount;

    public Text countText;
    public Text totalText;

    public int stage;
    public bool finstage;

    // Start is called before the first frame update
    void Start()
    {
        itemCount = 0;
    }

    void Awake()
    {
        totalText.text = "/ " + totalCount;
    }

    public void GetItem(int count)
    {
        countText.text = count.ToString();
    }

    public void MoveNextStage()
    {
        if(itemCount == totalCount)
        {
            if(!finstage)
            {
                SceneManager.LoadScene(stage + 1);

            }
            else
            {
                ExitGame();
            }
        }
        else
        {
            SceneManager.LoadScene(stage);
        }
    }
    public void AgainStage()
    {
        SceneManager.LoadScene(stage);
    }


    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
#endif
    }
}