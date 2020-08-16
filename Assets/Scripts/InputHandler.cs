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
  private Character characterScript;

  private Vector2 draggingOffset = Vector2.zero;
  // TODO: Write a class that contains path line and path nodes

  private TouchStatus checkTouchStatus(Vector2 fingerPosition) {
    if (characterScript.pathEndCircle && 
        Vector2.Distance(fingerPosition, characterScript.pathEndCircle.transform.position) 
          <= Drawing.circleRadius) {
      return TouchStatus.dragging;
    }
    return TouchStatus.drawing;
  }

  void Start() {
    character = GameObject.FindWithTag("Character");
    characterScript = character.GetComponent<Character>();
  }

  void Update() {
    Vector2 fingerPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    if (Input.GetMouseButtonDown(0)) {
      if (checkTouchStatus(fingerPosition) == TouchStatus.drawing) {
        if (characterScript.pathLine) {
          Destroy(characterScript.pathLine);
          Destroy(characterScript.pathEndCircle);
          characterScript.pathNodes.Clear();
        }
        // drawingLine = Drawing.CreateLine(character.transform.position, fingerPosition);
        characterScript.pathLine = Drawing.CreateLine(character.transform.position, fingerPosition);
        // drawingNodes = new List<Vector2>(){character.transform.position, fingerPosition};
        characterScript.pathNodes = new List<Vector2>(){character.transform.position, 
                                                        fingerPosition};
        // drawingEndCircle = Drawing.CreateCircle(fingerPosition);
        characterScript.pathEndCircle = Drawing.CreateCircle(fingerPosition);
      } else if (checkTouchStatus(fingerPosition) == TouchStatus.dragging) {
        draggingOffset = new Vector2(characterScript.pathEndCircle.transform.position.x, 
                                     characterScript.pathEndCircle.transform.position.y) 
                         - fingerPosition;
      }
    }
    if (Input.GetMouseButton(0)) {
      if (checkTouchStatus(fingerPosition) == TouchStatus.drawing) {
        if (Vector2.Distance(fingerPosition, characterScript.pathEndCircle.transform.position) 
            > 0.1f) {
          characterScript.pathNodes.Add(fingerPosition);
          characterScript.pathEndCircle.transform.position = new Vector3(fingerPosition.x,
                                                                         fingerPosition.y,
                                                                         -1.0f);
          Drawing.AddOneNodeToLine(characterScript.pathLine, fingerPosition);
        }
      } else {  // dragging
        if (Vector2.Distance(fingerPosition, characterScript.pathEndCircle.transform.position) 
            > 0.1f) {
          characterScript.pathNodes.Add(fingerPosition + draggingOffset);
          characterScript.pathEndCircle.transform.position = 
            new Vector3(fingerPosition.x + draggingOffset.x,
                        fingerPosition.y + draggingOffset.y,
                         -1.0f);
          Drawing.AddOneNodeToLine(characterScript.pathLine, fingerPosition + draggingOffset);
        }
      }
    }
  }
}
