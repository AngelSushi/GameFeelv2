using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemscore : MonoBehaviour
{
    Vector3 startPos;
    public float duration;
    public AnimationCurve anim;
    float currentTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        float t = anim.Evaluate(currentTime / duration);
        Vector3 position = Vector3.Lerp(startPos, PlayerManager.Instance.transform.position, t);
        transform.position = position;
        if(currentTime >= duration)
        {
            Destroy(gameObject);
        }
    }
}
