using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public List<GameObject> enemies;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(IsEmpty())
        {
            Destroy(this.gameObject);
        }
    }

    public bool IsEmpty()
    {
        foreach(var enemy in enemies)
        {
            if(enemy != null)
            {
                return false;
            }
        }
        return true;
    }
}
