using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMouvement : MonoBehaviour
{
	public Vector3[] lPosition;
	public Vector3[] lRotation;

	public GameObject player;

	public float speed = 10f;

	public bool isTransitioning = false;

	public Camera cam;

	public int currentPositionIndex = 1;

	public void Update()
	{
		CheckGround();
		Vector3 pos = lPosition[currentPositionIndex] + player.transform.position;

		transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z + 3f);

		cam.transform.localPosition = Vector3.Lerp(cam.transform.localPosition, pos, speed * Time.deltaTime);
		cam.transform.eulerAngles = Vector3.Lerp(cam.transform.eulerAngles, lRotation[currentPositionIndex], (speed / 4) * Time.deltaTime);
	}

	public void CheckGround()
	{
		RaycastHit hit;
		if (Physics.Raycast(player.transform.position, Vector3.down, out hit, 1.1f))
		{
			if (hit.collider.tag == "Platform")
			{
				currentPositionIndex = 1;
			}

			if (hit.collider.tag == "RampTop")
			{
				currentPositionIndex = 0;
			}

			if (hit.collider.tag == "RampBot")
			{
				currentPositionIndex = 2;
			}


		}
	}
}
