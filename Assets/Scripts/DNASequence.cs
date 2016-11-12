using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class DNASequence {
    public string sequenceName;
    public List<Nucleosome> nucleosomes;

    public DNASequence(string sequence, List<Nucleosome> list)
    {
        sequenceName = sequence;
        nucleosomes = list;
    }
}