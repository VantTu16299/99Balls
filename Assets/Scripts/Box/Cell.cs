using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public float w = 40;
    public float h = 40;
    // Start is called before the first frame update
    void Start()
    {
        RectTransform rt = this.gameObject.GetComponent<RectTransform>();
        if (rt != null)
        {
            rt.sizeDelta = new Vector2(w, h);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
