using Aegis;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldSpec : MonoBehaviour
{
    // Start is called before the first frame update
    public float capacity = Random.Range(50f, 250f);
    public float rechargeDelay = Random.Range(0.5f, 5f);
    public float rechargeRate = Random.Range(1f, 25f);
    public EffectTypes Type = (EffectTypes)Random.Range(0, 3);

}
