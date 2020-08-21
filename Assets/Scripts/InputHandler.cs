using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class InputHandler : MonoBehaviour {
  
  enum TouchStatus {
    notTouching,
    drawing,
    dragging
  }

  // private GameObject character;
  private GameObject level;
  private Character character;
  private Level levelScript;

  private Vector2 draggingOffset = Vector2.zero;

  private TouchStatus checkTouchStatus(MapNode fingerPos) {
    if (character.IsMoving()) {
      MapNode circlePos = MapHandler.GetMapNode(character.GetPathEndCirclePos());
      if (fingerPos == circlePos) {
        return TouchStatus.dragging;
      }
    }
    return TouchStatus.drawing;
  }

  void Start() {
    character = GameObject.FindWithTag("Character").GetComponent<Character>();
    level = GameObject.FindWithTag("Level");
    levelScript = level.GetComponent<Level>();
  }

  void Update() {
    MapNode fingerPos = 
      MapHandler.GetMapNode(GameUtil.ToGameUnitInt(
        Camera.main.ScreenToWorldPoint(Input.mousePosition)));
    MapNode characterPos = MapHandler.GetMapNode(character.GetPositionInt());
    Assert.IsNotNull(fingerPos);
    Assert.IsNotNull(characterPos);

    if (Input.GetMouseButtonDown(0)) {
      if (checkTouchStatus(fingerPos) == TouchStatus.drawing) {
        character.ResetPath();
        // Path finding
        List<Vector2> path = MapHandler.FindPath(characterPos, fingerPos);
        Assert.IsNotNull(path);
        character.SetPath(path);
        character.SetEndCircle(fingerPos);
      } else if (checkTouchStatus(fingerPos) == TouchStatus.dragging) {
        // do nothing
      }
    } 
    if (Input.GetMouseButton(0)) {
      character.UpdatePath(fingerPos);
    }
  }
}
