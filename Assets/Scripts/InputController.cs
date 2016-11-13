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
    private Vector2 selectedSize = new Vector2(55, 55);
    private Vector2 unselectedSize = new Vector2(50, 50);
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
            changeTool(ActionEnum.Tool.Methylation);
        }
        if(Input.GetKeyDown(KeyCode.F))
        {
            changeTool(ActionEnum.Tool.Demethylation);
        }
        if(Input.GetKeyDown(KeyCode.Z))
        {
            changeTool(ActionEnum.Tool.Ubiquitylation);
        }
        if(Input.GetKeyDown(KeyCode.X))
        {
            changeTool(ActionEnum.Tool.Deubiquitylation);
        }
        if(Input.GetKeyDown(KeyCode.C))
        {
            changeTool(ActionEnum.Tool.Phosphorylation);
        }
        if(Input.GetKeyDown(KeyCode.V))
        {
            changeTool(ActionEnum.Tool.Dephosphorylation);
        }
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            GameManager.instance.ChangeNode(1);
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            GameManager.instance.ChangeNode(2);
        }
        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            GameManager.instance.ChangeNode(3);
        }
        if(Input.GetKeyDown(KeyCode.Alpha4))
        {
            GameManager.instance.ChangeNode(4);
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            GameManager.instance.LockSelection();
        }
	}

    void changeTool(ActionEnum.Tool tool)
    {
        if (lastButton != null)
        {
            changeSelection(lastButton, unselectedSize);
        }
        GameManager.instance.selectedTool = tool;
        Transform newButton = toolChildren.Single(t => t.gameObject.name == tool.ToString());
        changeSelection(newButton, selectedSize);
        lastButton = newButton;
        toolLabel.text = string.Format("Current Tool: {0}", tool.ToString().Replace('_', ' '));
    }

    void changeSelection(Transform transform, Vector2 size)
    {
        transform.GetComponent<RectTransform>().sizeDelta = size;
    }
}