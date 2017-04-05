using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearestObject : MonoBehaviour {

    public GUISkin guiSkin;
    public GameObject[] planets;
    public GameObject star, station, cameraPrefab, cameraPrefabStar;
    public static bool activated = false;
    Camera mainCamera;
    Vector3 screenPoint;
    // Use this for initialization
    void Start () {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        planets = GameObject.FindGameObjectsWithTag("Planet");

        star = GameObject.FindGameObjectWithTag("Sun");
        GameObject tempCameraStar = Instantiate(cameraPrefabStar, star.transform);
        tempCameraStar.transform.localPosition = new Vector3(0, 1, 0);
        tempCameraStar.transform.rotation = new Quaternion(0, 0, 0, 0);
        RenderTexture tempCameraTextureStar = new RenderTexture(75, 75, 0);
        Camera tempCameraStarCam = tempCameraStar.GetComponent<Camera>();
        tempCameraStarCam.targetTexture = tempCameraTextureStar;
        tempCameraStarCam.orthographicSize = star.transform.localScale.x / 2;
        tempCameraStarCam.farClipPlane = star.transform.localScale.x;


        if (GameObject.Find("SceneLoader").GetComponent<SceneLoader>().seed == 0)
        {
            loadBase();
        }
	}
    public void loadBase()
    {
        station = GameObject.FindGameObjectWithTag("Base");
        GameObject tempCamera = Instantiate(cameraPrefab, station.transform);
        tempCamera.transform.localPosition = new Vector3(0, 0, 1);
        tempCamera.transform.rotation = new Quaternion(0, 0, 0, 0);
        RenderTexture tempCameraTexture = new RenderTexture(75, 75, 0);
        tempCamera.GetComponent<Camera>().targetTexture = tempCameraTexture;
    }
    public void loadPlanets()
    {
        GameObject[] tempPlanets = new GameObject[0];
        tempPlanets = GameObject.FindGameObjectsWithTag("Planet");
        foreach (GameObject planet in tempPlanets)
        {
            GameObject tempCamera = Instantiate(cameraPrefab, planet.transform);
            tempCamera.transform.localPosition = new Vector3(0, 1, 0);
            tempCamera.transform.rotation = new Quaternion(0, 0, 0, 0);
            RenderTexture tempCameraTexture = new RenderTexture(75, 75, 0);
            tempCamera.GetComponent<Camera>().targetTexture = tempCameraTexture;
        }

        planets = tempPlanets;
        loadStar();
        
    }

    private void loadStar()
    {
        star = GameObject.FindGameObjectWithTag("Sun");

        GameObject tempCameraStar = Instantiate(cameraPrefabStar, star.transform);
        
        tempCameraStar.transform.localPosition = new Vector3(0, 1, 0);
        tempCameraStar.transform.rotation = new Quaternion(0, 0, 0, 0);
        RenderTexture tempCameraTextureStar = new RenderTexture(75, 75, 0);
        Camera tempCameraStarCam = tempCameraStar.GetComponent<Camera>();
        tempCameraStarCam.targetTexture = tempCameraTextureStar;
        tempCameraStarCam.orthographicSize = star.transform.localScale.x / 2;
        tempCameraStarCam.farClipPlane = star.transform.localScale.x;
    }

    private void OnGUI()
    {
        GUI.skin = guiSkin;
        if (!star)
        {
            loadStar();
        }
        if(planets.Length > 0 && planets[0] == null && !station )
        {
            loadPlanets();
        }
        if (activated)
        {
            for (int i = 0; i < planets.Length; i++)
            {
                
                screenPoint = mainCamera.WorldToViewportPoint(planets[i].transform.position);
               
                

                bool onScreen = checkScreenPoint(screenPoint);
                

                if (!onScreen)
                {
                    Vector3 finalPoint = mainCamera.ViewportToScreenPoint(new Vector3(Mathf.Clamp(screenPoint.x, 0.03751593f, 0.7533659f), 1 - Mathf.Clamp(screenPoint.y, 0.15313911f, 0.9256391f), 0));
                    planets[i].transform.GetChild(1).eulerAngles = new Vector3(90, 90, 0 - planets[i].transform.eulerAngles.z);
                    GUIContent content = new GUIContent("ExoPlanet: " + i, planets[i].transform.GetChild(1).GetComponent<Camera>().targetTexture, "Station");
                    GUI.Box(new Rect(finalPoint.x, finalPoint.y, 100, 100), content);
                }
            }
        }
        if (star)
        {
            Vector3 screenPoint = mainCamera.WorldToViewportPoint(star.transform.position);
            bool onScreen = checkScreenPoint(screenPoint);

            if (!onScreen)
            {
                Vector3 finalPoint = mainCamera.ViewportToScreenPoint(new Vector3(Mathf.Clamp(screenPoint.x, 0.03751593f, 0.7533659f), 1 - Mathf.Clamp(screenPoint.y, 0.15313911f, 0.9256391f), 0));
                star.transform.GetChild(1).eulerAngles = new Vector3(90, 90, 0 - star.transform.eulerAngles.z);
                GUIContent content = new GUIContent("Star", star.transform.GetChild(1).GetComponent<Camera>().targetTexture, "Station");
                GUI.Box(new Rect(finalPoint.x, finalPoint.y, 100, 100), content);
            }
        }

        if (station)
        {
            
            Vector3 screenPoint = mainCamera.WorldToViewportPoint(station.transform.position);
            bool onScreen = checkScreenPoint(screenPoint);

            if (!onScreen)
            {
                Vector3 finalPoint = mainCamera.ViewportToScreenPoint(new Vector3(Mathf.Clamp(screenPoint.x, 0.03751593f, 0.7533659f), 1 - Mathf.Clamp(screenPoint.y, 0.15313911f, 0.9256391f), 0));
                station.transform.GetChild(3).eulerAngles = new Vector3(90, 90, 0 - station.transform.eulerAngles.z) ;
                GUIContent content = new GUIContent("Station", station.transform.GetChild(3).GetComponent<Camera>().targetTexture, "Station");
                GUI.Box(new Rect(finalPoint.x,finalPoint.y, 100, 100), content);
            }
        }else if(GameObject.Find("SceneLoader").GetComponent<SceneLoader>().seed == 0)
        {
            loadBase();
        }
    }

    bool checkScreenPoint(Vector3 point)
    {
        bool onScreen = false;
        if (point.x > 0.03711593f && point.x < 0.8133659f)
        {
            if (point.y > 0.03313911f && point.y < 0.9256391f)
            {
                onScreen = point.z > 0;
            }
            else
            {
                onScreen = false;
            }
        }
        else if (point.x > 0.8133659f && point.x < 0.9638347f)
        {
            if (point.y > 0.315639f && point.y < 0.6831391f)
            {
                onScreen = point.z > 0;
            }
            else
            {
                onScreen = false;
            }
        }
        else
        {
            onScreen = false;
        }
        return onScreen;
    }
}
