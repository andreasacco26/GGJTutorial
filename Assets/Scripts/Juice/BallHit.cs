using Cinemachine;
using DG.Tweening;
using UnityEngine;

// TODO list:
// Scale ball on hit
// Change color on hit
// Scale paddle based on mouse offset
// Cutify: paddle face
// Cutify: paddle face animation
// Bricks: crumble

public class BallHit : MonoBehaviour
{
    [SerializeField] float brickMaxTimer = 1f;
    [SerializeField] CinemachineVirtualCamera vcam;
    [SerializeField] float amplitude;
    [SerializeField] float shakeTime;
    [SerializeField] float tweenDuration;
    private float brickSoundTimer = 0.0f;
    private int dings;
    private float shakeTimer;
    private float shakeTimerTotal;
    private float shakeInitialAmplitude;

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

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (wallSoundActive && other.gameObject.CompareTag("Wall"))
        {
            AudioManager.Instance.PlaySfx("ball-wall");
        }
        if (brickSoundActive && other.gameObject.CompareTag("Brick"))
        {
            PlayBrickSounds();
        }
        if (paddleSoundActive && other.gameObject.CompareTag("Paddle"))
        {
            AudioManager.Instance.PlaySfx("paddle");
        }
        if (cameraShakeActive)
        {
            vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = amplitude;
            // TODO: improve this shit. Create a CameraManager singleton.
            shakeTimer = shakeTime;
            shakeTimerTotal = shakeTime;
            shakeInitialAmplitude = amplitude;
        }
        if (ballTweenActive)
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
    }

    void Update()
    {
        brickSoundTimer += Time.deltaTime;
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain =
            Mathf.Lerp(shakeInitialAmplitude, 0f, 1 - (shakeTimer / shakeTimerTotal));
        }
    }

    public void ToggleBrickSound() { brickSoundActive = !brickSoundActive; }
    public void TogglePaddleSound() { paddleSoundActive = !paddleSoundActive; }
    public void ToggleWallSound() { wallSoundActive = !wallSoundActive; }
    public void ToggleCameraShake() { cameraShakeActive = !cameraShakeActive; }
    public void ToggleBallTween() { ballTweenActive = !ballTweenActive; }
}
