using UnityEngine;
using System.Collections;

public enum AttackType
{
    Physical
    , Fire
    , Tree
    , Rock
}

public class Attack : MonoBehaviour
{
    public static Attack _instance;

    public string myName = "spell";
    public bool isActive;
    public float upgradeCost; // how much money for the next upgrade
    public int level;
    public float power;
    public float duration;
    public float timeLeft;
    public AttackType attackType;
    public bool isContinuousAttack;    
    public float recoverySlower; // the higher this number, the slower the recovery
    public float recoveryBooster;

    public AudioClip activateSound;
    private AudioSource audioSource;

    void Awake()
    {
        _instance = this;
        recoveryBooster = 1;
        recoverySlower = 1;
        level = 0;
        Disactivate();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Lower timeLeft while active
        if (isActive)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft <= 0)
            {
                Disactivate();
            }
        }

        // Regain fuel(duration) when not active
        if (!isActive && (timeLeft < duration))
        {
            timeLeft += (Time.deltaTime / recoverySlower * recoveryBooster); // takes 2x as long to recover
        }
    }

    public void ToggleActivatation()
    {
        if (!isActive)
        {
            Activate();
        }
        else
        {
            Disactivate();
        }
    }

    public virtual void Activate()
    {
        isActive = true;
        GetComponent<Renderer>().enabled = true;
        if(activateSound != null)
        {
            audioSource.PlayOneShot(activateSound, 1.0f);
        }
        //this.gameObject.SetActive(true);
    }

    public virtual void Disactivate()
    {
        isActive = false;
        GetComponent<Renderer>().enabled = false;
        //this.gameObject.SetActive(false);
    }


    public virtual void Upgrade(int myLevel)
    {
        Debug.Log("Parent upgrade was called!");
        Debug.Log("No upgrade level selected!");
    }

}
