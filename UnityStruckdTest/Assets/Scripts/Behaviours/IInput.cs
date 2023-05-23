using UnityEngine;

namespace UnityStruckdTest.Behaviours
{
    public interface IInput
    {
        Vector2 GetLastInputAt(int index);
        Vector2 GetCurrentInputAt(int index);
        Vector3 GetLastInputWorld(int index);
        Vector3 GetCurrentInputWorld(int index);
        TouchPhase GetCurrentInputState(int index);
        GameObject GetPressedGameObject();
        int InputCount { get; }
        Vector2 DefaultInput { get; }
        int GetPointerFingerID { get; }
    }
}