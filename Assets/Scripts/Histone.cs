using UnityEngine;
using System.Collections;

[System.Serializable]
public class Histone{
    public string name;
    public bool isActive;
    public ActionEnum.State[] requiredState;
    public ActionEnum.State[] currentState;
}
