using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartFlower : MonoBehaviour
{
    public Sprite[] sprites;
    public float speed;
    private int num = 0;
    private UnityEngine.UI.Image image;
    // Start is called before the first frame update
    private void Awake()
    {
        image = GetComponent<UnityEngine.UI.Image>();
    }

    void Start()
    {
        SpriteAnimation();
    }

    private void SpriteAnimation()
    {
        StartCoroutine(SwitchSprite());
    }

    private IEnumerator SwitchSprite()
    {
        image.sprite = sprites[num];
        yield return new WaitForSeconds(speed);
        if (num < sprites.Length - 1) num++;
        else num = 0;
        SpriteAnimation();
        yield break;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
