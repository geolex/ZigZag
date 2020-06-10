using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	static GameManager m_instance = null;

	// Data
	public int m_gamesPlayed = 0;
	public int m_bestScore = 0;

	public static GameManager GetInstance()
	{
		if (!m_instance)
		{
			m_instance = new GameManager();
		}
		return m_instance;
	}

	public void Restart(int _score)
	{
		m_bestScore = Mathf.Max(_score, m_bestScore);



		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

		++m_gamesPlayed;
	}
}
