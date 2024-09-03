using System;
using UnityEngine;

public class LoseTrigger : MonoBehaviour
{
    public Action OnBallEnter;

    private void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.CompareTag(TagStorage.BallTag))
            return;
        
        OnBallEnter?.Invoke();
    }
}
