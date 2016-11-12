using UnityEngine;
using System.Collections;

[System.Serializable]
public class CellNode{
    public string name;
    public ActionRequired toolRequired;
    public string labelText;
    public Sprite picture;

    public enum ActionRequired
    {
        Histone_Methylation,
        Histone_Demethylation,
        Coiling,
        Uncoiling,
        Acetylation,
        Decetylation,
        DNA_Methylation,
        DNA_Demethylation
    };
}
