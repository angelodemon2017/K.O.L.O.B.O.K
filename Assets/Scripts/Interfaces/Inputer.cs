using UnityEngine;

public interface Inputer
{
    void TransAction(bool? space, bool? force);
    void HandleMoving(Vector2 vector2);
    void HandleMouseLook(Vector2 vector2);
    void SetKinematic(bool isTrue);
}