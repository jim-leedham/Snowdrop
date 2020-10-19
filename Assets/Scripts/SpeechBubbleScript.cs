using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Snowdrop
{
    public class SpeechBubbleScript : MonoBehaviour
    {
        #region EDITOR_SIDE

        [SerializeField]
        [Range(10, 30)]
        private int charactersPerSecond = 0;

        [SerializeField]
        [Range(0.5f, 2.0f)]
        private float fadeTime = 0;

        [SerializeField]
        private Text textbox = null;

        #endregion

        private float elapsed = 0;
        private string toDisplay = "";

        private void Update()
        {
            elapsed += Time.deltaTime;

            if (textbox.text == toDisplay)
            {
                if (elapsed > fadeTime)
                {
                    gameObject.SetActive(false);
                }
                return;
            }

            if (toDisplay.Length < textbox.text.Length)
            {
                textbox.text = "";
            }
                
            if (toDisplay.Length > textbox.text.Length)
            {
                float characterDuration = 1.0f / charactersPerSecond;
                if (elapsed > characterDuration)
                {
                    elapsed %= characterDuration;
                    textbox.text = toDisplay.Substring(0, textbox.text.Length + 1);
                }
            }
        }

        public void Display(string newText)
        {
            elapsed = 0;
            textbox.text = "";
            toDisplay = newText;
            gameObject.SetActive(true);
        }
    }
}