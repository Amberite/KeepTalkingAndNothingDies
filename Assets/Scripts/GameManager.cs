using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class GameManager : MonoBehaviour {
    [SerializeField]
    public List<CellNode> cellNodes;
    private List<Sequence> possibleSections;

    public static GameManager instance = null;

    public ActionEnum.Tool selectedTool;
	
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);

        selectedTool = ActionEnum.Tool.None;

        possibleSections = new List<Sequence>();
        possibleSections.Add(new Sequence("Test", getListOfCells(new List<string>() { "TEST" })));
    }

    public void sectionCompleted()
    {

    }

    private List<CellNode> getListOfCells(IEnumerable<string> cellNames)
    {
        List<CellNode> cells = new List<CellNode>();
        foreach(string s in cellNames)
        {
            cells.Add(getCellByName(s));
        }
        return cells;
    }

    private CellNode getCellByName(string searchName)
    {
        var node = cellNodes.Where(m => m.name.ToLower() == searchName.ToLower()).First();
        return node;
    }

    public void LockSelection()
    {
        Debug.Log("Selection locked for section");
    }

    public void ChangeNode(int selection)
    {
        if(selectedTool != ActionEnum.Tool.None)
            Debug.Log(string.Format("Node {0} had {1} used on it", selection, selectedTool));
    }
}