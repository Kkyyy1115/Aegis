using Aegis;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpec : MonoBehaviour
{
    // Start is called before the first frame update
    public float Damage = Random.Range(1f, 50f);
    public float ChargeTime = Random.Range(0.5f, 3f);
    public EffectTypes Type = (EffectTypes)Random.Range(0, 3);
}
