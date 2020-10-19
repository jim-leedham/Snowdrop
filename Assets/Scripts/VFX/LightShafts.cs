using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightShafts : MonoBehaviour
{
    [SerializeField] private List<SpriteData> sprites = new List<SpriteData>();

    [System.Serializable]
    private class SpriteData
    {
        public SpriteRenderer renderer = null;

        public float minXScale = 0, maxXScale = 0;
        public float startXScale = 0, endXScale = 0;

        public float minOpacity = 0, maxOpacity = 0;
        public float startOpacity = 0, endOpacity = 0;
    }

    public float countdown = 0;
    public float count = 0;

    // Start is called before the first frame update
    void Start()
    {
        foreach(SpriteData sprite in sprites)
        {
            sprite.startXScale = sprite.renderer.transform.localScale.x;
            sprite.endXScale = Random.Range(sprite.minXScale, sprite.maxXScale);

            sprite.startOpacity = sprite.renderer.color.a;
            sprite.endOpacity = Random.Range(sprite.minOpacity, sprite.maxOpacity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (SpriteData sprite in sprites)
        {
            Vector3 scale = sprite.renderer.transform.localScale;
            scale.x = Mathf.Lerp(sprite.startXScale, sprite.endXScale, ((count - countdown) / count));
            sprite.renderer.transform.localScale = scale;

            Color color = sprite.renderer.color;
            color.a = Mathf.Lerp(sprite.startOpacity, sprite.endOpacity, ((count - countdown) / count));
            sprite.renderer.color = color;
        }

        countdown -= Time.deltaTime;
        if (countdown < 0.0f)
        {
            foreach (SpriteData sprite in sprites)
            {
                sprite.startXScale = sprite.endXScale;
                sprite.endXScale = Random.Range(sprite.minXScale, sprite.maxXScale);


                sprite.startOpacity = sprite.endOpacity;
                sprite.endOpacity = Random.Range(sprite.minOpacity, sprite.maxOpacity);
            }
            countdown = count;
        }
    }
}
