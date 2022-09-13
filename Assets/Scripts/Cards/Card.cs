using System;
using UnityEngine;

public abstract class Card : MonoBehaviour
{
    public const float AnimationsDelay = 0.33f;

    public event Action onSpawn;

    [HideInInspector] public string cardName;
    [HideInInspector] public Sprite sprite;

    public CardSlot slot { get; private set; }

    public bool isMoving { get; private set; } = false;

    private Transform _target;

    private float _moveSpeed = 60f;
    private float _moveStopRadius = 1f;

    public void SetSlot(CardSlot slot)
    {
        if (slot == null)
        {
            return;
        }

        if (this.slot != null)
        {
            this.slot.card = null;
        }

        this.slot = slot;
        this.slot.card = this;
    }

    public void SpawnInSlot(CardSlot slot)
    {
        if (slot == null)
        {
            return;
        }

        SetSlot(slot);

        transform.position = slot.transform.position;

        onSpawn?.Invoke();
    }

    public void MoveToSlot(CardSlot slot)
    {
        if (slot == null)
        {
            return;
        }

        SetSlot(slot);

        TranslateTowards(this.slot.transform);
    }

    public void TranslateTowards(Transform target)
    {
        _target = target;

        isMoving = true;

        // _moveSpeed = Vector3.Distance(transform.position, target.position) / Card.AnimationsDelay;
    }

    private void Update()
    {
        if (_target != null)
        {
            Vector3 direction = (_target.position - transform.position).normalized;

            transform.Translate(direction * _moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, _target.position) <= _moveStopRadius)
            {
                transform.position = _target.position;

                _target = null;

                isMoving = false;
            }
        }
    }
}
