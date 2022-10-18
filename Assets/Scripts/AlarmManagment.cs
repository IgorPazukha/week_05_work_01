using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmManagment : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Color _rechedColorIn;
    [SerializeField] private Color _rechedColorOut;
    [SerializeField] private AudioSource _audio;
    [SerializeField] private float _duration;

    private float _minVolume = 0f;
    private float _maxVolume = 1f;
    private float _durationCorrutine = 430;

    private void Start()
    {
        _audio.Stop();
        _audio.volume = 0f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Player>(out Player player))
        {
            _audio.Play();
            StartCoroutine(Fade(_durationCorrutine, _maxVolume));
            _spriteRenderer.color = _rechedColorIn;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
           if(_audio.volume > 0)
            {
                StartCoroutine(FadeOut());
            }
            _spriteRenderer.color = _rechedColorOut;
        }
    }

    private IEnumerator Fade(float duration, float target)
    {
        for(int i = 0; i < duration; i++)
        {
            _audio.volume = Mathf.MoveTowards(_audio.volume, target, Time.deltaTime * _duration);

            yield return null;
        }
    }

    private IEnumerator FadeOut()
    {
        while(_audio.volume > 0)
        {
            _audio.volume = Mathf.MoveTowards(_audio.volume, _minVolume, Time.deltaTime * _duration);

            yield return null;
        }
    }
}