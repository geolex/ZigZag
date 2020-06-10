using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
	[Header("UI Element")]
	[SerializeField] Text m_scoreText;
	[SerializeField] Text m_gamesPlayedText;

    // Start is called before the first frame update
    void Start()
    {
		m_scoreText.text = GameManager.GetInstance().m_bestScore.ToString();
		m_scoreText.text = GameManager.GetInstance().m_gamesPlayed.ToString();
	}

	public void StartGame()
	{
		SceneManager.LoadScene("SampleScene");
	}
}
