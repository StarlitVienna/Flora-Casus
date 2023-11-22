using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAfterImageHandler : MonoBehaviour
{
    public Transform playerTransform;
    public SpriteRenderer playerSpriteRenderer;

    private SpriteRenderer spriteRenderer;

    private float duration = 0.1f;
    private float activated;

    private float alpha;
    private float alphaScale = 0.85f;

    private Color color;

    private void Start()
    {
        alpha = 1.0f;
        color = new Color(1f, 1f, 1f, alpha);
    }

    private void OnEnable()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = playerSpriteRenderer.sprite;
        transform.position = playerTransform.position;
        activated = Time.time;
    }

    private void Update()
    {
        alpha *= alphaScale;
        color.a = alpha;
        spriteRenderer.color = color;
        if (Time.time >= (duration + activated))
        {
            
        }
    }

}
