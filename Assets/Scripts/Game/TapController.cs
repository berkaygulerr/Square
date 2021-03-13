using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Rigidbody2D))]
public class TapController : MonoBehaviour
{
    private GameManager gameManager;

    [SerializeField] private float speed = 4;
    private float limitPos = 2.20f;

    public bool ClickedRight => Input.GetMouseButton(0) && mousePosition.x > 0 && rb.position.x < limitPos;
    public bool ClickedLeft => Input.GetMouseButton(0) && mousePosition.x < 0 && rb.position.x > -limitPos;

    bool mouseIsNotOverUI;

    Rigidbody2D rb;

    Vector2 mousePosition;
    Vector2 direction = Vector2.zero;

    void Start()
    {
        gameManager = GameManager.Instance;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Returns false if the UI object is clicked
        mouseIsNotOverUI = EventSystem.current.currentSelectedGameObject == null;

        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        MovementInput();
    }

    void FixedUpdate()
    {
        if (!gameManager.IsDead && gameManager.IsStarted && mouseIsNotOverUI)
            Move();
    }

    void MovementInput()
    {
        if (ClickedRight)
            direction = Vector2.right;
        else if (ClickedLeft)
            direction = Vector2.left;
        else
            direction = Vector2.zero;
    }

    void Move()
    {
        rb.MovePosition(rb.position + (direction * speed * Time.fixedDeltaTime));
    }
}
