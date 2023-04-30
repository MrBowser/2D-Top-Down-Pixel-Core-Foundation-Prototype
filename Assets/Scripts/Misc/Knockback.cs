using System.Collections;
using System.Collections.Generic;
//using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    [SerializeField] private float knockBackTime = .2f;

    public bool GettingKnockedBack { get; private set; }
    private Rigidbody2D rb;




    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void GetKnockBack(Transform damageSource, float knockBackThrust)
    {
        GettingKnockedBack = true;
        Vector2 difference = (transform.position - damageSource.position).normalized * knockBackThrust * rb.mass;
        
        rb.AddForce(difference, ForceMode2D.Impulse);
        StartCoroutine(KnockRoutine());
    }

    private IEnumerator KnockRoutine()
    {
        yield return new WaitForSeconds(knockBackTime);
        rb.velocity = Vector2.zero;
        GettingKnockedBack = false;
    }
}
