using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Study
{
    public partial class SoundManager
    {
        AudioSource _bgm = null;
        private void Awake()
        {
            base.Initialize();
        }
    }
    public partial class SoundManager : Singleton<SoundManager>
    {
        public float bgmVolume { get; private set; }
        public float effVolume { get; private set; }
        AudioSource myBGM
        {
            get
            {
                if (_bgm == null)
                {
                    _bgm = GetComponent<AudioSource>();
                }
                return _bgm;
            }
        }
        // Start is called before the first frame update
        void Start()
        {
            bgmVolume = 1.0f - PlayerPrefs.GetFloat("BGM_VOLUME");
            effVolume = 1.0f - PlayerPrefs.GetFloat("EFF_VOLUME");
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void UpdateBGMVolume(float v)
        {
            bgmVolume = Mathf.Clamp(v, 0.0f, 1.0f);
            // PlayerPrefs : 간단한 데이터들을 레지스트리에 저장하고 불러올 수 있음, 쉽게 수정할 수 있어서 보안에 취약
            PlayerPrefs.SetFloat("BGM_VOLUME", 1.0f - bgmVolume);
        }

        public void UpdateEffVolume(float v)
        {
            effVolume = Mathf.Clamp(v, 0.0f, 1.0f);
            PlayerPrefs.SetFloat("EFFECT_VOLUME", 1.0f - effVolume);
        }

        public void PlayOneShot(AudioSource audio)
        {
            audio.volume = effVolume;
            audio.Play();
        }
    }
}
