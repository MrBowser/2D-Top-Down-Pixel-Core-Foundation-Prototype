using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapeProjectile : MonoBehaviour
{
    [SerializeField] private float duration = 1f;
    //note animCurve setup in unity gui
    [SerializeField] private AnimationCurve animCurve;
    [SerializeField] private float heightY = 3f;
    [SerializeField] private GameObject grapeProjectileShadow;
    [SerializeField] private GameObject splatterPrefab;

    private void Start()
    {
        //note the -.3f in the vector 3 y has the shadow instantiate below the grape not directly on it
        GameObject grapeShadow = Instantiate(grapeProjectileShadow, transform.position + new Vector3(0,-.3f,0),Quaternion.identity);

        Vector3 playerPos = PlayerController.Instance.transform.position;
        Vector3 grapeShadowStartPosition = grapeShadow.transform.position;

        StartCoroutine(ProjectileCurveRoutine(transform.position, playerPos));
        StartCoroutine(MoveGrapeShadowRoutine(grapeShadow,grapeShadowStartPosition,playerPos));
    }

    private IEnumerator ProjectileCurveRoutine(Vector3 startPosition, Vector3 endPosition)
    {
        float timePassed = 0f;

        while (timePassed< duration)
        {
            timePassed+= Time.deltaTime;

            float linearT = timePassed/ duration;
            float heightT = animCurve.Evaluate(linearT);
            float height = Mathf.Lerp(0f, heightY, heightT);

            //below gets us from the spawn point to the player with location of grape changing over time (straight line is the first vector 2
            //the second added vector 2 gets us the curve we are looking for
            transform.position = Vector2.Lerp(startPosition, endPosition, linearT) + new Vector2(0f,height);
            yield return null;

        }
        Instantiate(splatterPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);


    }

    private IEnumerator MoveGrapeShadowRoutine(GameObject grapeShadow, Vector3 startPosition, Vector3 endPosition)
    {

        float timePassed = 0f;

        while (timePassed < duration)
        {
            timePassed += Time.deltaTime;

            float linearT = timePassed / duration;
            
            //below gets us from the spawn point to the player with location of grape shadow changing over time (
            grapeShadow.transform.position = Vector2.Lerp(startPosition, endPosition, linearT);

            yield return null;
        }

        Destroy(grapeShadow);
       
    }
}
