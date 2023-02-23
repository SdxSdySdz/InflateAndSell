using UnityEngine;
using Random = UnityEngine.Random;

namespace CodeBase.GameLogic.WorkSpacing
{
    public class Character : MonoBehaviour
    {
        [SerializeField] private Renderer _renderer;
        [Range(0, 1)] 
        [SerializeField] private float _shadeBrightness;

        private void Awake()
        {
            Color color = Random.ColorHSV();
            Color shadedColor = color * _shadeBrightness;
            shadedColor.a = 1;
            
            _renderer.material.SetColor("_Color", color);
            _renderer.material.SetColor("_ColorDim", shadedColor);
        }
    }
}