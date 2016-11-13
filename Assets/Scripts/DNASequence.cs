using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class DNASequence {
    public string sequenceName;
    public List<Histone> nucleosomes;

    public DNASequence(string sequence, List<Histone> list)
    {
        sequenceName = sequence;
        nucleosomes = list;
    }
}