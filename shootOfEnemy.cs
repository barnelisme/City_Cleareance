using UnityEngine;
using System.Collections;

public class shootOfEnemy : MonoBehaviour {

	static public GameObject bullet;
	public float delayTime = 100f;
	static private float counter = 0f;
	public GameObject bulletHole;

	/***************************************/
	public Transform MainPlayer;
	public GameObject Enemy;
	Animation EnemyAnimation;
	private int TypesOfEnemy;
	private float SpeedRun = 4f, SpeedWalk =2f;
	private string TypeOfSoldier = "";
	private string[] ListOfTypeOfSoldier = new string[4];//;{ "field", "field", "field", "sniper" };
	static public float distance;
	float EnemyChargeDistance = 15;
	float EnemyShootFromFarDistance = 26;
	/***************************************/

	// Use this for initialization
	void Start () {
		
		//GetTypeOfSoldier ();
		MainPlayer = GameObject.Find("FPSController").GetComponent<Transform>();
		//Enemy = GameObject.Find("ArmyPilot(Clone)").GetComponent<Transform>();
		//EnemyAnimation = GameObject.Find("ArmyPilot(Clone)").GetComponent<Animation>();
		//EnemyAnimation = Enemy.gameObject.GetComponent<Animation>();
	}

	// Update is called once per frame
	void Update () {

		EnemyShootManual ();
		//animate ();
		//GetDistance ();

	}

	public void EnemyShootManual()
	{
		if (Input.GetKey (KeyCode.R) && counter > delayTime) 
		{
			Instantiate (bullet, transform.position, transform.rotation); 
			counter = 0;	



		}
		counter += Time.deltaTime;
	}//end of public void EnemyShootManual()

	public void EnemyShoot()
	{
		/*if (counter > delayTime) 
		{
			Instantiate (bullet, transform.position, transform.rotation); 
			counter = 0;	
			GetComponent<AudioSource>().Play ();
			RaycastHit hit;
			Ray ray = new Ray (transform.position, transform.forward);

			if(Physics.Raycast(ray,out hit, 100f))
			{
				if (hit.transform.gameObject.name != "FPSController") 
				{
					(Instantiate (bulletHole, hit.point, Quaternion.FromToRotation (Vector3.up, hit.normal))as GameObject).transform.parent = hit.transform; 

				} 
				if (hit.transform.gameObject.name == "FPSController") 
				{
					hit.transform.gameObject.SendMessage("ApplyDamage");
				}
				//print ("Who is hit:"+ hit.transform.gameObject.name);

			}

		}
		counter += Time.deltaTime;//*/
	}
	/*void animate()
	{
		if (TypeOfSoldier == "field") 
		{ //field soldier, this type would charge toward the MainPlayer
			if (Input.GetKey (KeyCode.Q) || EnemyVision.distance <= EnemyChargeDistance) 
			{
				//GetComponent<Animation> ().Play ("Run Firing");
				EnemyAnimation ["Run Firing"].speed = 2.0f;
				EnemyAnimation.Play ("Run Firing");
				//Enemy.transform.position += Enemy.transform.forward * Time.deltaTime * SpeedRun;
				transform.position += transform.forward * Time.deltaTime * SpeedRun;
				EnemyShoot ();

			}
			else if (Input.GetKey (KeyCode.Q) || (EnemyVision.distance > EnemyChargeDistance &&  EnemyVision.distance <= EnemyShootFromFarDistance) )
			{
				//GetComponent<Animation> ().Play ("Run Firing");
				EnemyAnimation ["Idle Firing"].speed = 0.4f;
				EnemyAnimation.Play ("Idle Firing");
				EnemyShoot ();

				//shootOfEnemy.EnemyShoot ();//the enemy is shooting
				//transform.position += transform.forward * Time.deltaTime * SpeedRun;
			}
			else 
			{
				if (!EnemyAnimation.IsPlaying ("Jump")) {
					EnemyAnimation ["Standing 2"].speed = 2f;
					EnemyAnimation.Play ("Standing 2");
				}
			
			}
		}
		if (TypeOfSoldier == "sniper" ) 
		{	
			if (Input.GetKey (KeyCode.Q)  || (EnemyVision.distance > EnemyChargeDistance &&  EnemyVision.distance <= EnemyShootFromFarDistance)) {
				//GetComponent<Animation> ().Play ("Run Firing");
				EnemyAnimation ["Idle Firing"].speed = 0.4f;
				EnemyAnimation.Play ("Idle Firing");
				EnemyShoot ();
				//transform.position += transform.forward * Time.deltaTime * SpeedRun;
			}
			else if (Input.GetKey (KeyCode.Q)  || EnemyVision.distance <= EnemyChargeDistance) 
			{
				//GetComponent<Animation> ().Play ("Run Firing");
				EnemyAnimation ["Walk Firing"].speed = 2.0f;
				EnemyAnimation.Play ("Walk Firing");
				EnemyShoot ();
				//Enemy.transform.position += Enemy.transform.forward * Time.deltaTime * SpeedWalk;
				Enemy.transform.position += Enemy.transform.forward * Time.deltaTime * SpeedWalk;
			}
			else 
			{
				if (!EnemyAnimation.IsPlaying ("Jump")) {
					EnemyAnimation ["Standing 2"].speed = 2f;
					EnemyAnimation.Play ("Standing 2");
				}
			}
		}
		if (Input.GetKey (KeyCode.Tab))
		{	
			GetTypeOfSoldier ();
		}

	}//end of void animate()//*/
	/*void GetTypeOfSoldier()
	{
		TypesOfEnemy = Random.Range (0, 4);//1,2,3 is field soldier, 4 is snipper
		if(TypesOfEnemy == 4)
		{
			TypesOfEnemy=3;
		}
		ListOfTypeOfSoldier[0] = "field";
		ListOfTypeOfSoldier[1] = "field";
		ListOfTypeOfSoldier[2] = "field";
		ListOfTypeOfSoldier[3] = "sniper";
		TypeOfSoldier = ListOfTypeOfSoldier [TypesOfEnemy];
		//print("Type of Soldier:"+TypeOfSoldier + "int:"+ TypesOfEnemy);
	}//*/
	public float GetDistance()//gets the distance between the enemy and the player
	{
		
		//distance = Vector3.Distance (Enemy.transform.position, MainPlayer.position);
		//print ("distance:" + distance + "Soldier Type:"+TypeOfSoldier);
		return distance;
	}//end of GetDistance

}
