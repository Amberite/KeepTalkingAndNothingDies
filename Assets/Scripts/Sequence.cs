using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Sequence {
    public string sequenceName;
    public List<CellNode> actions;

    public Sequence(string sequence, List<CellNode> list)
    {
        sequenceName = sequence;
        actions = list;
    }
}