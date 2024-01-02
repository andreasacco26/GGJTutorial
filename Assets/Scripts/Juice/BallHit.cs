using DG.Tweening;
using UnityEngine;

// TODO list:
// Scale paddle based on mouse offset
// Cutify: paddle face
// Cutify: paddle face animation
// Bricks: crumble

public class BallHit : MonoBehaviour
{
    [SerializeField] float brickMaxTimer = 1f;

    [SerializeField] float amplitude;
    [SerializeField] float shakeTime;
    [SerializeField] float tweenDuration;
    private float brickSoundTimer = 0.0f;
    private int dings;

    private bool wallSoundActive = false;
    private bool paddleSoundActive = false;
    private bool brickSoundActive = false;
    private bool cameraShakeActive = false;
    private bool ballTweenActive = false;

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
        float tweenDuration = 0.015f;
        mySequence.Append(transform.DOScale(0.5f, tweenDuration));
        mySequence.Append(sr.DOColor(Color.white, tweenDuration));
        mySequence.Append(transform.DOScale(0.3f, tweenDuration));
        mySequence.Append(sr.DOColor(originalColor, tweenDuration));
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (wallSoundActive && other.gameObject.CompareTag("Wall")) AudioManager.Instance.PlaySfx("ball-wall");
        if (brickSoundActive && other.gameObject.CompareTag("Brick")) PlayBrickSounds();
        if (paddleSoundActive && other.gameObject.CompareTag("Paddle")) AudioManager.Instance.PlaySfx("paddle");
        if (cameraShakeActive) CameraManager.Instance.ShakeCamera(amplitude, shakeTime);
        if (ballTweenActive) BallTween();
    }

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
