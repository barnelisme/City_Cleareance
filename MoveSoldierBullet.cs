using UnityEngine;
using System.Collections;

public class MoveSoldierBullet : MonoBehaviour {

	public float speed = 10f;
	public float TimeToLive = 1f;
	private float counter = 0f;
	// Use this for initialization
	void Start () 
	{

	}

	// Update is called once per frame
	void Update () 
	{
		transform.Translate (0,0,speed);
		if (counter >= TimeToLive)//(transform.position.y <= 2 || transform.position.y > 100)
		{
			Destroy(this.gameObject);
		}//*/
		counter += Time.deltaTime;
	}//void Update () 


}
