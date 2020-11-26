using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessingShader : ScriptableRendererFeature //Post processing shader directly applied as a rendering feature in the rendering pipeline.
{
    public class CustomRenderPass : ScriptableRenderPass //Declare our CustomRenderPass
    {
        public RenderTargetIdentifier source; //Source camera
        private RenderTargetHandle tempRenderTargetHandler; //We need a temperal texture to write the source image to, to edit that in the shader, before returning the result to the camera
        public Material material; //Material to use on the post processing shader. Is set to public to allow it to be modified dynamically.
        private string identifier; //String identifier that shows up when debugging through window>analysis>frame debugger

        //Declare CustomRenderPass constructor
        public CustomRenderPass(Material material, string identifier)
        {
            this.material = material;
            this.identifier = identifier;
            tempRenderTargetHandler.Init("_TemporaryTexture"); //Initialize the temporary texture. Unity has a default temporary texture what is used here
        }

        //Rendering logic that gets executed every frame
        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            CommandBuffer commandBuffer = CommandBufferPool.Get(identifier); //Allocate memory for our shader and give our rendering pass the custom indentifier
            commandBuffer.GetTemporaryRT(tempRenderTargetHandler.id, renderingData.cameraData.cameraTargetDescriptor); //Set the temporal render texture with the rendering camera's input
            Blit(commandBuffer, source, tempRenderTargetHandler.Identifier(), material); //Apply the shader to the temporal render texture
            Blit(commandBuffer, tempRenderTargetHandler.Identifier(), source); //Apply the shader result to the camera's output towards the user's screen
            context.ExecuteCommandBuffer(commandBuffer); //Execute all functions shown above
            CommandBufferPool.Release(commandBuffer); //Clear up the buffer's memory
        }
    }

    [System.Serializable]
    public class Settings //Settings that show up in the editor. Are not a part of the CustomRenderPass but is needed to properly initialize the CustomRenderPass instance
    {
        public Material material = null; //Allows us to change what material to apply to this rendering feature
        public string identifier = "CustomRenderPass"; //Allows us to give the rendering feature a custom name
        public RenderPassEvent renderPassEvent = RenderPassEvent.BeforeRenderingPostProcessing; //Allows us to determine in what stage of the rendering process you apply the shader
    }

    public Settings settings = new Settings();

    //Initialize the CustomRenderPass
    public CustomRenderPass customRenderPas;
    public override void Create()
    {
        //Construct the CustomRenderPass
        customRenderPas = new CustomRenderPass(settings.material, settings.identifier);
        //Configures where the render pass should be injected in the rendering process.
        customRenderPas.renderPassEvent = settings.renderPassEvent;
    }

    //Inject the initialized CustomRenderPass into the rendering pipeline
    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        customRenderPas.source = renderer.cameraColorTarget;
        renderer.EnqueuePass(customRenderPas);
    }
}


