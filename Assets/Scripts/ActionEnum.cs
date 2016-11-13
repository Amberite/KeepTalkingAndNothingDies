using UnityEngine;
using System.Collections;

public class ActionEnum{

	public enum Tool
    {
        None,
        Acetylation,
        Decetylation,
        Methylation,
        Demethylation,
        Ubiquitylation,
        Deubiquitylation,
        Phosphorylation,
        Dephosphorylation
    };

    public enum State
    {
        None,
        Me,
        Ac
    }
}
