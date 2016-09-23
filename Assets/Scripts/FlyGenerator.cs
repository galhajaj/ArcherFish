using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FlyGenerator : MonoBehaviour 
{
    public GameObject FlyObject;
    public int FliesLinesNumber;
    public int MinFliesInLines;
    public int MaxFliesInLines;
    /*public float MaxTimeToGenerateFly;
    public float MaxTimeToDestroyFly;

    private float _timeToGenerateFly = 0.0F;*/

	// Use this for initialization
	void Start () 
    {
        for (int i = 0; i < FliesLinesNumber; ++i)
            createFlyLine();
	}
	
	// Update is called once per frame
	void Update () 
    {
        /*_timeToGenerateFly -= Time.deltaTime;

        if (_timeToGenerateFly <= 0.0F)
        {
            _timeToGenerateFly = Random.Range(0.0F, MaxTimeToGenerateFly);
            spawnRandomFly();
        }*/
	}

    /*private void spawnRandomFly()
    {
        Vector3 screenPosition = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0,Screen.width), Random.Range(0,Screen.height), Camera.main.farClipPlane/2));
        GameObject fly = Instantiate(FlyObject,screenPosition,Quaternion.identity) as GameObject;
        Destroy(fly, Random.Range(0.0F, MaxTimeToDestroyFly));
    }*/

    private List<GameObject> createFlyLine()
    {
        List<GameObject> flies = new List<GameObject>();

        // two random position and their dir
        Vector3 randomScreenPosition1 = getRandomScreenPosition();
        Vector3 randomScreenPosition2 = getRandomScreenPosition();
        Vector3 dir = (randomScreenPosition2 - randomScreenPosition1) / (randomScreenPosition2 - randomScreenPosition1).magnitude;

        // create flies on projection of dir
        int fliesOnLine = Random.Range(MinFliesInLines, MaxFliesInLines);
        for (int i = 0; i < fliesOnLine; ++i)
        {
            Vector3 randomScreenPosition = getRandomScreenPosition();
            Vector3 randomPositionOnDir = Vector3.Project(randomScreenPosition, dir);
            if (!isInsideTheFliesArea(randomPositionOnDir))
                continue;
            GameObject fly = Instantiate(FlyObject,randomPositionOnDir,Quaternion.identity) as GameObject;
            flies.Add(fly);
        }

        return flies;
    }

    private Vector3 getRandomScreenPosition()
    {
        return Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0,Screen.width), Random.Range(0,Screen.height), Camera.main.farClipPlane/2));
    }

    private bool isInsideTheFliesArea(Vector3 pos)
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(pos);

        if (screenPos.x > 0.1*Screen.width && screenPos.x < 0.9*Screen.width && screenPos.y > 0.1*Screen.height && screenPos.y < 0.9*Screen.height)
            return true;
        return false;
    }
}
