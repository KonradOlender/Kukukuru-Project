using UnityEngine;
using System.Collections.Generic;

public class Hearts : MonoBehaviour
{
    public float frameRate;
    public List<Sprite> frames;


    public IEnumerator startAnimation()


    {
        
        foreach(Sprite frame in Frames)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = frame;
            yield return new WaitForSeconds(frameRate);
        }
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        {

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
