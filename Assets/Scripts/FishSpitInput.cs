using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class FishSpitInput : MonoBehaviour 
{
    readonly float MIN_DISTANCE_TO_MANAGE_OBJECT = 1.0F;

    public Text ScoreText;

    private List<Fly> _fliesSelected = new List<Fly>();

    public int SpitCounter = 11;
    public int Score = 0;

	// Use this for initialization
	void Start () 
    {
        
	}
	
	// Update is called once per frame
	void Update () 
    {
        ScoreText.text = "Score: " + Score.ToString();

        if (Input.GetMouseButtonDown(0))
        {
            GameObject closestFlyToClick = findClosestObjectToMousePosition();

            if (closestFlyToClick != null)
            {
                closestFlyToClick.GetComponent<Fly>().IsSelected = !closestFlyToClick.GetComponent<Fly>().IsSelected;
            }
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

        myLine.AddComponent<BoxCollider2D>();
        BoxCollider2D bc = myLine.GetComponent<BoxCollider2D>();
        bc.size = new Vector3(Vector3.Distance(start, end), width, 1.0F);
        bc.transform.position = (start + end) / 2;
        bc.offset = new Vector2(0.0F, 0.0F);
        float angle = Mathf.Abs(start.y - end.y) / Mathf.Abs(start.x - end.x);
        if ((start.y < end.y && start.x > end.x) || (end.y < start.y && end.x > start.x))
            angle *= -1;
        angle = Mathf.Rad2Deg * Mathf.Atan(angle);
        bc.transform.Rotate(0, 0, angle);


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

            DrawLine(firstPos, secondPos, Color.blue, 0.05F);
            SpitCounter--;
            
            _fliesSelected[1].IsSelected = false;
            _fliesSelected[0].IsSelected = false;
        }
    }

    public void RemoveFly(Fly fly)
    {
        _fliesSelected.Remove(fly);
    }

private GameObject findClosestObjectToMousePosition()
{
    Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
    Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
    mouseWorldPosition.z = 0.0F;

    GameObject closestObject = null;
    float closestDistance = 100000.0F;
    foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Fly"))
    {
        if (obj.GetComponent<Fly>() == null)
            continue;

        float distance = Vector3.Distance(obj.transform.position, mouseWorldPosition);
        if (distance < closestDistance)
        {
            closestDistance = distance;
            closestObject = obj;
            //_deltaOfClickFromObject = obj.transform.position - mouseWorldPosition;
        }
    }

    if (closestDistance <= MIN_DISTANCE_TO_MANAGE_OBJECT)
        return closestObject;

    return null;
}
}
