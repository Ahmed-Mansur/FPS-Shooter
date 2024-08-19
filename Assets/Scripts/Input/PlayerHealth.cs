using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    public int health = 100;
    public int maxHealth = 100;
    public bool isDead = false;
    public GameObject deathScreen;
    public GameObject player;
    public Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        UpdateHealthUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            HandleInteraction();
        }

        health = Mathf.Clamp(health, 0, maxHealth);
        UpdateHealthUI();
    }

    void HandleInteraction()
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 3))
        {
            if (hit.collider.gameObject.CompareTag("Damagable"))
            {
                TakeDamage(hit.collider.gameObject);
            }
            else if (hit.collider.gameObject.CompareTag("Healable"))
            {
                HealDamage(hit.collider.gameObject);
            }
        }
    }

    void TakeDamage(GameObject damagable)
    {
        Enemy enemy = damagable.GetComponent<Enemy>();
        if (enemy != null)
        {
            health -= enemy.damage;
            Debug.Log("Player health: " + health);
        }
    }

    void HealDamage(GameObject healable)
    {
        Enemy enemy = healable.GetComponent<Enemy>(); // Assuming healable objects also use the Enemy script for healing value
        if (enemy != null)
        {
            health += enemy.damage; // Consider renaming 'damage' to something more fitting like 'healAmount'
            Debug.Log("Player health: " + health);
        }
    }

    void UpdateHealthUI()
    {
        healthText.text = health.ToString();
    }
}
