using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats myInstance { get; set; }
    public static PlayerStats MyInstance
    {
        get
        {
            if (myInstance == null)
            {
                myInstance = FindObjectOfType<PlayerStats>();
            }
            return myInstance;
        }
    }

    public float CurrentHealth { get => currentHealth; set => currentHealth = value; }
    public float CurrentStamina { get => currentStamina; set => currentStamina = value; }
    public float MaxHealth { get => maxHealth; set => maxHealth = value; }
    public float MaxStamina { get => maxStamina; set => maxStamina = value; }
    public float CarryWeight { get => carryWeight; set => carryWeight = value; }

    [SerializeField]
    private float currentHealth, maxHealth, carryWeight;
    [SerializeField]
    private float currentStamina, maxStamina;
    [SerializeField]
    private Image healthFillImage, staminaFillImage;


    // Start is called before the first frame update
    void Start()
    {
        CurrentHealth = MaxHealth;
        CurrentStamina = MaxStamina;
    }

    public void SetStats()
    {
        //Stop stats going out of bounds.
        if (CurrentHealth <= 0)
                CurrentHealth = 0;
        if (CurrentHealth >= MaxHealth)
                CurrentHealth = MaxHealth;
        if (CurrentStamina <= -10)
                CurrentStamina = -10;
        if (CurrentStamina >= MaxStamina)
                CurrentStamina = MaxStamina;

    }


    // Update is called once per frame
    void LateUpdate()
    {
        SetStats();
        if (Input.GetKeyDown(KeyCode.Z))
            CurrentHealth -= Random.Range(0,10);
        if (Input.GetKeyDown(KeyCode.X))
            CurrentHealth += Random.Range(0, 10);

        healthFillImage.fillAmount = CurrentHealth / 100;
        staminaFillImage.fillAmount = CurrentStamina / 100;

        currentStamina += 10 * Time.deltaTime;

    }
}
