using UnityEngine;
using System.Collections;

public class SceneLoader : MonoBehaviour {

	public int distanceFromSol, seed;
	public string nameOfSystem;
	public GameObject planet, star, hydrogen, station;
    
    // Use this for initialization
    void Start () {
        
        if (loadData.data.sceneName.Equals("new")){
            loadData.data.sceneName = System.DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
            loadData.data.secX = 0;
            loadData.data.secZ = 0;
            loadBase();
        }else if(loadData.data.sceneName.Equals("resume"))
        {
            if(!loadData.data.loadResume())
            {
                loadData.data.sceneName = System.DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
                loadData.data.secX = 0;
                loadData.data.secZ = 0;
                loadBase();
            }
        }else
        {
            loadData.data.loadSceneName();
        }
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

        spawnSun(size, temperature);
        
        ParticleSystem ps;
        ParticleSystem.MainModule psMain;
        ParticleSystem.ShapeModule psShape;

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
            //scale the collider to be bigger
            spawnedPlanet.GetComponent<SphereCollider>().radius = 1.1f;
            //add partical system
            spawnedPlanet.AddComponent<ParticleSystem>();
            ps = spawnedPlanet.GetComponent<ParticleSystem>();
            ps.Pause();
             psMain = ps.main;
             psShape = ps.shape;
            psMain.startSpeed = .5f;
            psShape.radius = .1f;
            psMain.startSize = .05f;
            spawnedPlanet.tag = "Planet";
			Vector3 position = spawnedPlanet.transform.position;
			position.z = position.y;
			position.y = 0 + yOffSet;
			spawnedPlanet.transform.position = position;
		}
	}
    public GameObject loadBase()
    {
        
        spawnSun();

        GameObject spawnedBase = GameObject.Instantiate(station);
        return spawnedBase;
    }

    private void spawnSun(int size = 250, int temperature = 25)
    {
        GameObject spawnedSun = GameObject.Instantiate(star);
        spawnedSun.transform.localScale = new Vector3(size, size, size);
        spawnedSun.name = "Star";
        spawnedSun.tag = "Sun";
        spawnedSun.GetComponent<star>().temperature = temperature;
        spawnedSun.AddComponent<SphereCollider>();
        spawnedSun.GetComponent<SphereCollider>().isTrigger = true;
        //resizing collider
        spawnedSun.GetComponent<SphereCollider>().radius = 1.25f;
        GameObject spawnedSunPS = spawnedSun.transform.GetChild(0).gameObject;
        spawnedSunPS.transform.localScale = new Vector3(spawnedSun.transform.localScale.x, spawnedSun.transform.localScale.y, 1);
        spawnedSunPS.AddComponent<ParticleSystem>();
        ParticleSystem ps;
        ps = spawnedSunPS.GetComponent<ParticleSystem>();
        ps.Pause();
        ParticleSystem.MainModule psMain = ps.main;
        ParticleSystem.ShapeModule psShape = ps.shape;
        ParticleSystem.CollisionModule psCollision = ps.collision;
        psMain.duration = 2f;
        psMain.startSize = .01f;
        psMain.startSpeed = 31f;
        psMain.prewarm = true;
        psShape.shapeType = ParticleSystemShapeType.ConeVolume;
        psShape.radius = .01f;
        psShape.angle = 0f;
        psShape.length = (spawnedSun.transform.localScale.x / 2);
        psCollision.enabled = true;
        psCollision.type = ParticleSystemCollisionType.World;
        psCollision.mode = ParticleSystemCollisionMode.Collision3D;
        psCollision.dampen = 1f;
        psCollision.bounce = 0f;
        psCollision.enableInteriorCollisions = false;
        psCollision.lifetimeLoss = 1f;
        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
        camera.GetComponent<Skybox>().material.SetInt("_Formuparam", randomIntFromSeed(450, 550));
        loadData.data.sun = spawnedSun;
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
    public void generateWithoutSeed()
    {
        int seed = (int)Random.Range(0f, 10000000000f);
        loadScene(seed);
    }

}
