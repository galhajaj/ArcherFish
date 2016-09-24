using UnityEngine;
using System.Collections;

public class MoveSpriteUpAndDown : MonoBehaviour 
{
    public float DistanceToMove = 0.1F;
    public float Speed = 0.5F;
    private float _distanceMoved = 0.0F;
    public bool IsGoUpFirst;
	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (IsGoUpFirst)
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + Speed * Time.deltaTime, this.transform.position.z);
        else
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - Speed * Time.deltaTime, this.transform.position.z);

        _distanceMoved += Time.deltaTime;

        if (_distanceMoved >= DistanceToMove)
        {
            _distanceMoved = 0.0F;
            IsGoUpFirst = !IsGoUpFirst;
        }
	}
}
