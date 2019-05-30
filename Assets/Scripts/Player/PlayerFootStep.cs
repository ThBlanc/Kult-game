using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerActions))]
[RequireComponent(typeof(AudioSource))]
public class PlayerFootStep : MonoBehaviour
{
    PlayerActions playerActions;
    AudioSource audioFootStep;

    void Awake()
    {
        playerActions = GetComponent<PlayerActions>();
        audioFootStep = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (playerActions.isMoving && !audioFootStep.isPlaying)
            audioFootStep.Play();
        else if (!playerActions.isMoving && audioFootStep.isPlaying)
            audioFootStep.Stop();
    }
}
