using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicPostProcessingHandler : MonoBehaviour
{
    [SerializeField] private PostProcessingEffect _postProcessingEffect;
    [SerializeField] private string input = "DynamicShader";
    [SerializeField] private List<string> keysListOnEnable = new List<string>();
    [SerializeField] private List<float> valuesListOnEnable = new List<float>();
    [SerializeField] private List<string> keysListOnDisable = new List<string>();
    [SerializeField] private List<float> valuesListOnDisable = new List<float>();

    private void Start()
    {
        for (int i = 0; i < keysListOnEnable.Count; i++)
        {
            _postProcessingEffect.m_ScriptablePass.material.SetFloat(keysListOnDisable[i], valuesListOnDisable[i]);
        }
    }
    void Update()
    {
        if (Input.GetAxis(input) > 0)
        {
            for (int i = 0; i < keysListOnEnable.Count; i++)
            {
                _postProcessingEffect.m_ScriptablePass.material.SetFloat(keysListOnEnable[i], valuesListOnEnable[i]);
            }
        }

        if (Input.GetAxis(input) < 0)
        {
            for (int i = 0; i < keysListOnDisable.Count; i++)
            {
                _postProcessingEffect.m_ScriptablePass.material.SetFloat(keysListOnDisable[i], valuesListOnDisable[i]);
            }
        }
    }
}
