using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicPostProcessingHandler : MonoBehaviour
{
    [SerializeField] private PostProcessingShader postProcessingEffect = null; //Only non-repeatable part is that this code needs a direct reference to a shader/material handler to fetch the material reference
    [SerializeField] private string input = "DynamicShader"; //What input to listen to. Positive value for this input is enable, negative is disable
    [SerializeField] private List<string> keysListOnEnable = new List<string>(); //Names of float properties to change on enable. The values for this list can be looked up in the shader code (PostProcessingShader.shader).
    [SerializeField] private List<float> valuesListOnEnable = new List<float>(); //Values for ^. Must be of equal length as ^
    [SerializeField] private List<string> keysListOnDisable = new List<string>(); //Names of float properties to change on disable
    [SerializeField] private List<float> valuesListOnDisable = new List<float>(); //Values for ^. Must be of equal length as ^
    private Material materialReference = null;

    private void Start()
    {
        if (postProcessingEffect == null)
        {
            return;
        }
        materialReference = postProcessingEffect.customRenderPass.materialReference;
        for (int i = 0; i < keysListOnDisable.Count; i++)
        {
            materialReference.SetFloat(keysListOnDisable[i], valuesListOnDisable[i]);
        }
    }
    void Update()
    {
        if (materialReference == null)
        {
            return;
        }
        if (Input.GetAxis(input) > 0) //Enable dynamic changes
        {
            for (int i = 0; i < keysListOnEnable.Count; i++) //Because of how this for loop is handled, if valuesList.Count < keysList.Count, there will be errors
            {
                materialReference.SetFloat(keysListOnEnable[i], valuesListOnEnable[i]);
            }
        }

        if (Input.GetAxis(input) < 0) //Disable dynamic changes
        {
            for (int i = 0; i < keysListOnDisable.Count; i++)
            {
                materialReference.SetFloat(keysListOnDisable[i], valuesListOnDisable[i]);
            }
        }
    }
}
