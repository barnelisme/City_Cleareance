using UnityEngine;
using System.Collections;

public class EnemyVision : MonoBehaviour {
	Transform MainPlayer;
	Animation EnemyAnimation;
	private int TypesOfEnemy;
	public GameObject bullet;

	public float delayTime = 2f;
	private float counter = 0f;

	private string TypeOfSoldier = "";
	private string[] ListOfTypeOfSoldier = new string[4];//;{ "field", "field", "field", "sniper" };
	static public float distance;
	float EnemyChargeDistance = 30;
	float EnemyShootFromFarDistance = 60;
	static public int NumberOfHits = 2;
	private float SpeedRun = 3f, SpeedWalk =1f;
	shootOfEnemy EnemyToShoot = new shootOfEnemy();///the enemy is shooting
	//Use this for initialization
	void Start () 
	{
		EnemyAnimation = gameObject.GetComponent<Animation>();
		GetTypeOfSoldier ();
		MainPlayer = GameObject.Find("FPSController").GetComponent<Transform>();
		EnemyAnimation = EnemyToShoot.GetComponent<Animation>();
		//print ("MainPlayer Found" + MainPlayer.gameObject.name);
	}
	
	// Update is called once per frame
	void Update () {
		EnemyView ();
		GetDistance ();
		animate ();
		counter += 1;//Time.deltaTime;//
		//print("counter"+counter);
		if (Input.GetKey (KeyCode.T) ) 
		{
			transform.FindChild ("enemyBulletSpawn").GetComponent<AudioSource> ().Play ();
		}

	}
	void EnemyView()
	{
		//if(distance < 40)
		//{
			Vector3 LookDir = MainPlayer.position - this.gameObject.transform.position;
			LookDir.y = 0;
			transform.LookAt (this.gameObject.transform.position + LookDir, Vector3.up);
		//}//end of void EnemyView()
	}
	public void ApplyDamage()
	{
		if (NumberOfHits > 0) 
		{
			NumberOfHits = NumberOfHits - 1;
			EnemyAnimation ["Jump"].speed = 1f;
			EnemyAnimation.Play ("Jump");
			//System.Threading.Thread.Sleep (5);
			//EnemyAnimation ["Idle Reload"].speed = 1.0f;
			//EnemyAnimation.Play ("Idle Reload");
		}
		if (NumberOfHits <= 0) 
		{
			Destroy (this.gameObject);
		}
		//print ("Number Of Hits:" + NumberOfHits);
	}
	public void GetDistance()//gets the distance between the enemy and the player
	{
		distance = Vector3.Distance(this.gameObject.transform.position, MainPlayer.position);
		//print ("distance:" + distance + "Soldier Type:"+TypeOfSoldier);

	}//end of GetDistance
	void OnGUI()
	{
		GUI.backgroundColor = Color.yellow;
		GUI.Button (new Rect (transform.position.z, transform.position.x, 15,15), "E");

	}
	void animate()
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
				EnemyShoot();
				//transform.position += transform.forward * Time.deltaTime * SpeedRun;
			}
			else if (Input.GetKey (KeyCode.Q)  || EnemyVision.distance <= EnemyChargeDistance) 
			{
				//GetComponent<Animation> ().Play ("Run Firing");
				EnemyAnimation ["Walk Firing"].speed = 2.0f;
				EnemyAnimation.Play ("Walk Firing");
				EnemyShoot ();
				//Enemy.transform.position += Enemy.transform.forward * Time.deltaTime * SpeedWalk;
				transform.position += transform.forward * Time.deltaTime * SpeedWalk;
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
			//GetTypeOfSoldier ();
		}

	}//end of void animate()
	void GetTypeOfSoldier()
	{
		TypesOfEnemy = Random.Range (0, 4);//1,2,3 is field soldier, 4 is snipper
		if(TypesOfEnemy == 4)
		{
			TypesOfEnemy=3;
		}
		ListOfTypeOfSoldier[0] = "field";
		ListOfTypeOfSoldier[1] = "field";
		ListOfTypeOfSoldier[2] = "sniper";
		ListOfTypeOfSoldier[3] = "sniper";
		TypeOfSoldier = ListOfTypeOfSoldier [TypesOfEnemy];
		//print("Type of Soldier:"+TypeOfSoldier + "int:"+ TypesOfEnemy);
	}
	public void EnemyShoot()
	{
		if (counter > delayTime) 
		{
			print("test:"+counter);
			Instantiate (bullet, transform.FindChild ("enemyBulletSpawn").GetComponent<Transform>().position, transform.FindChild ("enemyBulletSpawn").GetComponent<Transform>().rotation); 
			counter = 0;	
			transform.FindChild ("enemyBulletSpawn").GetComponent<AudioSource> ().Play ();
			RaycastHit hit;
			Ray ray = new Ray (transform.FindChild ("enemyBulletSpawn").GetComponent<Transform>().position, transform.FindChild ("enemyBulletSpawn").GetComponent<Transform>().forward);

			if(Physics.Raycast(ray,out hit, 100f))
			{
				if (hit.transform.gameObject.name != "FPSController") 
				{
					//(Instantiate (bulletHole, hit.point, Quaternion.FromToRotation (Vector3.up, hit.normal))as GameObject).transform.parent = hit.transform; 

				} 
				if (hit.transform.gameObject.name == "FPSController") 
				{
					hit.transform.gameObject.SendMessage("ApplyDamage");
				}
				//print ("Who is hit:"+ hit.transform.gameObject.name);

			}

		}
		//counter += Time.deltaTime;//
	}//public void EnemyShoot()


}
