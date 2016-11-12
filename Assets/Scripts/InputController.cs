using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class InputController : MonoBehaviour {
    public Transform toolHolder;
    public Text toolLabel;
    private List<Transform> toolChildren = new List<Transform>();
    private Transform lastButton = null;
	// Use this for initialization
	void Start () {
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
        for(int i = 0; i < toolHolder.transform.childCount; i++)
        {
            toolChildren.Add(toolHolder.transform.GetChild(i));
        }
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.A))
        {
            changeTool(ActionEnum.Tool.Acetylation);
        }
        if(Input.GetKeyDown(KeyCode.S))
        {
            changeTool(ActionEnum.Tool.Decetylation);
        }
        if(Input.GetKeyDown(KeyCode.D))
        {
            changeTool(ActionEnum.Tool.Coiling);
        }
        if(Input.GetKeyDown(KeyCode.F))
        {
            changeTool(ActionEnum.Tool.Uncoiling);
        }
        if(Input.GetKeyDown(KeyCode.Z))
        {
            changeTool(ActionEnum.Tool.Histone_Methylation);
        }
        if(Input.GetKeyDown(KeyCode.X))
        {
            changeTool(ActionEnum.Tool.Histone_Demethylation);
        }
        if(Input.GetKeyDown(KeyCode.C))
        {
            changeTool(ActionEnum.Tool.DNA_Methylation);
        }
        if(Input.GetKeyDown(KeyCode.V))
        {
            changeTool(ActionEnum.Tool.DNA_Demethylation);
        }
	}

    void changeTool(ActionEnum.Tool tool)
    {
        if (lastButton != null)
        {
            changeColour(lastButton, Color.white);
        }
        GameManager.instance.selectedTool = tool;
        Transform newButton = toolChildren.Single(t => t.gameObject.name == tool.ToString());
        changeColour(newButton, Color.green);
        lastButton = newButton;
        toolLabel.text = string.Format("Current Tool: {0}", tool.ToString().Replace('_', ' '));
    }

    void changeColour(Transform transform, Color color)
    {
        transform.GetComponent<Image>().color = color;
    }
}