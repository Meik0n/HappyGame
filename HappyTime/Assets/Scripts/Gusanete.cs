using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gusanete : MonoBehaviour {

    private bool facingRight = false;
    public float ChaseSpeed;
    public float distanceToAttack = 3f;
    private Transform target;

    [Header("Scale on attack")]
    public float scaleFactorOnAttack = 1;
    protected Vector3 initialScale;
    public float attackScaleTime = 1f;
    public AnimationCurve growAnim = new AnimationCurve(new Keyframe(0, 0), new Keyframe(0.5f, 1), new Keyframe(1, 0));

    protected enum states
    {
        growing, chasing
    }
    protected states currentState = states.chasing;


    void Start ()
    {
        initialScale = transform.localScale;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        StartCoroutine(Chase());
    }

    protected IEnumerator Chase()
    {
        currentState = states.chasing;

        while (currentState == states.chasing)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(target.position.x, target.position.y), ChaseSpeed * Time.deltaTime);

            if (target.position.x > transform.position.x)
            {
                if (!facingRight)
                {
                    Flip();
                }
            }
            else if (target.position.x < transform.position.x)
            {
                if (facingRight)
                {
                    Flip();
                }
            }

            if (Vector2.Distance(transform.position, target.position) < distanceToAttack)
            {
                StartCoroutine(Grow());
            }

            yield return null;
        }
    }

    protected IEnumerator Grow()
    {
        currentState = states.growing;
        var maxScale = initialScale;
        maxScale.x = initialScale.x * scaleFactorOnAttack;

        var accumTime = 0.0f;
        while (accumTime <= 1)
        {
            accumTime += Time.deltaTime / attackScaleTime;

            transform.localScale = Vector3.Lerp(initialScale, maxScale, growAnim.Evaluate(accumTime));

            yield return null;
        }
        transform.localScale = initialScale;
        StartCoroutine(Chase());
    }

    void Flip()
    {
        transform.Rotate(0f, 180f, 0f);
        facingRight = !facingRight;
    }
}
