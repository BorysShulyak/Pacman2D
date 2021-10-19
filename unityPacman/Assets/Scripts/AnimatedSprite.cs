using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class AnimatedSprite : MonoBehaviour
{
    public SpriteRenderer spriteRenderer { get; private set; }
    public Sprite[] sprites;
    public float animationTime = 0.25f;
    public int animationFrame { get; private set; }
    public bool isAnimationLooped = true;

    private void Awake() {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start() {
        InvokeRepeating(nameof(AnimateSprite), this.animationTime, this.animationTime);
    }

    public void RestartAnimation() {
        this.animationFrame = -1;
        AnimateSprite();
    }

    private void AnimateSprite() {
        if(!this.spriteRenderer.enabled) {
            return;
        }

        this.animationFrame++;
        if(this.animationFrame >= this.sprites.Length && this.isAnimationLooped) {
            this.animationFrame = 0;
        }

        if(this.animationFrame >= 0 && this.animationFrame < this.sprites.Length) {
            this.spriteRenderer.sprite = this.sprites[this.animationFrame];
        }
    }

}
