using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Mottel {
  /// <summary>
  /// Tracks the type of enemy this is.
  /// </summary>
  public enum objectType { Enemy1, Enemy2, Boss }
  /// <summary>
  /// Tracks the current game mode.
  /// </summary>
  public enum GameMode { Menu, Normal, BulletHell };
  /// <summary>
  /// Set the x/y min/max in an easy object.
  /// </summary>
  [System.Serializable]
  public class Boundry {
  public float xMin, xMax, yMin, yMax;
  }
  /// <summary>
  /// Used in MoveByRB2D to determine movement type.
  /// </summary>
  public enum MovementType { Up, Down, Centipede, Boss, LeftBullet, RightBullet };
}
