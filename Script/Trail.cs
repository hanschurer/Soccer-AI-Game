using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trail : MonoBehaviour
{

    // Script to use the trail function on the ball. 
    // WeaponTrail is from POCKET RPG WEAPON TRAILS FOR UNITY
    // This script simply calls the functions in it.
    public WeaponTrail Trail;
    private float animationIncrement = 0.003f;
    private float t = 0.033f;
    private float tempT = 0;

    void LateUpdate()
    {
        t = Mathf.Clamp(Time.deltaTime, 0, 0.066f);

        if (t > 0)
        {
            while (tempT < t)
            {
                tempT += animationIncrement;

                if (Trail.time > 0)
                {
                    Trail.Itterate(Time.time - t + tempT);
                }
                else
                {
                    Trail.ClearTrail();
                }
            }

            tempT -= t;

            if (Trail.time > 0)
            {
                Trail.UpdateTrail(Time.time, t);
            }
        }
    }
}
