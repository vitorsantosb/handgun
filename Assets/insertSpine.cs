using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class insertSpine : MonoBehaviour
{
    public GameObject camObj;
    public GameObject thisPlayer;
    void Start()
    {
        thisPlayer.GetComponent<AimConstraint>();
        SetPlayerConstraim();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetPlayerConstraim()
    {
        var comps = thisPlayer.GetComponentsInChildren<AimConstraint>();
        camObj = GameObject.FindGameObjectWithTag("camSpine");

        foreach (var comp in comps)

        {

            comp.worldUpObject = camObj.transform;

            var contraintSource = new ConstraintSource { sourceTransform = camObj.transform, weight = 1};

            comp.SetSource(0, contraintSource);

        }


    }
}
