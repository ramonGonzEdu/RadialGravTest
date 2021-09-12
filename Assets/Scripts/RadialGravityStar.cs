using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialGravityStar : MonoBehaviour
{

	public float gravityScale;
	public float distanceScale = 10;
	void Start()
	{
		if (gravityScale == 0) gravityScale = 4.0f / 3.0f * (float)(Math.PI) * (transform.localScale.x * transform.localScale.y * transform.localScale.z);
	}
}
