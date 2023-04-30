using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash : MonoBehaviour
{
    [SerializeField] private Material whiteFlashMat;
    [SerializeField] private float restoreDefaultMatTime = .2f;

    public float GetRestoreDefaultMatTime() { return restoreDefaultMatTime; }

    private Material defaultMat;
    private SpriteRenderer spriteRenderer;


    private void Awake()
    {
        spriteRenderer= GetComponent<SpriteRenderer>();
        defaultMat = spriteRenderer.material;
    }


    public IEnumerator FlashRoutine()
    {
        //this changes the sprite to white for restoredefaultmatTime then goes back to default
        spriteRenderer.material = whiteFlashMat;
        yield return new WaitForSeconds(restoreDefaultMatTime);
        spriteRenderer.material = defaultMat;
    }
}
