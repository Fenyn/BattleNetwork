using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Card : MonoBehaviour{
    abstract public void DoAction();
    abstract public string GetCardTypeAsString();
}
