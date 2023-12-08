using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraClearColorController : MonoBehaviour
{
    [SerializeField] private Camera m_Camera;
    [SerializeField] private Color _DayColor;

    void Update()
    {
        UpdateColor();
    }

    private void UpdateColor()
    {
        switch (GameManager.Instance.CurrentGameState)
        {
            case Define.GameState.InGame:
                m_Camera.backgroundColor = Color.Lerp(Color.black, _DayColor, ((TimeController.Instance.CurrentTime.Hour + 3) % 24) / 11f);
                break;
        }
    }
}
