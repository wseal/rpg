using UnityEngine;

public class playerAnimationEvent : MonoBehaviour
{

  private Player player;

  void Awake()
  {
    player = GetComponentInParent<Player>();
  }

  public void DamageEnemies() => player.DamageEnemies();

  void AttackStarted()
  {
    Debug.Log("attack start...");
    player.EnableJumpAndMovement(false);
  }

  void AttackEnded()
  {
    Debug.Log("attack end...");
    player.EnableJumpAndMovement(true);
  }
}