using System.Collections;
using UnityEngine;

namespace Bogadanul.Assets.Scripts.Tree
{
    public class TreeShootTarget : TreeShootBase
    {
        private IProjectileTarget Currentprojectile = null;
        public override IEnumerator Shooting(Transform target)
        {
            if (towerDetail.enemies.Count > 0)
            {
                CanShoot = false;
                instance = ObjectPooler.Ins.GetPooledObject(2);
                bullet = instance.GetComponent<Bullet>();
                instance.transform.position = attackPoint.position;
                instance.SetActive(true);
                // instance = Instantiate(projectileObject, transform.position, Quaternion.identity, transform.parent);
                Currentprojectile = CacheProjectile<IProjectileTarget>(instance);
                Currentprojectile.Init(target);
                yield return StartCoroutine(fireRater.Wait());
                CanShoot = true;
            }
        }
    }
}