using UnityEngine;
using System.Collections;

public class Fly : MonoBehaviour 
{
    private bool _isSelected = false;
    public bool IsSelected
    {
        get { return _isSelected; }
        set 
        { 
            _isSelected = value; 

            if (_isSelected)
            {
                this.GetComponent<SpriteRenderer>().color = Color.yellow;
                _manager.AddSelectedFly(this);
            }
            else
            {
                this.GetComponent<SpriteRenderer>().color = Color.white;
                _manager.RemoveFly(this);
            }
        }
    }

    private FishSpitInput _manager; 

	// Use this for initialization
	void Start () 
    {
        _manager = GameObject.Find("Manager").GetComponent<FishSpitInput>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        
	}

    /*void OnMouseDown()
    {
        IsSelected = !IsSelected;
    }*/

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.GetComponent<Fly>() != null) // fly at the beginning generations
        {
            Destroy(this.gameObject);
        }
        else // spit
        {
            Destroy(this.gameObject.GetComponent<CircleCollider2D>());
            this.gameObject.GetComponent<Rigidbody2D>().gravityScale = 1.0F;
            _manager.Score += _manager.SpitCounter;

            if (PlayerPrefs.GetInt("BestScore") < _manager.Score)
                PlayerPrefs.SetInt("BestScore", _manager.Score);
        }
    }
}
