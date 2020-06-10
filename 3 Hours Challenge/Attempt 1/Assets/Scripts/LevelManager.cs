using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
	Vector3 hidden = new Vector3(-10f, 0, -10f);


	[Header("Scene Objects")]
	[SerializeField] BallController m_player;

	[Header("UI Objects")]
	[SerializeField] Text m_scoreText;

	[Header("Level Generation")]
	[SerializeField] GameObject m_platformPrefab;
	[SerializeField] int m_platformBufferSize;              // How many platforms the game has in advance


	[SerializeField] Platform[] m_platformPool;

	int m_lastPlatformIndex = -1;								// No Platform at startup
	Vector3 m_lastPlatformPosition;

	private void Awake()
	{
		InitPlatformPool();

		SpawnStartArea();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			SceneManager.LoadScene("MenuScene");
		}

		float playerTravelledDistance = m_player.transform.position.x + m_player.transform.position.z;

		Vector3 newPlatformPosition;
		while ((Mathf.RoundToInt(playerTravelledDistance) + 9 % m_platformBufferSize) > m_lastPlatformIndex)
		{
			int value = Random.Range(0, 100);

			if(value % 2 == 0)
			{

				newPlatformPosition = m_platformPool[m_lastPlatformIndex % m_platformBufferSize].transform.position + new Vector3(0f, 0f, 1f);
			}
			else
			{
				newPlatformPosition = m_platformPool[m_lastPlatformIndex % m_platformBufferSize].transform.position + new Vector3(1f, 0f, 0f);
			}

			++m_lastPlatformIndex;

			Platform buffer = m_platformPool[m_lastPlatformIndex % m_platformBufferSize];

			buffer.Relocate(newPlatformPosition);
			buffer.gameObject.GetComponent<MeshRenderer>().material.color = Color.HSVToRGB(playerTravelledDistance / 255, 1f, 1f);
			
		}

		m_scoreText.text = m_player.GetScore().ToString();
	}


	private void SpawnStartArea()
	{
		for (int x = 0; x < 3; ++x)
		{
			for (int y = 0; y < 3; ++y)
			{
				++m_lastPlatformIndex;
				m_platformPool[m_lastPlatformIndex % m_platformBufferSize].transform.position = new Vector3(x, 0f, y);
			}
		}
	}

	private void InitPlatformPool()
	{
		m_platformPool = new Platform[m_platformBufferSize];

		for(int i = 0; i < m_platformPool.Length; ++i)
		{
			m_platformPool[i] = Instantiate(m_platformPrefab, hidden, Quaternion.identity).GetComponent<Platform>();
		}
	}
}
