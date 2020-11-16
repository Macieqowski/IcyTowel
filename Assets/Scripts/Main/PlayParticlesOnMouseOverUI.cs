using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.Main
{
    public class PlayParticlesOnMouseOverUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public void OnPointerEnter(PointerEventData eventData)
        {
            _particles.Play();
            /*
            var colors = ColorBlock.defaultColorBlock;
            colors.highlightedColor = Color.red;
            _button.colors = colors;*/

        }

        public void OnPointerExit(PointerEventData eventData)
        {
           // _button.colors = ColorBlock.defaultColorBlock;
            _particles.Stop();
        }

        protected void Awake()
        {
            _button = GetComponent<Button>();
        }

        [SerializeField]
        private ParticleSystem _particles;

        private Button _button;
    }
}