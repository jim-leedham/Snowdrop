using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Snowdrop
{
    public class SpeechBubble : MonoBehaviour
    {
        private SpriteRenderer bubbleSpriteRenderer;
        private Text text;
        [SerializeField] private GameObject speechAnimation = null;


        [SerializeField] private float characterRevealTime = 0.3f;
        private float characterRevealCountdown = 0.0f;
        private int numCharactersRevealed = 0;
        private string textToReveal = "";

        void Awake()
        {
            bubbleSpriteRenderer = GetComponent<SpriteRenderer>();
            text = GetComponentInChildren<Text>();
        }

        private void Update()
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
                        Game.Instance.Player.FinishSpeaking();
                    }
                }
            }
        }

        public void Enable(string newText)
        {
            //bubbleSpriteRenderer.enabled = true;
            //text.text = newText;

            characterRevealCountdown = characterRevealTime;
            numCharactersRevealed = 0;
            textToReveal = newText;
            Game.Instance.Player.BeginSpeaking();

            speechAnimation.SetActive(true);
            speechAnimation.GetComponent<Animator>().SetTrigger("enabled");
        }

        public void Disable()
        {
            //bubbleSpriteRenderer.enabled = false;
            text.text = "";

            textToReveal = "";
            Game.Instance.Player.FinishSpeaking();
            speechAnimation.GetComponent<Animator>().SetTrigger("disabled");
            speechAnimation.SetActive(false);
        }
    }
}