using UnityEngine;
using System.Collections;

public class FlyGenerator : MonoBehaviour 
{
    public GameObject FlyObject;
    public float MaxTimeToGenerateFly;
    public float MaxTimeToDestroyFly;

    private float _timeToGenerateFly = 0.0F;

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        _timeToGenerateFly -= Time.deltaTime;

        if (_timeToGenerateFly <= 0.0F)
        {
            _timeToGenerateFly = Random.Range(0.0F, MaxTimeToGenerateFly);
            spawnRandomFly();
        }
	}

    private void spawnRandomFly()
    {
        Vector3 screenPosition = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0,Screen.width), Random.Range(0,Screen.height), Camera.main.farClipPlane/2));
        GameObject fly = Instantiate(FlyObject,screenPosition,Quaternion.identity) as GameObject;
        Destroy(fly, Random.Range(0.0F, MaxTimeToDestroyFly));
    }
}
