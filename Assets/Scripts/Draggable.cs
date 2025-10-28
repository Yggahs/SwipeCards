using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class Draggable : MonoBehaviour
{
    public Camera myCam;

    private float startXPos;
    private float startYPos;

    private bool isDragging = false;

    [SerializeField]
    public GameManager gameManager;

    bool goLeft = false;
    bool isMoving = false;
    [SerializeField]
    Vector2 targetPosition;
    [SerializeField]
    float moveDistance = 5f;
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        myCam = Camera.main;
    }

    private void Update()
    {
        if (isDragging)
        {
            DragObject();
        }
    }

    private void OnMouseDown()
    {
        Vector2 mousePos = Input.mousePosition;

        mousePos = myCam.ScreenToWorldPoint(mousePos);


        startXPos = mousePos.x - transform.localPosition.x;
        startYPos = mousePos.y - transform.localPosition.y;

        isDragging = true;
    }

    private void OnMouseUp()
    {
        isDragging = false;
    }

    public void DragObject()
    {
        Vector2 mousePos = Input.mousePosition;
        mousePos = myCam.ScreenToWorldPoint(mousePos);
        Vector2 destination = new Vector2(mousePos.x - startXPos, mousePos.y - startYPos);
        transform.localPosition = Vector2.Lerp(transform.localPosition, destination, Time.deltaTime * 5f);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        isDragging = false;
        isMoving = true;
        if (collision.gameObject.name == "CollisionZoneL")
        {
            goLeft = true;
        }
        targetPosition = (Vector2)transform.position + ((goLeft ? -Vector2.right : Vector2.right) * moveDistance );
    }

    void FixedUpdate()
    {
        if (isMoving)
        {
            transform.position = Vector2.Lerp(transform.position, targetPosition, Time.deltaTime * 1.5f);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        gameManager.score += GetComponent<CardLook>().cardData.scoreValue; // spostare logica punteggio in gamemanager
        Destroy(gameObject);
        gameManager.GetComponent <DisappearingArrows>().Disable();// pure questo in gamemanager
        gameManager.GetComponent<GameManager>().InstantiateCard(); // spostare logica punteggio in gamemanager
        
    }


}


