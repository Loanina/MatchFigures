using UnityEngine;

namespace Core.Input
{
    public class ClickDetector : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera;

        private void Awake()
        {
            if (mainCamera == null)
                mainCamera = Camera.main;
        }

        private void Update()
        {
#if UNITY_EDITOR
            if (UnityEngine.Input.GetMouseButtonDown(0))
            {
                Vector2 mousePos = mainCamera.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
                ProcessClick(mousePos);
            }
#else
       if (UnityEngine.Input.touchCount > 0 && UnityEngine.Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Vector2 touchPos = mainCamera.ScreenToWorldPoint(UnityEngine.Input.GetTouch(0).position);
            ProcessClick(touchPos);
        }
#endif
        }

        private void ProcessClick(Vector2 worldPos)
        {
            var hit = Physics2D.Raycast(worldPos, Vector2.zero);
            if (hit.collider != null)
            {
                var clickable = hit.collider.GetComponentInParent<IClickable>();
                if (clickable != null) clickable.OnClick();
            }
        }
    }
}