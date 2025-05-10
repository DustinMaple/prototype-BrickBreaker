using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField] private Sprite[] _healthSprites;

    [SerializeField] private int _maxHealth = 1;

    [SerializeField] private bool _unBreakable = false;

    public bool UnBreakable => _unBreakable;
    
    private int _currentHealth;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _currentHealth = _maxHealth;
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateSprite();
    }

    private void UpdateSprite()
    {
        if (_currentHealth == 0)
        {
            return;
        }

        int index = Math.Min(_healthSprites.Length, _currentHealth - 1);
        _spriteRenderer.sprite = _healthSprites[index];
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        ReduceHealth();
    }

    private void ReduceHealth()
    {
        _currentHealth--;

        if (_currentHealth == 0)
        {
            GameManager.Instance.AddScore(100);
            Destroy(gameObject);
        }
        else
        {
            UpdateSprite();
        }
    }
}
