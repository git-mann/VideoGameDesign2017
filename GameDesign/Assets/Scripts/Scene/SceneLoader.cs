using UnityEngine;
using System.Collections;

public class SceneLoader : MonoBehaviour {

	public int distanceFromSol, seed;
	public string nameOfSystem;
	public GameObject planet, star, hydrogen;

	// Use this for initialization
	void Start () {
		loadScene(seed);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void loadScene (int seed)
	{
		int size, yOffSet, radius, orbitSpeed = 0;

		intializeRandom (seed);

		distanceFromSol = randomIntFromSeed(1, 1000);
		nameOfSystem = "DMGC-"+seed;
		int numberOfBodies = randomIntFromSeed (1, 5);

		size = randomIntFromSeed(100, 1000);
		int temperature = randomIntFromSeed(1, 60);
		GameObject spawnedSun = GameObject.Instantiate(star);
		spawnedSun.transform.localScale = new Vector3(size,size,size);
		spawnedSun.name = "Star";
		spawnedSun.GetComponent<star>().temperature = temperature;
        spawnedSun.AddComponent<SphereCollider>();
        spawnedSun.GetComponent<SphereCollider>().isTrigger = true;
		GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
		camera.GetComponent<Skybox>().material.SetInt("_Formuparam", randomIntFromSeed(450,550));

		for (int i = 0; i < numberOfBodies; i++)
		{
			size = randomIntFromSeed(5, 100);
			yOffSet = randomIntFromSeed(-5, 5);
			radius = randomIntFromSeed(1500, 10000);
			orbitSpeed = randomIntFromSeed(1, 100);
			GameObject spawnedPlanet = GameObject.Instantiate(planet);
			spawnedPlanet.GetComponent<planet>().orbitSpeed = orbitSpeed;
			spawnedPlanet.transform.localScale = new Vector3(size,size,size);
			spawnedPlanet.transform.position = Random.insideUnitCircle * radius;
            //adding a collider to the planet
            spawnedPlanet.AddComponent<SphereCollider>();
            //setting the trigger to true so we can use it later
            spawnedPlanet.GetComponent<SphereCollider>().isTrigger = true;
            spawnedPlanet.tag = "Planet";
			Vector3 position = spawnedPlanet.transform.position;
			position.z = position.y;
			position.y = 0 + yOffSet;
			spawnedPlanet.transform.position = position;
		}
	}

	private void intializeRandom(int seed)
	{
		Random.InitState(seed);
	}

	private int randomIntFromSeed (int min, int max)
	{
		int number = Random.Range(min, max);
		return number;
	}
}
