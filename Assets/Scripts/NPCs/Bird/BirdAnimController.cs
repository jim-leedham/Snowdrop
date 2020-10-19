using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdAnimController : MonoBehaviour
{
    public void OnFlownAway()
    {
        transform.parent.gameObject.GetComponent<Bird>().OnFlownAway();
    }
}
