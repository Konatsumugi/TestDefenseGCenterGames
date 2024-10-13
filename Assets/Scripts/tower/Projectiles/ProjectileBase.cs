using System.Collections;
using UnityEngine;
using UnityEngine.Events;


    public abstract class ProjectileBase : MonoBehaviour
    {
        [SerializeField]
        protected int damage = 1;

        [SerializeField]
        public float speed = 0;

        
        [SerializeField]
        private UnityEvent OnDie;

        public virtual void DestroyProjectile()
        {
            OnDie?.Invoke();
           gameObject.SetActive(false);
            
        }

        public virtual void DestroyProjectile(float time)
        {
            Destroy(gameObject, time);
        }
    }
