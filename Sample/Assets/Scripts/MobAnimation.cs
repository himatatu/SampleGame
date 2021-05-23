using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable()
    {
        transform.position = new Vector3(-30, 0, 0);
        transform.DOLocalJump(new Vector3(), 30,  5, 2);
    }
}
