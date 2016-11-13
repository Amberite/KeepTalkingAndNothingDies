using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

using Random = System.Random;

public class GameManager : MonoBehaviour {
    [SerializeField]
    public List<Histone> cellNodes;
    private List<DNASequence> possibleSections;
    private DNASequence currentSequence = new DNASequence();
    private Histone currentHistone;
    private int currentIndex = -1;
    public float totalTime;
    private float curTime;

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

        possibleSections = new List<DNASequence>();
        possibleSections.Add(new DNASequence("Blood Cell", getListOfCells(new List<string>() { "PU.1", "Sox2", "Oct4", "Gata2", "MyoD", "CD34", "Thy1", "Vim" })));

        Random r = new Random(DateTime.Now.Millisecond);
        DNASequence tempSequence = possibleSections[r.Next(0, possibleSections.Count)];
        currentSequence.sequenceName = tempSequence.sequenceName;
        while(tempSequence.nucleosomes.Count > 0)
        {
            Histone toMove = tempSequence.nucleosomes[r.Next(0, tempSequence.nucleosomes.Count)];
            Array values = Enum.GetValues(typeof(ActionEnum.State));
            toMove.currentState = new ActionEnum.State[3];
            for (int i = 0; i < toMove.currentState.Length; i++)
            {
                toMove.currentState[i] = (ActionEnum.State)values.GetValue(r.Next(values.Length));
            }
            currentSequence.nucleosomes.Add(toMove);
            tempSequence.nucleosomes.Remove(toMove);
        }
        currentHistone = currentSequence.nucleosomes[0];
        currentIndex = 0;
        curTime = totalTime;
        for(int i = 0; i < currentHistone.currentState.Length; i++)
        {
            Debug.Log(currentHistone.currentState[i] + " " + currentHistone.requiredState[i]);
        }
    }

    void Update()
    {
        curTime -= Time.deltaTime;
        if (curTime <= 0)
            Debug.Log("OUT OF TIME FUCKER");
    }

    void OnGUI()
    {
        GUILayout.Label(curTime.ToString());
    }

    private List<Histone> getListOfCells(IEnumerable<string> cellNames)
    {
        List<Histone> cells = new List<Histone>();
        foreach(string s in cellNames)
        {
            cells.Add(getCellByName(s));
        }
        return cells;
    }

    private Histone getCellByName(string searchName)
    {
        var node = cellNodes.Where(m => m.name.ToLower() == searchName.ToLower()).First();
        return node;
    }

    public void LockSelection()
    {
        currentIndex++;
        if(currentIndex < currentSequence.nucleosomes.Count)
        {
            currentHistone = currentSequence.nucleosomes[currentIndex];
            for (int i = 0; i < currentHistone.currentState.Length; i++)
            {
                Debug.Log(currentHistone.currentState[i] + " " + currentHistone.requiredState[i]);
            }
        }
        else
        {
            Debug.Log("LOAD END SCENE");
        }
    }

    public void ChangeNode(int selection)
    {
        int nodeIndex = selection - 1;
        if (selectedTool != ActionEnum.Tool.None)
        {
            ActionEnum.State curState = currentHistone.currentState[nodeIndex];
            ActionEnum.State goalState = currentHistone.requiredState[nodeIndex];
            switch (selectedTool)
            {
                case ActionEnum.Tool.Acetylation:
                    if (curState == ActionEnum.State.None && goalState == ActionEnum.State.Ac)
                        ChangeState(nodeIndex, ActionEnum.State.Ac);
                    else
                        GivePenalty();
                    break;
                case ActionEnum.Tool.Decetylation:
                    if (curState == ActionEnum.State.Ac && goalState != ActionEnum.State.Ac)
                        ChangeState(nodeIndex, ActionEnum.State.None);
                    else
                        GivePenalty();
                    break;
                case ActionEnum.Tool.Methylation:
                    if (curState == ActionEnum.State.None && goalState == ActionEnum.State.Me)
                        ChangeState(nodeIndex, ActionEnum.State.Me);
                    else
                        GivePenalty();
                    break;
                case ActionEnum.Tool.Demethylation:
                    if (curState == ActionEnum.State.Me && goalState != ActionEnum.State.Me)
                        ChangeState(nodeIndex, ActionEnum.State.Me);
                    else
                        GivePenalty();
                    break;
            }
        }
    }

    private void ChangeState(int selection, ActionEnum.State newState)
    {
        currentHistone.currentState[selection] = newState;
        if (currentHistone.currentState[selection] == currentHistone.requiredState[selection])
        {
            Debug.Log("NOW ITS DONE");
        }
    }

    private void GivePenalty()
    {
        Debug.Log("THAT WAS WRONG");
        curTime -= 15.0f;
    }
}