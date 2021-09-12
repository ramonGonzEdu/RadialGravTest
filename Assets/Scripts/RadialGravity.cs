using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialGravity : MonoBehaviour
{
	public GameObject[] gravitySources;
	Rigidbody rb;

	// Start is called before the first frame update
	void Start()
	{
		rb = gameObject.GetComponent<Rigidbody>();

	}



	public GameObject MainSource()
	{
		GameObject closest = gravitySources[0];

		foreach (GameObject source in gravitySources)
		{
			float distanceToAttractor = (transform.position - source.transform.position).sqrMagnitude;
			float distanceToCurrentClosest = (transform.position - closest.transform.position).sqrMagnitude;
			if (distanceToAttractor < distanceToCurrentClosest) closest = source;
		}


		return closest;
	}

	// Update is called once per frame
	void Update()
	{
		// Time.deltaTime;
		foreach (GameObject source in gravitySources)
		{
			float mass = source.GetComponent<RadialGravityStar>().gravityScale;
			float distance = (transform.position - source.transform.position).magnitude;
			Vector3 changeVector = Vector3.Scale((source.transform.position - transform.position), new Vector3(1 / distance, 1 / distance, 1 / distance));
			float distance2 = distance * source.GetComponent<RadialGravityStar>().distanceScale;
			Vector3 changeVector2 = Vector3.Scale(changeVector, new Vector3(mass / (distance2 * distance2), mass / (distance2 * distance2), mass / (distance2 * distance2)));
			if (distance < source.transform.localScale.x)
			{

				rb.AddForce(changeVector2);
			}
		}

	}
}
