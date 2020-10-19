using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Snowdrop
{
    public class Speech : Singleton<Speech>
    {
        [SerializeField] private float speechVisibleTime = 0;
        private float speechVisibleCountdown;

        private SpeechBubble speechBubble;


        [SerializeField] private bool subtitled = false;

        [SerializeField] private GameObject subtitleCanvas = null;
        [SerializeField] private Text text = null;

        [SerializeField] private float characterRevealTime = 0.3f;
        private float characterRevealCountdown = 0.0f;
        private int numCharactersRevealed = 0;
        private string textToReveal = "";

        [SerializeField] private float disabledTime = 3.0f;
        private float disableCountdown;

        protected override void Awake()
        {
            base.Awake();

            if(subtitled)
            {
                subtitleCanvas.SetActive(true);
            }
        }

        public void SetSpeechBubble(SpeechBubble speechBubble)
        {
            this.speechBubble = speechBubble;
        }

        public void Emit(string speech)
        {
            if (subtitled)
            {
                characterRevealCountdown = characterRevealTime;
                numCharactersRevealed = 0;
                textToReveal = speech;
            }
            else
            {
                speechBubble.Enable(speech);
                speechVisibleCountdown = speechVisibleTime;
            }
        }

        public void Disable()
        {
            if(subtitled)
            {
                text.text = "";
            }
            else
            {
                speechBubble.Disable();
                speechVisibleCountdown = 0.0f;
            }
        }

        void Update()
        {
            if (subtitled)
            {
                if (textToReveal != "")
                {
                    characterRevealCountdown -= Time.deltaTime;
                    if (characterRevealCountdown < 0.0f)
                    {
                        numCharactersRevealed += 1;
                        string textToRender = textToReveal.Substring(0, numCharactersRevealed);
                        text.text = textToRender;
                        if (numCharactersRevealed < textToReveal.Length)
                        {
                            characterRevealCountdown = characterRevealTime;
                        }
                        else
                        {
                            textToReveal = "";
                            disableCountdown = disabledTime;
                        }
                    }
                }
                else if (disableCountdown > 0.0f)
                {
                    disableCountdown -= Time.deltaTime;
                    if (disableCountdown < 0.0f)
                    {
                        text.text = "";
                    }
                }
            }
            else
            {
                if (speechVisibleCountdown > 0.0f)
                {
                    speechVisibleCountdown -= Time.deltaTime;
                    if (speechVisibleCountdown < 0.0f)
                    {
                        speechBubble.Disable();
                    }
                }
            }
        }
    }
}