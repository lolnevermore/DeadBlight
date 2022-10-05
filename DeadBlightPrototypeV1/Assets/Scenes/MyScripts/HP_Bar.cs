using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP_Bar : MonoBehaviour
{
    [SerializeField] Image ForegroundImage;

    private float Change;

    void OnAwake()
    {
         float Change = GetComponentInParent<LifeBar>().CHPpercent;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
