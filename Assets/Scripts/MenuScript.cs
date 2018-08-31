using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MenuScript : MonoBehaviour {

    private Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>();

    public Text Forward, Backward, Jump, Left, Right;

    private GameObject currentKey;
	void Start () {
     
        keys.Add("Forward", KeyCode.W);
        keys.Add("backward", KeyCode.S);
        keys.Add("Jump", KeyCode.Space);
        keys.Add("Left", KeyCode.A);
        keys.Add("Right", KeyCode.D);

        Forward.text = keys["Forward"].ToString();
        Backward.text = keys["Backward"].ToString();
        Jump.text = keys["Jump"].ToString();
        Left.text = keys["Left"].ToString();
        Right.text = keys["Right"].ToString();
   

    }

	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(keys["Forward"]))
        {
            Debug.Log("Forward");
        }
        if (Input.GetKeyDown(keys["Backward"]))
        {
            Debug.Log("Backward");
        }
        if (Input.GetKeyDown(keys["Jump"]))
        {
            Debug.Log("Jump");
        }
        if (Input.GetKeyDown(keys["Left"]))
        {
            Debug.Log("Left");
        }
        if (Input.GetKeyDown(keys["Right"]))
        {
            Debug.Log("Right");
        }
    }
    private void OnGUI()
    {
        if(currentKey != null)
        {
            Event e = Event.current;
            if (e.isKey)
            {
                keys[currentKey.name] = e.keyCode;
                currentKey.GetComponent<Text>().text = e.keyCode.ToString();
                currentKey = null;
            }
        }
    }
    public void ChangeKey(GameObject clicked)
    {
        currentKey = clicked;
    }
}
