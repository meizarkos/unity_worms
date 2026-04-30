using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public TextMeshProUGUI texthealth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        texthealth.text = "Health : 100";
    }

    // Update is called once per frame
    void Update()
    {
        CheckWinCondition();
    }

    bool CheckWinCondition()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Ennemy");

        if (enemies.Length == 0)
        {
            Debug.Log("YOU WIN!");

            PlayerPrefs.SetInt("GameResult", 1); // WIN
            PlayerPrefs.Save();

            SceneLoader.Instance.LoadScene("Home");

            return true;
        }

        return false;
    }

    public void PlayerLoseHp(int damage)
    {
        Debug.Log("Player lose " + damage + " hp");
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

        Player player = playerObj.GetComponent<Player>();

        player.health -= damage;

        if (player.health <= 0)
        {
            PlayerPrefs.SetInt("GameResult", 0);
            PlayerPrefs.Save();
            ReloadScene();
        }

        texthealth.text = "Health : " + player.health;
    }

    public void ReloadScene()
    {
        SceneLoader.Instance.LoadScene("Home");
    }
}
