using UnityEngine;
using UnityEngine.EventSystems;

public class Shield : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public PlayerController playerController;

    private float _pressedTime;
    private bool _isPressed;
    
    private void Update()
    {
        if (_isPressed)
        {
            _pressedTime += Time.deltaTime;
            playerController.shieldIsActive = true;

            if (_pressedTime > 2)
                playerController.shieldIsActive = false;
        }
        else
        {
            _pressedTime = 0;
            playerController.shieldIsActive = false;
        }
            
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isPressed = false;
    }
}
