using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapIcon : ImageProperty
{
    public Transform myTarget;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (myTarget != null)
        {
            Vector3 viewPos = Camera.allCameras[1].WorldToViewportPoint(myTarget.position);
            viewPos.z = 0.0f;
            //πÃ¥œ∏  ≈©±‚∞° 150.0¿Ã∂Ûº≠ ∞ˆ«ÿ¡‹
            viewPos *= 150.0f;
            transform.position = viewPos;
            (transform as RectTransform).anchoredPosition = viewPos;
        }
    }

    public void SetColor(Color color)
    {
        myImage.color = color;
    }
}
