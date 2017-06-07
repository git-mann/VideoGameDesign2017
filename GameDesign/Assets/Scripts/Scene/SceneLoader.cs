using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class SceneLoader : NetworkBehaviour {
    const int primeNumX = 23173;
    const int primeNumY = 17321;
    const int primeNumModulus = 19309;
    public static SceneLoader scene;

    [Server]
    private void Awake()
    {
        scene = this;

        GameObject sun = GameObject.Instantiate((GameObject)Resources.Load("Prefabs/Star"));
        sun.GetComponent<star>().temperature = 25;
        sun.transform.localScale = new Vector3(250, 250,1);
        NetworkServer.Spawn(sun);
        GameObject station = GameObject.Instantiate((GameObject)Resources.Load("Prefabs/"));
        

    }
    [Server]
    public void Generate(SectorCenter center)
    {
        switch (center.id)
        {
            case 5:

                    break;
            default:
                Debug.Log(center.id);
                createSystem(getRandomSeed(center.sectorX, center.sectorY),(int) center.transform.position.x, (int)center.transform.position.y, center);
                break;
        }
        
    }
    //this is a way to get the same number every time based upon the seed
    //http://preshing.com/20121224/how-to-generate-a-sequence-of-unique-random-integers/
    int getRandomSeed(int x, int y)
    {
        int Return = ((primeNumX - x) * (primeNumY - y))%primeNumModulus;
        //Debug.Log(Return);
        return Return % loadData.data.saveId;
    }
    void createSystem(int seed, int posX, int posY, SectorCenter sector)
    {
        if (sector.Sun != null)
        {
            NetworkServer.Destroy(sector.Sun.gameObject);
        }
        //Debug.Log(seed);
        GameObject sun = GameObject.Instantiate((GameObject)Resources.Load("Prefabs/Star"));
        sun.transform.position = new Vector3(posX, posY, 0);
        star spawnS = sun.GetComponent<star>();
        Random.InitState(seed);
        spawnS.temperature = (int)Random.Range(1f, 60f);
        float size = Random.Range(100, 1000);
        sun.transform.localScale = new Vector3(size, size, 1);
        NetworkServer.Spawn(sun);
        //Debug.Log("spawn");
    }

    public override void OnStartServer()
    {
        
    }
    /*
	public int distanceFromSol, seed;
	public string nameOfSystem;
	public GameObject planet, star, hydrogen, station;
    public Material psMaterial;

    //array of classes which has x-Coord and z-Coord each sector is at
    sector[] places = new sector[9];
    public override void OnStartServer()
    {
        firstLoad();
        Debug.Log("start");
    }

    private void Awake()
    {
        if (isServer) {
            firstLoad();
            Debug.Log("Load god damnit");
        }
        else
        {
            Debug.Log("I do not want to cooperate");
        }
        
    }
    
    [Server]
    void firstLoad()
    {
        if (loadData.data.sceneName.Equals("new"))
        {
            loadData.data.sceneName = System.DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
            loadData.data.secX = 0;
            loadData.data.secZ = 0;
            loadBase();
            loadData.data.setSun();
        }
        else if (loadData.data.sceneName.Equals("resume"))
        {
            if (!loadData.data.loadResume())
            {
                loadData.data.sceneName = System.DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
                loadData.data.secX = 0;
                loadData.data.secZ = 0;
                loadBase();
            }
        }
        else
        {
            loadData.data.loadSceneName();
        }
    }
    // Use this for initialization
    void start()
    {
        
       
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void loadScene (int providedSeed)
	{
        NearestObject.activated = false;
		int size, yOffSet, radius, orbitSpeed = 0;
        seed = providedSeed;
        intializeRandom (providedSeed);

		distanceFromSol = randomIntFromSeed(1, 1000);
		nameOfSystem = "DMGC-"+ providedSeed;
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
            NetworkServer.Spawn(spawnedPlanet);
		}
        GameObject.FindWithTag("Player").GetComponent<NearestObject>().loadPlanets();
        NearestObject.activated = true;
    }
    public GameObject loadBase()
    {
        
        spawnSun();
        distanceFromSol = 0;
        seed = 0;
        nameOfSystem = "BDMSC-" + seed;
        GameObject spawnedBase = GameObject.Instantiate(station);
        NetworkServer.Spawn(spawnedBase);
        return spawnedBase;
    }

    private void spawnSun(int size = 250, int temperature = 25)
    {
        GameObject spawnedSun = GameObject.Instantiate(star);
        spawnedSun.transform.localScale = new Vector3(size, size, size);
        spawnedSun.name = "Star";
        spawnedSun.tag = "Sun";
        spawnedSun.GetComponent<star>().temperature = temperature;
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
        psMain.startSize = .015f;
        psMain.startSpeed = 31f;
        psMain.prewarm = true;
        spawnedSunPS.GetComponent<ParticleSystemRenderer>().material = psMaterial;
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

        loadData.data.sun = spawnedSun;
        NetworkServer.Spawn(spawnedSun);
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
    */
}

