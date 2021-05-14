using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KSW
{
    public class PanelFade : UIBase
    {
        Image m_fadeImage;

        protected override void Start()
        {
            base.Start();

            m_fadeImage = transform.Find("Image").GetComponent<Image>();
            m_fadeImage.enabled = false;
            m_fadeImage.color = new Color(0f, 0f, 0f, 1f);
        }

        private void OnEnable()
        {
            GameEvent.instance.EventFadeIn += OnEventFadeIn;
            GameEvent.instance.EventFadeOut += OnEventFadeOut;
        }

        private void OnDisable()
        {
            GameEvent.instance.EventFadeIn -= OnEventFadeIn;
            GameEvent.instance.EventFadeOut -= OnEventFadeOut;
        }

        void OnEventFadeIn(float time)
        {
            m_fadeImage.enabled = true;
            StartCoroutine(CoroutineFade(time, 0f));
        }

        void OnEventFadeOut(float time)
        {
            StartCoroutine(CoroutineFade(time, 1f));
        }

        IEnumerator CoroutineFade(float time, float targetValue)
        {
            float currentTime = 0f;
            float alpha = m_fadeImage.color.a;
            Color color = m_fadeImage.color;

            bool isIn = targetValue > 0.1f ? false : true;

            while (currentTime < time)
            {
                currentTime += Time.deltaTime;
                alpha = isIn ? (1f - currentTime / time) : (currentTime / time);

                color = m_fadeImage.color;
                color.a = alpha;
                m_fadeImage.color = color;

                yield return null;
            }

            color.a = targetValue;
            m_fadeImage.color = color;
        }
    }
}