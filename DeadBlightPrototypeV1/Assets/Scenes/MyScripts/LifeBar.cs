using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeBar : MonoBehaviour
{
    // Keeps track of max and current HP of player
    [SerializeField] private int maxHP =  100;
    [SerializeField] private int currentHP;

    // Tracks the percentage change of HP. Used for HP bar.
    public float CHPpercent;

    // Start is called before the first frame update
    void OnEnable()
    {
      //Sets the current hp to be equal to the max HP
        currentHP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("h") && currentHP >0)
        {
            ManageHP(-10);
        }
    }

    // Used to reduce or increase HP of player.
    public void ManageHP(int amount)
    {
        currentHP += amount;

        float CHPpercent = (float)currentHP / (float)maxHP;
        Debug.Log(CHPpercent);
        
    }
}
