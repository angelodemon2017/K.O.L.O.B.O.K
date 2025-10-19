using UnityEngine;
using Zenject;

public class MusicService : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    private SignalBus _signalBus;

    private float Volume => 1f;

    [Inject]
    public void Constructor(
        SignalBus signalBus)
    {
        _signalBus = signalBus;

        _signalBus.Subscribe<PlayClipSignal>(Handle);
    }

    private void Handle(PlayClipSignal playClipSignal)
    {
        PlayClip(playClipSignal.Clip);
    }

    private void PlayClip(AudioClip audioClip)
    {
        if (Volume <= 0)
            return;

        _audioSource.PlayOneShot(audioClip);
    }

    private void OnDestroy()
    {
        _signalBus.Unsubscribe<PlayClipSignal>(Handle);
    }
}