using Code.Components.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI
{
    public class MusicParameters:MonoBehaviour
    {
        [SerializeField] private MonoBehaviour _music;
        [SerializeField] private MonoBehaviour _sound;
        [SerializeField] private Slider _musicSlider;
        [SerializeField] private Slider _soundSlider;
        [SerializeField] private Button _soundParameters;
        private IChangeSoundVolume _musicVolume;
        private IChangeSoundVolume _soundVolume;
        private CanvasGroup _canvas;

        private void Awake()
        {
            _canvas = GetComponent<CanvasGroup>();
            if (_music is IChangeSoundVolume music)
                _musicVolume = music;
            if (_sound is IChangeSoundVolume sound)
                _soundVolume = sound;
            _musicSlider.onValueChanged.AddListener(_musicVolume.Change);
            _soundSlider.onValueChanged.AddListener(_soundVolume.Change);
            _soundParameters.onClick.AddListener(ChangeVisible);
        }

        private void ChangeVisible()
        {
            if (_canvas.alpha == 0)
                _canvas.alpha = 1;
            else
                _canvas.alpha = 0;
        }
    }
}