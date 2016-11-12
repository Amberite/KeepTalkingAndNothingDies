using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class GameManager : MonoBehaviour {
    [SerializeField]
    public List<CellNode> cellNodes;
    private List<Sequence> sections;

    public static GameManager instance;
	
    void Awake()
    {
        if (instance != this)
            Destroy(instance);
        if (instance == null)
            instance = this;

        sections = new List<Sequence>();
        sections.Add(new Sequence("Test", getListOfCells(new List<string>() { "TEST" })));

        Debug.Log(sections[0].actions.Count);
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
}
