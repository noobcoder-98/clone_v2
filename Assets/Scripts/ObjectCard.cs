using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectCard : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public GameObject objectDrag;
    public GameObject objectGame;
    public Canvas canvas;

    private GameObject objectDragInstance;
    private GameManager gameManager;
    public void OnDrag(PointerEventData eventData)
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = Camera.main.transform.position.z + Camera.main.nearClipPlane;
        objectDragInstance.transform.position = mousePosition;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        objectDragInstance = Instantiate(objectDrag, canvas.transform);
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        objectDragInstance.transform.position = mousePosition;
        objectDragInstance.GetComponent<ObjectDragging>().card = this;

        gameManager.draggingObject = objectDragInstance;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        gameManager.PlaceObject();
        gameManager.draggingObject = null;
        Destroy(objectDragInstance);
    }

    private void Start()
    {
        gameManager = GameManager.instance;
    }
}
