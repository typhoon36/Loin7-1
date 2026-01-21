using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Text _ScoreText;

    public int _Score;

     void Awake()
    {
        Instance = this;

    }


    public void AddScore(int score)
    {
        score += _Score;
        _ScoreText.text = _Score.ToString();

    }
}
