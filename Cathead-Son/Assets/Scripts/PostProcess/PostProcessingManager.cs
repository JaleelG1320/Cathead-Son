using System.Collections;
using UnityEngine;

public class PostProcessingManager : MonoBehaviour
{
    [Header("Shader List")]
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

    private void UpdateShader()
    {
        _circleWipeMaterial.SetFloat("_Radius", _circleWipeRadius);

    }
}
