using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public enum InteractionState
{
    Default // Can attack and interact
    , Shop // Interaction with shopping RuinStones (AKA power sources)
    , Construct // Holding a construction item
}

public class TP_Status : MonoBehaviour {

    public static TP_Status _instance;
    public float health = 100;
    public InteractionState interactionState;

    public bool isInRangeOfRunestone = false;
    public bool hasFlamethrower = false;
    
    void Awake () {
        _instance = this;
        interactionState = InteractionState.Default;
	}
}
