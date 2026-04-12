using System;
using UnityEngine;

public class AnimSprite : MonoBehaviour
{
    public Sprite[] spritesRun;
    private SpriteRenderer sprite;
    public Sprite[] spritesIdle;
    public Sprite[] spritesJump;
    private int _spriteIndexRun = 0;  
    private int _spriteIndexIdle = 0;
    private int _spriteIndexJump = 0;
    public float spriteChangeDelay = 0.1f;
    public Rigidbody2D rb;
    private float _spriteLastChange = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        _spriteLastChange += Time.deltaTime;
        if(Math.Abs(rb.linearVelocity.y) > 0.5f)
        {
            if(_spriteLastChange >= spriteChangeDelay) {
                _spriteLastChange = 0f;
                sprite.sprite = spritesJump[_spriteIndexJump];
                _spriteIndexJump = (_spriteIndexJump + 1) % spritesJump.Length;
            }
        }
        else if(Math.Abs(rb.linearVelocity.x) > 0.2f) {
            if(_spriteLastChange >= spriteChangeDelay) {
                _spriteLastChange = 0f;
                sprite.sprite = spritesRun[_spriteIndexRun];
                _spriteIndexRun = ( _spriteIndexRun + 1) % spritesRun.Length;
            }
        }
        else {
            if(_spriteLastChange >= spriteChangeDelay) {
                _spriteLastChange = 0f;
                sprite.sprite = spritesIdle[_spriteIndexIdle];
                _spriteIndexIdle = ( _spriteIndexIdle + 1) % spritesIdle.Length;
            }
        }
    }
}
