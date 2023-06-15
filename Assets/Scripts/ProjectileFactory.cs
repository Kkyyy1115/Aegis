using Aegis;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileFactory : MonoBehaviour
{
    public ProjectileSpec GenerateRandomProjectiles()
    {

        ProjectileSpec projectileSpec = new ProjectileSpec();

        var ProjectileRating = projectileSpec.Damage * 2 + Mathf.Pow((3 - projectileSpec.ChargeTime), 4);

        if (ProjectileRating > 100) {
            var scaleFactor = 100 / ProjectileRating;
            projectileSpec.Damage *= scaleFactor;
            projectileSpec.ChargeTime *= scaleFactor;
        }

        return projectileSpec;

    }


    public GameObject Build(ProjectileSpec projectileSpec, GameObject Projectile, GameObject ProjectileSpawn)
    {
        GameObject newProjectile = Instantiate(Projectile, ProjectileSpawn.transform.position, Quaternion.identity);
        newProjectile.GetComponent<ProjectileController>().SetSpec(projectileSpec);
        
        return newProjectile;
    }


}
