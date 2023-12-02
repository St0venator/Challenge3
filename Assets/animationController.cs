using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationController : MonoBehaviour
{
    Animator anim;
    bool isParachute;

    public float paraTimeMultiplier = 1.0f;
    public AnimationCurve parachuteCurve;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        isParachute = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StopAllCoroutines();    
            if (!isParachute)
            {
                StartCoroutine(flyGlideTransition(0.0f, 0.5f, 2));
            }
            else
            {
                StartCoroutine(flyGlideTransition(0.5f, 0.0f, 2));
            }
            isParachute = !isParachute;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            StopAllCoroutines();
            StartCoroutine(flyGlideTransition(1.0f, 0.0f, 1));
        }
    }

    IEnumerator flyGlideTransition(float start, float end, int layerNum)
    {
        float i = 0;
        float animWeight;

        while (i < 1)
        {
            i += Time.deltaTime * paraTimeMultiplier;

            animWeight = Mathf.Lerp(start, end, parachuteCurve.Evaluate(i));

            anim.SetLayerWeight(layerNum, animWeight);

            if(i >= 0.9f)
            {
                anim.SetLayerWeight(1, 0.0f);
            }
            yield return null;
        }
    }

    IEnumerator punchTimer()
    {
        anim.SetLayerWeight(1, 1);

        yield return new WaitForSeconds(2);

        anim.SetLayerWeight(1, 0);
    }
}
