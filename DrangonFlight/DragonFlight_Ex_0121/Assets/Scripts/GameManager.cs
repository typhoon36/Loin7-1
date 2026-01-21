using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //SingleTon
    public static GameManager instance;

    public Text scoreText;


    int score = 0;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Start()
    {

    }

    void Update()
    {

    }

    public void AddScore(int num)
    {
        score += num;
        scoreText.text = "Score : " + score; //텍스트에 반영

        if (score > 1000)
        {
            SceneManager.LoadScene(1); 
        }

    }

}
