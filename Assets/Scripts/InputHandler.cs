using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour {
  
  enum TouchStatus {
    notTouching,
    drawing,
    dragging
  }

  private GameObject character;
  private GameObject level;
  private Character characterScript;
  private Level levelScript;

  private Vector2 draggingOffset = Vector2.zero;
  // TODO: Write a class that contains path line and path nodes

  private TouchStatus checkTouchStatus(Vector2 fingerPosition) {
    if (characterScript.pathEndCircle && 
          Vector2.Distance(fingerPosition, GameUtilities.WorldToGameUnit(characterScript.pathEndCircle.transform.position))
          <= Drawing.circleRadius) {
      return TouchStatus.dragging;
    }
    return TouchStatus.drawing;
  }

  void Start() {
    character = GameObject.FindWithTag("Character");
    characterScript = character.GetComponent<Character>();
    level = GameObject.FindWithTag("Level");
    levelScript = level.GetComponent<Level>();
  }

  void Update() {
    Vector2 fingerPosition = 
      GameUtilities.WorldToGameUnit(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    Vector2 characterPosition =
      GameUtilities.WorldToGameUnit(character.transform.position);
    if (Input.GetMouseButtonDown(0)) {
      if (checkTouchStatus(fingerPosition) == TouchStatus.drawing) {
        if (characterScript.pathLine) {
          Destroy(characterScript.pathLine);
          Destroy(characterScript.pathEndCircle);
          characterScript.pathNodes.Clear();
        }
        characterScript.pathLine = Drawing.CreateLine(characterPosition, fingerPosition);
        characterScript.pathNodes = new List<Vector2>(){characterPosition, fingerPosition};
        characterScript.pathEndCircle = Drawing.CreateCircle(fingerPosition);
      } else if (checkTouchStatus(fingerPosition) == TouchStatus.dragging) {
        draggingOffset = new Vector2(GameUtilities.WorldToGameUnit(characterScript.pathEndCircle.transform.position.x), 
                                     GameUtilities.WorldToGameUnit(characterScript.pathEndCircle.transform.position.y)) 
                           - fingerPosition;
      }
    } 
    if (Input.GetMouseButton(0)) {
      if (checkTouchStatus(fingerPosition) == TouchStatus.drawing) {
        if (Vector2.Distance(fingerPosition,
              GameUtilities.WorldToGameUnit(characterScript.pathEndCircle.transform.position)) >= 1.0f) {
          characterScript.pathNodes.Add(fingerPosition);
          characterScript.pathEndCircle.transform.position = 
            new Vector3(GameUtilities.GameToWorldUnit(fingerPosition.x),
                        GameUtilities.GameToWorldUnit(fingerPosition.y),
                        -1.0f);
          Drawing.AddOneNodeToLine(characterScript.pathLine, fingerPosition);
        }
      } else {  // dragging
        if (Vector2.Distance(fingerPosition,
              GameUtilities.WorldToGameUnit(characterScript.pathEndCircle.transform.position)) >= 1.0f) {
          characterScript.pathNodes.Add(fingerPosition + draggingOffset);
          characterScript.pathEndCircle.transform.position = 
            new Vector3(GameUtilities.GameToWorldUnit(fingerPosition.x + draggingOffset.x),
                        GameUtilities.GameToWorldUnit(fingerPosition.y + draggingOffset.y),
                        -1.0f);
          Drawing.AddOneNodeToLine(characterScript.pathLine, fingerPosition + draggingOffset);
        }
      }
    }
  }
}
