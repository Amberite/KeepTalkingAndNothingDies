﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

using Random = System.Random;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    [SerializeField]
    public List<Histone> cellNodes;
    private List<DNASequence> possibleSections;
    private DNASequence currentSequence = new DNASequence();
    private Histone currentHistone;
    private int currentIndex = -1;
    public float totalTime;
    private float curTime;

    public AudioSource effectSource;

    public AudioClip[] clickSounds;
    public AudioClip[] goodBloops;
    public AudioClip[] badBloops;

    public GameObject tailRoot;
    public Sprite[] flagSprites;

    public GameObject oreoHolder;

    public static GameManager instance = null;

    public ActionEnum.Tool selectedTool;

    private Animation anim;

    public Text histoneLabel;
    public Text timeLabel;

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
            GameObject tailNode = tailRoot.transform.GetChild(i).gameObject;
            tailNode.GetComponent<Image>().sprite = flagSprites[(int)currentHistone.currentState[i]];
            Debug.Log(currentHistone.currentState[i] + " " + currentHistone.requiredState[i]);
        }
        anim = oreoHolder.GetComponent<Animation>();
        histoneLabel.text = currentHistone.name;
    }

    void Update()
    {
        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            curTime -= Time.deltaTime;
            if (curTime <= 0)
            {
                SceneManager.LoadScene(1);
            }
            string minutes = Mathf.Floor(curTime / 60).ToString("00");
            string seconds = (curTime % 60).ToString("00");
            timeLabel.text = string.Format("Time remaining: {0}:{1}", minutes, seconds);
        }
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
        if(!anim.isPlaying)
        {
            currentIndex++;
            if (currentIndex == 1)
                anim.Play("ScrollUpStart");
            else if (currentIndex < currentSequence.nucleosomes.Count - 1)
                anim.Play("ScrollUpMiddle");
            else if (currentIndex == currentSequence.nucleosomes.Count - 1)
                anim.Play("ScrollUpEnd");
            if (currentIndex < currentSequence.nucleosomes.Count)
            {
                currentHistone = currentSequence.nucleosomes[currentIndex];
                for (int i = 0; i < currentHistone.currentState.Length; i++)
                {
                    GameObject tailNode = tailRoot.transform.GetChild(i).gameObject;
                    tailNode.GetComponent<Image>().sprite = flagSprites[(int)currentHistone.currentState[i]];
                }
            }
            else
            {
                Debug.Log("LOAD END SCENE");
            }
            histoneLabel.text = currentHistone.name;
        }
    }

    public void SetTool(ActionEnum.Tool tool)
    {
        Random r = new Random(DateTime.Now.Millisecond);
        effectSource.clip = clickSounds[r.Next(clickSounds.Length)];
        effectSource.Play();
        selectedTool = tool;
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
                        ChangeState(nodeIndex, ActionEnum.State.None);
                    else
                        GivePenalty();
                    break;
            }
        }
    }

    private void ChangeState(int selection, ActionEnum.State newState)
    {
        Random r = new Random(DateTime.Now.Millisecond);
        effectSource.clip = goodBloops[r.Next(goodBloops.Length)];
        effectSource.Play();
        currentHistone.currentState[selection] = newState;
        Image tailFlag = tailRoot.transform.GetChild(selection).GetComponent<Image>();
        Debug.Log(tailFlag.name);
        tailFlag.sprite = flagSprites[(int)newState];
    }

    private void GivePenalty()
    {
        Random r = new Random(DateTime.Now.Millisecond);
        effectSource.clip = badBloops[r.Next(badBloops.Length)];
        effectSource.Play();
        Debug.Log("THAT WAS WRONG");
        curTime -= 15.0f;
    }
}