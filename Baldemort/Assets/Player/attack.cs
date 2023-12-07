using UnityEngine;

public class attack : MonoBehaviour
{
    public GameObject projectilePrefabA;
    public GameObject projectilePrefabB;
    public GameObject projectilePrefabC;
    public GameObject projectilePrefabD;

    //spells sound effect
    [SerializeField] private AudioSource spellSoundA;
    [SerializeField] private AudioSource spellSoundB;
    [SerializeField] private AudioSource spellSoundC;
    [SerializeField] private AudioSource spellSoundD;

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
        // if the right mouse button is pressed and the attack is not on cooldown or the player has enough mana to attack
        if (Input.GetMouseButtonDown(1) && Time.time >= nextAttackTime && player.currentMana >= 10)
        {
            if (Time.time >= nextAttackTime)
            {
                switch (spellSlot.selectedSpell)
                {
                    case allSpellSlot.SpellType.A:
                        spellSoundA.Play();
                        PerformAttack(projectilePrefabA);
                        break;
                    case allSpellSlot.SpellType.B:
                        spellSoundB.Play();
                        PerformAttack(projectilePrefabB);
                        break;
                    case allSpellSlot.SpellType.C:
                        spellSoundC.Play();
                        PerformAttack(projectilePrefabC);
                        break;
                    case allSpellSlot.SpellType.D:
                        spellSoundD.Play();
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