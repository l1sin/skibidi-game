using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

namespace Sounds
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager Instance;
        public AudioMixer AudioMixer;
        [SerializeField] private GameObject _soundPrefab;

        private void Start()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }
            DontDestroyOnLoad(gameObject);
        }

        public void PlaySound(AudioClip clip, AudioMixerGroup audioMixerGroup = null)
        {
            var newSound = Instantiate(_soundPrefab);
            var audio = newSound.GetComponent<AudioSource>();
            audio.clip = clip;
            if (audioMixerGroup != null) audio.outputAudioMixerGroup = audioMixerGroup;
            audio.Play();
        }

        public void PlaySoundRandom(AudioClip[] clips, AudioMixerGroup audioMixerGroup = null)
        {
            var newSound = Instantiate(_soundPrefab);
            var audio = newSound.GetComponent<AudioSource>();
            var audioClip = clips[Random.Range(0, clips.Length)];
            if (audioMixerGroup != null) audio.outputAudioMixerGroup = audioMixerGroup;
            audio.clip = audioClip;
            audio.Play();
        }

        public void AudioMixerSetFloat(string paramName, float value)
        {
            AudioMixer.SetFloat(paramName, value);
        }

        private void ResetMixer(Scene arg0, LoadSceneMode arg1)
        {
            AudioMixerSetFloat("VolumeMaster", 0);
        }

        private void OnEnable()
        {
            SceneManager.sceneLoaded += ResetMixer;
        }   

        private void OnDisable()
        {
            SceneManager.sceneLoaded += ResetMixer;
        }
    }
}

