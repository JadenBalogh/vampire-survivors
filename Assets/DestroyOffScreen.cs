using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOffScreen : MonoBehaviour
{
    private void OnBecomeInvisible()
    {
        Destroy(gameObject);
    }
}
