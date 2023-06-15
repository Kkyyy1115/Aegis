using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Aegis;
using System.Diagnostics.Tracing;

public class TestFactory : MonoBehaviour
{


    [SerializeField] private GameObject Projectile;
    [SerializeField] private GameObject ProjectileSpawn;
    [SerializeField] private GameObject shield;
    private int num = 0;
    private float timeInterval = 0.0f;
    public GameObject projectile_gameObject;
    // Update is called once per frame
    private GameObject shieldObject;
    private EffectTypes Projectiletype;
    private EffectTypes shieldtype;


    void Awake()
    {
        Build();
        num = Random.Range(5, 11);
    }
    private void Build() {

        var shieldObject = new ShieldFactory();
        ShieldSpec shieldObject_ = shieldObject.GenerativeRandomSheild();
        shieldObject.Build(shieldObject_, shield);
        this.shieldtype = shieldObject_.Type;

    }

    void timeSchedule() {
        if (num > 0)
        {
            var projectile = new ProjectileFactory();
            ProjectileSpec projectile_ = projectile.GenerateRandomProjectiles();
            this.Projectiletype = projectile_.Type;
            projectile_.Damage *= DamageEngine.GetTypeFactor(this.Projectiletype, this.shieldtype);
            projectile_gameObject = projectile.Build(projectile_, Projectile, ProjectileSpawn);
            Destroy(Instantiate(projectile_gameObject, this.ProjectileSpawn.transform.position, Quaternion.identity));
            timeInterval = projectile_.ChargeTime + 1.5f;
            num -= 1;

        }

    }
    void Update()
    {

        timeInterval -= Time.deltaTime;
        if (timeInterval <= 0 && shield.GetComponent<ShieldController>().currentCapacity > 0)
        {
            timeSchedule();
            Debug.Log("fire");
        }

        else if (shield.GetComponent<ShieldController>().currentCapacity <= 0) {
            shield.GetComponent<ShieldController>().rechargeRate = 0;
            Debug.Log("Projectile win!");
        }

    }
}
