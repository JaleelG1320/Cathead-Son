using System.Collections;
using UnityEngine;

public class PostProcessingManager : MonoBehaviour
{

    public Shader _grayScaleShader;
    public Shader _circleWipeShader;


    [Header("Grayscale Shader")]
    private Material _grayScaleMaterial;
    
    
    

    [Header("Circle Wipe Shader")]
    [Range(0,1.2f)]
    public float _circleWipeRadius;
    private float _radiusSpeed;
    public float _duration = 2f;
    private Material _circleWipeMaterial;

    public static PostProcessingManager instance;


    // Start is called before the first frame update
    void Start()
    {
        _grayScaleMaterial = new Material(_grayScaleShader);
        _circleWipeMaterial = new Material(_circleWipeShader);

        _circleWipeRadius = 1.2f;
        UpdateShader();
    }

    // Update is called once per frame
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {   
        Graphics.Blit(source, destination, _circleWipeMaterial);
        
    }

    public void SwitchShader(RenderTexture source, RenderTexture destination)
    {
        if (GameManager.instance.bnwActive == true)
        {
            Graphics.Blit(source, destination, _grayScaleMaterial);
        }
        else
        {
            Graphics.Blit(source, destination, _circleWipeMaterial);
        }
    }

    public void Update()
    {
        UpdateShader();
    }

    public void FadeCircleOut()
    {
        StopCoroutine(FadeCircleWipe(2f, 0f, 1.2f));
        StartCoroutine(FadeCircleWipe(2f, 1.2f, 0f));
    }

    public void FadeCircleIn()
    {
        StopCoroutine(FadeCircleWipe(2f, 1.2f, 0f));
        StartCoroutine(FadeCircleWipe(2f, 0f, 1.2f));
    }

    private IEnumerator FadeCircleWipe(float duration, float start, float finish)
    {
        Debug.Log("Working");
        _circleWipeRadius = start;
        UpdateShader();

        var time = 0f;
        while (time <= duration)
        {
            time += Time.deltaTime;
            //Debug.Log(_circleWipeRadius);
            var t = time / duration;
            _circleWipeRadius = Mathf.Lerp(start, finish, t);
            
            UpdateShader();
            yield return null;
        }

        _circleWipeRadius = finish;
        UpdateShader();
    }

    public void UpdateShader()
    {
        _circleWipeMaterial.SetFloat("_Radius", _circleWipeRadius);

    }

    public void CompositeTextures(RenderTexture tex1, RenderTexture tex2, RenderTexture output) 
    {

        // Create/reuse a temporary RenderTexture as an intermediate
        // (this can be cheaper than reserving memory for it throughout).
        var temp = RenderTexture.GetTemporary(tex1.width, tex1.height, 0, tex1.format);

        // Perform the first shader operation, modifying tex1
        // and storing the result in a temporary buffer.
        Graphics.Blit(tex1, temp, _grayScaleMaterial, -1);

        // Assign tex2 as an auxiliary texture 
        // to be sampled by our second shader operation.
        _circleWipeMaterial.SetTexture("_Tex2", tex2);

        // Perform the compositing step to populate the output, 
        // with the modified tex1 piped through as _MainTex
        // and tex2 piped to a sampler called _Tex2.
        Graphics.Blit(temp, output, _circleWipeMaterial, -1);

        // Recycle the temporary render target we used.
        RenderTexture.ReleaseTemporary(temp);
    }
}
