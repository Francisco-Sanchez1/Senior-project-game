using UnityEngine;

public class attack : MonoBehaviour
{
    public GameObject projectilePrefabA;
    public GameObject projectilePrefabB;
    public GameObject projectilePrefabC;
    public GameObject projectilePrefabD;
    
    public float attackSpeed = 20f;
    public float cooldownTime = 2f;
    private float nextAttackTime = 0f;
    private Vector2 attackDirection;
    private PlayerCntrl player;

    private allSpellSlot spellSlot;

    private void Start()
    {
        player = GetComponent<PlayerCntrl>();
        spellSlot = FindObjectOfType<allSpellSlot>(); // Find the allSpellSlot component in the scene.

        if (spellSlot == null)
        {
            Debug.LogError("allSpellSlot component not found in the scene!");
        }
    }

    void Update()
    {
        GetAttackDirection();
        HandleAttack();
    }

    void GetAttackDirection()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        attackDirection = (mousePosition - (Vector2)transform.position).normalized;
    }

    void HandleAttack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (Time.time >= nextAttackTime)
            {
                switch (spellSlot.selectedSpell)
                {
                    case allSpellSlot.SpellType.A:
                        PerformAttack(projectilePrefabA);
                        break;
                    case allSpellSlot.SpellType.B:
                        PerformAttack(projectilePrefabB);
                        break;
                    case allSpellSlot.SpellType.C:
                        PerformAttack(projectilePrefabC);
                        break;
                    case allSpellSlot.SpellType.D:
                        PerformAttack(projectilePrefabD);
                        break;
                }
                nextAttackTime = Time.time + cooldownTime;
            }
            else
            {
                Debug.Log("Attack is on cooldown.");
            }
        }
    }

    void PerformAttack(GameObject projectilePrefab)
    {
        BaseProjectile currentProjectile = projectilePrefab.GetComponent<BaseProjectile>();
        float manaCost = currentProjectile.manaCost;

        if (player.currentMana >= manaCost)
        {
            float angle = Mathf.Atan2(attackDirection.y, attackDirection.x) * Mathf.Rad2Deg;
            Quaternion projectileRotation = Quaternion.Euler(0, 0, angle);

            GameObject projectile = Instantiate(projectilePrefab, transform.position, projectileRotation);
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            rb.velocity = attackDirection * attackSpeed;
            Destroy(projectile, 1.5f);

            player.useMana(manaCost);
        }
        else
        {
            Debug.Log("Not enough mana to attack.");
        }
    }
}