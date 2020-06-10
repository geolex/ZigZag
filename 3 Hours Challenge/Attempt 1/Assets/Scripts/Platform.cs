using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Platform : MonoBehaviour
{
	[Header("Destruction Behaviour")]
	[SerializeField] float m_timeBeforeFall = 10f;

	[SerializeField] float m_fallingTime = 2f;

	Rigidbody m_rigidbody = null;

	private void Start()
	{
		m_rigidbody = GetComponent<Rigidbody>();
	}

	public void Relocate(Vector3 _pos)
	{
		transform.position = _pos + (Vector3.up * 10f);
		transform.position = Vector3.MoveTowards(transform.position, _pos, 10f);

		// Doesn't work with Pooling
		//StartCoroutine("RelocationCoroutine", _pos);
	}

	IEnumerator RelocationCoroutine(Vector3 _pos)
	{
		m_rigidbody.isKinematic = false;

		yield return new WaitForSeconds(m_fallingTime);

		transform.position = _pos + (Vector3.up * 10f);
		transform.position = Vector3.MoveTowards(transform.position, _pos, 10f);

		m_rigidbody.isKinematic = true;

	}
}
