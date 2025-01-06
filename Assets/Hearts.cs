using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

public class Hearts : MonoBehaviour
{
    public float frameRate;
    public List<Sprite> frames;


    public IEnumerator startAnimation()
    {
        foreach(Sprite frame in frames)
        {
            this.gameObject.GetComponent<Image>().sprite = frame;
            yield return new WaitForSeconds(frameRate);
        }
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TriggerAnim()
    {
        StartCoroutine(startAnimation());
    }

    public void ResetSprites()
    {
        this.gameObject.GetComponent<Image>().sprite = frames[0];
    }
}
