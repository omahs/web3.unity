using UnityEngine;

public class HueAnimation : MonoBehaviour
{
    public Camera Camera;
    public float Speed = 1f;
    [Range(0f, 1f)] public float Value; 

    private void Update()
    {
        Value = Mathf.Repeat(Value + Time.deltaTime * Speed, 1f);
        var color = Color.HSVToRGB(Value, 1f, 1f);
        Camera.backgroundColor = color;
    }
}
