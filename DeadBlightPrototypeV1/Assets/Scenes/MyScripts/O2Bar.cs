using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class O2Bar : MonoBehaviour
{
    // Keeps track of max and current Oxygen level of player
    [SerializeField] private int maxO2 = 100;
    [SerializeField] private float currentO2;
    [SerializeField] Image ForegroundImage;

    public float o2Ppercent;
    public float o2Degen;

    private LifeBar lifebar;
    private int getHP;
    void Awake()
    {
        lifebar = GetComponent<LifeBar>();
        
    }
    // Start is called before the first frame update
    void OnEnable()
    {
        currentO2 = maxO2;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentO2 > 0)
        {
            currentO2 -= 1f * o2Degen * Time.deltaTime;
            o2Ppercent = (currentO2 / (float)maxO2);
            ForegroundImage.fillAmount = o2Ppercent;
        }
        
    }
    private void NoOxygen()
    {
        lifebar.ManageHP(-10);
    }
}
