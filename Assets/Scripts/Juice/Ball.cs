using DG.Tweening;
using UnityEngine;

// TODO list:
// Ball: particles
// Paddle: confetti
// Bricks: crumble
// Display settings: what are the most common screen sizes for brick breakers?
// Score: put a small text in the canvas, update it +10pts from the ball whenever it hits a brick. Counter multiplies results
// Toon shader?
// Bloom?
public class Ball : MonoBehaviour
{
    [Tooltip("Short interval during which player can do brick combos. Used for SFX and VFX"), SerializeField] float brickMaxTimer = 1f;
    [Header("Camera Effects")]
    [Tooltip("Amplitude of the camera shake."), SerializeField] float amplitude;
    [Tooltip("Time a single camera shake lasts."), SerializeField] float shakeTime;
    [Header("Tweening")]
    [Tooltip("How long the scale effect on the ball lasts."), SerializeField] float ballTweenDuration;
    [Tooltip("Maximum scale for the ball when tweening"), SerializeField] float maxScale;

    private float brickSoundTimer = 0.0f;
    private int dings;
    private bool wallSoundActive = false;
    private bool paddleSoundActive = false;
    private bool brickSoundActive = false;
    private bool cameraShakeActive = false;
    private bool ballTweenActive = false;
    private float initialScale;

    private void PlayBrickSounds()
    {
        string brickSfxName = "pling";
        if (brickSoundTimer < brickMaxTimer)
        {
            ++dings;
        }
        else
        {
            brickSoundTimer = 0.0f;
            dings = 1;
        }
        brickSfxName += dings;
        AudioManager.Instance.PlaySfx(brickSfxName);
    }

    private void BallTween()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        Color originalColor = sr.color;
        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(transform.DOScale(maxScale, ballTweenDuration));
        mySequence.Append(sr.DOColor(Color.white, ballTweenDuration));
        mySequence.Append(transform.DOScale(initialScale, ballTweenDuration));
        mySequence.Append(sr.DOColor(originalColor, ballTweenDuration));
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (wallSoundActive && other.gameObject.CompareTag("Wall")) AudioManager.Instance.PlaySfx("ball-wall");
        if (brickSoundActive && other.gameObject.CompareTag("Brick")) PlayBrickSounds();
        if (paddleSoundActive && other.gameObject.CompareTag("Paddle")) AudioManager.Instance.PlaySfx("paddle");
        if (cameraShakeActive) CameraManager.Instance.ShakeCamera(amplitude, shakeTime);
        if (ballTweenActive) BallTween();
    }

    // We assume scale on the x is equal to scale on the y.
    void Start() => initialScale = transform.localScale.x;

    void Update()
    {
        brickSoundTimer += Time.deltaTime;
    }

    public void ToggleBrickSound() { brickSoundActive = !brickSoundActive; }
    public void TogglePaddleSound() { paddleSoundActive = !paddleSoundActive; }
    public void ToggleWallSound() { wallSoundActive = !wallSoundActive; }
    public void ToggleCameraShake() { cameraShakeActive = !cameraShakeActive; }
    public void ToggleBallTween() { ballTweenActive = !ballTweenActive; }
}
