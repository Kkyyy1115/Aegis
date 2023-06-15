using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Aegis;

public class ShieldController : MonoBehaviour   
{
    [SerializeField] private float capacity = 100.0f;
    [SerializeField] public float rechargeRate = 1.0f;
    [SerializeField] private float rechargeDelay = 1.0f;
    [SerializeField] private EffectTypes type = EffectTypes.Kinetic;
    [SerializeField] private EffectTypeColors effectTypeColors;
    [SerializeField] private GameObject scrollingText;
    [SerializeField] private GameObject healthBar;
    [SerializeField] public float currentCapacity = 0.0f;
    private HealthBarController healthBarController;
    private bool IsDamage = false;
    private float DelayTimer = 0.0f;
 

    void Awake()
    {
        this.DelayTimer = this.rechargeDelay;
        this.currentCapacity = this.capacity;   

        if (!this.healthBar.TryGetComponent<HealthBarController>(out this.healthBarController))
        {
            Debug.Log("ShieldController expects a health bar.");
        }

        this.healthBarController.ChangeValue(this.currentCapacity / this.capacity);

        this.gameObject.GetComponent<Renderer>().material.SetColor("_Color", this.effectTypeColors.GetColorByEffectType(this.type));     
    }

    private void TakeDamage(float damage)
    {
        float oldCapacity = this.currentCapacity;
        this.currentCapacity -= damage;
        
        if (currentCapacity < 0.0f)
        {
            currentCapacity = 0.0f;
        }
        if (currentCapacity <= 0.0f && oldCapacity > 0)
        {
            FindObjectOfType<SoundManager>().PlaySoundEffect("Explode");
        }
        else if (currentCapacity > 0)
        {
            FindObjectOfType<SoundManager>().PlaySoundEffect("Shrink");
        }

        this.healthBarController.ChangeValue(currentCapacity / capacity);
        
        if(this.scrollingText && oldCapacity > 0)
        {
            this.ShowScrollingText(damage.ToString());
        }
    }

    private void ShowScrollingText(string message)
    {
        var scrollingText = Instantiate(this.scrollingText, this.transform.position, Quaternion.identity);
        scrollingText.GetComponent<TextMesh>().text = message;
    }

    private void OnTriggerEnter(Collider other)
    {
        if ("Projectile" == other.tag)
        {
            var damage = other.GetComponent<ProjectileController>().GetDamage();
            TakeDamage(damage);
            this.IsDamage = true;
        }
    }

    public void SetSpec(ShieldSpec shield) {

        this.capacity =  shield.capacity;
        this.rechargeRate = shield.rechargeRate;
        this.rechargeDelay = shield.rechargeDelay;
        this.type = shield.Type;
    }

    // Update is called once per frame
    void Update()
    {
        var capacityRatio = currentCapacity / capacity;
        this.transform.localScale = new Vector3(capacityRatio, capacityRatio, capacityRatio);
        this.gameObject.GetComponent<Renderer>().material.SetColor("_Color", effectTypeColors.GetColorByEffectType(this.type));
    }

    void FixedUpdate()
    {
        if (this.currentCapacity != this.capacity)
        {
            Debug.Log(this.IsDamage);
            DelayTimer -= Time.deltaTime;
            if (DelayTimer <= 0)
            {
                if (this.currentCapacity <= this.capacity)
                {
                    this.currentCapacity += this.rechargeRate * Time.deltaTime;
                    this.healthBarController.ChangeValue(currentCapacity / capacity);
                }
                else
                {
                    this.currentCapacity = this.capacity;
                }

            }

            if (this.IsDamage)
            {
                this.DelayTimer = this.rechargeDelay;
                this.IsDamage = false;
            }

        }

        
    }
}   
