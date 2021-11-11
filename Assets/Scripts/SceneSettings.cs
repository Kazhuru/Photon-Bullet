using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSettings : MonoBehaviour
{
    [SerializeField] AudioClip sceneAudioClip;
    public AudioClip GetSceneAudioClip() { return sceneAudioClip; }
}
