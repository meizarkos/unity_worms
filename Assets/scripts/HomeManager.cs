using TMPro;
using UnityEngine;

public class HomeManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public TextMeshProUGUI  textResult;
    void Start()
    {
        textResult.text = PlayerPrefs.GetInt("GameResult", -1) == 1 ? "You Win !" : "You Lose !";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeScene()
    {
        SceneLoader.Instance.LoadScene("GameScene");
    }
}
