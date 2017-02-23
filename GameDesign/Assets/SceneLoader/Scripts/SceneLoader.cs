using UnityEngine;
using System.Collections;

public class SceneLoader : MonoBehaviour {

	public int distanceFromSol, seed;
	public string nameOfSystem;
	public GameObject planet;

	// Use this for initialization
	void Start () {
		loadScene(seed);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void loadScene (int seed)
	{
		int size = 0;
		int yOffSet = 0;
		int radius = 0;
		int orbitSpeed = 0;

		intializeRandom (seed);

		int numberOfBodies = randomIntFromSeed (1, 5);

		size = randomIntFromSeed(50, 500);
		GameObject spawnedSun = GameObject.Instantiate(planet);
		spawnedSun.transform.localScale = new Vector3(size,size,size);
		spawnedSun.name = "Star";
	


		for (int i = 0; i < numberOfBodies; i++)
		{
			size = randomIntFromSeed(5, 100);
			yOffSet = randomIntFromSeed(-5, 5);
			radius = randomIntFromSeed(1000, 10000);
			orbitSpeed = randomIntFromSeed(1, 100);
			GameObject spawnedPlanet = GameObject.Instantiate(planet);
			spawnedPlanet.GetComponent<movement>().orbitSpeed = orbitSpeed;
			spawnedPlanet.transform.localScale = new Vector3(size,size,size);
			spawnedPlanet.transform.position = Random.insideUnitCircle * radius;
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
