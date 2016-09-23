using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class FishSpitInput : MonoBehaviour 
{
    public Text Test1Text;
    public Text Test2Text;

    private List<Fly> _fliesSelected = new List<Fly>();

	// Use this for initialization
	void Start () 
    {
        
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.touchCount == 1)
        {
            Touch touch1 = Input.GetTouch(0);
            Test1Text.text = Camera.main.ScreenToWorldPoint(new Vector3(touch1.position.x, touch1.position.y, 0.0F)).ToString();
        }

        if (Input.touchCount >= 2)
        {
            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);

            Test1Text.text = Camera.main.ScreenToWorldPoint(new Vector3(touch1.position.x, touch1.position.y, 0.0F)).ToString();
            Test2Text.text = Camera.main.ScreenToWorldPoint(new Vector3(touch2.position.x, touch2.position.y, 0.0F)).ToString();

            Vector3 vec1 = Camera.main.ScreenToWorldPoint(new Vector3(touch1.position.x, touch1.position.y, 0.0F));
            Vector3 vec2 = Camera.main.ScreenToWorldPoint(new Vector3(touch2.position.x, touch2.position.y, 0.0F));
            vec1.z = 490.0F;
            vec2.z = 490.0F;

            DrawLine(vec1, vec2, Color.yellow, 0.05F);
        }
	}

    void DrawLine(Vector3 start, Vector3 end, Color color, float width, float duration = 0.2f)
    {
        GameObject myLine = new GameObject();
        myLine.transform.position = start;
        myLine.AddComponent<LineRenderer>();
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
        lr.SetColors(color, color);
        lr.SetWidth(width, width);
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        GameObject.Destroy(myLine, duration);
    }

    public void AddSelectedFly(Fly fly)
    {
        _fliesSelected.Add(fly);

        if (_fliesSelected.Count == 2)
        {
            Vector3 fly1Pos = _fliesSelected[0].gameObject.transform.position;
            Vector3 fly2Pos = _fliesSelected[1].gameObject.transform.position;
            Vector3 dir = (fly1Pos - fly2Pos) / (fly1Pos - fly2Pos).magnitude;
            Vector3 firstPos = fly1Pos + dir * -100.0F;
            Vector3 secondPos = fly1Pos + dir * 100.0F;

            DrawLine(
                //_fliesSelected[0].gameObject.transform.position, 
                //_fliesSelected[1].gameObject.transform.position, 
                firstPos, secondPos,
                Color.yellow, 0.05F);
            
            _fliesSelected[1].IsSelected = false;
            _fliesSelected[0].IsSelected = false;
        }
    }

    public void RemoveFly(Fly fly)
    {
        _fliesSelected.Remove(fly);
    }
}
