using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[RequireComponent(typeof(Camera))]
public class VideoSettingsManager : MonoBehaviour
{
    [Header("Override Settings")]
    public int vsync = 0;
    public int antiAliasing = 0;
    [Header("Camera Settings")]
    public RenderingPath renderingPath;
    [Header("Post Processing")]
    public bool enablePP = false;
    void Awake()
    {
        Application.targetFrameRate = int.MaxValue;
        QualitySettings.vSyncCount = vsync;
        QualitySettings.antiAliasing = antiAliasing;

        Camera cam = GetComponent<Camera>();
        cam.renderingPath = renderingPath;

        if (enablePP) {
            var pp = GetComponent<PostProcessVolume>();
            pp.enabled = true;
            cam.renderingPath = RenderingPath.DeferredLighting;
        }
    }
}
