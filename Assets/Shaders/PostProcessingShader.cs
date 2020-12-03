using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessingShader : ScriptableRendererFeature //Post processing shader directly applied as a rendering feature in the rendering pipeline.
{
    public class CustomRenderPass : ScriptableRenderPass
    {
        public RenderTargetIdentifier sourceCamera;
        private RenderTargetHandle tempRenderTargetHandler;
        public Material materialReference;
        private string customIdentifier; // String identifier that shows up when debugging through window>analysis>frame debugger

        public CustomRenderPass(Material material, string identifier)
        {
            this.materialReference = material;
            this.customIdentifier = identifier;
            tempRenderTargetHandler.Init("_TemporaryTexture"); // Initialize the temporary texture. Unity has a default temporary texture what is used here
        }

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            CommandBuffer commandBuffer = CommandBufferPool.Get(customIdentifier);
            commandBuffer.GetTemporaryRT(tempRenderTargetHandler.id, renderingData.cameraData.cameraTargetDescriptor); // Set the temporal render texture with the rendering camera's input
            Blit(commandBuffer, sourceCamera, tempRenderTargetHandler.Identifier(), materialReference); // Apply the shader to the temporal render texture
            Blit(commandBuffer, tempRenderTargetHandler.Identifier(), sourceCamera); // Apply the shader result to the camera's output towards the user's screen
            context.ExecuteCommandBuffer(commandBuffer);
            CommandBufferPool.Release(commandBuffer);
        }
    }

    [System.Serializable]
    public class Settings // Settings that show up in the editor.
    {
        public Material materialReference = null;
        public string customIdentifier = "CustomRenderPass";
        public RenderPassEvent renderPassEvent = RenderPassEvent.BeforeRenderingPostProcessing;
    }

    public Settings settings = new Settings();
    public CustomRenderPass customRenderPass;
    public override void Create()
    {
        customRenderPass = new CustomRenderPass(settings.materialReference, settings.customIdentifier);
        customRenderPass.renderPassEvent = settings.renderPassEvent; // Configures where the render pass should be injected in the rendering process.
    }

    // Inject the initialized CustomRenderPass into the rendering pipeline
    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        customRenderPass.sourceCamera = renderer.cameraColorTarget;
        renderer.EnqueuePass(customRenderPass);
    }
}


