using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.Port;

public class ShieldFactory : MonoBehaviour
{
    public ShieldSpec GenerativeRandomSheild() 
    {

        ShieldSpec shieldSpec = new ShieldSpec();
        var ShieldRating = shieldSpec.capacity + (5 - shieldSpec.rechargeDelay) * 5 + shieldSpec.rechargeRate * (shieldSpec.rechargeRate / 2);
        if (ShieldRating > 300) {
            var scaleFactor = 300 / ShieldRating;
            shieldSpec.capacity *= scaleFactor;
            shieldSpec.rechargeDelay *= scaleFactor;
            shieldSpec.rechargeRate *= scaleFactor;
        }

        return shieldSpec;
    }


    public void Build(ShieldSpec shieldSpec, GameObject shield) {
       
        shield.GetComponent<ShieldController>().SetSpec(shieldSpec);
    }

}
