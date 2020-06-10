using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
public class BallController : MonoBehaviour
{
	[Header("Ball Properties")]
	[SerializeField][Range(1f,10f)] float m_ballSpeed = 2f;
	[SerializeField] int m_score;

	// Movement
	Vector3 m_currentPos;
	Vector3 m_nextPos;
	Vector3 m_direction = new Vector3(1f,0f,0f);

	// Timing
	float m_timeToMove;
	[SerializeField] float m_timer = 0f;

	// Components
	Rigidbody m_rigidbody;

	public int GetScore()
	{
		return m_score;
	}






	private void Awake()
	{
		m_timeToMove = 1 / m_ballSpeed;
		m_currentPos = transform.position;
		m_nextPos = m_currentPos + m_direction;

		m_rigidbody = GetComponent<Rigidbody>();
	}

	private void Update()
	{

		Vector3 buffer = m_rigidbody.velocity;

		// If ball is free-falling
		if (buffer.y < -1f)
		{
			StartCoroutine("RestartCoroutine");
		}
		else
		{
			buffer.x = m_direction.x * m_ballSpeed;
			buffer.z = m_direction.z * m_ballSpeed;

			m_rigidbody.velocity = buffer;

			// Swaps Direction every time button is pressed
			if (Input.GetMouseButtonDown(0))
			{
				// Swaps m_directions axis to Zig Zag
				float swapBuffer = m_direction.x;
				m_direction.x = m_direction.z;
				m_direction.z = swapBuffer;

				++m_score;
			}
		}
	}

	IEnumerator RestartCoroutine()
	{
		yield return new WaitForSeconds(0.5f);
		GameManager.GetInstance().Restart(m_score);
	}




	/* LERPING NO-PHYSICS OPTION
	 * 
	 * 
	 * 
	private void Update()
	{
		m_timer += Time.deltaTime;

		// Swaps Direction every time button is pressed
		if (Input.GetMouseButtonDown(0))
		{
			// Swaps m_directions axis to Zig Zag
			float buffer = m_direction.x;
			m_direction.x = m_direction.z;
			m_direction.z = buffer;

			++m_score;
		}

		// When ball had reached end of travel, find new positions.
		if (m_timer > m_timeToMove)
		{
			m_timer -= m_timeToMove;

			m_currentPos = m_nextPos;
			m_nextPos = m_currentPos + m_direction;
		}

		// Update the balls position with a Lerp
		transform.position = Vector3.Lerp(m_currentPos, m_nextPos, (m_timer / m_timeToMove));
	}
	*/
}
