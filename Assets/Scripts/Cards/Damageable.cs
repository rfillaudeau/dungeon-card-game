using System;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    public event Action<int> onHealthUpdated;
    public event Action<int> onDamaged;
    public event Action<int> onHealed;
    public event Action onDie;

    public int health => _health;
    public int maxHealth => _maxHealth;
    public bool isDead => _health == 0;

    [SerializeField] private int _maxHealth = 1;
    [SerializeField] private int _health;

    public void TakeDamage(int damage)
    {
        if (isDead)
        {
            return;
        }

        _health -= damage;

        if (health < 0)
        {
            _health = 0;
        }

        onHealthUpdated?.Invoke(health);
        onDamaged?.Invoke(damage);

        if (isDead)
        {
            onDie?.Invoke();
        }
    }

    public void Heal(int heal)
    {
        if (isDead)
        {
            return;
        }

        _health += heal;

        if (_health > _maxHealth)
        {
            _health = _maxHealth;
        }

        onHealthUpdated?.Invoke(health);
        onHealed?.Invoke(heal);
    }

    public void SetMaxHealth(int maxHealth)
    {
        _maxHealth = maxHealth;
    }

    public void UpgradeMaxHealth(int upgrade)
    {
        _maxHealth += upgrade;
        _health = maxHealth;

        onHealthUpdated?.Invoke(health);
    }

    private void Start()
    {
        _health = maxHealth;

        onHealthUpdated?.Invoke(health);
    }
}
