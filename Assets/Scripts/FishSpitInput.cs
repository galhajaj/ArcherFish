using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FishSpitInput : MonoBehaviour 
{
    public Text Test1Text;
    public Text Test2Text;
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
            vec1.z = 490.0F;
            Vector3 vec2 = Camera.main.ScreenToWorldPoint(new Vector3(touch2.position.x, touch2.position.y, 0.0F));
            vec2.z = 490.0F;

            //DrawLine(Camera.main.ScreenToWorldPoint(touch1.position), Camera.main.ScreenToWorldPoint(touch2.position), Color.yellow, 0.1F);
            DrawLine(vec1, vec2, Color.yellow, 0.1F);
            //DrawLine(touch1.position, touch2.position, Color.yellow, 0.1F);


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
}
